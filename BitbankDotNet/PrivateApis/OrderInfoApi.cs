using BitbankDotNet.Entities;
using BitbankDotNet.Extensions;
using System.Threading.Tasks;
using System.Web;

// ReSharper disable once CheckNamespace
namespace BitbankDotNet
{
    partial class BitbankClient
    {
        /// <summary>
        /// [PrivateAPI]注文情報を取得します。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <param name="orderId">注文ID</param>
        /// <returns>注文情報</returns>
        public Task<Order> GetOrderAsync(CurrencyPair pair, long orderId)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["pair"] = pair.GetEnumMemberValue();
            query["order_id"] = orderId.ToString();

            return PrivateApiGetAsync<Order>("/v1/user/spot/order?" + query);
        }

        /// <summary>
        /// [PrivateAPI]複数の注文情報を取得します。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <param name="orderIds">複数の注文ID</param>
        /// <returns>注文情報</returns>
        public async Task<Order[]> GetOrdersAsync(CurrencyPair pair, long[] orderIds)
            => (await PrivateApiPostAsync<OrderList, OrdersInfoBody>("/v1/user/spot/orders_info", new OrdersInfoBody
            {
                Pair = pair,
                OrderIds = orderIds
            }).ConfigureAwait(false)).Orders;
    }
}