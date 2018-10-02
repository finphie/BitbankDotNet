using System;
using System.Collections.Concurrent;
using System.Reflection;
using System.Runtime.Serialization;

namespace BitbankDotNet.Extensions
{
    public static class EnumExtensions
    {
        static readonly ConcurrentDictionary<Enum, string> Cache = new ConcurrentDictionary<Enum, string>();

        public static string GetEnumMemberValue<T>(this T value)
            where T : struct, Enum
            => Cache.GetOrAdd(value, _ =>
                typeof(T).GetField(value.ToString()).GetCustomAttribute<EnumMemberAttribute>().Value);
    }
}