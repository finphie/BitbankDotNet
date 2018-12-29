using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using BenchmarkDotNet.Attributes;

namespace BitbankDotNet.Benchmarks
{
    /// <summary>
    /// 桁数を取得する
    /// </summary>
    [Config(typeof(BenchmarkConfig))]
    [SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "ベンチマーク")]
    public class CountDigitBenchmark
    {
        [SuppressMessage("ReSharper", "ImpureMethodCallOnReadonlyValueField", Justification = "不要")]
        public static IEnumerable<ulong> Values => new[]
        {
            (ulong)DateTimeOffset.Parse("2018/01/01T00:00:00Z").ToUnixTimeMilliseconds(),
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

        /// <summary>
        /// CoreFXでの実装です。
        /// </summary>
        /// <remarks>
        /// System.Buffers.Text.FormattingHelpers.CountDigits
        /// cf. https://github.com/dotnet/corefx/blob/v2.2.0/src/Common/src/CoreLib/System/Buffers/Text/FormattingHelpers.CountDigits.cs#L13-L66
        /// </remarks>
        /// <param name="value">数値</param>
        /// <returns>桁数</returns>
        [Benchmark]
        [ArgumentsSource(nameof(Values))]
        public int CoreFx(ulong value)
        {
            var digits = 1;
            uint part;
            if (value >= 10000000)
            {
                if (value >= 100000000000000)
                {
                    part = (uint)(value / 100000000000000);
                    digits += 14;
                }
                else
                {
                    part = (uint)(value / 10000000);
                    digits += 7;
                }
            }
            else
                part = (uint)value;

            if (part < 10)
            {
                // no-op
            }
            else if (part < 100)
                digits += 1;
            else if (part < 1000)
                digits += 2;
            else if (part < 10000)
                digits += 3;
            else if (part < 100000)
                digits += 4;
            else if (part < 1000000)
                digits += 5;
            else
                digits += 6;

            return digits;
        }

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
        /// <param name="value">数値</param>
        /// <returns>桁数</returns>
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