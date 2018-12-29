using System;
using BitbankDotNet.Resolvers;
using SpanJson;

namespace BitbankDotNet.Entities
{
    /// <summary>
    /// ティッカー情報
    /// </summary>
    public class Ticker
    {
        /// <summary>
        /// 現在の売り注文の最安値
        /// </summary>
        public decimal Sell { get; set; }

        /// <summary>
        /// 現在の買い注文の最高値
        /// </summary>
        public decimal Buy { get; set; }

        /// <summary>
        /// 過去24時間の最高値取引価格
        /// </summary>
        public decimal High { get; set; }

        /// <summary>
        /// 過去24時間の最安値取引価格
        /// </summary>
        public decimal Low { get; set; }

        /// <summary>
        /// 最新取引価格
        /// </summary>
        public decimal Last { get; set; }

        /// <summary>
        /// 過去24時間の出来高
        /// </summary>
        public decimal Vol { get; set; }

        /// <summary>
        /// 日時
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <inheritdoc />
        public override string ToString()
            => JsonSerializer.Generic.Utf16.Serialize<Ticker, BitbankResolver<char>>(this);
    }
}