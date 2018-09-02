using BitbankDotNet.Api.Entities;
using BitbankDotNet.Resolvers;
using SpanJson;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BitbankDotNet.Api
{
    public class PublicApi
    {
        readonly HttpClient _client;

        const string Url = "https://public.bitbank.cc";

        public PublicApi(HttpClient client)
        {
            _client = client;
            _client.BaseAddress = new Uri(Url);
            _client.Timeout = TimeSpan.FromSeconds(10);
        }

        public async Task<T> Get<T>(string path, string pair)
        {
            try
            {
                var response = await _client.GetAsync(pair + "/" + path).ConfigureAwait(false);
                var json = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                    return JsonSerializer.Generic.Utf8.Deserialize<T, BitbankResolver<byte>>(json);

                Error error;
                try
                {
                    error = JsonSerializer.Generic.Utf8.Deserialize<Error>(json);
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

        public async Task<Ticker> GetTicker(string pair)
            => (await Get<TickerResponse>("ticker", pair).ConfigureAwait(false)).Data;

        public async Task<Depth> GetDepth(string pair)
            => (await Get<DepthResponse>("depth", pair).ConfigureAwait(false)).Data;

        public void GetTransactions()
        {

        }

        public void GetCandlestick()
        {

        }    
    }
}