using BitbankDotNet.Entities;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
namespace BitbankDotNet
{
    partial class BitbankClient
    {
        /// <summary>
        /// [PrivateAPI]注文をキャンセルします。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <param name="orderId">注文ID</param>
        /// <returns>注文情報</returns>
        public Task<Order> CancelOrderAsync(CurrencyPair pair, long orderId)
            => PrivateApiPostAsync<Order, OrderInfoBody>("/v1/user/spot/cancel_order", new OrderInfoBody
            {
                Pair = pair,
                OrderId = orderId
            });

        /// <summary>
        /// [PrivateAPI]複数の注文をキャンセルします。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <param name="orderIds">複数の注文ID</param>
        /// <returns>注文情報</returns>
        public async Task<Order[]> CancelOrdersAsync(CurrencyPair pair, long[] orderIds)
            => (await PrivateApiPostAsync<OrderList, OrdersInfoBody>("/v1/user/spot/cancel_orders", new OrdersInfoBody
            {
                Pair = pair,
                OrderIds = orderIds
            }).ConfigureAwait(false)).Orders;
    }
}