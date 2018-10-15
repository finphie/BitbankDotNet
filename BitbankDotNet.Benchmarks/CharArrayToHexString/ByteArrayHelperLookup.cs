using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace BitbankDotNet.Benchmarks.CharArrayToHexString
{
    static class ByteArrayHelperLookup
    {
        static readonly uint[] Table;

        static ByteArrayHelperLookup()
        {
            Table = new uint[256];
            for (var i = 0; i < Table.Length; i++)
            {
                var s = i.ToString("x2");
                Table[i] = BitConverter.IsLittleEndian
                    ? s[0] + ((uint)s[1] << 16)
                    : s[1] + ((uint)s[0] << 16);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToHexString(byte[] source)
        {
            var result = new string(default, source.Length * 2);
            ref var resultStart = ref Unsafe.As<char, uint>(ref MemoryMarshal.GetReference(result.AsSpan()));
            ref var tableStart = ref Table[0];
            for (var i = 0; i < source.Length; i++)
                Unsafe.Add(ref resultStart, i) = Unsafe.Add(ref tableStart, source[i]);

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe string ToHexStringUnsafe(byte[] source)
        {
            var result = new string(default, source.Length * 2);
            fixed (char* resultPointer = result)
            fixed (uint* tablePointer = &Table[0])
            {
                var pointer = (uint*)resultPointer;
                for (var i = 0; i < source.Length; i++)
                    pointer[i] = tablePointer[source[i]];
            }

            return result;
        }
    } 
}