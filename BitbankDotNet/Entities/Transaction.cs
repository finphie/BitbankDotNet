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
        public double Price { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public double Amount { get; set; }

        /// <summary>
        /// 約定日時
        /// </summary>
        [DataMember(Name = "executed_at")]
        public DateTime ExecutedAt { get; set; }

        public override string ToString()
            => JsonSerializer.PrettyPrinter.Print(
                JsonSerializer.Generic.Utf16.SerializeToArrayPool<Transaction, BitbankResolver<char>>(this));
    }

    class TransactionList
    {
        public Transaction[] Transactions { get; set; }
    }
}