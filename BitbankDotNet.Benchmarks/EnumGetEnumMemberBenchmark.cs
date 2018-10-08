using BenchmarkDotNet.Attributes;
using EnumsNET;
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
        public string ConcurrentDictionaryBaseEnumKey()
            => TestEnum.Test1.GetEnumMemberValueConcurrentDictionaryBaseEnumKey();

        [Benchmark]
        public string ConcurrentDictionaryEnumKey()
            => TestEnum.Test1.GetEnumMemberValueConcurrentDictionaryEnumKey();

        [Benchmark]
        public string Hashtable()
            => TestEnum.Test1.GetEnumMemberValueHashtable();

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
  
    static class EnumExtensions
    {
        static readonly ConcurrentDictionary<Enum, string> DictionaryCache = new ConcurrentDictionary<Enum, string>();
        static readonly Hashtable HashtableCache = new Hashtable();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetEnumMemberValue<T>(this T value)
            where T : struct, Enum
            => typeof(T)
                .GetField(value.ToString())
                .GetCustomAttribute<EnumMemberAttribute>().Value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetEnumMemberValueConcurrentDictionaryBaseEnumKey<T>(this T value)
            where T : struct, Enum
            => DictionaryCache.GetOrAdd(value, e =>
                typeof(T)
                    .GetField(e.ToString())
                    .GetCustomAttribute<EnumMemberAttribute>().Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetEnumMemberValueConcurrentDictionaryEnumKey<T>(this T value)
            where T : struct, Enum
        {
            return Cache<T>.Dic.GetOrAdd(value, e =>
                typeof(T)
                    .GetField(value.ToString())
                    .GetCustomAttribute<EnumMemberAttribute>().Value);
        }

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

        static class Cache<T>
            where T : struct, Enum
        {
            public static readonly ConcurrentDictionary<T, string> Dic = new ConcurrentDictionary<T, string>();
        }
    }
}