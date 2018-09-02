using BitbankDotNet.Formatters;
using SpanJson;
using System;

namespace BitbankDotNet.Api.Entities
{
    public class Ticker
    {
        [JsonCustomSerializer(typeof(DoubleAsStringFormatter))]
        public double Sell { get; set; }

        [JsonCustomSerializer(typeof(DoubleAsStringFormatter))]
        public double Buy { get; set; }

        [JsonCustomSerializer(typeof(DoubleAsStringFormatter))]
        public double High { get; set; }

        [JsonCustomSerializer(typeof(DoubleAsStringFormatter))]
        public double Low { get; set; }

        [JsonCustomSerializer(typeof(DoubleAsStringFormatter))]
        public double Last { get; set; }

        [JsonCustomSerializer(typeof(DoubleAsStringFormatter))]
        public double Vol { get; set; }

        public DateTime Timestamp { get; set; }

        public override string ToString()
            => JsonSerializer.Generic.Utf16.Serialize(this);
    }

    class TickerResponse : Response<Ticker>
    {
    }
}