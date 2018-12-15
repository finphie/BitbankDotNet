using System;
using System.Diagnostics.CodeAnalysis;
using BenchmarkDotNet.Attributes;

namespace BitbankDotNet.Benchmarks
{
    /// <summary>
    /// GetHashCodeのベンチマーク
    /// VS2017の機能で自動生成できる<see cref="HashCode"/>を利用すると良い。
    /// </summary>
    /// <remarks>
    /// .NET Core 2.1.4 (CoreCLR 4.6.26814.03, CoreFX 4.6.26814.02), 64bit RyuJIT
    ///          Method |     Mean |     Error |    StdDev | Scaled |  Gen 0 | Allocated |
    /// --------------- |---------:|----------:|----------:|-------:|-------:|----------:|
    ///   AnonymousType | 36.86 ns | 0.1272 ns | 0.1128 ns |   2.03 | 0.0356 |      56 B |
    ///      ValueTuple | 33.16 ns | 0.0509 ns | 0.0397 ns |   1.83 |      - |       0 B |
    ///       ReSharper | 12.94 ns | 0.0366 ns | 0.0343 ns |   0.71 |      - |       0 B |
    ///     HashHelpers | 13.65 ns | 0.0644 ns | 0.0602 ns |   0.75 |      - |       0 B |
    ///  SystemHashCode | 18.16 ns | 0.0526 ns | 0.0492 ns |   1.00 |      - |       0 B |
    /// </remarks>
    [Config(typeof(BenchmarkConfig))]
    public class GetHashCodeBenchmark
    {
        public long Property1 { get; set; } = long.MaxValue;

        public SampleClass Property2 { get; set; } = new SampleClass();

        public string Property3 { get; set; } = "abcdefghij";

        public double Property4 { get; set; } = double.MaxValue;

        public int Property5 { get; set; } = 1;

        [Benchmark]
        public int AnonymousType()
            => new { Property1, Property2, Property3, Property4, Property5 }.GetHashCode();

        [Benchmark]
        public int ValueTuple()
            => (Property1, Property2, Property3, Property4, Property5).GetHashCode();

        [Benchmark]
        [SuppressMessage("Globalization", "CA1307:Specify StringComparison", Justification = "ベンチマーク")]
        public int ReSharper()
        {
            unchecked
            {
                var hash = Property1.GetHashCode();
                hash = (hash * 397) ^ (Property2?.GetHashCode() ?? 0);
                hash = (hash * 397) ^ (Property3?.GetHashCode() ?? 0);
                hash = (hash * 397) ^ Property4.GetHashCode();
                hash = (hash * 397) ^ Property5;
                return hash;
            }
        }

        [Benchmark]
        [SuppressMessage("Globalization", "CA1307:Specify StringComparison", Justification = "ベンチマーク")]
        public int HashHelpers()
        {
            var hash = Property1.GetHashCode();
            hash = Combine(hash, Property2?.GetHashCode() ?? 0);
            hash = Combine(hash, Property3?.GetHashCode() ?? 0);
            hash = Combine(hash, Property4.GetHashCode());
            hash = Combine(hash, Property5);
            return hash;
        }

        [Benchmark(Baseline = true)]
        public int SystemHashCode()
            => HashCode.Combine(Property1, Property2, Property3, Property4, Property5);

        // cf. https://github.com/dotnet/corefx/blob/v2.2.0/src/Common/src/System/Numerics/Hashing/HashHelpers.cs
        static int Combine(int h1, int h2)
        {
            var rol5 = ((uint)h1 << 5) | ((uint)h1 >> 27);
            return ((int)rol5 + h1) ^ h2;
        }
    }

    public class SampleClass
    {
        public string Property1 { get; set; }

        public double Property2 { get; set; }

        public int Property3 { get; set; }
    }
}
