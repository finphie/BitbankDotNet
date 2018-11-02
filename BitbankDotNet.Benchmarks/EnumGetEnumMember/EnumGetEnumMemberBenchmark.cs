using BenchmarkDotNet.Attributes;
using EnumsNET;
using System.Runtime.Serialization;

namespace BitbankDotNet.Benchmarks.EnumGetEnumMember
{
    /// <summary>
    /// EnumMemberを取得
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    [Config(typeof(BenchmarkConfig))]
    public class EnumGetEnumMemberBenchmark
    {  
        [Benchmark]
        public string Standard()
            => EnumHelper.GetEnumMemberValue(TestEnum.Test1);

        [Benchmark]
        public string ConcurrentDictionaryBaseEnumKey()
            => EnumHelperConcurrentDictionaryBaseEnumKey.GetEnumMemberValue(TestEnum.Test1);

        [Benchmark]
        public string ConcurrentDictionaryEnumKey()
            => EnumHelperConcurrentDictionaryEnumKey.GetEnumMemberValue(TestEnum.Test1);

        [Benchmark]
        public string Hashtable()
            => EnumHelperHashtable.GetEnumMemberValue(TestEnum.Test1);

        [Benchmark]
        public string EnumsNet()
            => TestEnum.Test1.GetAttributes().Get<EnumMemberAttribute>().Value;

        enum TestEnum
        {
            [EnumMember(Value = "1")]
            Test1,
            [EnumMember(Value = "2")]
            Test2
        }
    }
}