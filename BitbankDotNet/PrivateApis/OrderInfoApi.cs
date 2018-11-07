using BitbankDotNet.Entities;
using BitbankDotNet.Extensions;
using System.Threading.Tasks;
using System.Web;

// ReSharper disable once CheckNamespace
namespace BitbankDotNet
{
    partial class BitbankClient
    {
        const string OrderInfoPath = "/v1/user/spot/order?";
        const string OrdersInfoPath = "/v1/user/spot/orders_info";

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
            var path = OrderInfoPath + query;

            return PrivateApiGetAsync<Order>(path);
        }

        /// <summary>
        /// [PrivateAPI]複数の注文情報を取得します。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <param name="orderIds">複数の注文ID</param>
        /// <returns>注文情報</returns>
        public async Task<Order[]> GetOrdersAsync(CurrencyPair pair, long[] orderIds)
        {
            var body = new OrdersInfoBody
            {
                Pair = pair,
                OrderIds = orderIds
            };
            var result = await PrivateApiPostAsync<OrderList, OrdersInfoBody>(OrdersInfoPath, body)
                .ConfigureAwait(false);

            return result.Orders;
        }
    }
}