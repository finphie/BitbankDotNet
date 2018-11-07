using BitbankDotNet.Entities;
using BitbankDotNet.Extensions;
using System;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
namespace BitbankDotNet
{
    partial class BitbankClient
    {
        const string CandlestickPath = "/candlestick/";

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
        /// [PublicAPI]指定された日付のローソク足データを返します。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <param name="type">ローソク足の期間</param>
        /// <param name="date">日付</param>
        /// <returns>ローソク足データ</returns>
        public Task<Ohlcv[]> GetCandlesticksAsync(CurrencyPair pair, CandleType type, DateTimeOffset date)
            => GetCandlesticksAsync(pair, type, date.UtcDateTime);

        /// <summary>
        /// [PublicAPI]指定された年（UTC）のローソク足データを返します。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <param name="type">ローソク足の期間</param>
        /// <param name="query">クエリ</param>
        /// <returns>ローソク足データ</returns>
        async Task<Ohlcv[]> GetCandlesticksAsync(CurrencyPair pair, CandleType type, string query)
        {
            var path = CandlestickPath + type.GetEnumMemberValue() + $"/{query}";
            var result = await PublicApiGetAsync<CandlestickList>(path, pair).ConfigureAwait(false);

            return result.Candlesticks[0].Ohlcv;
        }
    }
}