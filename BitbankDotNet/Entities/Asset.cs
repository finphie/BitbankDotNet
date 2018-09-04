using System.Runtime.Serialization;

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

        // TODO: Asset.WithdrawalFee専用のFormatterが必要
        // ドキュメントではstringが返ってくることになっているが、
        // JPYの場合のみ、オブジェクトが返ってくるので対応する必要あり。（threshold/under/over）
        //[DataMember(Name = "withdrawal_fee")]
        //public double WithdrawalFee { get; set; }

        // stop_depositとstop_withdrawalはドキュメントには載っていない。
        // 使い道もなさそうなのでコメントアウトしておく。
        //[DataMember(Name = "stop_deposit")]
        //public bool StopDeposit { get; set; }
        //[DataMember(Name = "stop_withdrawal")]
        //public bool StopWithdrawal { get; set; }
    }

    class AssetList
    {
        public Asset[] Assets { get; set; }
    }

    class AssetResponse : Response<AssetList>
    {
    }
}