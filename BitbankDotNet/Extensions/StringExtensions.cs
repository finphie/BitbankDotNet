using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace BitbankDotNet.Extensions
{
    static class StringExtensions
    {
        /// <summary>
        /// ASCII文字列をUTF-8のbyte配列に変換
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void FromAsciiStringToUtf8Bytes(this string source, in Span<byte> destination)
        {
            ref var sourceStart = ref MemoryMarshal.GetReference(source.AsSpan());
            for (var i = 0; i < destination.Length; i++)
                destination[i] = (byte)Unsafe.Add(ref sourceStart, i);
        }
    }
}