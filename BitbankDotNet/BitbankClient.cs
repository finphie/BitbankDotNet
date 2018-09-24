using BitbankDotNet.Entities;
using BitbankDotNet.Extensions;
using BitbankDotNet.Resolvers;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BitbankDotNet.Helpers;
using SpanJson;
using static SpanJson.JsonSerializer.Generic.Utf16;
using static SpanJson.JsonSerializer.Generic.Utf8;

[assembly: InternalsVisibleTo(nameof(BitbankDotNet) + ".Tests")]

namespace BitbankDotNet
{
    /// <summary>
    /// Bitbank REST API Client
    /// </summary>
    public partial class BitbankClient
    {
        // Public API
        const string PublicUrl = "https://public.bitbank.cc/";

        // Private API
        const string PrivateUrl = "https://api.bitbank.cc";

        readonly HttpClient _client;

        readonly string _apiKey;
        readonly byte[] _apiSecret;

        public BitbankClient(HttpClient client, TimeSpan timeout = default)
            : this(client, string.Empty, string.Empty, timeout)
        {
        }

        public BitbankClient(HttpClient client, string apiKey, string apiSecret, TimeSpan timeout = default)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _client.Timeout = timeout == default ? TimeSpan.FromSeconds(10) : timeout;
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // APIキーとAPIシークレットが設定されていない場合
            if (string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(apiSecret))
                return;
            _apiKey = apiKey;
            _apiSecret = Encoding.UTF8.GetBytes(apiSecret);
        }

        // リクエスト送信
        async Task<T> SendAsync<T>(HttpRequestMessage request)
            where T : class, IResponse
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
                    var result = Deserialize<T, BitbankResolver<byte>>(json);
                    if (result.Success == 1)
                        return result;
                }

                try
                {
                    var error = Deserialize<ErrorResponse, BitbankResolver<byte>>(json).Data;
                    ThrowHelper.ThrowBitbankApiException(response, error.Code);
                }
                catch (Exception ex)
                {
                    // デシリアライズでスローされる可能性がある例外
                    // 1.JsonParserException
                    // 2.IndexOutOfRangeException
                    // また、nullチェックを省略しているため、NullReferenceExceptionも考慮する必要がある。
                    ThrowHelper.ThrowBitbankJsonDeserializeException(ex, response);
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
        async Task<T> GetAsync<T>(string path, CurrencyPair pair)
            where T : class, IResponse
            => await SendAsync<T>(new HttpRequestMessage(HttpMethod.Get, PublicUrl + pair.GetEnumMemberValue() + path))
                .ConfigureAwait(false);

        // Private API Getリクエスト
        async Task<T> GetAsync<T>(string path)
            where T : class, IResponse
            => await SendAsync<T>(MakePrivateRequestHeader(HttpMethod.Get, path, path)).ConfigureAwait(false);

        // Private API Postリクエスト
        async Task<T> PostAsync<T, TBody>(string path, TBody body)
            where T : class, IResponse
        {
            var json = Serialize<TBody, BitbankResolver<char>>(body);

            var request = MakePrivateRequestHeader(HttpMethod.Post, path, json);
            request.Content = new StringContent(json);

            return await SendAsync<T>(request).ConfigureAwait(false);
        }

        // PrivateAPIのリクエストヘッダーを作成
        HttpRequestMessage MakePrivateRequestHeader(HttpMethod method, string path, string signMessage)
        {
            var timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();

            var request = new HttpRequestMessage(method, PrivateUrl + path);
            request.Headers.Add("ACCESS-KEY", _apiKey);
            request.Headers.Add("ACCESS-NONCE", timestamp);
            request.Headers.Add("ACCESS-SIGNATURE", CreateSign(timestamp + signMessage));

            return request;
        }

        // 署名作成
        string CreateSign(string message)
        {
            using (var hmac = new HMACSHA256(_apiSecret))
            {
                var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(message));

                // TODO: 後で高速化する
                return BitConverter.ToString(hash).ToLower().Replace("-", "");
            }
        }
    }
}