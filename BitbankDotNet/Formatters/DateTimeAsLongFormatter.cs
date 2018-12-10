using System;
using SpanJson;

namespace BitbankDotNet.Formatters
{
    /// <summary>
    /// <see cref="DateTime"/>構造体のフォーマッター
    /// </summary>
    sealed class DateTimeAsLongFormatter : ICustomJsonFormatter<DateTime>
    {
        public static readonly DateTimeAsLongFormatter Default = new DateTimeAsLongFormatter();

        public DateTime Deserialize(ref JsonReader<byte> reader)
            => DateTimeOffset.FromUnixTimeMilliseconds(reader.ReadUtf8Int64()).DateTime;

        public DateTime Deserialize(ref JsonReader<char> reader)
            => DateTimeOffset.FromUnixTimeMilliseconds(reader.ReadUtf16Int64()).DateTime;

        public void Serialize(ref JsonWriter<byte> writer, DateTime value, int nestingLimit)
            => writer.WriteUtf8Int64(((DateTimeOffset)DateTime.SpecifyKind(value, DateTimeKind.Utc))
                .ToUnixTimeMilliseconds());

        public void Serialize(ref JsonWriter<char> writer, DateTime value, int nestingLimit)
            => writer.WriteUtf16Int64(((DateTimeOffset)DateTime.SpecifyKind(value, DateTimeKind.Utc))
                .ToUnixTimeMilliseconds());
    }
}