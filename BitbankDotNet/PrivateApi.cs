using BitbankDotNet.Entities;
using BitbankDotNet.Extensions;
using System;
using System.Threading.Tasks;
using System.Web;

namespace BitbankDotNet
{
    partial class BitbankClient
    {
        /// <summary>
        /// [PrivateAPI]アセット一覧を返します。
        /// </summary>
        /// <returns>アセット一覧</returns>
        public async Task<Asset[]> GetAssetsAsync()
            => (await PrivateApiGetAsync<AssetList>("/v1/user/assets").ConfigureAwait(false)).Assets;

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
        Task<Order> SendLimitOrderAsync(CurrencyPair pair, double price, double amount, OrderSide side, OrderType type)
            => PrivateApiPostAsync<Order, LimitOrderBody>("/v1/user/spot/order", new LimitOrderBody
            {
                Pair = pair,
                Amount = amount,
                Price = price,
                Side = side,
                Type = type
            });

        /// <summary>
        /// [PrivateAPI]新規成行注文を行います。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <param name="amount">数量</param>
        /// <param name="side">注文の方向</param>
        /// <param name="type">注文の種類</param>
        /// <returns>注文情報</returns>
        Task<Order> SendMarketOrderAsync(CurrencyPair pair, double amount, OrderSide side, OrderType type)
            => PrivateApiPostAsync<Order, MarketOrderBody>("/v1/user/spot/order", new MarketOrderBody
            {
                Pair = pair,
                Amount = amount,
                Side = side,
                Type = type
            });

        /// <summary>
        /// [PrivateAPI]新規指値買い注文を行います。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <param name="price">価格</param>
        /// <param name="amount">数量</param>
        /// <returns>注文情報</returns>
        public Task<Order> SendBuyOrderAsync(CurrencyPair pair, double price, double amount)
            => SendLimitOrderAsync(pair, price, amount, OrderSide.Buy, OrderType.Limit);

        /// <summary>
        /// [PrivateAPI]新規成行買い注文を行います。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <param name="amount">数量</param>
        /// <returns>注文情報</returns>
        public Task<Order> SendBuyOrderAsync(CurrencyPair pair, double amount)
            => SendMarketOrderAsync(pair, amount, OrderSide.Buy, OrderType.Market);

        /// <summary>
        /// [PrivateAPI]新規指値売り注文を行います。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <param name="price">価格</param>
        /// <param name="amount">数量</param>
        /// <returns>注文情報</returns>
        public Task<Order> SendSellOrderAsync(CurrencyPair pair, double price, double amount)
            => SendLimitOrderAsync(pair, price, amount, OrderSide.Sell, OrderType.Limit);

        /// <summary>
        /// [PrivateAPI]新規成行売り注文を行います。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <param name="amount">数量</param>
        /// <returns>注文情報</returns>
        public Task<Order> SendSellOrderAsync(CurrencyPair pair, double amount)
            => SendMarketOrderAsync(pair, amount, OrderSide.Sell, OrderType.Market);

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

            return (await PrivateApiGetAsync<OrderList>("/v1/user/spot/active_orders?" + query).ConfigureAwait(false)).Orders;
        }

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

            return (await PrivateApiGetAsync<TradeList>("/v1/user/spot/trade_history?" + query).ConfigureAwait(false)).Trades;
        }

        /// <summary>
        /// [PrivateAPI]出金アカウントを取得します。
        /// </summary>
        /// <param name="asset">通貨名</param>
        /// <returns>出金アカウント情報</returns>
        public async Task<WithdrawalAccount[]> GetWithdrawalAccountsAsync(AssetName asset)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["asset"] = asset.GetEnumMemberValue();

            return (await PrivateApiGetAsync<WithdrawalAccountList>("/v1/user/withdrawal_account?" + query).ConfigureAwait(false)).Accounts;
        }

        /// <summary>
        /// [PrivateAPI]出金リクエストを行います。
        /// </summary>
        /// <param name="asset">アセット名</param>
        /// <param name="amount">引き出し量</param>
        /// <param name="uuid">出金アカウントのUUID</param>
        /// <param name="otpToken">二段階認証トークン</param>
        /// <param name="smsToken">SMS認証トークン</param>
        /// <returns></returns>
        public Task<Withdrawal> RequestWithdrawalAsync(AssetName asset, double amount, string uuid, int otpToken, int smsToken)
            => PrivateApiPostAsync<Withdrawal, WithdrawalBody>("/v1/user/request_withdrawal", new WithdrawalBody
            {
                Asset = asset,
                Amount = amount,
                Uuid = uuid,
                OtpToken = otpToken,
                SmsToken = smsToken
            });
    }
}