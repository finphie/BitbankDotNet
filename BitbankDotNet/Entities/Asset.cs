using System.Runtime.Serialization;
using BitbankDotNet.Formatters;
using BitbankDotNet.Resolvers;
using SpanJson;

namespace BitbankDotNet.Entities
{
    /// <summary>
    /// アセット一覧
    /// </summary>
    public class Asset
    {
        /// <summary>
        /// アセット名
        /// </summary>
        [DataMember(Name = "asset")]
        public AssetName Name { get; set; }

        /// <summary>
        /// 小数点の表示精度
        /// </summary>
        [DataMember(Name = "amount_precision")]
        public int AmountPrecision { get; set; }

        /// <summary>
        /// 保有量
        /// </summary>
        [DataMember(Name = "onhand_amount")]
        public decimal OnhandAmount { get; set; }

        /// <summary>
        /// ロックされている量
        /// </summary>
        [DataMember(Name = "locked_amount")]
        public decimal LockedAmount { get; set; }

        /// <summary>
        /// 利用可能な量
        /// </summary>
        [DataMember(Name = "free_amount")]
        public decimal FreeAmount { get; set; }

        /// <summary>
        /// 手数料
        /// </summary>
        [DataMember(Name = "withdrawal_fee")]
        [JsonCustomSerializer(typeof(BitbankWithdrawalFeeFormatter))]
        public WithdrawalFee WithdrawalFee { get; set; }

#if false
        /// <summary>
        /// 入金停止
        /// </summary>
        [DataMember(Name = "stop_deposit")]
        public bool StopDeposit { get; set; }

        /// <summary>
        /// 出金停止
        /// </summary>
        [DataMember(Name = "stop_withdrawal")]
        public bool StopWithdrawal { get; set; }
#endif

        public override string ToString()
            => JsonSerializer.Generic.Utf16.Serialize<Asset, BitbankResolver<char>>(this);
    }

    class AssetList
    {
        public Asset[] Assets { get; set; }
    }
}