using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using BenchmarkDotNet.Attributes;
using EnumsNET;

namespace BitbankDotNet.Benchmarks.EnumGetEnumMember
{
    /// <summary>
    /// EnumMemberを取得する処理のベンチマーク
    /// </summary>
    /// <remarks>
    /// .NET Core 2.1.5 (CoreCLR 4.6.26919.02, CoreFX 4.6.26919.02), 64bit RyuJIT
    ///                          Method |          Mean |     Error |    StdDev |  Gen 0 | Allocated |
    /// ------------------------------- |---------------|-----------|-----------|--------|-----------|
    ///                        Standard | 2,031.4964 ns | 6.1901 ns | 5.7902 ns | 0.2289 |     360 B |
    /// ConcurrentDictionaryBaseEnumKey |    39.5527 ns | 0.1766 ns | 0.1652 ns | 0.0152 |      24 B |
    ///     ConcurrentDictionaryEnumKey |    18.0027 ns | 0.0629 ns | 0.0557 ns |      - |       0 B |
    ///                       Hashtable |    61.0096 ns | 0.2228 ns | 0.2084 ns | 0.0304 |      48 B |
    ///                           Array |     0.0016 ns | 0.0044 ns | 0.0039 ns |      - |       0 B |
    ///                        EnumsNet |    21.3545 ns | 0.0670 ns | 0.0627 ns |      - |       0 B |
    /// </remarks>
    [Config(typeof(BenchmarkConfig))]
    [SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "ベンチマーク")]
    public class EnumGetEnumMemberBenchmark
    {
        public EnumGetEnumMemberBenchmark()
            => RuntimeHelpers.RunClassConstructor(typeof(EnumMemberCache<TestEnum>).TypeHandle);

        [Benchmark]
        public string Standard()
            => EnumHelper.GetEnumMemberValue(TestEnum.A);

        [Benchmark]
        public string ConcurrentDictionaryBaseEnumKey()
            => EnumHelperConcurrentDictionaryBaseEnumKey.GetEnumMemberValue(TestEnum.A);

        [Benchmark]
        public string ConcurrentDictionaryEnumKey()
            => EnumHelperConcurrentDictionaryEnumKey.GetEnumMemberValue(TestEnum.A);

        [Benchmark]
        public string Hashtable()
            => EnumHelperHashtable.GetEnumMemberValue(TestEnum.A);

        [Benchmark]
        public string Array()
            => EnumMemberCache<TestEnum>.Get(TestEnum.A);

        [Benchmark]
        public string EnumsNet()
            => TestEnum.A.GetAttributes().Get<EnumMemberAttribute>().Value;
    }
}