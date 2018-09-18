using BitbankDotNet.Resolvers;
using SpanJson;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace BitbankDotNet.Entities
{
    /// <summary>
    /// 出金情報
    /// </summary>
    public class Withdrawal : IEquatable<Withdrawal>
    {
        /// <summary>
        /// 出金アカウントのID
        /// </summary>
        public Guid Uuid { get; set; }

        /// <summary>
        /// アセット名
        /// </summary>
        public AssetName Asset { get; set; }

        /// <summary>
        /// アカウントのID
        /// </summary>
        [DataMember(Name = "account_uuid")]
        public Guid AccountUuid { get; set; }

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

        public override bool Equals(object obj)
            => Equals(obj as Withdrawal);

        [SuppressMessage("ReSharper", "CompareOfFloatsByEqualityOperator")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Withdrawal other)
            => other != null &&
               Uuid.Equals(other.Uuid) &&
               Asset == other.Asset &&
               AccountUuid.Equals(other.AccountUuid) &&
               Amount == other.Amount &&
               Fee == other.Fee &&
               Label == other.Label &&
               Address == other.Address &&
               TxId == other.TxId &&
               Status == other.Status &&
               RequestedAt == other.RequestedAt;

        [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
        public override int GetHashCode()
        {
            var hash = new HashCode();
            hash.Add(Uuid);
            hash.Add(Asset);
            hash.Add(AccountUuid);
            hash.Add(Amount);
            hash.Add(Fee);
            hash.Add(Label);
            hash.Add(Address);
            hash.Add(TxId);
            hash.Add(Status);
            hash.Add(RequestedAt);

            return hash.ToHashCode();
        }

        public override string ToString()
            => JsonSerializer.Generic.Utf16.Serialize<Withdrawal, BitbankResolver<char>>(this);
    }

    class WithdrawalResponse : Response<Withdrawal>
    {
    }
}