using BitbankDotNet.Formatters;
using SpanJson;
using System.Collections.Generic;

namespace BitbankDotNet.Api.Entities
{
    public class Depth
    {
        [JsonCustomSerializer(typeof(BitbankDepthFormatter))]
        public List<double[]> Asks { get; set; }
        [JsonCustomSerializer(typeof(BitbankDepthFormatter))]
        public List<double[]> Bids { get; set; }

        public override string ToString()
            => JsonSerializer.Generic.Utf16.Serialize(this);
    }

    class DepthResponse : Response<Depth>
    {
    }
}