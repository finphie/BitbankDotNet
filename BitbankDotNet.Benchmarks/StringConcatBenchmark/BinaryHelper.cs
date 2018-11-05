using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace BitbankDotNet.Benchmarks.StringConcatBenchmark
{
    static partial class BinaryHelper
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

            const int count4 = sizeof(long) / sizeof(char);
            while (charCount >= count4)
            {
                ref var s = ref Unsafe.As<char, long>(ref Unsafe.Add(ref source, i));
                ref var d = ref Unsafe.As<char, long>(ref Unsafe.Add(ref destination, i));
                d = s;
                i += count4;
                charCount -= count4;
            }

            const int count2 = sizeof(int) / sizeof(char);
            if (charCount >= count2)
            {
                ref var s = ref Unsafe.As<char, int>(ref Unsafe.Add(ref source, i));
                ref var d = ref Unsafe.As<char, int>(ref Unsafe.Add(ref destination, i));
                d = s;
                i += count2;
                charCount -= count2;
            }

            const int count1 = sizeof(char) / sizeof(char);
            if (charCount >= count1)
                Unsafe.Add(ref destination, i) = Unsafe.Add(ref source, i);
        }
    }
}