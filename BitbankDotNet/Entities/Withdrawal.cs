using BitbankDotNet.Resolvers;
using SpanJson;
using System;
using System.Runtime.Serialization;

namespace BitbankDotNet.Entities
{
    /// <summary>
    /// 出金情報
    /// </summary>
    public class Withdrawal : IEntity, IEntityResponse
    {
        /// <summary>
        /// 出金アカウントのID
        /// </summary>
        public string Uuid { get; set; }

        /// <summary>
        /// アセット名
        /// </summary>
        public AssetName Asset { get; set; }

        /// <summary>
        /// アカウントのID
        /// </summary>
        [DataMember(Name = "account_uuid")]
        public string AccountUuid { get; set; }

        /// <summary>
        /// 引き出し量
        /// </summary>
        public double Amount { get; set; }

        /// <summary>
        /// 引き出し手数料
        /// </summary>
        public double Fee { get; set; }

        /// <summary>
        /// ラベル
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// 引き出し先アドレス
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 引き出し送金トランザクションID
        /// </summary>
        public string TxId { get; set; }

        /// <summary>
        /// ステータス
        /// </summary>
        public WithdrawalStatus Status { get; set; }

        /// <summary>
        /// リクエスト日時
        /// </summary>
        [DataMember(Name = "requested_at")]
        public DateTime RequestedAt { get; set; }

        public override string ToString()
            => JsonSerializer.Generic.Utf16.Serialize<Withdrawal, BitbankResolver<char>>(this);
    }
}