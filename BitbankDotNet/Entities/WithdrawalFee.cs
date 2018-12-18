using BitbankDotNet.Resolvers;
using SpanJson;

namespace BitbankDotNet.Entities
{
    /// <summary>
    /// 出金手数料
    /// </summary>
    public class WithdrawalFee
    {
        /// <summary>
        /// 手数料変動のしきい値
        /// </summary>
        public decimal Threshold { get; set; }

        /// <summary>
        /// 手数料（<see cref="Threshold"/>未満）
        /// </summary>
        public decimal Under { get; set; }

        /// <summary>
        /// 手数料（<see cref="Threshold"/>以上）
        /// </summary>
        public decimal Over { get; set; }

        /// <inheritdoc />
        public override string ToString()
            => JsonSerializer.Generic.Utf16.Serialize<WithdrawalFee, BitbankResolver<char>>(this);
    }
}