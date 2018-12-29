using System;
using System.Runtime.Serialization;
using BitbankDotNet.Resolvers;
using SpanJson;

namespace BitbankDotNet.Entities
{
    /// <summary>
    /// 約定履歴
    /// </summary>
    public class Transaction
    {
        /// <summary>
        /// 取引ID
        /// </summary>
        [DataMember(Name = "transaction_id")]
        public int TransactionId { get; set; }

        /// <summary>
        /// 注文の方向
        /// </summary>
        public OrderSide Side { get; set; }

        /// <summary>
        /// 価格
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 約定日時
        /// </summary>
        [DataMember(Name = "executed_at")]
        public DateTime ExecutedAt { get; set; }

        /// <inheritdoc />
        public override string ToString()
            => JsonSerializer.Generic.Utf16.Serialize<Transaction, BitbankResolver<char>>(this);
    }

    /// <summary>
    /// 約定履歴のリスト
    /// </summary>
    class TransactionList
    {
        /// <summary>
        /// 約定履歴のリスト
        /// </summary>
        public Transaction[] Transactions { get; set; }
    }
}