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
    public class Transaction : IEquatable<Transaction>
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

        public override bool Equals(object obj)
            => Equals(obj as Transaction);

        [SuppressMessage("ReSharper", "CompareOfFloatsByEqualityOperator")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Transaction other)
            => other != null &&
               TransactionId == other.TransactionId &&
               Side == other.Side &&
               Price == other.Price &&
               Amount == other.Amount &&
               ExecutedAt == other.ExecutedAt;

        [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
        public override int GetHashCode()
            => HashCode.Combine(TransactionId, Side, Price, Amount, ExecutedAt);

        public override string ToString()
            => JsonSerializer.Generic.Utf16.Serialize<Transaction, BitbankResolver<char>>(this);
    }

    class TransactionList
    {
        public Transaction[] Transactions { get; set; }
    }

    class TransactionResponse : Response<TransactionList>
    {
    }
}