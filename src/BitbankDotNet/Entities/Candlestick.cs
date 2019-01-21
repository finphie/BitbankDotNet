using System.Runtime.Serialization;

namespace BitbankDotNet.Entities
{
    /// <summary>
    /// ローソク足
    /// </summary>
    class Candlestick
    {
        /// <summary>
        /// ローソク足の期間
        /// </summary>
        public CandleType Type { get; set; }

        /// <summary>
        /// ローソク足データのリスト
        /// </summary>
        public Ohlcv[] Ohlcv { get; set; }
    }

    /// <summary>
    /// ローソク足データのリスト
    /// </summary>
    class CandlestickList
    {
        /// <summary>
        /// ローソク足データのリスト
        /// </summary>
        [DataMember(Name = "candlestick")]
        public Candlestick[] Candlesticks { get; set; }
    }
}