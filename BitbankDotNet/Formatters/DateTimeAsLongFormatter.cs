using SpanJson;
using System;

namespace BitbankDotNet.Formatters
{
    sealed class DateTimeAsLongFormatter : ICustomJsonFormatter<DateTime>
    {
        public static readonly DateTimeAsLongFormatter Default = new DateTimeAsLongFormatter();

        public DateTime Deserialize(ref JsonReader<byte> reader)
            => DateTimeOffset.FromUnixTimeMilliseconds(reader.ReadUtf8Int64()).DateTime;

        public DateTime Deserialize(ref JsonReader<char> reader)
            => DateTimeOffset.FromUnixTimeMilliseconds(reader.ReadUtf16Int64()).DateTime;

        public void Serialize(ref JsonWriter<byte> writer, DateTime value, int nestingLimit)
            => writer.WriteUtf8Int64(((DateTimeOffset) value).ToUnixTimeMilliseconds());

        public void Serialize(ref JsonWriter<char> writer, DateTime value, int nestingLimit)
            => writer.WriteUtf16Int64(((DateTimeOffset) value).ToUnixTimeMilliseconds());
    }
}