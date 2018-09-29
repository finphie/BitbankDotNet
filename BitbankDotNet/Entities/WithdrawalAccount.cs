using BitbankDotNet.Resolvers;
using SpanJson;
using System;

namespace BitbankDotNet.Entities
{
    /// <summary>
    /// 出金アカウント情報
    /// </summary>
    public class WithdrawalAccount : IEntity
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
            => JsonSerializer.PrettyPrinter.Print(
                JsonSerializer.Generic.Utf16.SerializeToArrayPool<WithdrawalAccount, BitbankResolver<char>>(this));
    }

    class WithdrawalAccountList : IEntity, IEntityResponse
    {
        public WithdrawalAccount[] Accounts { get; set; }
    }
}