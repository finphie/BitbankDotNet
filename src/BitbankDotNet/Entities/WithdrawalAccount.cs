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

        /// <inheritdoc />
        public override string ToString()
            => JsonSerializer.Generic.Utf16.Serialize<WithdrawalAccount, BitbankResolver<char>>(this);
    }

    /// <summary>
    /// 出金アカウント情報のリスト
    /// </summary>
    class WithdrawalAccountList
    {
        /// <summary>
        /// 出金アカウント情報のリスト
        /// </summary>
        public WithdrawalAccount[] Accounts { get; set; }
    }
}