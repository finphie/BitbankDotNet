using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace BitbankDotNet.Extensions
{
    /// <summary>
    /// <see cref="string"/>の拡張メソッド
    /// </summary>
    static class StringExtensions
    {
        /// <summary>
        /// ASCII文字列をUTF-8のbyte配列に変換します。
        /// </summary>
        /// <param name="source">ASCII文字列</param>
        /// <param name="destination">出力先のbyte配列</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void FromAsciiStringToUtf8Bytes(this string source, in Span<byte> destination)
        {
            ref var sourceStart = ref MemoryMarshal.GetReference(source.AsSpan());
            for (var i = 0; i < destination.Length; i++)
                destination[i] = (byte)Unsafe.Add(ref sourceStart, i);
        }
    }
}