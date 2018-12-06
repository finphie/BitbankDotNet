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
        public double Threshold { get; set; }

        /// <summary>
        /// 手数料（<see cref="Threshold"/>未満）
        /// </summary>
        public double Under { get; set; }

        /// <summary>
        /// 手数料（<see cref="Threshold"/>以上）
        /// </summary>
        public double Over { get; set; }
    }
}