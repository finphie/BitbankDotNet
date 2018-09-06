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
            => (await GetAsync<TickerResponse>("/ticker", pair).ConfigureAwait(false)).Data;

        /// <summary>
        /// [PublicAPI]板情報を返します。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <returns>板情報</returns>
        public async Task<Depth> GetDepthAsync(CurrencyPair pair)
            => (await GetAsync<DepthResponse>("/depth", pair).ConfigureAwait(false)).Data;

        /// <summary>
        /// [PublicAPI]最新の約定履歴を返します。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <returns>約定履歴</returns>
        public async Task<Transaction[]> GetTransactionAsync(CurrencyPair pair)
            => (await GetAsync<TransactionResponse>("/transactions", pair).ConfigureAwait(false)).Data.Transactions;

        /// <summary>
        /// [PublicAPI]指定された日付（UTC）の全約定履歴を返します。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <param name="date">日付</param>
        /// <returns>約定履歴</returns>
        public async Task<Transaction[]> GetTransactionAsync(CurrencyPair pair, DateTime date)
            => (await GetAsync<TransactionResponse>($"transactions/{date:yyyyMMdd}", pair).ConfigureAwait(false)).Data.Transactions;

        /// <summary>
        /// [PublicAPI]指定された日付（UTC）の全約定履歴を返します。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <param name="date">日付</param>
        /// <returns>約定履歴</returns>
        public async Task<Transaction[]> GetTransactionAsync(CurrencyPair pair, DateTimeOffset date)
            => await GetTransactionAsync(pair, date.UtcDateTime).ConfigureAwait(false);

        async Task<Ohlcv[]> GetCandlestickAsync(CurrencyPair pair, CandleType type, string query)
            => (await GetAsync<CandlestickResponse>($"/candlestick/{type.GetEnumMemberValue()}/{query}", pair).ConfigureAwait(false)).Data.Candlesticks[0].Ohlcv;

        /// <summary>
        /// [PublicAPI]指定された年（UTC）のローソク足データを返します。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <param name="type">ローソク足の期間</param>
        /// <param name="year">年</param>
        /// <returns>ローソク足データ</returns>
        public async Task<Ohlcv[]> GetCandlestickAsync(CurrencyPair pair, CandleType type, int year)
            => await GetCandlestickAsync(pair, type, year.ToString()).ConfigureAwait(false);

        /// <summary>
        /// [PublicAPI]指定された日付（UTC）のローソク足データを返します。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <param name="type">ローソク足の期間</param>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <param name="day">日</param>
        /// <returns>ローソク足データ</returns>
        public async Task<Ohlcv[]> GetCandlestickAsync(CurrencyPair pair, CandleType type, int year, int month, int day)
            => await GetCandlestickAsync(pair, type, $"{year}{month}{day}").ConfigureAwait(false);

        /// <summary>
        /// [PublicAPI]指定された日付（UTC）のローソク足データを返します。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <param name="type">ローソク足の期間</param>
        /// <param name="date">日付</param>
        /// <returns>ローソク足データ</returns>
        public async Task<Ohlcv[]> GetCandlestickAsync(CurrencyPair pair, CandleType type, DateTime date)
            => await GetCandlestickAsync(pair, type, $"{date:yyyyMMdd}").ConfigureAwait(false);

        /// <summary>
        /// [PublicAPI]指定された日付（UTC）のローソク足データを返します。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <param name="type">ローソク足の期間</param>
        /// <param name="date">日付</param>
        /// <returns>ローソク足データ</returns>
        public async Task<Ohlcv[]> GetCandlestickAsync(CurrencyPair pair, CandleType type, DateTimeOffset date)
            => await GetCandlestickAsync(pair, type, date.UtcDateTime).ConfigureAwait(false);
    }
}