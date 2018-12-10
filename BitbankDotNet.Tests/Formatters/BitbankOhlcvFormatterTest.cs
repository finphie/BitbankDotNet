using System;
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
    public class BitbankOhlcvFormatterTest
    {
        static readonly Ohlcv Entity = new Ohlcv
        {
            Open = 0.1,
            High = 1.2,
            Low = 2.3,
            Close = 3.4,
            Volume = 4.5,
            Date = DateTimeOffset.Parse("2018-01-02T03:04:05.678Z").UtcDateTime
        };

        const string Json = "[\"0.1\",\"1.2\",\"2.3\",\"3.4\",\"4.5\",1514862245678]";
        static readonly byte[] UJson = Encoding.UTF8.GetBytes(Json);

        [Fact]
        public void Deserialize_UTF8のJSON文字列を入力_Ohlcvを返す()
        {
            var deserialize = Deserialize<Ohlcv, BitbankResolver<byte>>(UJson);

            Assert.NotNull(deserialize);
            Assert.Equal(Entity.Open, deserialize.Open);
            Assert.Equal(Entity.High, deserialize.High);
            Assert.Equal(Entity.Low, deserialize.Low);
            Assert.Equal(Entity.Close, deserialize.Close);
            Assert.Equal(Entity.Volume, deserialize.Volume);
            Assert.Equal(Entity.Date, deserialize.Date);
        }

        [Fact]
        public void Deserialize_UTF16のJSON文字列を入力_Ohlcvを返す()
        {
            var deserialize = Deserialize<Ohlcv, BitbankResolver<char>>(Json);

            Assert.NotNull(deserialize);
            Assert.Equal(Entity.Open, deserialize.Open);
            Assert.Equal(Entity.High, deserialize.High);
            Assert.Equal(Entity.Low, deserialize.Low);
            Assert.Equal(Entity.Close, deserialize.Close);
            Assert.Equal(Entity.Volume, deserialize.Volume);
            Assert.Equal(Entity.Date, deserialize.Date);
        }

        [Fact]
        public void Serialize_Ohlcvを入力_UTF8のJSON文字列を出力()
        {
            var serialize = Serialize<Ohlcv, BitbankResolver<byte>>(Entity);

            Assert.NotNull(serialize);
            Assert.Equal(UJson, serialize);
        }

        [Fact]
        public void Serialize_Ohlcvを入力_UTF16のJSON文字列を出力()
        {
            var serialize = Serialize<Ohlcv, BitbankResolver<char>>(Entity);

            Assert.NotNull(serialize);
            Assert.Equal(Json, serialize);
        }
    }
}