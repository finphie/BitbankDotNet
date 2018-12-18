using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace BitbankDotNet.Benchmarks.EnumGetEnumMember
{
    static class EnumMemberCache<T>
        where T : struct, Enum
    {
        // ReSharper disable once StaticMemberInGenericType
        static readonly string[] Table;

        [SuppressMessage("Performance", "CA1810:Initialize reference type static fields inline", Justification = "キャッシュを事前に作成するため、静的コンストラクターを明示的に呼び出す")]
        static EnumMemberCache()
        {
            var values = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static);
            Table = new string[values.Length];
            for (var i = 0; i < values.Length; i++)
                Table[i] = values[i].GetCustomAttribute<EnumMemberAttribute>().Value;
        }

        public static string Get(T value) => Table[Unsafe.As<T, int>(ref value)];
    }
}