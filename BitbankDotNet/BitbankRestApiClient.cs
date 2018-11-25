using BitbankDotNet.Caches;
using BitbankDotNet.Entities;
using BitbankDotNet.Extensions;
using BitbankDotNet.Helpers;
using BitbankDotNet.Resolvers;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static SpanJson.JsonSerializer.Generic.Utf8;

[assembly: InternalsVisibleTo(nameof(BitbankDotNet) + ".CodeGenerator")]
[assembly: InternalsVisibleTo(nameof(BitbankDotNet) + ".Tests")]

namespace BitbankDotNet
{
    /// <summary>
    /// Bitbank REST API Client
    /// </summary>
    public sealed partial class BitbankRestApiClient : IDisposable
    {
        // Public API
        const string PublicUrl = "https://public.bitbank.cc/";

        // Private API
        const string PrivateUrl = "https://api.bitbank.cc";

        // HMAC-SHA256のハッシュサイズ（バイト単位）
        const int HashSize = 32;

        // 16進数署名文字列の長さ（UTF-16）
        // HMAC-SHA256は32バイトのbyte配列
        const int SignHexUtf16StringLength = HashSize * 2;

        static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(10);

        static readonly MediaTypeHeaderValue ContentType =
            new MediaTypeHeaderValue("application/json") {CharSet = Encoding.UTF8.WebName};

        readonly HttpClient _client;

        readonly string _apiKey;

        readonly IncrementalHash _incrementalHash;
        readonly string _signHexUtf16String = new string(default, SignHexUtf16StringLength);

        ulong _nonce = (ulong)DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        static BitbankRestApiClient()
        {
            // EnumMemberCacheでは、静的コンストラクターでリフレクションを利用している。
            // 初回アクセスは遅いので、静的コンストラクターを強制的に実行しておく。
            RuntimeHelpers.RunClassConstructor(typeof(EnumMemberCache<AssetName>).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(EnumMemberCache<CurrencyPair>).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(EnumMemberCache<CandleType>).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(EnumMemberCache<SortOrder>).TypeHandle);
        }

        /// <summary>
        /// <see cref="BitbankRestApiClient"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="client"><see cref="HttpClient"/>クラスのインスタンス</param>
        public BitbankRestApiClient(HttpClient client)
            : this(client, string.Empty, string.Empty, DefaultTimeout)
        {
        }

        /// <summary>
        /// <see cref="BitbankRestApiClient"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="client"><see cref="HttpClient"/>クラスのインスタンス</param>
        /// <param name="timeout">タイムアウトまでの時間</param>
        public BitbankRestApiClient(HttpClient client, TimeSpan timeout)
            : this(client, string.Empty, string.Empty, timeout)
        {
        }

        /// <summary>
        /// <see cref="BitbankRestApiClient"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="client"><see cref="HttpClient"/>クラスのインスタンス</param>
        /// <param name="apiKey">APIキー</param>
        /// <param name="apiSecret">APIシークレットキー</param>
        public BitbankRestApiClient(HttpClient client, string apiKey, string apiSecret)
            : this(client, apiKey, apiSecret, DefaultTimeout)
        {
        }

        /// <summary>
        /// <see cref="BitbankRestApiClient"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="client"><see cref="HttpClient"/>クラスのインスタンス</param>
        /// <param name="apiKey">APIキー</param>
        /// <param name="apiSecret">APIシークレットキー</param>
        /// <param name="timeout">タイムアウトまでの時間</param>
        public BitbankRestApiClient(HttpClient client, string apiKey, string apiSecret, TimeSpan timeout)
        {
            if (!BitConverter.IsLittleEndian)
                throw ThrowHelper.ThrowBigEndianNotSupported();

            _client = client ?? throw new ArgumentNullException(nameof(client));
            _client.Timeout = timeout;

            // APIキーとAPIシークレットが設定されていない場合
            if (string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(apiSecret))
                return;
            _apiKey = apiKey;
            _incrementalHash = IncrementalHash.CreateHMAC(HashAlgorithmName.SHA256, Encoding.UTF8.GetBytes(apiSecret));
        }

        public void Dispose()
        {
            _client?.Dispose();
            _incrementalHash?.Dispose();
        }

