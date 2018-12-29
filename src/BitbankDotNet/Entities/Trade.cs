using System;
using System.Runtime.Serialization;
using BitbankDotNet.Resolvers;
using SpanJson;

namespace BitbankDotNet.Entities
{
    /// <summary>
    /// 約定履歴
    /// </summary>
    public class Trade
    {
        /// <summary>
        /// 注文ID
        /// </summary>
        [DataMember(Name = "trade_id")]
        public long TradeId { get; set; }

        /// <summary>
        /// 通貨ペア
        /// </summary>
        public CurrencyPair Pair { get; set; }

        /// <summary>
        /// 取引ID
        /// </summary>
        [DataMember(Name = "order_id")]
        public long OrderId { get; set; }

        /// <summary>
        /// 注文の方向
        /// </summary>
        public OrderSide Side { get; set; }

        /// <summary>
        /// 注文の種類
        /// </summary>
        public OrderType Type { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 価格
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 注文方法
        /// </summary>
        [DataMember(Name = "maker_taker")]
        public LiquidityType MakerTaker { get; set; }

        /// <summary>
        /// 基本手数料
        /// </summary>
        [DataMember(Name = "fee_amount_base")]
        public decimal FeeAmountBase { get; set; }

        /// <summary>
        /// 手数料見積もり
        /// </summary>
        [DataMember(Name = "fee_amount_quote")]
        public decimal FeeAmountQuote { get; set; }

        /// <summary>
        /// 日時
        /// </summary>
        [DataMember(Name = "executed_at")]
        public DateTime ExecutedAt { get; set; }

        /// <inheritdoc />
        public override string ToString()
            => JsonSerializer.Generic.Utf16.Serialize<Trade, BitbankResolver<char>>(this);
    }

    /// <summary>
    /// 約定履歴のリスト
    /// </summary>
    class TradeList
    {
        /// <summary>
        /// 約定履歴のリスト
        /// </summary>
        public Trade[] Trades { get; set; }
    }
}