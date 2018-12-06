using BitbankDotNet.Formatters;
using BitbankDotNet.Resolvers;
using SpanJson;
using System.Runtime.Serialization;

namespace BitbankDotNet.Entities
{
    /// <summary>
    /// アセット一覧
    /// </summary>
    public class Asset : IEntity
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
        public double OnhandAmount { get; set; }

        /// <summary>
        /// ロックされている量
        /// </summary>
        [DataMember(Name = "locked_amount")]
        public double LockedAmount { get; set; }

        /// <summary>
        /// 利用可能な量
        /// </summary>
        [DataMember(Name = "free_amount")]
        public double FreeAmount { get; set; }

        /// <summary>
        /// 手数料
        /// </summary>
        [DataMember(Name = "withdrawal_fee")]
        [JsonCustomSerializer(typeof(BitbankWithdrawalFeeFormatter))]
        public WithdrawalFee WithdrawalFee { get; set; }

        // stop_depositとstop_withdrawalはドキュメントには載っていない。
        // 使い道もなさそうなのでコメントアウトしておく。
        //[DataMember(Name = "stop_deposit")]
        //public bool StopDeposit { get; set; }
        //[DataMember(Name = "stop_withdrawal")]
        //public bool StopWithdrawal { get; set; }

        public override string ToString()
            => JsonSerializer.PrettyPrinter.Print(
                JsonSerializer.Generic.Utf16.SerializeToArrayPool<Asset, BitbankResolver<char>>(this));
    }

    class AssetList : IEntity, IEntityResponse
    {
        public Asset[] Assets { get; set; }
    }
}