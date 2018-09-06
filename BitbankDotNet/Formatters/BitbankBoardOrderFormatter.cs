﻿using BitbankDotNet.Entities;
using SpanJson;

namespace BitbankDotNet.Formatters
{
    sealed class BitbankBoardOrderFormatter : ICustomJsonFormatter<BoardOrder>
    {
        public static readonly BitbankBoardOrderFormatter Default = new BitbankBoardOrderFormatter();
        static readonly DoubleAsStringFormatter ElementFormatter = DoubleAsStringFormatter.Default;

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

        public void Serialize(ref JsonWriter<byte> writer, BoardOrder value, int nestingLimit)
        {
            throw new System.NotImplementedException();
        }

        public void Serialize(ref JsonWriter<char> writer, BoardOrder value, int nestingLimit)
        {
            throw new System.NotImplementedException();
        }
    }
}