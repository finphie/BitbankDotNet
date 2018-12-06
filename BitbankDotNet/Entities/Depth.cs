﻿using BitbankDotNet.Resolvers;
using SpanJson;

namespace BitbankDotNet.Entities
{
    public class BoardOrder
    {
        /// <summary>
        /// 価格
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public double Amount { get; set; }

        public override string ToString()
            => JsonSerializer.PrettyPrinter.Print(
                JsonSerializer.Generic.Utf16.SerializeToArrayPool<BoardOrder, BitbankResolver<char>>(this));
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
            => JsonSerializer.PrettyPrinter.Print(
                JsonSerializer.Generic.Utf16.SerializeToArrayPool<Depth, BitbankResolver<char>>(this));
    }
}