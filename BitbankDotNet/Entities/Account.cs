using BitbankDotNet.Resolvers;
using SpanJson;
using System;

namespace BitbankDotNet.Entities
{
    /// <summary>
    /// 出金アカウント情報
    /// </summary>
    public class Account
    {
        /// <summary>
        /// 出金アカウントのID
        /// </summary>
        public Guid Uuid { get; set; }

        /// <summary>
        /// ラベル
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// 出金先アドレス
        /// </summary>
        public string Address { get; set; }

        public override string ToString()
            => JsonSerializer.Generic.Utf16.Serialize<Account, BitbankResolver<char>>(this);
    }

    class AccountList
    {
        public Account[] Accounts { get; set; }
    }

    class AccountResponse : Response<AccountList>
    {
    }
}