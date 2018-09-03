using SpanJson;
using System;
using System.Collections.Generic;

namespace BitbankDotNet.Formatters
{
    sealed class BitbankDepthFormatter : ICustomJsonFormatter<List<double[]>>
    {
        public static readonly BitbankDepthFormatter Default = new BitbankDepthFormatter();
        static readonly DoubleAsStringArrayFormatter ElementFormatter = DoubleAsStringArrayFormatter.Default;

        public List<double[]> Deserialize(ref JsonReader<byte> reader)
        {
            var list = new List<double[]>(4);
            reader.ReadUtf8BeginArrayOrThrow();
            var count = 0;
            while (!reader.TryReadUtf8IsEndArrayOrValueSeparator(ref count))
                list.Add(ElementFormatter.Deserialize(ref reader));

            return list;
        }

        public List<double[]> Deserialize(ref JsonReader<char> reader)
        {
            var list = new List<double[]>(4);
            reader.ReadUtf16BeginArrayOrThrow();
            var count = 0;
            while (!reader.TryReadUtf16IsEndArrayOrValueSeparator(ref count))
                list.Add(ElementFormatter.Deserialize(ref reader));

            return list;
        }

        public void Serialize(ref JsonWriter<byte> writer, List<double[]> value, int nestingLimit)
        {
            throw new NotImplementedException();
        }

        public void Serialize(ref JsonWriter<char> writer, List<double[]> value, int nestingLimit)
        {
            throw new NotImplementedException();
        }     
    }
}