using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using BitbankDotNet.Caches;

namespace BitbankDotNet.Extensions
{
    /// <summary>
    /// <see cref="Enum"/>の拡張メソッド
    /// </summary>
    static class EnumExtensions
    {
        /// <summary>
        /// <see cref="EnumMemberAttribute"/>を取得します。
        /// </summary>
        /// <typeparam name="T">列挙型</typeparam>
        /// <param name="value">対象の列挙体</param>
        /// <returns><see cref="EnumMemberAttribute"/>の値</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetEnumMemberValue<T>(this T value)
            where T : struct, Enum
            => EnumMemberCache<T>.Get(value);
    }
}