        /// <summary>
        /// HTTPリクエスト送信します。
        /// </summary>
        /// <typeparam name="T"><see cref="Entities"/>名前空間内のクラス</typeparam>
        /// <param name="request"><see cref="HttpRequestMessage"/>クラスのインスタンス</param>
        /// <returns><see cref="Entities"/>名前空間内にあるクラスのインスタンス</returns>
        /// <exception cref="BitbankDotNetException">APIリクエストでエラーが発生しました。</exception>
        async Task<T> SendAsync<T>(HttpRequestMessage request)
            where T : class, IEntityResponse
        {
            Error error = null;
            try
            {
                var response = await _client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
                var json = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);

                // ステータスコードで200以外（404）を返す場合、Successが0となる。
                // Bitbankの公式ライブラリでは、ステータスコードのチェックはしていない。
                // そのため、IsSuccessStatusCodeは省略できるはずだが、一応チェックしておく。
                if (response.IsSuccessStatusCode)
                {
                    var result = Deserialize<Response<T>, BitbankResolver<byte>>(json);
                    if (result.Success == 1)
                        return result.Data;
                }

                error = Deserialize<Response<Error>, BitbankResolver<byte>>(json).Data;
            }
            catch (TaskCanceledException ex)
            {
                ThrowHelper.ThrowBitbankRequestTimeoutException(ex);
            }
            catch (Exception ex)
            {
                // ここに到達した場合、デシリアライズでエラーが発生しているはず。
                // 1.JsonParserException
                // 2.IndexOutOfRangeException
                // また、nullチェックを省略しているため、NullReferenceExceptionも考慮する必要がある。
                ThrowHelper.ThrowBitbankJsonDeserializeException(ex);
            }

            ThrowHelper.ThrowBitbankApiException(error?.Code ?? default);

            // ここには到達しないはず
            return default;
        }

        /// <summary>
        /// [Public API]Getリクエストを送信します。
        /// </summary>
        /// <typeparam name="T"><see cref="Entities"/>名前空間内のクラス</typeparam>
        /// <param name="path">リクエストのパス</param>
        /// <returns><see cref="Entities"/>名前空間内にあるクラスのインスタンス</returns>
        /// <exception cref="BitbankDotNetException">APIリクエストでエラーが発生しました。</exception>
        Task<T> PublicApiGetAsync<T>(string path)
            where T : class, IEntityResponse
            => SendAsync<T>(new HttpRequestMessage(HttpMethod.Get, PublicUrl + path));

        /// <summary>
        /// [Public API]Getリクエストを送信します。
        /// </summary>
        /// <typeparam name="T"><see cref="Entities"/>名前空間内のクラス</typeparam>
        /// <param name="path">リクエストのパス</param>
        /// <param name="pair">通貨ペア</param>
        /// <returns><see cref="Entities"/>名前空間内にあるクラスのインスタンス</returns>
        /// <exception cref="BitbankDotNetException">APIリクエストでエラーが発生しました。</exception>
        Task<T> PublicApiGetAsync<T>(string path, CurrencyPair pair)
            where T : class, IEntityResponse
            => SendAsync<T>(new HttpRequestMessage(HttpMethod.Get, PublicUrl + pair.GetEnumMemberValue() + path));

        /// <summary>
        /// [Private API]Getリクエストを送信します。
        /// </summary>
        /// <typeparam name="T"><see cref="Entities"/>名前空間内のクラス</typeparam>
        /// <param name="path">リクエストのパス</param>
        /// <param name="utf8Path">リクエストのパス（UTF-8）</param>
        /// <returns><see cref="Entities"/>名前空間内にあるクラスのインスタンス</returns>
        /// <exception cref="BitbankDotNetException">APIリクエストでエラーが発生しました。</exception>
        Task<T> PrivateApiGetAsync<T>(string path, in Span<byte> utf8Path)
            where T : class, IEntityResponse
            => SendAsync<T>(MakePrivateRequestHeader(HttpMethod.Get, path, utf8Path));

        /// <summary>
        /// [Private API]Postリクエストを送信します。
        /// </summary>
        /// <typeparam name="T"><see cref="Entities"/>名前空間内のクラス</typeparam>
        /// <typeparam name="TBody"></typeparam>
        /// <param name="path">リクエストのパス</param>
        /// <param name="body">リクエストボディ</param>
        /// <returns><see cref="Entities"/>名前空間内にあるクラスのインスタンス</returns>
        /// <exception cref="BitbankDotNetException">APIリクエストでエラーが発生しました。</exception>
        Task<T> PrivateApiPostAsync<T, TBody>(string path, TBody body)
            where T : class, IEntityResponse
        {
            var json = Serialize<TBody, BitbankResolver<byte>>(body);

            var request = MakePrivateRequestHeader(HttpMethod.Post, path, json);
            request.Content = new ByteArrayContent(json);
            request.Content.Headers.ContentType = ContentType;

            return SendAsync<T>(request);
        }

