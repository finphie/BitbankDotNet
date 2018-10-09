using BenchmarkDotNet.Attributes;
using System;
using System.Linq;

namespace BitbankDotNet.Benchmarks
{
    /// <summary>
    /// char配列をUTF-16文字列に変換
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    [Config(typeof(BenchmarkConfig))]
    public class CharArrayConcatBenchmark
    {
        char[] _source;

        [Params(10, 100, 512, 1024, 2048, 10000)]
        public int ArraySize { get; set; }

        [GlobalSetup]
        public void Setup() => _source = Enumerable.Repeat('a', ArraySize).ToArray();

        [Benchmark]
        public string NewString() => new string(_source);

        [Benchmark]
        public string StringConcat() => string.Concat(_source);

        [Benchmark]
        public string StringJoin() => string.Join("", _source);

        [Benchmark]
        public string SpanToString() => _source.AsSpan().ToString();
    }
}