using BitbankDotNet.Entities;
using BitbankDotNet.Extensions;
using System;
using System.Threading.Tasks;

namespace BitbankDotNet
{
    public partial class BitbankClient
    {
        /// <summary>
        /// [PublicAPI]ティッカー情報を返します。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <returns>ティッカー情報</returns>
        public Task<Ticker> GetTickerAsync(CurrencyPair pair)
            => PublicApiGetAsync<Ticker>("/ticker", pair);

        /// <summary>
        /// [PublicAPI]板情報を返します。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <returns>板情報</returns>
        public Task<Depth> GetDepthAsync(CurrencyPair pair)
            => PublicApiGetAsync<Depth>("/depth", pair);

        /// <summary>
        /// [PublicAPI]最新の約定履歴を返します。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <returns>約定履歴</returns>
        public async Task<Transaction[]> GetTransactionsAsync(CurrencyPair pair)
            => (await PublicApiGetAsync<TransactionList>("/transactions", pair).ConfigureAwait(false)).Transactions;

        /// <summary>
        /// [PublicAPI]指定された日付（UTC）の全約定履歴を返します。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <param name="query">クエリ</param>
        /// <returns>約定履歴</returns>
        async Task<Transaction[]> GetTransactionsAsync(CurrencyPair pair, string query)
            => (await PublicApiGetAsync<TransactionList>($"/transactions/{query}", pair).ConfigureAwait(false)).Transactions;

        /// <summary>
        /// [PublicAPI]指定された日付（UTC）の全約定履歴を返します。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <param name="day">日</param>
        /// <returns>約定履歴</returns>
        public Task<Transaction[]> GetTransactionsAsync(CurrencyPair pair, int year, int month, int day)
            => GetTransactionsAsync(pair, $"{year:D2}{month:D2}{day:D2}");

        /// <summary>
        /// [PublicAPI]指定された日付（UTC）の全約定履歴を返します。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <param name="date">日付</param>
        /// <returns>約定履歴</returns>
        public Task<Transaction[]> GetTransactionsAsync(CurrencyPair pair, DateTime date)
            => GetTransactionsAsync(pair, $"{date:yyyyMMdd}");

        /// <summary>
        /// [PublicAPI]指定された日付（UTC）の全約定履歴を返します。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <param name="date">日付</param>
        /// <returns>約定履歴</returns>
        public Task<Transaction[]> GetTransactionsAsync(CurrencyPair pair, DateTimeOffset date)
            => GetTransactionsAsync(pair, date.UtcDateTime);

        /// <summary>
        /// [PublicAPI]指定された年（UTC）のローソク足データを返します。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <param name="type">ローソク足の期間</param>
        /// <param name="query">クエリ</param>
        /// <returns>ローソク足データ</returns>
        async Task<Ohlcv[]> GetCandlesticksAsync(CurrencyPair pair, CandleType type, string query)
            => (await PublicApiGetAsync<CandlestickList>($"/candlestick/{type.GetEnumMemberValue()}/{query}", pair).ConfigureAwait(false)).Candlesticks[0].Ohlcv;

        /// <summary>
        /// [PublicAPI]指定された年（UTC）のローソク足データを返します。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <param name="type">ローソク足の期間</param>
        /// <param name="year">年</param>
        /// <returns>ローソク足データ</returns>
        public Task<Ohlcv[]> GetCandlesticksAsync(CurrencyPair pair, CandleType type, int year)
            => GetCandlesticksAsync(pair, type, year.ToString());

        /// <summary>
        /// [PublicAPI]指定された日付（UTC）のローソク足データを返します。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <param name="type">ローソク足の期間</param>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <param name="day">日</param>
        /// <returns>ローソク足データ</returns>
        public Task<Ohlcv[]> GetCandlesticksAsync(CurrencyPair pair, CandleType type, int year, int month, int day)
            => GetCandlesticksAsync(pair, type, $"{year:D2}{month:D2}{day:D2}");

        /// <summary>
        /// [PublicAPI]指定された日付（UTC）のローソク足データを返します。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <param name="type">ローソク足の期間</param>
        /// <param name="date">日付</param>
        /// <returns>ローソク足データ</returns>
        public Task<Ohlcv[]> GetCandlesticksAsync(CurrencyPair pair, CandleType type, DateTime date)
            => GetCandlesticksAsync(pair, type, $"{date:yyyyMMdd}");

        /// <summary>
        /// [PublicAPI]指定された日付（UTC）のローソク足データを返します。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <param name="type">ローソク足の期間</param>
        /// <param name="date">日付</param>
        /// <returns>ローソク足データ</returns>
        public Task<Ohlcv[]> GetCandlesticksAsync(CurrencyPair pair, CandleType type, DateTimeOffset date)
            => GetCandlesticksAsync(pair, type, date.UtcDateTime);

        /// <summary>
        /// [PublicAPI]取引所ステータスを返します。
        /// </summary>
        /// <returns>取引所ステータス</returns>
        public async Task<HealthStatus[]> GetStatus()
            => (await PublicApiGetAsync<HealthStatusList>("spot/status").ConfigureAwait(false)).Statuses;
    }
}