using BitbankDotNet.Resolvers;
using SpanJson;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace BitbankDotNet.Entities
{
    /// <summary>
    /// 約定履歴
    /// </summary>
    public class Trade : IEquatable<Trade>
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
        public double Amount { get; set; }

        /// <summary>
        /// 価格
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember(Name = "maker_taker")]
        public string MakerTaker { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember(Name = "fee_amount_base")]
        public string FeeAmountBase { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember(Name = "fee_amount_quote")]
        public string FeeAmountQuote { get; set; }

        /// <summary>
        /// 日時
        /// </summary>
        [DataMember(Name = "executed_at")]
        public DateTime ExecutedAt { get; set; }

        public override bool Equals(object obj)
            => Equals(obj as Trade);

        [SuppressMessage("ReSharper", "CompareOfFloatsByEqualityOperator")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Trade other)
            => other != null &&
               TradeId == other.TradeId &&
               Pair == other.Pair &&
               OrderId == other.OrderId &&
               Side == other.Side &&
               Type == other.Type &&
               Amount == other.Amount &&
               Price == other.Price &&
               MakerTaker == other.MakerTaker &&
               FeeAmountBase == other.FeeAmountBase &&
               FeeAmountQuote == other.FeeAmountQuote &&
               ExecutedAt == other.ExecutedAt;

        [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
        public override int GetHashCode()
        {
            var hash = new HashCode();
            hash.Add(TradeId);
            hash.Add(Pair);
            hash.Add(OrderId);
            hash.Add(Side);
            hash.Add(Type);
            hash.Add(Amount);
            hash.Add(Price);
            hash.Add(MakerTaker);
            hash.Add(FeeAmountBase);
            hash.Add(FeeAmountQuote);
            hash.Add(ExecutedAt);

            return hash.ToHashCode();
        }

        public override string ToString()
            => JsonSerializer.Generic.Utf16.Serialize<Trade, BitbankResolver<char>>(this);
    }

    class TradeList
    {
        public Trade[] Trades { get; set; }
    }

    class TradesResponse : Response<TradeList>
    {
    }
}