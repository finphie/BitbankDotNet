using System.Buffers.Text;
using System.Globalization;
using BitbankDotNet.Helpers;
using SpanJson;

namespace BitbankDotNet.Formatters
{
    /// <summary>
    /// <see cref="decimal"/>のフォーマッター
    /// </summary>
    sealed class DecimalAsStringFormatter : ICustomJsonFormatter<decimal>
    {
        public static readonly DecimalAsStringFormatter Default = new DecimalAsStringFormatter();

        public decimal Deserialize(ref JsonReader<byte> reader)
        {
            if (!Utf8Parser.TryParse(reader.ReadUtf8StringSpan(), out decimal value, out _))
                ThrowHelper.ThrowJsonParserException(JsonParserException.ParserError.InvalidNumberFormat, reader.Position);

            return value;
        }

        public decimal Deserialize(ref JsonReader<char> reader)
            => decimal.Parse(reader.ReadUtf16StringSpan(), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);

        public void Serialize(ref JsonWriter<byte> writer, decimal value, int nestingLimit)
        {
            writer.WriteDoubleQuote();
            writer.WriteUtf8Decimal(value);
            writer.WriteDoubleQuote();
        }

        public void Serialize(ref JsonWriter<char> writer, decimal value, int nestingLimit)
        {
            writer.WriteDoubleQuote();
            writer.WriteUtf16Decimal(value);
            writer.WriteDoubleQuote();
        }
    }
}