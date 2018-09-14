using System;
using BenchmarkDotNet.Attributes;

namespace BitbankDotNet.Benchmarks
{
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

        // cf. https://github.com/dotnet/corefx/blob/master/src/Common/src/System/Numerics/Hashing/HashHelpers.cs
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
