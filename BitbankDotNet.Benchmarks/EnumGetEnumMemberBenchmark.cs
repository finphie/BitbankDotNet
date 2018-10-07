using BenchmarkDotNet.Attributes;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace BitbankDotNet.Benchmarks
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
            => TestEnum.Test1.GetEnumMemberValue();

        [Benchmark]
        public string ConcurrentDictionary()
            => TestEnum.Test1.GetEnumMemberValueConcurrentDictionary();

        [Benchmark]
        public string Hashtable()
            => TestEnum.Test1.GetEnumMemberValueHashtable();

        enum TestEnum
        {
            [EnumMember(Value = "1")]
            Test1
        }
    }

    static class EnumExtensions
    {
        static readonly ConcurrentDictionary<Enum, string> DictionaryCache
            = new ConcurrentDictionary<Enum, string>();

        static readonly Hashtable HashtableCache = new Hashtable();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetEnumMemberValue<T>(this T value)
            where T : struct, Enum
            => typeof(T)
                .GetField(value.ToString())
                .GetCustomAttribute<EnumMemberAttribute>().Value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetEnumMemberValueConcurrentDictionary<T>(this T value)
            where T : struct, Enum
            => DictionaryCache.GetOrAdd(value, e =>
                typeof(T)
                    .GetField(e.ToString())
                    .GetCustomAttribute<EnumMemberAttribute>().Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetEnumMemberValueHashtable<T>(this T value)
            where T : struct, Enum
        {
            if (HashtableCache.ContainsKey(value))
                return HashtableCache[value] as string;
            var memberValue = typeof(T)
                .GetField(value.ToString())
                .GetCustomAttribute<EnumMemberAttribute>().Value;
            HashtableCache[value] = memberValue;
            return memberValue;
        }     
    }
}