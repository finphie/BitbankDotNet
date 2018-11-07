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
        const string ActiveOrderPath = "/v1/user/spot/active_orders?";

        /// <summary>
        /// [PrivateAPI]アクティブな注文を取得します。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <param name="count">取得する注文数</param>
        /// <param name="fromId">取得開始注文ID</param>
        /// <param name="endId">取得終了注文ID</param>
        /// <param name="since">開始時間</param>
        /// <param name="end">終了時間</param>
        /// <returns>注文情報</returns>
        public async Task<Order[]> GetActiveOrdersAsync(CurrencyPair pair, long count, long fromId, long endId, DateTimeOffset since, DateTimeOffset end)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["pair"] = pair.GetEnumMemberValue();
            query["from_id"] = fromId.ToString();
            query["end_id"] = fromId.ToString();
            query["since"] = since.ToUnixTimeMilliseconds().ToString();
            query["end"] = end.ToUnixTimeMilliseconds().ToString();

            var path = ActiveOrderPath + query;
            var result = await PrivateApiGetAsync<OrderList>(path).ConfigureAwait(false);

            return result.Orders;
        }
    }
}