        /// <summary>
        /// [Private API]リクエストヘッダーを作成します。
        /// </summary>
        /// <param name="method">HTTPリクエストメソッド</param>
        /// <param name="path">リクエストのパス</param>
        /// <param name="signMessage">署名作成対象のデータ</param>
        /// <returns><see cref="HttpRequestMessage"/>クラスのインスタンス</returns>
        HttpRequestMessage MakePrivateRequestHeader(HttpMethod method, string path, in Span<byte> signMessage)
        {
            // オーバーフローする可能性がある。
            // a. ToUnixTimeMillisecondsで取得できるUnix時間の最大値は、253,402,300,799,999（9999/12/31T23:59:59.999Z）
            // b. ulongの最大値は、18,446,744,073,709,551,615
            // つまり、最小で18,446,490,671,408,751,615(b-a-1)回インクリメントできる。
            // 従って、オーバーフローのチェックは行わない。
            var timestamp = _nonce++.ToString();

            CreateSign(timestamp, signMessage);

            var request = new HttpRequestMessage(method, PrivateUrl + path);

            // TryAddWithoutValidationの方がAddやコレクション初期化子より速い。
            request.Headers.TryAddWithoutValidation("ACCESS-KEY", _apiKey);
            request.Headers.TryAddWithoutValidation("ACCESS-NONCE", timestamp);
            request.Headers.TryAddWithoutValidation("ACCESS-SIGNATURE", _signHexUtf16String);

            return request;
        }

        /// <summary>
        /// HMAC-SHA256の署名を作成します。
        /// </summary>
        /// <param name="nonce">ACCESS-NONCE</param>
        /// <param name="data">署名作成対象のデータ</param>
        void CreateSign(string nonce, in Span<byte> data)
        {
            // nonceの最大文字数は、ulongの最大桁数である20文字
            // dataは、150文字以下のはず。
            // nonceとdataはUTF-8として利用するが、ASCII文字しかないので1文字1バイト
            // 合計で170バイトなのでスタックでも問題ない。
            Span<byte> buffer = stackalloc byte[nonce.Length + data.Length];
            ref var bufferStart = ref MemoryMarshal.GetReference(buffer);

            // 数値のUTF-16文字列をUTF-8のbyte配列に変換
            // 今回の場合、数値のUTF-16文字列が他に必要となるので、stringへの変換コストは無視できる。
            // ベンチマークを取った結果、文字列を変換する方が数値を直接変換するよりも速くなった。
            // なお、Encoding.GetBytesやUtf8Formatterを利用するのは遅いため、
            // 独自に高速化したバージョン同士で比較した。（ベンチマークプロジェクト参照）
            nonce.FromAsciiStringToUtf8Bytes(buffer);

            // ReSharper disable once CommentTypo
            // HMACSHA256よりIncrementalHashの方が速い。
            // また、AppendDataを2回呼び出すより、バッファにコピーして一括で処理した方が速い。
            ref var dataStart = ref MemoryMarshal.GetReference(data);
            Unsafe.CopyBlockUnaligned(ref Unsafe.Add(ref bufferStart, nonce.Length), ref dataStart, (uint)data.Length);
            _incrementalHash.AppendData(buffer);

            // 出力先バッファは固定サイズなので、戻り値やbytesWrittenのチェックは省略できる。
            // cf. https://github.com/dotnet/corefx/blob/v2.1.5/src/Common/src/Internal/Cryptography/HashProviderCng.cs#L87-L104
            // cf. https://github.com/dotnet/corefx/blob/v2.1.5/src/System.Security.Cryptography.Algorithms/src/Internal/Cryptography/HashProviderDispenser.Unix.cs#L151-L166
            // cf. https://github.com/dotnet/corefx/blob/v2.1.5/src/System.Security.Cryptography.Algorithms/src/Internal/Cryptography/HashProviderDispenser.OSX.cs#L121-L142
            Span<byte> hash = stackalloc byte[HashSize];
            _incrementalHash.TryGetHashAndReset(hash, out _);
            SpanHelper.ToHexString(hash, _signHexUtf16String);
        }
    }
}