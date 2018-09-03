using BitbankDotNet.Resolvers;
using SpanJson;
using SpanJson.Formatters;

namespace BitbankDotNet.Formatters
{
    sealed class BitbankDepthFormatter : ICustomJsonFormatter<double[][]>
    {
        public static readonly BitbankDepthFormatter Default = new BitbankDepthFormatter();
        static readonly DoubleAsStringArrayFormatter ElementFormatter = DoubleAsStringArrayFormatter.Default;

        public double[][] Deserialize(ref JsonReader<byte> reader)
            => ArrayFormatter<double[], byte, BitbankResolver<byte>>.Default.Deserialize(ref reader);

        public double[][] Deserialize(ref JsonReader<char> reader)
            => ArrayFormatter<double[], char, BitbankResolver<char>>.Default.Deserialize(ref reader);

        public void Serialize(ref JsonWriter<byte> writer, double[][] value, int nestingLimit)
            => ArrayFormatter<double[], byte, BitbankResolver<byte>>.Default.Serialize(ref writer, value, nestingLimit);

        public void Serialize(ref JsonWriter<char> writer, double[][] value, int nestingLimit)
            => ArrayFormatter<double[], char, BitbankResolver<char>>.Default.Serialize(ref writer, value, nestingLimit);
    }
}