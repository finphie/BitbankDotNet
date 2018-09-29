using BitbankDotNet.Resolvers;
using SpanJson;
using System;
using System.Runtime.Serialization;

namespace BitbankDotNet.Entities
{
    /// <summary>
    /// ローソク足データ
    /// </summary>
    public class Ohlcv : IEntity
    {
        /// <summary>
        /// 始値
        /// </summary>
        public double Open { get; set; }

        /// <summary>
        /// 高値
        /// </summary>
        public double High { get; set; }

        /// <summary>
        /// 安値
        /// </summary>
        public double Low { get; set; }

        /// <summary>
        /// 終値
        /// </summary>
        public double Close { get; set; }

        /// <summary>
        /// 出来高
        /// </summary>
        public double Volume { get; set; }

        /// <summary>
        /// 日時
        /// </summary>
        public DateTime Date { get; set; }

        public override string ToString()
            => JsonSerializer.PrettyPrinter.Print(
                JsonSerializer.Generic.Utf16.SerializeToArrayPool<Ohlcv, BitbankResolver<char>>(this));
    }

    class Candlestick : IEntity
    {
        public CandleType Type { get; set; }

        public Ohlcv[] Ohlcv { get; set; }
    }

    class CandlestickList : IEntity, IEntityResponse
    {
        [DataMember(Name = "candlestick")]
        public Candlestick[] Candlesticks { get; set; }
    }
}