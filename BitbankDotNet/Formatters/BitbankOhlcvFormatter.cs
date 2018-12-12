using BitbankDotNet.Entities;
using SpanJson;

namespace BitbankDotNet.Formatters
{
    /// <summary>
    /// <see cref="Ohlcv"/>クラスのフォーマッター
    /// </summary>
    sealed class BitbankOhlcvFormatter : ICustomJsonFormatter<Ohlcv>
    {
        public static readonly BitbankOhlcvFormatter Default = new BitbankOhlcvFormatter();
        static readonly DoubleAsStringFormatter ElementDoubleAsStringFormatter = DoubleAsStringFormatter.Default;
        static readonly DateTimeAsLongFormatter ElementDateTimeAsLongFormatter = DateTimeAsLongFormatter.Default;

        public Ohlcv Deserialize(ref JsonReader<byte> reader)
        {
            reader.ReadUtf8BeginArrayOrThrow();
            var ohlcv = new Ohlcv();
            var count = 0;

            reader.TryReadUtf8IsEndArrayOrValueSeparator(ref count);
            ohlcv.Open = ElementDoubleAsStringFormatter.Deserialize(ref reader);

            reader.TryReadUtf8IsEndArrayOrValueSeparator(ref count);
            ohlcv.High = ElementDoubleAsStringFormatter.Deserialize(ref reader);

            reader.TryReadUtf8IsEndArrayOrValueSeparator(ref count);
            ohlcv.Low = ElementDoubleAsStringFormatter.Deserialize(ref reader);

            reader.TryReadUtf8IsEndArrayOrValueSeparator(ref count);
            ohlcv.Close = ElementDoubleAsStringFormatter.Deserialize(ref reader);

            reader.TryReadUtf8IsEndArrayOrValueSeparator(ref count);
            ohlcv.Volume = ElementDoubleAsStringFormatter.Deserialize(ref reader);

            reader.TryReadUtf8IsEndArrayOrValueSeparator(ref count);
            ohlcv.Date = ElementDateTimeAsLongFormatter.Deserialize(ref reader);

            reader.TryReadUtf8IsEndArrayOrValueSeparator(ref count);

            return ohlcv;
        }

        public Ohlcv Deserialize(ref JsonReader<char> reader)
        {
            reader.ReadUtf16BeginArrayOrThrow();
            var ohlcv = new Ohlcv();
            var count = 0;

            reader.TryReadUtf16IsEndArrayOrValueSeparator(ref count);
            ohlcv.Open = ElementDoubleAsStringFormatter.Deserialize(ref reader);

            reader.TryReadUtf16IsEndArrayOrValueSeparator(ref count);
            ohlcv.High = ElementDoubleAsStringFormatter.Deserialize(ref reader);

            reader.TryReadUtf16IsEndArrayOrValueSeparator(ref count);
            ohlcv.Low = ElementDoubleAsStringFormatter.Deserialize(ref reader);

            reader.TryReadUtf16IsEndArrayOrValueSeparator(ref count);
            ohlcv.Close = ElementDoubleAsStringFormatter.Deserialize(ref reader);

            reader.TryReadUtf16IsEndArrayOrValueSeparator(ref count);
            ohlcv.Volume = ElementDoubleAsStringFormatter.Deserialize(ref reader);

            reader.TryReadUtf16IsEndArrayOrValueSeparator(ref count);
            ohlcv.Date = ElementDateTimeAsLongFormatter.Deserialize(ref reader);

            reader.TryReadUtf16IsEndArrayOrValueSeparator(ref count);

            return ohlcv;
        }

        public void Serialize(ref JsonWriter<byte> writer, Ohlcv value, int nestingLimit)
        {
            writer.WriteUtf8BeginArray();

            ElementDoubleAsStringFormatter.Serialize(ref writer, value.Open, nestingLimit);
            writer.WriteUtf8ValueSeparator();
            ElementDoubleAsStringFormatter.Serialize(ref writer, value.High, nestingLimit);
            writer.WriteUtf8ValueSeparator();
            ElementDoubleAsStringFormatter.Serialize(ref writer, value.Low, nestingLimit);
            writer.WriteUtf8ValueSeparator();
            ElementDoubleAsStringFormatter.Serialize(ref writer, value.Close, nestingLimit);
            writer.WriteUtf8ValueSeparator();
            ElementDoubleAsStringFormatter.Serialize(ref writer, value.Volume, nestingLimit);
            writer.WriteUtf8ValueSeparator();
            ElementDateTimeAsLongFormatter.Serialize(ref writer, value.Date, nestingLimit);

            writer.WriteUtf8EndArray();
        }

        public void Serialize(ref JsonWriter<char> writer, Ohlcv value, int nestingLimit)
        {
            writer.WriteUtf16BeginArray();

            ElementDoubleAsStringFormatter.Serialize(ref writer, value.Open, nestingLimit);
            writer.WriteUtf16ValueSeparator();
            ElementDoubleAsStringFormatter.Serialize(ref writer, value.High, nestingLimit);
            writer.WriteUtf16ValueSeparator();
            ElementDoubleAsStringFormatter.Serialize(ref writer, value.Low, nestingLimit);
            writer.WriteUtf16ValueSeparator();
            ElementDoubleAsStringFormatter.Serialize(ref writer, value.Close, nestingLimit);
            writer.WriteUtf16ValueSeparator();
            ElementDoubleAsStringFormatter.Serialize(ref writer, value.Volume, nestingLimit);
            writer.WriteUtf16ValueSeparator();
            ElementDateTimeAsLongFormatter.Serialize(ref writer, value.Date, nestingLimit);

            writer.WriteUtf16EndArray();
        }
    }
}