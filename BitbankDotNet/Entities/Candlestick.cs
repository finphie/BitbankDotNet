using BitbankDotNet.Resolvers;
using SpanJson;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace BitbankDotNet.Entities
{
    /// <summary>
    /// ローソク足データ
    /// </summary>
    public class Ohlcv : IEquatable<Ohlcv>
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

        public override bool Equals(object obj)
            => Equals(obj as Ohlcv);

        [SuppressMessage("ReSharper", "CompareOfFloatsByEqualityOperator")]
        public bool Equals(Ohlcv other)
            => other != null &&
               Open == other.Open &&
               High == other.High &&
               Low == other.Low &&
               Close == other.Close &&
               Volume == other.Volume &&
               Date == other.Date;

        [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
        public override int GetHashCode()
            => HashCode.Combine(Open, High, Low, Close, Volume, Date);

        public override string ToString()
            => JsonSerializer.Generic.Utf16.Serialize<Ohlcv, BitbankResolver<char>>(this);
    }

    class Candlestick
    {
        public CandleType Type { get; set; }

        public Ohlcv[] Ohlcv { get; set; }
    }

    class CandlestickList
    {
        [DataMember(Name = "candlestick")]
        public Candlestick[] Candlesticks { get; set; }
    }

    class CandlesticksResponse : Response<CandlestickList>
    {
    }
}