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
    public sealed partial class BitbankClient : IDisposable
    {
        // Public API
        const string PublicUrl = "https://public.bitbank.cc/";

        // Private API
        const string PrivateUrl = "https://api.bitbank.cc";

        // 16進数署名文字列の長さ（UTF-16）
        // HMAC-SHA256は32バイトのbyte配列
        const int SignHexUtf16StringLength = 32 * 2;

        static readonly MediaTypeHeaderValue ContentType =
            new MediaTypeHeaderValue("application/json") {CharSet = Encoding.UTF8.WebName};

        readonly HttpClient _client;

        readonly string _apiKey;

        readonly HMACSHA256 _hmac;
        readonly byte[] _hash;
        readonly string _signHexUtf16String = new string(default, SignHexUtf16StringLength);

        ulong _nonce = (ulong)DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        public BitbankClient(HttpClient client, TimeSpan timeout = default)
            : this(client, string.Empty, string.Empty, timeout)
        {
        }

        public BitbankClient(HttpClient client, string apiKey, string apiSecret, TimeSpan timeout = default)
        {
            if (!BitConverter.IsLittleEndian)
                throw ThrowHelper.ThrowBigEndianNotSupported();

            _client = client ?? throw new ArgumentNullException(nameof(client));
            _client.Timeout = timeout == default ? TimeSpan.FromSeconds(10) : timeout;

            // APIキーとAPIシークレットが設定されていない場合
            if (string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(apiSecret))
                return;
            _apiKey = apiKey;
            _hmac = new HMACSHA256(Encoding.UTF8.GetBytes(apiSecret));

            // HashSizeはビット単位
            _hash = new byte[_hmac.HashSize / 8];
        }

        public void Dispose() => _hmac?.Dispose();

        // リクエスト送信
        async Task<T> SendAsync<T>(HttpRequestMessage request)
            where T : class, IEntityResponse
        {
            try
            {
                var response = await _client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
                var json = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);

                // ステータスコードで200以外（404）を返す場合、Successが0となる。
                // Bitbankの公式ライブラリでは、ステータスコードのチェックはしていない。
                // そのため、IsSuccessStatusCodeは省略できるはずだが、一応チェックしておく。
                if (response.IsSuccessStatusCode)
                {
                    // ここで、JsonParserExceptionがスローされる条件
                    // 1.EntityFormatterの実装に不具合がある場合
                    // 2.不正なJSONが入力された場合
                    // そのため、catchしなくても問題ないはず。
                    // ただし、JSONが空かどうかは確認したほうが良いかもしれない。
                    var result = Deserialize<Response<T>, BitbankResolver<byte>>(json);
                    if (result.Success == 1)
                        return result.Data;
                }

                try
                {
                    var error = Deserialize<Response<Error>, BitbankResolver<byte>>(json).Data;
                    ThrowHelper.ThrowBitbankApiException(response.StatusCode, error.Code);
                }
                catch (Exception ex) when (!(ex is BitbankApiException))
                {
                    // デシリアライズでスローされる可能性がある例外
                    // 1.JsonParserException
                    // 2.IndexOutOfRangeException
                    // また、nullチェックを省略しているため、NullReferenceExceptionも考慮する必要がある。
                    ThrowHelper.ThrowBitbankJsonDeserializeException(ex, response.StatusCode);
                }
            }
            catch (TaskCanceledException ex)
            {
                ThrowHelper.ThrowBitbankRequestTimeoutException(ex);
            }

            // ここには到達しないはず
            return default;
        }

        // Public API Getリクエスト
        Task<T> PublicApiGetAsync<T>(string path)
            where T : class, IEntityResponse
            => SendAsync<T>(new HttpRequestMessage(HttpMethod.Get, PublicUrl + path));

        // Public API Getリクエスト
        Task<T> PublicApiGetAsync<T>(string path, CurrencyPair pair)
            where T : class, IEntityResponse
            => SendAsync<T>(new HttpRequestMessage(HttpMethod.Get, PublicUrl + pair.GetEnumMemberValue() + path));

        // Private API Getリクエスト
        Task<T> PrivateApiGetAsync<T>(string path)
            where T : class, IEntityResponse
            => SendAsync<T>(MakePrivateRequestHeader(HttpMethod.Get, path, Encoding.UTF8.GetBytes(path)));

        // Private API Postリクエスト
        Task<T> PrivateApiPostAsync<T, TBody>(string path, TBody body)
            where T : class, IEntityResponse
        {
            var json = Serialize<TBody, BitbankResolver<byte>>(body);

            var request = MakePrivateRequestHeader(HttpMethod.Post, path, json);
            request.Content = new ByteArrayContent(json);
            request.Content.Headers.ContentType = ContentType;

            return SendAsync<T>(request);
        }

        // TODO: 高速化する
        // PrivateAPIのリクエストヘッダーを作成
        HttpRequestMessage MakePrivateRequestHeader(HttpMethod method, string path, byte[] signMessage)
        {
            // オーバーフローする可能性がある。
            // a. ToUnixTimeMillisecondsで取得できるUnix時間の最大値は、253,402,300,799,999（9999/12/31T23:59:59.999Z）
            // b. ulongの最大値は、18,446,744,073,709,551,615
            // つまり、最小で18,446,490,671,408,751,615(b-a-1)回インクリメントできる。
            // 従って、オーバーフローのチェックは行わない。
            var timestamp = _nonce++.ToString();

            CreateSign(timestamp, signMessage);
            var request = new HttpRequestMessage(method, PrivateUrl + path);
            request.Headers.Add("ACCESS-KEY", _apiKey);
            request.Headers.Add("ACCESS-NONCE", timestamp);
            request.Headers.Add("ACCESS-SIGNATURE", _signHexUtf16String);

            return request;
        }

        // TODO: 高速化する
        // 署名作成
        void CreateSign(string nonce, byte[] message)
        {
            Span<byte> buffer = new byte[nonce.Length + message.Length];
            ref var bufferStart = ref MemoryMarshal.GetReference(buffer);
            var length = Encoding.UTF8.GetBytes(nonce, buffer);
            Unsafe.CopyBlockUnaligned(ref Unsafe.Add(ref bufferStart, length), ref message[0], (uint)message.Length);

            // 出力先バッファは固定（=長さが一定）なので、戻り値やbytesWrittenのチェックは省略できる。
            // cf. https://github.com/dotnet/corefx/blob/v2.1.5/src/System.Security.Cryptography.Primitives/src/System/Security/Cryptography/HashAlgorithm.cs#L51-L75
            _hmac.TryComputeHash(buffer.Slice(0, length + message.Length), _hash, out _);
            _hash.ToHexString(_signHexUtf16String);
        }
    }
}