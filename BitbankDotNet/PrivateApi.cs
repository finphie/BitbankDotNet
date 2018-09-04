using BitbankDotNet.Entities;
using BitbankDotNet.Resolvers;
using SpanJson;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BitbankDotNet
{
    /// <summary>
    /// Private API
    /// </summary>
    public class PrivateApi
    {
        // 相対URLで指定するには、末尾にスラッシュが必要
        // cf. https://stackoverflow.com/q/23438416
        const string Url = "https://api.bitbank.cc/v1/";

        readonly HttpClient _client;

        readonly string _apiKey;
        readonly byte[] _apiSecret;

        public PrivateApi(HttpClient client, string apiKey, string apiSecret)
        {
            _client = client;
            _client.BaseAddress = new Uri(Url);
            _client.Timeout = TimeSpan.FromSeconds(10);
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            _apiKey = apiKey;
            _apiSecret = Encoding.UTF8.GetBytes(apiSecret);
        }

        async Task<T> Get<T>(string path)
            where T : class, IResponse
        {
            var timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();

            var request = new HttpRequestMessage(HttpMethod.Get, path);
            request.Headers.Add("ACCESS-KEY", _apiKey);
            request.Headers.Add("ACCESS-NONCE", timestamp);
            request.Headers.Add("ACCESS-SIGNATURE", CreateSign(timestamp + "/v1/" + path));

            try
            {
                var response = await _client.SendAsync(request).ConfigureAwait(false);
                var json = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonSerializer.Generic.Utf8.Deserialize<T, BitbankResolver<byte>>(json);
                    if (result.Success == 1)
                        return result;
                }

                Error error;
                try
                {
                    error = JsonSerializer.Generic.Utf8.Deserialize<ErrorResponse, BitbankResolver<byte>>(json).Data;
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

        string CreateSign(string message)
        {
            using (var hmac = new HMACSHA256(_apiSecret))
            {
                var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(message));
                return BitConverter.ToString(hash).ToLower().Replace("-", "");
            }
        }

        /// <summary>
        /// アセット一覧を返します。
        /// </summary>
        /// <returns>アセット一覧</returns>
        public async Task<Asset[]> GetAsset()
            => (await Get<AssetResponse>("user/assets").ConfigureAwait(false)).Data.Assets;
    }
}