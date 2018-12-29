using System;
using System.Collections.Concurrent;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace BitbankDotNet.Benchmarks.EnumGetEnumMember
{
    static class EnumHelperConcurrentDictionaryEnumKey
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetEnumMemberValue<T>(T value)
            where T : struct, Enum
            => Cache<T>.Dic.GetOrAdd(value, e =>
                typeof(T)
                    .GetField(e.ToString())
                    .GetCustomAttribute<EnumMemberAttribute>().Value);

        static class Cache<T>
            where T : struct, Enum
        {
            public static readonly ConcurrentDictionary<T, string> Dic = new ConcurrentDictionary<T, string>();
        }
    }
}