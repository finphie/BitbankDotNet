using System.Diagnostics.CodeAnalysis;
using System.Text;
using BitbankDotNet.Resolvers;
using SpanJson;
using Xunit;
using static SpanJson.JsonSerializer.Generic.Utf16;
using static SpanJson.JsonSerializer.Generic.Utf8;

namespace BitbankDotNet.Tests.Formatters
{
    [SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "ユニットテスト")]
    public class DecimalAsStringFormatterTest
    {
        const string Json = "\"0.1\"";
        static readonly byte[] UJson = Encoding.UTF8.GetBytes(Json);

        [Fact]
        public void Deserialize_UTF8のJSON文字列を入力_decimalを返す()
        {
            var deserialize = Deserialize<decimal, BitbankResolver<byte>>(UJson);

            Assert.Equal(0.1M, deserialize);
        }

        [Fact]
        public void Deserialize_UTF8の不正なJSON文字列を入力_BitbankApiExceptionをスローする()
            => Assert.Throws<JsonParserException>(() =>
                Deserialize<decimal, BitbankResolver<byte>>(Encoding.UTF8.GetBytes("\"a\"")));

        [Fact]
        public void Deserialize_UTF16のJSON文字列を入力_decimalを返す()
        {
            var deserialize = Deserialize<decimal, BitbankResolver<char>>(Json);

            Assert.Equal(0.1M, deserialize);
        }

        [Fact]
        public void Serialize_decimalを入力_UTF8のJSON文字列を出力()
        {
            var serialize = Serialize<decimal, BitbankResolver<byte>>(0.1M);

            Assert.NotNull(serialize);
            Assert.Equal(UJson, serialize);
        }

        [Fact]
        public void Serialize_decimalを入力_UTF16のJSON文字列を出力()
        {
            var serialize = Serialize<decimal, BitbankResolver<char>>(0.1M);

            Assert.NotNull(serialize);
            Assert.Equal(Json, serialize);
        }
    }
}