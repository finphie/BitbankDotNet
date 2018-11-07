using BitbankDotNet.Entities;
using BitbankDotNet.Extensions;
using System;
using System.Threading.Tasks;
using System.Web;

// ReSharper disable once CheckNamespace
namespace BitbankDotNet
{
    partial class BitbankClient
    {
        const string TradeHistoryPath = "/v1/user/spot/trade_history?";

        /// <summary>
        /// [PrivateAPI]約定履歴を取得します。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <param name="count">取得する注文数</param>
        /// <param name="orderId">注文ID</param>
        /// <param name="since">開始時間</param>
        /// <param name="end">終了時間</param>
        /// <param name="sort">順序</param>
        /// <returns>約定履歴</returns>
        public async Task<Trade[]> GetTradeHistoryAsync(CurrencyPair pair, long count, long orderId, DateTimeOffset since, DateTimeOffset end, SortOrder sort)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["pair"] = pair.GetEnumMemberValue();
            query["count"] = count.ToString();
            query["order_id"] = orderId.ToString();
            query["since"] = since.ToUnixTimeMilliseconds().ToString();
            query["end"] = end.ToUnixTimeMilliseconds().ToString();
            query["order"] = sort.GetEnumMemberValue();
            var path = TradeHistoryPath + query;
            var result = await PrivateApiGetAsync<TradeList>(path).ConfigureAwait(false);

            return result.Trades;
        }
    }
}