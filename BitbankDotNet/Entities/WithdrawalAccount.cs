using BitbankDotNet.Resolvers;
using SpanJson;

namespace BitbankDotNet.Entities
{
    /// <summary>
    /// 出金アカウント情報
    /// </summary>
    public class WithdrawalAccount
    {
        /// <summary>
        /// 出金アカウントのID
        /// </summary>
        public string Uuid { get; set; }

        /// <summary>
        /// ラベル
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// 出金先アドレス
        /// </summary>
        public string Address { get; set; }

        public override string ToString()
            => JsonSerializer.Generic.Utf16.Serialize<WithdrawalAccount, BitbankResolver<char>>(this);
    }

    class WithdrawalAccountList
    {
        public WithdrawalAccount[] Accounts { get; set; }
    }
}