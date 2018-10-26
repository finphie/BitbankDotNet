using BenchmarkDotNet.Attributes;
using SpanJson.Helpers;
using System;
using System.Collections.Generic;

namespace BitbankDotNet.Benchmarks
{
    /// <summary>
    /// 桁数を取得する
    /// </summary>
    [Config(typeof(BenchmarkConfig))]
    public class CountDigitBenchmark
    {
        public IEnumerable<ulong> Values => new[]
        {
            (ulong)DateTimeOffset.Parse("2018/01/01T00:00:00Z").ToUnixTimeMilliseconds(),
            // ReSharper disable once ImpureMethodCallOnReadonlyValueField
            (ulong)DateTimeOffset.MaxValue.ToUnixTimeMilliseconds(),
            ulong.MaxValue
        };

        [Benchmark]
        [ArgumentsSource(nameof(Values))]
        public int Loop(ulong value)
        {
            var digits = 1;
            while ((value /= 10) != 0)
                digits++;
            return digits;
        }

        [Benchmark]
        [ArgumentsSource(nameof(Values))]
        public int MathFLog10(ulong value) => (int)MathF.Log10(value) + 1;

        [Benchmark]
        [ArgumentsSource(nameof(Values))]
        public int MathLog10(ulong value) => (int)Math.Log10(value) + 1;

        [Benchmark]
        [ArgumentsSource(nameof(Values))]
        public int SpanJson(ulong value) => FormatterUtils.CountDigits(value);

        [Benchmark]
        [ArgumentsSource(nameof(Values))]
        public int If(ulong value)
            => value >= 10_000_000_000_000_000_000 ? 20
                : value >= 1_000_000_000_000_000_000 ? 19
                : value >= 100_000_000_000_000_000 ? 18
                : value >= 10_000_000_000_000_000 ? 17
                : value >= 1_000_000_000_000_000 ? 16
                : value >= 100_000_000_000_000 ? 15
                : value >= 10_000_000_000_000 ? 14
                : value >= 1_000_000_000_000 ? 13
                : value >= 100_000_000_000 ? 12
                : value >= 10_000_000_000 ? 11
                : value >= 1_000_000_000 ? 10
                : value >= 100_000_000 ? 9
                : value >= 10_000_000 ? 8
                : value >= 1_000_000 ? 7
                : value >= 100_000 ? 6
                : value >= 10_000 ? 5
                : value >= 1_000 ? 4
                : value >= 100 ? 3
                : value >= 10 ? 2
                : 1;

        /// <summary>
        /// Unix時間用に最適化（13桁以上20桁以下）
        /// </summary>
        [Benchmark]
        [ArgumentsSource(nameof(Values))]
        public int Min13Digits(ulong value)
            => value < 10_000_000_000_000 ? 13
                : value < 100_000_000_000_000 ? 14
                : value < 1_000_000_000_000_000 ? 15
                : value < 10_000_000_000_000_000 ? 16
                : value < 100_000_000_000_000_000 ? 17
                : value < 1_000_000_000_000_000_000 ? 18
                : value < 10_000_000_000_000_000_000 ? 19
                : 20;
    }
}
