using System;
using System.Runtime.Serialization;

namespace BitbankDotNet.Entities
{
    public class Ohlcv
    {
        public double Open { get; set; }

        public double High { get; set; }

        public double Low { get; set; }

        public double Close { get; set; }

        public double Volume { get; set; }

        public DateTime Date { get; set; }
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

    class CandlestickResponse : Response<CandlestickList>
    {
    }
}