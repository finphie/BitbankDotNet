using BitbankDotNet.Formatters;
using SpanJson;
using System;
using System.Runtime.Serialization;

namespace BitbankDotNet.Api.Entities
{
    public class Transaction
    {
        [DataMember(Name = "transaction_id")]
        public int TransactionId { get; set; }

        public OrderSide Side { get; set; }

        [JsonCustomSerializer(typeof(DoubleAsStringFormatter))]
        public double Price { get; set; }

        [JsonCustomSerializer(typeof(DoubleAsStringFormatter))]
        public double Amount { get; set; }

        [DataMember(Name = "executed_at")]
        public DateTime ExecutedAt { get; set; }

        public override string ToString()
            => JsonSerializer.Generic.Utf16.Serialize(this);
    }

    class TransactionList
    {    
        public Transaction[] Transactions { get; set; }
    }

    class TransactionsResponse : Response<TransactionList>
    {
    }
}