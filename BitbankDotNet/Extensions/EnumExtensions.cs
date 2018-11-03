using BitbankDotNet.Caches;
using System;
using System.Runtime.CompilerServices;

namespace BitbankDotNet.Extensions
{
    public static class EnumExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetEnumMemberValue<T>(this T value)
            where T : struct, Enum
            => EnumMemberCache<T>.Get(value);
    }
}