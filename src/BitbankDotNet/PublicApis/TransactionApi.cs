using System;
using System.Threading.Tasks;
using BitbankDotNet.Entities;

// ReSharper disable once CheckNamespace
namespace BitbankDotNet
{
    partial class BitbankRestApiClient
    {
        const string TransactionPath = "/transactions";

        /// <summary>
        /// [Public API]最新の約定履歴を返します。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <returns>約定履歴</returns>
        /// <exception cref="BitbankDotNetException">APIリクエストでエラーが発生しました。</exception>
        public async Task<Transaction[]> GetTransactionsAsync(CurrencyPair pair)
        {
            var result = await PublicApiGetAsync<TransactionList>(TransactionPath, pair).ConfigureAwait(false);
            return result.Transactions;
        }

        /// <summary>
        /// [Public API]指定された日付（UTC）の全約定履歴を返します。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <param name="day">日</param>
        /// <returns>約定履歴</returns>
        /// <exception cref="BitbankDotNetException">APIリクエストでエラーが発生しました。</exception>
        public Task<Transaction[]> GetTransactionsAsync(CurrencyPair pair, int year, int month, int day)
            => GetTransactionsAsync(pair, $"{year}{month:D2}{day:D2}");

        /// <summary>
        /// [Public API]指定された日付（UTC）の全約定履歴を返します。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <param name="date">日付</param>
        /// <returns>約定履歴</returns>
        /// <exception cref="BitbankDotNetException">APIリクエストでエラーが発生しました。</exception>
        public Task<Transaction[]> GetTransactionsAsync(CurrencyPair pair, DateTime date)
            => GetTransactionsAsync(pair, $"{date:yyyyMMdd}");

        /// <summary>
        /// [Public API]指定された日付の全約定履歴を返します。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <param name="date">日付</param>
        /// <returns>約定履歴</returns>
        /// <exception cref="BitbankDotNetException">APIリクエストでエラーが発生しました。</exception>
        public Task<Transaction[]> GetTransactionsAsync(CurrencyPair pair, DateTimeOffset date)
            => GetTransactionsAsync(pair, date.UtcDateTime);

        /// <summary>
        /// [Public API]指定された日付（UTC）の全約定履歴を返します。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <param name="query">クエリ</param>
        /// <returns>約定履歴</returns>
        /// <exception cref="BitbankDotNetException">APIリクエストでエラーが発生しました。</exception>
        async Task<Transaction[]> GetTransactionsAsync(CurrencyPair pair, string query)
        {
            var path = TransactionPath + "/" + query;
            var result = await PublicApiGetAsync<TransactionList>(path, pair).ConfigureAwait(false);
            return result.Transactions;
        }
    }
}