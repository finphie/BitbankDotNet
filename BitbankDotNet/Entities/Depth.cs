using BitbankDotNet.Resolvers;
using SpanJson;

namespace BitbankDotNet.Entities
{
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

        /// <inheritdoc />
        public override string ToString()
            => JsonSerializer.Generic.Utf16.Serialize<Depth, BitbankResolver<char>>(this);
    }

    /// <summary>
    /// 板情報
    /// </summary>
    public class BoardOrder
    {
        /// <summary>
        /// 価格
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public decimal Amount { get; set; }

        /// <inheritdoc />
        public override string ToString()
            => JsonSerializer.Generic.Utf16.Serialize<BoardOrder, BitbankResolver<char>>(this);
    }
}