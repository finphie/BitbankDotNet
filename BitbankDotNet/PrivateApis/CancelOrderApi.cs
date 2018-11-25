using BitbankDotNet.Entities;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
namespace BitbankDotNet
{
    partial class BitbankRestApiClient
    {
        const string CancelOrderPath = "/v1/user/spot/cancel_order";
        const string CancelOrdersPath = "/v1/user/spot/cancel_orders";

        /// <summary>
        /// [Private API]注文をキャンセルします。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <param name="orderId">注文ID</param>
        /// <returns>注文情報</returns>
        /// <exception cref="BitbankDotNetException">APIリクエストでエラーが発生しました。</exception>
        public Task<Order> CancelOrderAsync(CurrencyPair pair, long orderId)
        {
            var body = new OrderInfoBody
            {
                Pair = pair,
                OrderId = orderId
            };
            return PrivateApiPostAsync<Order, OrderInfoBody>(CancelOrderPath, body);
        }

        /// <summary>
        /// [Private API]複数の注文をキャンセルします。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <param name="orderIds">複数の注文ID</param>
        /// <returns>注文情報</returns>
        /// <exception cref="BitbankDotNetException">APIリクエストでエラーが発生しました。</exception>
        public async Task<Order[]> CancelOrdersAsync(CurrencyPair pair, long[] orderIds)
        {
            var body = new OrdersInfoBody
            {
                Pair = pair,
                OrderIds = orderIds
            };
            var result = await PrivateApiPostAsync<OrderList, OrdersInfoBody>(CancelOrdersPath, body)
                .ConfigureAwait(false);

            return result.Orders;
        }
    }
}