using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace BitbankDotNet.Benchmarks.EnumGetEnumMember
{
    static class EnumHelper
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetEnumMemberValue<T>(T value)
            where T : struct, Enum
            => typeof(T)
                .GetField(value.ToString())
                .GetCustomAttribute<EnumMemberAttribute>().Value;
    }
}