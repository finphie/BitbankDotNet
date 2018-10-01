using BitbankDotNet.Formatters;
using SpanJson.Resolvers;
using System.Collections.Generic;
using System.Text;
using Xunit;
using static SpanJson.JsonSerializer.Generic.Utf16;
using static SpanJson.JsonSerializer.Generic.Utf8;

using WithdrawalFee = BitbankDotNet.Entities.Asset.WithdrawalFeeObject;

namespace BitbankDotNet.Tests.Formatters
{
    public class BitbankWithdrawalFeeFormatterTest
    {
        const string JpyJson = "{\"threshold\":\"30000\",\"under\":\"540\",\"over\":\"756\"}";
        const string NotJpyJson = "\"0.001\"";

        public static IEnumerable<object[]> TestData => new[]
        {
            new object[] {JpyJson, 30000.0, 540.0, 756.0},
            new object[] {NotJpyJson, 0.0, 0.001, 0.001}
        };

        [Theory]
        [MemberData(nameof(TestData))]
        public void Deserialize_UTF8のJSON文字列を入力_WithdrawalFeeObjectを返す(string json, double threshold, double under, double over)
        {
            var deserialize = Deserialize<WithdrawalFee, WithdrawalFeeResolver<byte>>(Encoding.UTF8.GetBytes(json));

            Assert.NotNull(deserialize);
            Assert.Equal(threshold, deserialize.Threshold);
            Assert.Equal(under, deserialize.Under);
            Assert.Equal(over, deserialize.Over);
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void Deserialize_UTF16のJSON文字列を入力_WithdrawalFeeObjectを返す(string json, double threshold, double under, double over)
        {
            var deserialize = Deserialize<WithdrawalFee, WithdrawalFeeResolver<char>>(json);

            Assert.NotNull(deserialize);
            Assert.Equal(threshold, deserialize.Threshold);
            Assert.Equal(under, deserialize.Under);
            Assert.Equal(over, deserialize.Over);
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void Serialize_WithdrawalFeeObjectを入力_UTF8のJSON文字列を出力(string json, double threshold, double under, double over)
        {
            var serialize = Serialize<WithdrawalFee, WithdrawalFeeResolver<byte>>(new WithdrawalFee
            {
                Threshold = threshold,
                Under = under,
                Over = over
            });

            Assert.NotNull(serialize);
            Assert.Equal(Encoding.UTF8.GetBytes(json), serialize);
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void Serialize_WithdrawalFeeObjectを入力_UTF16のJSON文字列を出力(string json, double threshold, double under, double over)
        {
            var serialize = Serialize<WithdrawalFee, WithdrawalFeeResolver<char>>(new WithdrawalFee
            {
                Threshold = threshold,
                Under = under,
                Over = over
            });

            Assert.NotNull(serialize);
            Assert.Equal(json, serialize);
        }

        sealed class WithdrawalFeeResolver<TSymbol> : ResolverBase<TSymbol, WithdrawalFeeResolver<TSymbol>>
            where TSymbol : struct
        {
            public WithdrawalFeeResolver() : base(new SpanJsonOptions
            {
                NullOption = NullOptions.ExcludeNulls,
                NamingConvention = NamingConventions.CamelCase,
                EnumOption = EnumOptions.String
            })
                => RegisterGlobalCustomFormatter<WithdrawalFee, BitbankWithdrawalFeeFormatter>();
        }
    }
}