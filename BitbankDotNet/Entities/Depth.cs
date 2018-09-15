using BitbankDotNet.Resolvers;
using SpanJson;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace BitbankDotNet.Entities
{
    public class BoardOrder : IEquatable<BoardOrder>
    {
        /// <summary>
        /// 価格
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public double Amount { get; set; }

        public override bool Equals(object obj)
            => Equals(obj as BoardOrder);

        [SuppressMessage("ReSharper", "CompareOfFloatsByEqualityOperator")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(BoardOrder other)
            => other != null &&
               Price == other.Price &&
               Amount == other.Amount;

        [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
        public override int GetHashCode()
            => HashCode.Combine(Price, Amount);

        public override string ToString()
            => JsonSerializer.Generic.Utf16.Serialize<BoardOrder, BitbankResolver<char>>(this);
    }

    /// <summary>
    /// 板情報
    /// </summary>
    public class Depth
    {
        /// <summary>
        /// 売り板
        /// </summary>
        public BoardOrder[] Asks { get; set; }

        /// <summary>
        /// 買い板
        /// </summary>
        public BoardOrder[] Bids { get; set; }

        public override string ToString()
            => JsonSerializer.Generic.Utf16.Serialize<Depth, BitbankResolver<char>>(this);
    }

    class DepthResponse : Response<Depth>
    {
    }
}