using BitbankDotNet.Resolvers;
using SpanJson;
using System;
using System.Runtime.Serialization;

namespace BitbankDotNet.Entities
{
    /// <summary>
    /// 注文情報
    /// </summary>
    public class Order : IEntity, IEntityResponse
    {
        /// <summary>
        /// 取引ID
        /// </summary>
        [DataMember(Name = "order_id")]
        public long OrderId { get; set; }

        /// <summary>
        /// 通貨ペア
        /// </summary>
        public CurrencyPair Pair { get; set; }

        /// <summary>
        /// 注文の方向
        /// </summary>
        public OrderSide Side { get; set; }

        /// <summary>
        /// 注文の種類
        /// </summary>
        public OrderType Type { get; set; }

        /// <summary>
        /// 注文時の数量
        /// </summary>
        [DataMember(Name = "start_amount")]
        public double StartAmount { get; set; }

        /// <summary>
        /// 未約定の数量
        /// </summary>
        [DataMember(Name = "remaining_amount")]
        public double RemainingAmount { get; set; }

        /// <summary>
        /// 約定済み数量
        /// </summary>
        [DataMember(Name = "executed_amount")]
        public double ExecutedAmount { get; set; }

        /// <summary>
        /// 注文価格
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// 平均約定価格
        /// </summary>
        [DataMember(Name = "average_price")]
        public double AveragePrice { get; set; }

        /// <summary>
        /// 注文日時
        /// </summary>
        [DataMember(Name = "ordered_at")]
        public DateTime OrderedAt { get; set; }

        /// <summary>
        /// 注文ステータス
        /// </summary>
        public OrderStatus Status { get; set; }

        public override string ToString()
            => JsonSerializer.PrettyPrinter.Print(
                JsonSerializer.Generic.Utf16.SerializeToArrayPool<Order, BitbankResolver<char>>(this));
    }

    class OrderList : IEntity, IEntityResponse
    {
        public Order[] Orders { get; set; }
    }
}