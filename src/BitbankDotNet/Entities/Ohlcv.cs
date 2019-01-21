using System;
using BitbankDotNet.Resolvers;
using SpanJson;

namespace BitbankDotNet.Entities
{
    /// <summary>
    /// OHLCVデータ
    /// </summary>
    public class Ohlcv
    {
        /// <summary>
        /// 始値
        /// </summary>
        public decimal Open { get; set; }

        /// <summary>
        /// 高値
        /// </summary>
        public decimal High { get; set; }

        /// <summary>
        /// 安値
        /// </summary>
        public decimal Low { get; set; }

        /// <summary>
        /// 終値
        /// </summary>
        public decimal Close { get; set; }

        /// <summary>
        /// 出来高
        /// </summary>
        public decimal Volume { get; set; }

        /// <summary>
        /// 日時
        /// </summary>
        public DateTime Date { get; set; }

        /// <inheritdoc />
        public override string ToString()
            => JsonSerializer.Generic.Utf16.Serialize<Ohlcv, BitbankResolver<char>>(this);
    }
}