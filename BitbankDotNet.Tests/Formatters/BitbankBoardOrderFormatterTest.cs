using System.Diagnostics.CodeAnalysis;
using System.Text;
using BitbankDotNet.Entities;
using BitbankDotNet.Resolvers;
using Xunit;
using static SpanJson.JsonSerializer.Generic.Utf16;
using static SpanJson.JsonSerializer.Generic.Utf8;

namespace BitbankDotNet.Tests.Formatters
{
    [SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "ユニットテスト")]
    public class BitbankBoardOrderFormatterTest
    {
        const string Json = "[\"0.1\",\"1.2\"]";

        static readonly BoardOrder Entity = new BoardOrder
        {
            Price = 0.1,
            Amount = 1.2
        };

        static readonly byte[] UJson = Encoding.UTF8.GetBytes(Json);

        [Fact]
        public void Deserialize_UTF8のJSON文字列を入力_BoardOrderを返す()
        {
            var deserialize = Deserialize<BoardOrder, BitbankResolver<byte>>(UJson);

            Assert.NotNull(deserialize);
            Assert.Equal(Entity.Price, deserialize.Price);
            Assert.Equal(Entity.Amount, deserialize.Amount);
        }

        [Fact]
        public void Deserialize_UTF16のJSON文字列を入力_BoardOrderを返す()
        {
            var deserialize = Deserialize<BoardOrder, BitbankResolver<char>>(Json);

            Assert.NotNull(deserialize);
            Assert.Equal(Entity.Price, deserialize.Price);
            Assert.Equal(Entity.Amount, deserialize.Amount);
        }

        [Fact]
        public void Serialize_BoardOrderを入力_UTF8のJSON文字列を出力()
        {
            var serialize = Serialize<BoardOrder, BitbankResolver<byte>>(Entity);

            Assert.NotNull(serialize);
            Assert.Equal(UJson, serialize);
        }

        [Fact]
        public void Serialize_BoardOrderを入力_UTF16のJSON文字列を出力()
        {
            var serialize = Serialize<BoardOrder, BitbankResolver<char>>(Entity);

            Assert.NotNull(serialize);
            Assert.Equal(Json, serialize);
        }
    }
}