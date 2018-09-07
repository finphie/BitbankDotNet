using BitbankDotNet.Entities;
using BitbankDotNet.Extensions;
using System.Threading.Tasks;
using System.Web;

namespace BitbankDotNet
{
    public partial class BitbankClient
    {
        /// <summary>
        /// [PrivateAPI]アセット一覧を返します。
        /// </summary>
        /// <returns>アセット一覧</returns>
        public async Task<Asset[]> GetAssetAsync()
            => (await GetAsync<AssetResponse>("/v1/user/assets").ConfigureAwait(false)).Data.Assets;

        /// <summary>
        /// [PrivateAPI]注文情報を取得します。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <param name="orderId">注文ID</param>
        /// <returns>注文情報</returns>
        public async Task<Order> GetOrderAsync(CurrencyPair pair, long orderId)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["pair"] = pair.GetEnumMemberValue();
            query["order_id"] = orderId.ToString();

            return (await GetAsync<OrderResponse>("/v1/user/spot/order?" + query).ConfigureAwait(false)).Data;
        }

        /// <summary>
        /// [PrivateAPI]複数の注文情報を取得します。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <param name="orderIds">複数の注文ID</param>
        /// <returns>注文情報</returns>
        public async Task<Order[]> GetOrdersAsync(CurrencyPair pair, long[] orderIds)
            => (await PostAsync<OrdersResponse, OrdersInfoBody>("/v1/user/spot/orders_info", new OrdersInfoBody
            {
                Pair = pair,
                OrderIds = orderIds
            }).ConfigureAwait(false)).Data.Orders;

        // TODO: 価格や数量では、APIやdoubleの有効桁数をチェックする。
        /// <summary>
        /// [PrivateAPI]新規指値注文を行います。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <param name="price">価格</param>
        /// <param name="amount">数量</param>
        /// <param name="side">注文の方向</param>
        /// <param name="type">注文の種類</param>
        /// <returns>注文情報</returns>
        async Task<Order> SendLimitOrderAsync(CurrencyPair pair, double price, double amount, OrderSide side, OrderType type)
            => (await PostAsync<OrderResponse, LimitOrderBody>("/v1/user/spot/order", new LimitOrderBody
            {
                Pair = pair,
                Amount = amount,
                Price = price,
                Side = side,
                Type = type
            }).ConfigureAwait(false)).Data;

        /// <summary>
        /// [PrivateAPI]新規成行注文を行います。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <param name="amount">数量</param>
        /// <param name="side">注文の方向</param>
        /// <param name="type">注文の種類</param>
        /// <returns>注文情報</returns>
        async Task<Order> SendMarketOrderAsync(CurrencyPair pair, double amount, OrderSide side, OrderType type)
            => (await PostAsync<OrderResponse, MarketOrderBody>("/v1/user/spot/order", new MarketOrderBody
            {
                Pair = pair,
                Amount = amount,
                Side = side,
                Type = type
            }).ConfigureAwait(false)).Data;

        /// <summary>
        /// [PrivateAPI]新規指値買い注文を行います。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <param name="price">価格</param>
        /// <param name="amount">数量</param>
        /// <returns>注文情報</returns>
        public async Task<Order> SendBuyOrderAsync(CurrencyPair pair, double price, double amount)
            => await SendLimitOrderAsync(pair, price, amount, OrderSide.Buy, OrderType.Limit).ConfigureAwait(false);

        /// <summary>
        /// [PrivateAPI]新規成行買い注文を行います。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <param name="amount">数量</param>
        /// <returns>注文情報</returns>
        public async Task<Order> SendBuyOrderAsync(CurrencyPair pair, double amount)
            => await SendMarketOrderAsync(pair, amount, OrderSide.Buy, OrderType.Market).ConfigureAwait(false);

        /// <summary>
        /// [PrivateAPI]新規指値売り注文を行います。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <param name="price">価格</param>
        /// <param name="amount">数量</param>
        /// <returns>注文情報</returns>
        public async Task<Order> SendSellOrderAsync(CurrencyPair pair, double price, double amount)
            => await SendLimitOrderAsync(pair, price, amount, OrderSide.Sell, OrderType.Limit).ConfigureAwait(false);

        /// <summary>
        /// [PrivateAPI]新規成行売り注文を行います。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <param name="amount">数量</param>
        /// <returns>注文情報</returns>
        public async Task<Order> SendSellOrderAsync(CurrencyPair pair, double amount)
            => await SendMarketOrderAsync(pair, amount, OrderSide.Sell, OrderType.Market).ConfigureAwait(false);

        /// <summary>
        /// [PrivateAPI]注文をキャンセルします。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <param name="orderId">注文ID</param>
        /// <returns>注文情報</returns>
        public async Task<Order> CancelOrderAsync(CurrencyPair pair, long orderId)
            => (await PostAsync<OrderResponse, OrderInfoBody>("/v1/user/spot/cancel_order", new OrderInfoBody
            {
                Pair = pair,
                OrderId = orderId
            }).ConfigureAwait(false)).Data;

        /// <summary>
        /// [PrivateAPI]複数の注文をキャンセルします。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <param name="orderIds">複数の注文ID</param>
        /// <returns>注文情報</returns>
        public async Task<Order[]> CancelOrdersAsync(CurrencyPair pair, long[] orderIds)
            => (await PostAsync<OrdersResponse, OrdersInfoBody>("/v1/user/spot/cancel_orders", new OrdersInfoBody
            {
                Pair = pair,
                OrderIds = orderIds
            }).ConfigureAwait(false)).Data.Orders;
    }
}