using System.Diagnostics.CodeAnalysis;
using BitbankDotNet.Entities;
using SpanJson;

namespace BitbankDotNet.Formatters
{
    /// <summary>
    /// <see cref="BoardOrder"/>クラスのフォーマッター
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "SpanJson Formatter")]
    sealed class BitbankBoardOrderFormatter : ICustomJsonFormatter<BoardOrder>
    {
        public static readonly BitbankBoardOrderFormatter Default = new BitbankBoardOrderFormatter();
        static readonly DecimalAsStringFormatter ElementFormatter = DecimalAsStringFormatter.Default;

        public object Arguments { get; set; }

        public BoardOrder Deserialize(ref JsonReader<byte> reader)
        {
            reader.ReadUtf8BeginArrayOrThrow();
            var boardOrder = new BoardOrder();
            var count = 0;

            reader.TryReadUtf8IsEndArrayOrValueSeparator(ref count);
            boardOrder.Price = ElementFormatter.Deserialize(ref reader);

            reader.TryReadUtf8IsEndArrayOrValueSeparator(ref count);
            boardOrder.Amount = ElementFormatter.Deserialize(ref reader);

            reader.TryReadUtf8IsEndArrayOrValueSeparator(ref count);

            return boardOrder;
        }

        public BoardOrder Deserialize(ref JsonReader<char> reader)
        {
            reader.ReadUtf16BeginArrayOrThrow();
            var boardOrder = new BoardOrder();
            var count = 0;

            reader.TryReadUtf16IsEndArrayOrValueSeparator(ref count);
            boardOrder.Price = ElementFormatter.Deserialize(ref reader);

            reader.TryReadUtf16IsEndArrayOrValueSeparator(ref count);
            boardOrder.Amount = ElementFormatter.Deserialize(ref reader);

            reader.TryReadUtf16IsEndArrayOrValueSeparator(ref count);

            return boardOrder;
        }

        public void Serialize(ref JsonWriter<byte> writer, BoardOrder value)
        {
            writer.WriteUtf8BeginArray();

            ElementFormatter.Serialize(ref writer, value.Price);
            writer.WriteUtf8ValueSeparator();
            ElementFormatter.Serialize(ref writer, value.Amount);

            writer.WriteUtf8EndArray();
        }

        public void Serialize(ref JsonWriter<char> writer, BoardOrder value)
        {
            writer.WriteUtf16BeginArray();

            ElementFormatter.Serialize(ref writer, value.Price);
            writer.WriteUtf16ValueSeparator();
            ElementFormatter.Serialize(ref writer, value.Amount);

            writer.WriteUtf16EndArray();
        }
    }
}