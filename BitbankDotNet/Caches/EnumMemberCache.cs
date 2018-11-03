using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace BitbankDotNet.Caches
{
    /// <summary>
    /// <see cref="EnumMemberAttribute"/>の値をキャッシュします。
    /// </summary>
    /// <typeparam name="T">int型で0から始まる連番の列挙体</typeparam>
    static class EnumMemberCache<T>
        where T : struct, Enum
    {
        // ReSharper disable once StaticMemberInGenericType
        static readonly string[] Table;

        /// <summary>
        /// <see cref="EnumMemberAttribute"/>の値を取得します。
        /// </summary>
        /// <param name="value">int型で0から始まる連番の列挙体</param>
        /// <returns><see cref="EnumMemberAttribute"/>の値（文字列）を返します。</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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