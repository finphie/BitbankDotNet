using BitbankDotNet.Entities;
using BitbankDotNet.Extensions;
using BitbankDotNet.Resolvers;
using SpanJson;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BitbankDotNet
{
    /// <summary>
    /// Public API
    /// </summary>
    public class PublicApi
    {
        const string Url = "https://public.bitbank.cc";

        readonly HttpClient _client;

        public PublicApi(HttpClient client)
        {
            _client = client;
            _client.BaseAddress = new Uri(Url);
            _client.Timeout = TimeSpan.FromSeconds(10);
        }

        async Task<T> Get<T>(string path, CurrencyPair pair)
            where T : class, IResponse
        {
            try
            {
                var response = await _client.GetAsync(pair.GetEnumMemberValue() + "/" + path).ConfigureAwait(false);
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

        /// <summary>
        /// ティッカー情報を返します。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <returns>ティッカー情報</returns>
        public async Task<Ticker> GetTicker(CurrencyPair pair)
            => (await Get<TickerResponse>("ticker", pair).ConfigureAwait(false)).Data;

        /// <summary>
        /// 板情報を返します。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <returns>板情報</returns>
        public async Task<Depth> GetDepth(CurrencyPair pair)
            => (await Get<DepthResponse>("depth", pair).ConfigureAwait(false)).Data;

        /// <summary>
        /// 最新の約定履歴を返します。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <returns>約定履歴</returns>
        public async Task<Transaction[]> GetTransaction(CurrencyPair pair)
            => (await Get<TransactionResponse>("transactions", pair).ConfigureAwait(false)).Data.Transactions;

        /// <summary>
        /// 指定された日付（UTC）の全約定履歴を返します。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <param name="date">日付</param>
        /// <returns>約定履歴</returns>
        public async Task<Transaction[]> GetTransaction(CurrencyPair pair, DateTime date)
            => (await Get<TransactionResponse>($"transactions/{date:yyyyMMdd}", pair).ConfigureAwait(false)).Data.Transactions;

        /// <summary>
        /// 指定された日付（UTC）の全約定履歴を返します。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <param name="date">日付</param>
        /// <returns>約定履歴</returns>
        public async Task<Transaction[]> GetTransaction(CurrencyPair pair, DateTimeOffset date)
            => await GetTransaction(pair, date.UtcDateTime).ConfigureAwait(false);

        async Task<Ohlcv[]> GetCandlestick(CurrencyPair pair, CandleType type, string query)
            => (await Get<CandlestickResponse>($"candlestick/{type.GetEnumMemberValue()}/{query}", pair).ConfigureAwait(false)).Data.Candlesticks[0].Ohlcv;

        /// <summary>
        /// 指定された年（UTC）のローソク足データを返します。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <param name="type">ローソク足の期間</param>
        /// <param name="year">年</param>
        /// <returns>ローソク足データ</returns>
        public async Task<Ohlcv[]> GetCandlestick(CurrencyPair pair, CandleType type, int year)
            => await GetCandlestick(pair, type, year.ToString()).ConfigureAwait(false);

        /// <summary>
        /// 指定された日付（UTC）のローソク足データを返します。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <param name="type">ローソク足の期間</param>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <param name="day">日</param>
        /// <returns>ローソク足データ</returns>
        public async Task<Ohlcv[]> GetCandlestick(CurrencyPair pair, CandleType type, int year, int month, int day)
            => await GetCandlestick(pair, type, $"{year}{month}{day}").ConfigureAwait(false);

        /// <summary>
        /// 指定された日付（UTC）のローソク足データを返します。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <param name="type">ローソク足の期間</param>
        /// <param name="date">日付</param>
        /// <returns>ローソク足データ</returns>
        public async Task<Ohlcv[]> GetCandlestick(CurrencyPair pair, CandleType type, DateTime date)
            => await GetCandlestick(pair, type, date.ToString("yyyyMMdd")).ConfigureAwait(false);

        /// <summary>
        /// 指定された日付（UTC）のローソク足データを返します。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <param name="type">ローソク足の期間</param>
        /// <param name="date">日付</param>
        /// <returns>ローソク足データ</returns>
        public async Task<Ohlcv[]> GetCandlestick(CurrencyPair pair, CandleType type, DateTimeOffset date)
            => await GetCandlestick(pair, type, date.UtcDateTime).ConfigureAwait(false);
    }
}