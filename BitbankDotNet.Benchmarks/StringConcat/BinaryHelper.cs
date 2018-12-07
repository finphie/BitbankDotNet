using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace BitbankDotNet.Benchmarks.StringConcat
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
            ref var s = ref Unsafe.As<char, byte>(ref source);
            ref var d = ref Unsafe.As<char, byte>(ref destination);

            const int count4 = sizeof(long) / sizeof(char);
            while (charCount >= count4)
            {
                Unsafe.WriteUnaligned(ref Unsafe.Add(ref d, i), Unsafe.ReadUnaligned<long>(ref Unsafe.Add(ref s, i)));
                i += sizeof(long);
                charCount -= count4;
            }

            const int count2 = sizeof(int) / sizeof(char);
            if (charCount >= count2)
            {
                Unsafe.WriteUnaligned(ref Unsafe.Add(ref d, i), Unsafe.ReadUnaligned<int>(ref Unsafe.Add(ref s, i)));
                i += sizeof(int);
                charCount -= count2;
            }

            const int count1 = sizeof(char) / sizeof(char);
            if (charCount >= count1)
                Unsafe.WriteUnaligned(ref Unsafe.Add(ref d, i), Unsafe.ReadUnaligned<char>(ref Unsafe.Add(ref s, i)));
        }
    }
}