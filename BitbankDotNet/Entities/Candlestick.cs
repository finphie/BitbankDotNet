﻿using System;
using System.Runtime.Serialization;
using BitbankDotNet.Resolvers;
using SpanJson;

namespace BitbankDotNet.Entities
{
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

    /// <summary>
    /// ローソク足データ
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

        public override string ToString()
            => JsonSerializer.PrettyPrinter.Print(
                JsonSerializer.Generic.Utf16.SerializeToArrayPool<Ohlcv, BitbankResolver<char>>(this));
    }
}