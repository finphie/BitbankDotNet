using System.Threading.Tasks;
using BitbankDotNet.Entities;

// ReSharper disable once CheckNamespace
namespace BitbankDotNet
{
    partial class BitbankRestApiClient
    {
        const string SendOrderPath = "/v1/user/spot/order";

        /// <summary>
        /// [Private API]新規指値買い注文を行います。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <param name="price">価格</param>
        /// <param name="amount">数量</param>
        /// <returns>注文情報</returns>
        /// <exception cref="BitbankDotNetException">APIリクエストでエラーが発生しました。</exception>
        public Task<Order> SendBuyOrderAsync(CurrencyPair pair, double price, double amount)
            => SendLimitOrderAsync(pair, price, amount, OrderSide.Buy, OrderType.Limit);

        /// <summary>
        /// [Private API]新規成行買い注文を行います。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <param name="amount">数量</param>
        /// <returns>注文情報</returns>
        /// <exception cref="BitbankDotNetException">APIリクエストでエラーが発生しました。</exception>
        public Task<Order> SendBuyOrderAsync(CurrencyPair pair, double amount)
            => SendMarketOrderAsync(pair, amount, OrderSide.Buy, OrderType.Market);

        /// <summary>
        /// [Private API]新規指値売り注文を行います。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <param name="price">価格</param>
        /// <param name="amount">数量</param>
        /// <returns>注文情報</returns>
        /// <exception cref="BitbankDotNetException">APIリクエストでエラーが発生しました。</exception>
        public Task<Order> SendSellOrderAsync(CurrencyPair pair, double price, double amount)
            => SendLimitOrderAsync(pair, price, amount, OrderSide.Sell, OrderType.Limit);

        /// <summary>
        /// [Private API]新規成行売り注文を行います。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <param name="amount">数量</param>
        /// <returns>注文情報</returns>
        /// <exception cref="BitbankDotNetException">APIリクエストでエラーが発生しました。</exception>
        public Task<Order> SendSellOrderAsync(CurrencyPair pair, double amount)
            => SendMarketOrderAsync(pair, amount, OrderSide.Sell, OrderType.Market);

        /// <summary>
        /// [Private API]新規指値注文を行います。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <param name="price">価格</param>
        /// <param name="amount">数量</param>
        /// <param name="side">注文の方向</param>
        /// <param name="type">注文の種類</param>
        /// <returns>注文情報</returns>
        /// <exception cref="BitbankDotNetException">APIリクエストでエラーが発生しました。</exception>
        Task<Order> SendLimitOrderAsync(CurrencyPair pair, double price, double amount, OrderSide side, OrderType type)
        {
            var body = new LimitOrderBody
            {
                Pair = pair,
                Amount = amount,
                Price = price,
                Side = side,
                Type = type
            };
            return PrivateApiPostAsync<Order, LimitOrderBody>(SendOrderPath, body);
        }

        /// <summary>
        /// [Private API]新規成行注文を行います。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <param name="amount">数量</param>
        /// <param name="side">注文の方向</param>
        /// <param name="type">注文の種類</param>
        /// <returns>注文情報</returns>
        /// <exception cref="BitbankDotNetException">APIリクエストでエラーが発生しました。</exception>
        Task<Order> SendMarketOrderAsync(CurrencyPair pair, double amount, OrderSide side, OrderType type)
        {
            var body = new MarketOrderBody
            {
                Pair = pair,
                Amount = amount,
                Side = side,
                Type = type
            };
            return PrivateApiPostAsync<Order, MarketOrderBody>(SendOrderPath, body);
        }
    }
}