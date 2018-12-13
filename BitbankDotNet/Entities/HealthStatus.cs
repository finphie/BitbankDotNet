using System.Runtime.Serialization;
using BitbankDotNet.Resolvers;
using SpanJson;

namespace BitbankDotNet.Entities
{
    public class HealthStatus
    {
        /// <summary>
        /// 通貨ペア
        /// </summary>
        public CurrencyPair Pair { get; set; }

        /// <summary>
        /// 取引所ステータス
        /// </summary>
        public SystemStatus Status { get; set; }

        /// <summary>
        /// 取引所ステータスに応じた最小注文数量
        /// </summary>
        [DataMember(Name = "min_amount")]
        public decimal MinAmount { get; set; }

        public override string ToString()
            => JsonSerializer.Generic.Utf16.Serialize<HealthStatus, BitbankResolver<char>>(this);
    }

    class HealthStatusList
    {
        public HealthStatus[] Statuses { get; set; }
    }
}