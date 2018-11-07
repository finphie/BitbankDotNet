using BitbankDotNet.Entities;
using System;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
namespace BitbankDotNet
{
    partial class BitbankClient
    {
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
    }
}