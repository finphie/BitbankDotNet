using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace BitbankDotNet.Benchmarks.EnumGetEnumMember
{
    static class EnumMemberCache<T>
        where T: struct, Enum
    {
        // ReSharper disable once StaticMemberInGenericType
        public static readonly string[] Table;

        public static string Get(T value) => Table[Unsafe.As<T, int>(ref value)];

        static EnumMemberCache()
        {
            var values = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static);
            Table = new string[values.Length];
            for (var i = 0; i < values.Length; i++)
                Table[i] = values[i].GetCustomAttribute<EnumMemberAttribute>().Value;
        }
    }
}