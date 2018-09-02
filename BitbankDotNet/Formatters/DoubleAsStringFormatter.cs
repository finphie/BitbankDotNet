using SpanJson;
using SpanJson.Formatters;
using System;
using System.Globalization;

namespace BitbankDotNet.Formatters
{
    sealed class DoubleAsStringFormatter : ICustomJsonFormatter<double>
    {
        public static readonly DoubleAsStringFormatter Default = new DoubleAsStringFormatter();

        public double Deserialize(ref JsonReader<byte> reader)
        {
            var value = StringUtf8Formatter.Default.Deserialize(ref reader);
            if (double.TryParse(value, out var doubleValue))
                return doubleValue;

            throw new InvalidOperationException("Invalid value.");
        }

        public double Deserialize(ref JsonReader<char> reader)
        {
            var value = StringUtf16Formatter.Default.Deserialize(ref reader);
            if (double.TryParse(value, out var doubleValue))
                return doubleValue;

            throw new InvalidOperationException("Invalid value.");
        }

        public void Serialize(ref JsonWriter<byte> writer, double value, int nestingLimit)
            => StringUtf8Formatter.Default.Serialize(ref writer, value.ToString(CultureInfo.InvariantCulture), nestingLimit);

        public void Serialize(ref JsonWriter<char> writer, double value, int nestingLimit)
            => StringUtf16Formatter.Default.Serialize(ref writer, value.ToString(CultureInfo.InvariantCulture), nestingLimit);
    }
}