﻿using BitbankDotNet.Formatters;
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
        [JsonCustomSerializer(typeof(BitbankDepthFormatter))]
        public double[][] Asks { get; set; }

        /// <summary>
        /// 買い板
        /// </summary>
        [JsonCustomSerializer(typeof(BitbankDepthFormatter))]
        public double[][] Bids { get; set; }

        public override string ToString()
            => JsonSerializer.Generic.Utf16.Serialize(this);
    }

    class DepthResponse : Response<Depth>
    {
    }
}