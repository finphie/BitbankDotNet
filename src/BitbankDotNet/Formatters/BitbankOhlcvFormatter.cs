using System.Diagnostics.CodeAnalysis;
using BitbankDotNet.Entities;
using SpanJson;

namespace BitbankDotNet.Formatters
{
    /// <summary>
    /// <see cref="Ohlcv"/>クラスのフォーマッター
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "SpanJson Formatter")]
    sealed class BitbankOhlcvFormatter : ICustomJsonFormatter<Ohlcv>
    {
        public static readonly BitbankOhlcvFormatter Default = new BitbankOhlcvFormatter();
        static readonly DecimalAsStringFormatter ElementDecimalAsStringFormatter = DecimalAsStringFormatter.Default;
        static readonly DateTimeAsLongFormatter ElementDateTimeAsLongFormatter = DateTimeAsLongFormatter.Default;

        public object Arguments { get; set; }

        public Ohlcv Deserialize(ref JsonReader<byte> reader)
        {
            reader.ReadUtf8BeginArrayOrThrow();
            var ohlcv = new Ohlcv();
            var count = 0;

            reader.TryReadUtf8IsEndArrayOrValueSeparator(ref count);
            ohlcv.Open = ElementDecimalAsStringFormatter.Deserialize(ref reader);

            reader.TryReadUtf8IsEndArrayOrValueSeparator(ref count);
            ohlcv.High = ElementDecimalAsStringFormatter.Deserialize(ref reader);

            reader.TryReadUtf8IsEndArrayOrValueSeparator(ref count);
            ohlcv.Low = ElementDecimalAsStringFormatter.Deserialize(ref reader);

            reader.TryReadUtf8IsEndArrayOrValueSeparator(ref count);
            ohlcv.Close = ElementDecimalAsStringFormatter.Deserialize(ref reader);

            reader.TryReadUtf8IsEndArrayOrValueSeparator(ref count);
            ohlcv.Volume = ElementDecimalAsStringFormatter.Deserialize(ref reader);

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
            ohlcv.Open = ElementDecimalAsStringFormatter.Deserialize(ref reader);

            reader.TryReadUtf16IsEndArrayOrValueSeparator(ref count);
            ohlcv.High = ElementDecimalAsStringFormatter.Deserialize(ref reader);

            reader.TryReadUtf16IsEndArrayOrValueSeparator(ref count);
            ohlcv.Low = ElementDecimalAsStringFormatter.Deserialize(ref reader);

            reader.TryReadUtf16IsEndArrayOrValueSeparator(ref count);
            ohlcv.Close = ElementDecimalAsStringFormatter.Deserialize(ref reader);

            reader.TryReadUtf16IsEndArrayOrValueSeparator(ref count);
            ohlcv.Volume = ElementDecimalAsStringFormatter.Deserialize(ref reader);

            reader.TryReadUtf16IsEndArrayOrValueSeparator(ref count);
            ohlcv.Date = ElementDateTimeAsLongFormatter.Deserialize(ref reader);

            reader.TryReadUtf16IsEndArrayOrValueSeparator(ref count);

            return ohlcv;
        }

        public void Serialize(ref JsonWriter<byte> writer, Ohlcv value)
        {
            writer.WriteUtf8BeginArray();

            ElementDecimalAsStringFormatter.Serialize(ref writer, value.Open);
            writer.WriteUtf8ValueSeparator();
            ElementDecimalAsStringFormatter.Serialize(ref writer, value.High);
            writer.WriteUtf8ValueSeparator();
            ElementDecimalAsStringFormatter.Serialize(ref writer, value.Low);
            writer.WriteUtf8ValueSeparator();
            ElementDecimalAsStringFormatter.Serialize(ref writer, value.Close);
            writer.WriteUtf8ValueSeparator();
            ElementDecimalAsStringFormatter.Serialize(ref writer, value.Volume);
            writer.WriteUtf8ValueSeparator();
            ElementDateTimeAsLongFormatter.Serialize(ref writer, value.Date);

            writer.WriteUtf8EndArray();
        }

        public void Serialize(ref JsonWriter<char> writer, Ohlcv value)
        {
            writer.WriteUtf16BeginArray();

            ElementDecimalAsStringFormatter.Serialize(ref writer, value.Open);
            writer.WriteUtf16ValueSeparator();
            ElementDecimalAsStringFormatter.Serialize(ref writer, value.High);
            writer.WriteUtf16ValueSeparator();
            ElementDecimalAsStringFormatter.Serialize(ref writer, value.Low);
            writer.WriteUtf16ValueSeparator();
            ElementDecimalAsStringFormatter.Serialize(ref writer, value.Close);
            writer.WriteUtf16ValueSeparator();
            ElementDecimalAsStringFormatter.Serialize(ref writer, value.Volume);
            writer.WriteUtf16ValueSeparator();
            ElementDateTimeAsLongFormatter.Serialize(ref writer, value.Date);

            writer.WriteUtf16EndArray();
        }
    }
}