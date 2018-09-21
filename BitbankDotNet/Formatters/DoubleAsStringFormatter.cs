using BitbankDotNet.Helpers;
using SpanJson;
using SpanJson.Formatters;
using System.Buffers.Text;
using System.Globalization;

namespace BitbankDotNet.Formatters
{
    sealed class DoubleAsStringFormatter : ICustomJsonFormatter<double>
    {
        public static readonly DoubleAsStringFormatter Default = new DoubleAsStringFormatter();

        public double Deserialize(ref JsonReader<byte> reader)
        {
            var span = reader.ReadUtf8StringSpan();
            if (!Utf8Parser.TryParse(span, out double value, out var consumed) || span.Length != consumed)
                ThrowHelper.ThrowJsonParserException(JsonParserException.ParserError.InvalidNumberFormat, reader.Position);

            return value;
        }

        public double Deserialize(ref JsonReader<char> reader)
            => double.Parse(reader.ReadUtf16StringSpan(), NumberStyles.Float, CultureInfo.InvariantCulture);

        // TODO: 後で書き直す
        public void Serialize(ref JsonWriter<byte> writer, double value, int nestingLimit)
            => StringUtf8Formatter.Default.Serialize(ref writer, value.ToString(CultureInfo.InvariantCulture), nestingLimit);

        // TODO: 後で書き直す
        public void Serialize(ref JsonWriter<char> writer, double value, int nestingLimit)
            => StringUtf16Formatter.Default.Serialize(ref writer, value.ToString(CultureInfo.InvariantCulture), nestingLimit);
    }
}