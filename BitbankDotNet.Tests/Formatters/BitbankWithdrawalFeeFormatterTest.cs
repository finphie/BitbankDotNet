using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using BitbankDotNet.Entities;
using BitbankDotNet.Formatters;
using SpanJson.Resolvers;
using Xunit;
using static SpanJson.JsonSerializer.Generic.Utf16;
using static SpanJson.JsonSerializer.Generic.Utf8;

namespace BitbankDotNet.Tests.Formatters
{
    [SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "ユニットテスト")]
    public class BitbankWithdrawalFeeFormatterTest
    {
        const string Threshold = "\"threshold\"";
        const string Under = "\"under\"";
        const string Over = "\"over\"";

        const double Threshold0 = 30000.0;
        const double Under0 = 540.0;
        const double Over0 = 756.0;

        const double Threshold1 = 0.0;
        const double Under1 = 0.001;
        const double Over1 = 0.001;

        public static IEnumerable<object[]> DeserializeTestData()
        {
            string CreateJsonString(string value) => $"{{{value}}}";

            var json0 = string.Join(",", GetThreshold(Threshold0), GetUnder(Under0), GetOver(Over0));
            var json1 = string.Join(",", GetThreshold(Threshold1), GetUnder(Under1), GetOver(Over1));

            yield return new object[] { CreateJsonString(json0), Threshold0, Under0, Over0 };
            yield return new object[] { CreateJsonString(json1), Threshold1, Under1, Over1 };
        }

        public static IEnumerable<object[]> SerializeTestData() => new[]
        {
            new object[]
                { new[] { GetThreshold(Threshold0), GetUnder(Under0), GetOver(Over0) }, Threshold0, Under0, Over0 },
            new object[] { new[] { $"\"{Under1}\"" }, Threshold1, Under1, Over1 }
        };

        [Theory]
        [MemberData(nameof(DeserializeTestData))]
        public void Deserialize_UTF8のJSON文字列を入力_WithdrawalFeeObjectを返す(string json, double threshold, double under, double over)
        {
            var deserialize = Deserialize<WithdrawalFee, WithdrawalFeeResolver<byte>>(Encoding.UTF8.GetBytes(json));

            Assert.NotNull(deserialize);
            Assert.Equal(threshold, deserialize.Threshold);
            Assert.Equal(under, deserialize.Under);
            Assert.Equal(over, deserialize.Over);
        }

        [Theory]
        [MemberData(nameof(DeserializeTestData))]
        public void Deserialize_UTF16のJSON文字列を入力_WithdrawalFeeObjectを返す(string json, double threshold, double under, double over)
        {
            var deserialize = Deserialize<WithdrawalFee, WithdrawalFeeResolver<char>>(json);

            Assert.NotNull(deserialize);
            Assert.Equal(threshold, deserialize.Threshold);
            Assert.Equal(under, deserialize.Under);
            Assert.Equal(over, deserialize.Over);
        }

        [Theory]
        [MemberData(nameof(SerializeTestData))]
        public void Serialize_WithdrawalFeeObjectを入力_UTF8のJSON文字列を出力(string[] json, double threshold, double under, double over)
        {
            var serialize = Serialize<WithdrawalFee, WithdrawalFeeResolver<byte>>(new WithdrawalFee
            {
                Threshold = threshold,
                Under = under,
                Over = over
            });

            Assert.NotNull(serialize);

            if (json.Length == 1)
            {
                Assert.All(Encoding.UTF8.GetBytes(json[0]), b => Assert.Contains(b, serialize));
                return;
            }
            foreach (var j in json)
                Assert.All(Encoding.UTF8.GetBytes(j), b => Assert.Contains(b, serialize));
        }

        [Theory]
        [MemberData(nameof(SerializeTestData))]
        public void Serialize_WithdrawalFeeObjectを入力_UTF16のJSON文字列を出力(string[] json, double threshold, double under, double over)
        {
            var serialize = Serialize<WithdrawalFee, WithdrawalFeeResolver<char>>(new WithdrawalFee
            {
                Threshold = threshold,
                Under = under,
                Over = over
            });

            Assert.NotNull(serialize);

            if (json.Length == 1)
            {
                Assert.Contains(json[0], serialize, StringComparison.Ordinal);
                return;
            }
            foreach (var j in json)
                Assert.Contains(j, serialize, StringComparison.Ordinal);
        }

        static string Join(string key, double value) => string.Join(":", key, $"\"{value}\"");
        static string GetThreshold(double value) => Join(Threshold, value);
        static string GetUnder(double value) => Join(Under, value);
        static string GetOver(double value) => Join(Over, value);

        sealed class WithdrawalFeeResolver<TSymbol> : ResolverBase<TSymbol, WithdrawalFeeResolver<TSymbol>>
            where TSymbol : struct
        {
            public WithdrawalFeeResolver()
                : base(new SpanJsonOptions
            {
                NullOption = NullOptions.ExcludeNulls,
                NamingConvention = NamingConventions.CamelCase,
                EnumOption = EnumOptions.String
            })
                => RegisterGlobalCustomFormatter<WithdrawalFee, BitbankWithdrawalFeeFormatter>();
        }
    }
}