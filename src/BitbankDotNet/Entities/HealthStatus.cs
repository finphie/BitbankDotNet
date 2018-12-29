using System.Runtime.Serialization;
using BitbankDotNet.Resolvers;
using SpanJson;

namespace BitbankDotNet.Entities
{
    /// <summary>
    /// 取引所ステータス
    /// </summary>
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

        /// <inheritdoc />
        public override string ToString()
            => JsonSerializer.Generic.Utf16.Serialize<HealthStatus, BitbankResolver<char>>(this);
    }

    /// <summary>
    /// 取引所ステータスのリスト
    /// </summary>
    class HealthStatusList
    {
        /// <summary>
        /// 取引所ステータスのリスト
        /// </summary>
        public HealthStatus[] Statuses { get; set; }
    }
}