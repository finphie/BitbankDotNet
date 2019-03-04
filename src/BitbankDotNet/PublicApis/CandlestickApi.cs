using System;
using System.Threading.Tasks;
using BitbankDotNet.Entities;
using BitbankDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace BitbankDotNet
{
    partial class BitbankRestApiClient
    {
        const string CandlestickPath = "/candlestick/";

        /// <summary>
        /// [Public API]指定された年（UTC）のローソク足データを返します。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <param name="type">ローソク足の期間</param>
        /// <param name="year">年</param>
        /// <returns>ローソク足データ</returns>
        /// <exception cref="BitbankDotNetException">APIリクエストでエラーが発生しました。</exception>
        public Task<Ohlcv[]> GetCandlesticksAsync(CurrencyPair pair, CandleType type, int year)
            => GetCandlesticksAsync(pair, type, year.ToString());

        /// <summary>
        /// [Public API]指定された日付（UTC）のローソク足データを返します。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <param name="type">ローソク足の期間</param>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <param name="day">日</param>
        /// <returns>ローソク足データ</returns>
        /// <exception cref="BitbankDotNetException">APIリクエストでエラーが発生しました。</exception>
        public Task<Ohlcv[]> GetCandlesticksAsync(CurrencyPair pair, CandleType type, int year, int month, int day)
            => GetCandlesticksAsync(pair, type, $"{year}{month:D2}{day:D2}");

        /// <summary>
        /// [Public API]指定された日付（UTC）のローソク足データを返します。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <param name="type">ローソク足の期間</param>
        /// <param name="date">日付</param>
        /// <returns>ローソク足データ</returns>
        /// <exception cref="BitbankDotNetException">APIリクエストでエラーが発生しました。</exception>
        public Task<Ohlcv[]> GetCandlesticksAsync(CurrencyPair pair, CandleType type, DateTime date)
            => GetCandlesticksAsync(pair, type, $"{date:yyyyMMdd}");

        /// <summary>
        /// [Public API]指定された日付のローソク足データを返します。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <param name="type">ローソク足の期間</param>
        /// <param name="date">日付</param>
        /// <returns>ローソク足データ</returns>
        /// <exception cref="BitbankDotNetException">APIリクエストでエラーが発生しました。</exception>
        public Task<Ohlcv[]> GetCandlesticksAsync(CurrencyPair pair, CandleType type, DateTimeOffset date)
            => GetCandlesticksAsync(pair, type, date.UtcDateTime);

        /// <summary>
        /// [Public API]指定された年（UTC）のローソク足データを返します。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <param name="type">ローソク足の期間</param>
        /// <param name="query">クエリ</param>
        /// <returns>ローソク足データ</returns>
        /// <exception cref="BitbankDotNetException">APIリクエストでエラーが発生しました。</exception>
        async Task<Ohlcv[]> GetCandlesticksAsync(CurrencyPair pair, CandleType type, string query)
        {
            var path = CandlestickPath + type.GetEnumMemberValue() + $"/{query}";
            var result = await PublicApiGetAsync<CandlestickList>(path, pair).ConfigureAwait(false);

            return result.Candlesticks[0].Ohlcv;
        }
    }
}