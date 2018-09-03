using BitbankDotNet.Entities;
using SpanJson;
using System;

namespace BitbankDotNet.Formatters
{
    sealed class BitbankOhlcvFormatter : ICustomJsonFormatter<Ohlcv>
    {
        public static readonly BitbankOhlcvFormatter Default = new BitbankOhlcvFormatter();
        static readonly DoubleAsStringFormatter ElementFormatter1 = DoubleAsStringFormatter.Default;
        static readonly DateTimeAsLongFormatter ElementFormatter2 = DateTimeAsLongFormatter.Default;

        public Ohlcv Deserialize(ref JsonReader<byte> reader)
        {
            reader.ReadUtf8BeginArrayOrThrow();
            var ohlcv = new Ohlcv();
            var count = 0;

            reader.TryReadUtf8IsEndArrayOrValueSeparator(ref count);
            ohlcv.Open = ElementFormatter1.Deserialize(ref reader);

            reader.TryReadUtf8IsEndArrayOrValueSeparator(ref count);
            ohlcv.High = ElementFormatter1.Deserialize(ref reader);

            reader.TryReadUtf8IsEndArrayOrValueSeparator(ref count);
            ohlcv.Low = ElementFormatter1.Deserialize(ref reader);

            reader.TryReadUtf8IsEndArrayOrValueSeparator(ref count);
            ohlcv.Close = ElementFormatter1.Deserialize(ref reader);

            reader.TryReadUtf8IsEndArrayOrValueSeparator(ref count);
            ohlcv.Volume = ElementFormatter1.Deserialize(ref reader);

            reader.TryReadUtf8IsEndArrayOrValueSeparator(ref count);
            ohlcv.Date = ElementFormatter2.Deserialize(ref reader);

            reader.TryReadUtf8IsEndArrayOrValueSeparator(ref count);

            return ohlcv;
        }

        public Ohlcv Deserialize(ref JsonReader<char> reader)
        {
            reader.ReadUtf16BeginArrayOrThrow();
            var ohlcv = new Ohlcv();
            var count = 0;

            reader.TryReadUtf16IsEndArrayOrValueSeparator(ref count);
            ohlcv.Open = ElementFormatter1.Deserialize(ref reader);

            reader.TryReadUtf16IsEndArrayOrValueSeparator(ref count);
            ohlcv.High = ElementFormatter1.Deserialize(ref reader);

            reader.TryReadUtf16IsEndArrayOrValueSeparator(ref count);
            ohlcv.Low = ElementFormatter1.Deserialize(ref reader);

            reader.TryReadUtf16IsEndArrayOrValueSeparator(ref count);
            ohlcv.Close = ElementFormatter1.Deserialize(ref reader);

            reader.TryReadUtf16IsEndArrayOrValueSeparator(ref count);
            ohlcv.Volume = ElementFormatter1.Deserialize(ref reader);

            reader.TryReadUtf16IsEndArrayOrValueSeparator(ref count);
            ohlcv.Date = ElementFormatter2.Deserialize(ref reader);

            reader.TryReadUtf16IsEndArrayOrValueSeparator(ref count);

            return ohlcv;
        }

        public void Serialize(ref JsonWriter<byte> writer, Ohlcv value, int nestingLimit)
        {
            throw new NotImplementedException();
        }

        public void Serialize(ref JsonWriter<char> writer, Ohlcv value, int nestingLimit)
        {
            throw new NotImplementedException();
        }
    }
}