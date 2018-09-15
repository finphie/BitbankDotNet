using BitbankDotNet.Resolvers;
using SpanJson;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace BitbankDotNet.Entities
{
    /// <summary>
    /// 出金アカウント情報
    /// </summary>  
    public class Account : IEquatable<Account>
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

        public override bool Equals(object obj)
            => Equals(obj as Account);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Account other)
            => other != null &&
               Uuid == other.Uuid &&
               Label == other.Label &&
               Address == other.Address;

        [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
        public override int GetHashCode()
            => HashCode.Combine(Uuid, Label, Address);

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