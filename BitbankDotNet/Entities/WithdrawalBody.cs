using System.Runtime.Serialization;

namespace BitbankDotNet.Entities
{
    /// <summary>
    /// 出金リクエストのリクエストボディ
    /// </summary>
    class WithdrawalBody
    {
        /// <summary>
        /// アセット名
        /// </summary>
        public AssetName Asset { get; set; }

        /// <summary>
        /// 出金アカウントのuuid
        /// </summary>
        public string Uuid { get; set; }

        /// <summary>
        /// 引き出し量
        /// </summary>
        public double Amount { get; set; }

        /// <summary>
        /// 二段階認証トークン
        /// </summary>
        [DataMember(Name = "otp_token")]
        public int OtpToken { get; set; }

        /// <summary>
        /// SMS認証トークン
        /// </summary>
        [DataMember(Name = "sms_token")]
        public int SmsToken { get; set; }
    }
}