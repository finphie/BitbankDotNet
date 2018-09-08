using BitbankDotNet.Entities;
using BitbankDotNet.Extensions;
using BitbankDotNet.Resolvers;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static SpanJson.JsonSerializer.Generic.Utf16;
using static SpanJson.JsonSerializer.Generic.Utf8;

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

        public BitbankClient(HttpClient client) : this(client, string.Empty, string.Empty)
        {
        }

        public BitbankClient(HttpClient client, string apiKey, string apiSecret)
        {
            _client = client ?? throw new NullReferenceException(nameof(client));
            _client.Timeout = TimeSpan.FromSeconds(10);
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
                var response = await _client.SendAsync(request).ConfigureAwait(false);
                var json = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    var result = Deserialize<T, BitbankResolver<byte>>(json);
                    if (result.Success == 1)
                        return result;
                }

                Error error;
                try
                {
                    error = Deserialize<ErrorResponse, BitbankResolver<byte>>(json).Data;
                }
                catch
                {
                    throw new BitbankApiException(
                        $"JSONデシリアライズでエラーが発生しました。Response StatusCode:{response.StatusCode} ReasonPhrase:{response.ReasonPhrase}");
                }

                throw new BitbankApiException($"ErrorCode:{error.Code}");
            }
            catch (TaskCanceledException ex)
            {
                throw new BitbankApiException("リクエストがタイムアウトしました。", ex);
            }
            catch (HttpRequestException ex)
            {
                throw new BitbankApiException("リクエストに失敗しました。", ex);
            }
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