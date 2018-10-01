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
        public async Task<Ticker> GetTickerAsync(CurrencyPair pair)
            => await GetAsync<Ticker>("/ticker", pair).ConfigureAwait(false);

        /// <summary>
        /// [PublicAPI]板情報を返します。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <returns>板情報</returns>
        public async Task<Depth> GetDepthAsync(CurrencyPair pair)
            => await GetAsync<Depth>("/depth", pair).ConfigureAwait(false);

        /// <summary>
        /// [PublicAPI]最新の約定履歴を返します。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <returns>約定履歴</returns>
        public async Task<Transaction[]> GetTransactionsAsync(CurrencyPair pair)
            => (await GetAsync<TransactionList>("/transactions", pair).ConfigureAwait(false)).Transactions;

        /// <summary>
        /// [PublicAPI]指定された日付（UTC）の全約定履歴を返します。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <param name="query">クエリ</param>
        /// <returns>約定履歴</returns>
        async Task<Transaction[]> GetTransactionsAsync(CurrencyPair pair, string query)
            => (await GetAsync<TransactionList>($"/transactions/{query}", pair).ConfigureAwait(false)).Transactions;

        /// <summary>
        /// [PublicAPI]指定された日付（UTC）の全約定履歴を返します。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <param name="day">日</param>
        /// <returns>約定履歴</returns>
        public async Task<Transaction[]> GetTransactionsAsync(CurrencyPair pair, int year, int month, int day)
            => await GetTransactionsAsync(pair, $"{year:D2}{month:D2}{day:D2}").ConfigureAwait(false);

        /// <summary>
        /// [PublicAPI]指定された日付（UTC）の全約定履歴を返します。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <param name="date">日付</param>
        /// <returns>約定履歴</returns>
        public async Task<Transaction[]> GetTransactionsAsync(CurrencyPair pair, DateTime date)
            => await GetTransactionsAsync(pair, $"{date:yyyyMMdd}").ConfigureAwait(false);

        /// <summary>
        /// [PublicAPI]指定された日付（UTC）の全約定履歴を返します。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <param name="date">日付</param>
        /// <returns>約定履歴</returns>
        public async Task<Transaction[]> GetTransactionsAsync(CurrencyPair pair, DateTimeOffset date)
            => await GetTransactionsAsync(pair, date.UtcDateTime).ConfigureAwait(false);

        /// <summary>
        /// [PublicAPI]指定された年（UTC）のローソク足データを返します。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <param name="type">ローソク足の期間</param>
        /// <param name="query">クエリ</param>
        /// <returns>ローソク足データ</returns>
        async Task<Ohlcv[]> GetCandlesticksAsync(CurrencyPair pair, CandleType type, string query)
            => (await GetAsync<CandlestickList>($"/candlestick/{type.GetEnumMemberValue()}/{query}", pair).ConfigureAwait(false)).Candlesticks[0].Ohlcv;

        /// <summary>
        /// [PublicAPI]指定された年（UTC）のローソク足データを返します。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <param name="type">ローソク足の期間</param>
        /// <param name="year">年</param>
        /// <returns>ローソク足データ</returns>
        public async Task<Ohlcv[]> GetCandlesticksAsync(CurrencyPair pair, CandleType type, int year)
            => await GetCandlesticksAsync(pair, type, year.ToString()).ConfigureAwait(false);

        /// <summary>
        /// [PublicAPI]指定された日付（UTC）のローソク足データを返します。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <param name="type">ローソク足の期間</param>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <param name="day">日</param>
        /// <returns>ローソク足データ</returns>
        public async Task<Ohlcv[]> GetCandlesticksAsync(CurrencyPair pair, CandleType type, int year, int month, int day)
            => await GetCandlesticksAsync(pair, type, $"{year:D2}{month:D2}{day:D2}").ConfigureAwait(false);

        /// <summary>
        /// [PublicAPI]指定された日付（UTC）のローソク足データを返します。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <param name="type">ローソク足の期間</param>
        /// <param name="date">日付</param>
        /// <returns>ローソク足データ</returns>
        public async Task<Ohlcv[]> GetCandlesticksAsync(CurrencyPair pair, CandleType type, DateTime date)
            => await GetCandlesticksAsync(pair, type, $"{date:yyyyMMdd}").ConfigureAwait(false);

        /// <summary>
        /// [PublicAPI]指定された日付（UTC）のローソク足データを返します。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <param name="type">ローソク足の期間</param>
        /// <param name="date">日付</param>
        /// <returns>ローソク足データ</returns>
        public async Task<Ohlcv[]> GetCandlesticksAsync(CurrencyPair pair, CandleType type, DateTimeOffset date)
            => await GetCandlesticksAsync(pair, type, date.UtcDateTime).ConfigureAwait(false);

        /// <summary>
        /// [PublicAPI]取引所ステータスを返します。
        /// </summary>
        /// <returns>取引所ステータス</returns>
        public async Task<HealthStatus[]> GetStatus()
            => (await PublicApiGetAsync<HealthStatusList>("spot/status").ConfigureAwait(false)).Statuses;
    }
}