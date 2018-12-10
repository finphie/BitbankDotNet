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
    public class DoubleAsStringFormatterTest
    {
        const string Json = "\"0.1\"";
        static readonly byte[] UJson = Encoding.UTF8.GetBytes(Json);

        [Fact]
        public void Deserialize_UTF8のJSON文字列を入力_doubleを返す()
        {
            var deserialize = Deserialize<double, BitbankResolver<byte>>(UJson);

            Assert.Equal(0.1, deserialize);
        }

        [Fact]
        public void Deserialize_UTF8の不正なJSON文字列を入力_BitbankApiExceptionをスローする()
            => Assert.Throws<JsonParserException>(() =>
                Deserialize<double, BitbankResolver<byte>>(Encoding.UTF8.GetBytes("\"a\"")));

        [Fact]
        public void Deserialize_UTF16のJSON文字列を入力_doubleを返す()
        {
            var deserialize = Deserialize<double, BitbankResolver<char>>(Json);

            Assert.Equal(0.1, deserialize);
        }

        [Fact]
        public void Serialize_doubleを入力_UTF8のJSON文字列を出力()
        {
            var serialize = Serialize<double, BitbankResolver<byte>>(0.1);

            Assert.NotNull(serialize);
            Assert.Equal(UJson, serialize);
        }

        [Fact]
        public void Serialize_doubleを入力_UTF16のJSON文字列を出力()
        {
            var serialize = Serialize<double, BitbankResolver<char>>(0.1);

            Assert.NotNull(serialize);
            Assert.Equal(Json, serialize);
        }
    }
}