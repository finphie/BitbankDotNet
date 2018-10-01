using BitbankDotNet.Resolvers;
using SpanJson;
using System.Runtime.Serialization;

namespace BitbankDotNet.Entities
{
    public class HealthStatus : IEntity
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
        public double MinAmount { get; set; }

        public override string ToString()
            => JsonSerializer.PrettyPrinter.Print(
                JsonSerializer.Generic.Utf16.SerializeToArrayPool<HealthStatus, BitbankResolver<char>>(this));
    }

    class HealthStatusList : IEntityResponse
    {
        public HealthStatus[] Statuses { get; set; }
    }
}