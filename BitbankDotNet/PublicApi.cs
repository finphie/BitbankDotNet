using BitbankDotNet.Entities;
using BitbankDotNet.Resolvers;
using SpanJson;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BitbankDotNet
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

        async Task<T> Get<T>(string path, string pair)
            where T : class, IResponse
        {
            try
            {
                var response = await _client.GetAsync(pair + "/" + path).ConfigureAwait(false);
                var json = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonSerializer.Generic.Utf8.Deserialize<T, BitbankResolver>(json);
                    if (result.Success == 1)
                        return result;
                }

                Error error;
                try
                {
                    error = JsonSerializer.Generic.Utf8.Deserialize<ErrorResponse, BitbankResolver>(json).Data;
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

        public async Task<Transaction[]> GetTransaction(string pair)
            => (await Get<TransactionResponse>("transactions", pair).ConfigureAwait(false)).Data.Transactions;

        public async Task<Transaction[]> GetTransaction(string pair, DateTime date)
            => (await Get<TransactionResponse>($"transactions/{date:yyyyMMdd}", pair).ConfigureAwait(false)).Data.Transactions;

        public async Task<Transaction[]> GetTransaction(string pair, DateTimeOffset date)
            => await GetTransaction(pair, date.UtcDateTime).ConfigureAwait(false);

        public void GetCandlestick()
        {

        }    
    }
}