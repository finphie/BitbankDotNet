using BitbankDotNet.Entities;
using BitbankDotNet.Resolvers;
using SpanJson;
using SpanJson.Formatters;

namespace BitbankDotNet.Formatters
{
    sealed class BitbankOhlcvArrayFormatter : ICustomJsonFormatter<Ohlcv[]>
    {
        public static readonly BitbankOhlcvArrayFormatter Default = new BitbankOhlcvArrayFormatter();

        public Ohlcv[] Deserialize(ref JsonReader<byte> reader)
            => ArrayFormatter<Ohlcv, byte, BitbankResolver<byte>>.Default.Deserialize(ref reader);

        public Ohlcv[] Deserialize(ref JsonReader<char> reader)
            => ArrayFormatter<Ohlcv, char, BitbankResolver<char>>.Default.Deserialize(ref reader);

        public void Serialize(ref JsonWriter<byte> writer, Ohlcv[] value, int nestingLimit)
            => ArrayFormatter<Ohlcv, byte, BitbankResolver<byte>>.Default.Serialize(ref writer, value, nestingLimit);

        public void Serialize(ref JsonWriter<char> writer, Ohlcv[] value, int nestingLimit)
            => ArrayFormatter<Ohlcv, char, BitbankResolver<char>>.Default.Serialize(ref writer, value, nestingLimit);
    }
}