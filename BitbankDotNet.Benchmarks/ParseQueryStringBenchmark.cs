using BenchmarkDotNet.Attributes;
using System.Web;

namespace BitbankDotNet.Benchmarks
{
    /// <summary>
    /// クエリ文字列作成処理のベンチマーク
    /// </summary>
    [Config(typeof(BenchmarkConfig))]
    public class ParseQueryStringBenchmark
    {
        const string EqualsSign = "=";
        const string AndSign = "&";

        string _key1;
        string _key2;

        string _value1;
        string _value2;

        [GlobalSetup]
        public void Setup()
        {
            _key1 = "key1";
            _key2 = "key2";

            _value1 = "value1";
            _value2 = "value2";
        }

        [Benchmark]
        public string HttpUtilityParseQueryString()
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query[_key1] = _value1;
            query[_key2] = _value2;

            return query.ToString();
        }

        [Benchmark]
        public string StringConcat1()
            => _key1 + EqualsSign + _value1 + AndSign +
               _key2 + EqualsSign + _value2;

        [Benchmark]
        public string StringConcat2()
        {
            var s = _key1 + EqualsSign + _value1 + AndSign;
            return s + _key2 + EqualsSign + _value2;
        }
    }
}