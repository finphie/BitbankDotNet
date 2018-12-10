using System;
using System.Runtime.CompilerServices;
using BitbankDotNet.Caches;

namespace BitbankDotNet.Extensions
{
    static class EnumExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetEnumMemberValue<T>(this T value)
            where T : struct, Enum
            => EnumMemberCache<T>.Get(value);
    }
}