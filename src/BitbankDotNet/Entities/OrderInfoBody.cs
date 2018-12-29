using System.Runtime.Serialization;

namespace BitbankDotNet.Entities
{
    /// <summary>
    /// 注文情報のリクエストボディ
    /// </summary>
    class OrderInfoBody
    {
        /// <summary>
        /// 通貨ペア
        /// </summary>
        public CurrencyPair Pair { get; set; }

        /// <summary>
        /// 注文ID
        /// </summary>
        [DataMember(Name = "order_id")]
        public long OrderId { get; set; }
    }

    /// <summary>
    /// 注文情報リストのリクエストボディ
    /// </summary>
    class OrdersInfoBody
    {
        /// <summary>
        /// 通貨ペア
        /// </summary>
        public CurrencyPair Pair { get; set; }

        /// <summary>
        /// 注文IDリスト
        /// </summary>
        [DataMember(Name = "order_ids")]
        public long[] OrderIds { get; set; }
    }
}