using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace BitbankDotNet.Benchmarks.StringConcatBenchmark
{
    static class BinaryHelper
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Copy(in ReadOnlySpan<char> source, ref byte destination, int byteCount)
        {
            ref var sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(source));
            Unsafe.CopyBlockUnaligned(ref destination, ref sourceStart, (uint)byteCount);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Copy(ref char source, ref char destination, int charCount)
        {
            var i = 0;
            while (charCount >= 4)
            {
                ref var s = ref Unsafe.As<char, long>(ref Unsafe.Add(ref source, i));
                ref var d = ref Unsafe.As<char, long>(ref Unsafe.Add(ref destination, i));
                d = s;
                i += 4;
                charCount -= 4;
            }
            if (charCount >= 2)
            {
                ref var s = ref Unsafe.As<char, int>(ref Unsafe.Add(ref source, i));
                ref var d = ref Unsafe.As<char, int>(ref Unsafe.Add(ref destination, i));
                d = s;
                i += 2;
                charCount -= 2;
            }
            if (charCount >= 1)
                Unsafe.Add(ref destination, i) = Unsafe.Add(ref source, i);
        }
    }
}