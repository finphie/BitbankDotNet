// ReSharper disable RedundantAssignment

using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace BitbankDotNet.Benchmarks.StringConcat
{
    partial class BinaryHelper
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [SuppressMessage("Maintainability", "CA1502:Avoid excessive complexity", Justification = "自動生成コード")]
        public static void CopyChar(ref char source, ref char destination, int charCount)
        {
            switch (charCount)
            {
                case 1:
                    CopyChar1(ref source, ref destination);
                    break;
                case 2:
                    CopyChar2(ref source, ref destination);
                    break;
                case 3:
                    CopyChar3(ref source, ref destination);
                    break;
                case 4:
                    CopyChar4(ref source, ref destination);
                    break;
                case 5:
                    CopyChar5(ref source, ref destination);
                    break;
                case 6:
                    CopyChar6(ref source, ref destination);
                    break;
                case 7:
                    CopyChar7(ref source, ref destination);
                    break;
                case 8:
                    CopyChar8(ref source, ref destination);
                    break;
                case 9:
                    CopyChar9(ref source, ref destination);
                    break;
                case 10:
                    CopyChar10(ref source, ref destination);
                    break;
                case 11:
                    CopyChar11(ref source, ref destination);
                    break;
                case 12:
                    CopyChar12(ref source, ref destination);
                    break;
                case 13:
                    CopyChar13(ref source, ref destination);
                    break;
                case 14:
                    CopyChar14(ref source, ref destination);
                    break;
                case 15:
                    CopyChar15(ref source, ref destination);
                    break;
                case 16:
                    CopyChar16(ref source, ref destination);
                    break;
                case 17:
                    CopyChar17(ref source, ref destination);
                    break;
                case 18:
                    CopyChar18(ref source, ref destination);
                    break;
                case 19:
                    CopyChar19(ref source, ref destination);
                    break;
                case 20:
                    CopyChar20(ref source, ref destination);
                    break;
                case 21:
                    CopyChar21(ref source, ref destination);
                    break;
                case 22:
                    CopyChar22(ref source, ref destination);
                    break;
                case 23:
                    CopyChar23(ref source, ref destination);
                    break;
                case 24:
                    CopyChar24(ref source, ref destination);
                    break;
                case 25:
                    CopyChar25(ref source, ref destination);
                    break;
                case 26:
                    CopyChar26(ref source, ref destination);
                    break;
                case 27:
                    CopyChar27(ref source, ref destination);
                    break;
                case 28:
                    CopyChar28(ref source, ref destination);
                    break;
                case 29:
                    CopyChar29(ref source, ref destination);
                    break;
                case 30:
                    CopyChar30(ref source, ref destination);
                    break;
                case 31:
                    CopyChar31(ref source, ref destination);
                    break;
                case 32:
                    CopyChar32(ref source, ref destination);
                    break;
                default:
                    ref var s = ref Unsafe.As<char, byte>(ref source);
                    ref var d = ref Unsafe.As<char, byte>(ref destination);
                    Unsafe.CopyBlockUnaligned(ref d, ref s, (uint)charCount * sizeof(char));
                    break;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CopyChar1(ref char source, ref char destination)
            => destination = source;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CopyChar2(ref char source, ref char destination)
        {
            ref var s = ref Unsafe.As<char, int>(ref source);
            ref var d = ref Unsafe.As<char, int>(ref destination);
            d = s;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CopyChar3(ref char source, ref char destination)
        {
            ref var s = ref Unsafe.As<char, int>(ref source);
            ref var d = ref Unsafe.As<char, int>(ref destination);
            d = s;
            Unsafe.Add(ref destination, 2) = Unsafe.Add(ref source, 2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CopyChar4(ref char source, ref char destination)
        {
            ref var s = ref Unsafe.As<char, long>(ref source);
            ref var d = ref Unsafe.As<char, long>(ref destination);
            d = s;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CopyChar5(ref char source, ref char destination)
        {
            ref var s = ref Unsafe.As<char, long>(ref source);
            ref var d = ref Unsafe.As<char, long>(ref destination);
            d = s;
            Unsafe.Add(ref destination, 4) = Unsafe.Add(ref source, 4);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CopyChar6(ref char source, ref char destination)
        {
            ref var s = ref Unsafe.As<char, long>(ref source);
            ref var d = ref Unsafe.As<char, long>(ref destination);
            d = s;
            s = ref Unsafe.As<char, long>(ref Unsafe.Add(ref source, 2));
            d = ref Unsafe.As<char, long>(ref Unsafe.Add(ref destination, 2));
            d = s;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CopyChar7(ref char source, ref char destination)
        {
            ref var s = ref Unsafe.As<char, long>(ref source);
            ref var d = ref Unsafe.As<char, long>(ref destination);
            d = s;
            s = ref Unsafe.As<char, long>(ref Unsafe.Add(ref source, 3));
            d = ref Unsafe.As<char, long>(ref Unsafe.Add(ref destination, 3));
            d = s;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CopyChar8(ref char source, ref char destination)
        {
            ref var s = ref Unsafe.As<char, long>(ref source);
            ref var d = ref Unsafe.As<char, long>(ref destination);
            d = s;
            Unsafe.Add(ref d, 1) = Unsafe.Add(ref s, 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CopyChar9(ref char source, ref char destination)
        {
            ref var s = ref Unsafe.As<char, long>(ref source);
            ref var d = ref Unsafe.As<char, long>(ref destination);
            d = s;
            Unsafe.Add(ref d, 1) = Unsafe.Add(ref s, 1);
            Unsafe.Add(ref destination, 8) = Unsafe.Add(ref source, 8);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CopyChar10(ref char source, ref char destination)
        {
            ref var s = ref Unsafe.As<char, long>(ref source);
            ref var d = ref Unsafe.As<char, long>(ref destination);
            d = s;
            Unsafe.Add(ref d, 1) = Unsafe.Add(ref s, 1);
            s = ref Unsafe.As<char, long>(ref Unsafe.Add(ref source, 6));
            d = ref Unsafe.As<char, long>(ref Unsafe.Add(ref destination, 6));
            d = s;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CopyChar11(ref char source, ref char destination)
        {
            ref var s = ref Unsafe.As<char, long>(ref source);
            ref var d = ref Unsafe.As<char, long>(ref destination);
            d = s;
            Unsafe.Add(ref d, 1) = Unsafe.Add(ref s, 1);
            s = ref Unsafe.As<char, long>(ref Unsafe.Add(ref source, 7));
            d = ref Unsafe.As<char, long>(ref Unsafe.Add(ref destination, 7));
            d = s;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CopyChar12(ref char source, ref char destination)
        {
            ref var s = ref Unsafe.As<char, long>(ref source);
            ref var d = ref Unsafe.As<char, long>(ref destination);
            d = s;
            Unsafe.Add(ref d, 1) = Unsafe.Add(ref s, 1);
            Unsafe.Add(ref d, 2) = Unsafe.Add(ref s, 2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CopyChar13(ref char source, ref char destination)
        {
            ref var s = ref Unsafe.As<char, long>(ref source);
            ref var d = ref Unsafe.As<char, long>(ref destination);
            d = s;
            Unsafe.Add(ref d, 1) = Unsafe.Add(ref s, 1);
            Unsafe.Add(ref d, 2) = Unsafe.Add(ref s, 2);
            Unsafe.Add(ref destination, 12) = Unsafe.Add(ref source, 12);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CopyChar14(ref char source, ref char destination)
        {
            ref var s = ref Unsafe.As<char, long>(ref source);
            ref var d = ref Unsafe.As<char, long>(ref destination);
            d = s;
            Unsafe.Add(ref d, 1) = Unsafe.Add(ref s, 1);
            Unsafe.Add(ref d, 2) = Unsafe.Add(ref s, 2);
            s = ref Unsafe.As<char, long>(ref Unsafe.Add(ref source, 10));
            d = ref Unsafe.As<char, long>(ref Unsafe.Add(ref destination, 10));
            d = s;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CopyChar15(ref char source, ref char destination)
        {
            ref var s = ref Unsafe.As<char, long>(ref source);
            ref var d = ref Unsafe.As<char, long>(ref destination);
            d = s;
            Unsafe.Add(ref d, 1) = Unsafe.Add(ref s, 1);
            Unsafe.Add(ref d, 2) = Unsafe.Add(ref s, 2);
            s = ref Unsafe.As<char, long>(ref Unsafe.Add(ref source, 11));
            d = ref Unsafe.As<char, long>(ref Unsafe.Add(ref destination, 11));
            d = s;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CopyChar16(ref char source, ref char destination)
        {
            ref var s = ref Unsafe.As<char, long>(ref source);
            ref var d = ref Unsafe.As<char, long>(ref destination);
            d = s;
            Unsafe.Add(ref d, 1) = Unsafe.Add(ref s, 1);
            Unsafe.Add(ref d, 2) = Unsafe.Add(ref s, 2);
            Unsafe.Add(ref d, 3) = Unsafe.Add(ref s, 3);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CopyChar17(ref char source, ref char destination)
        {
            ref var s = ref Unsafe.As<char, long>(ref source);
            ref var d = ref Unsafe.As<char, long>(ref destination);
            d = s;
            Unsafe.Add(ref d, 1) = Unsafe.Add(ref s, 1);
            Unsafe.Add(ref d, 2) = Unsafe.Add(ref s, 2);
            Unsafe.Add(ref d, 3) = Unsafe.Add(ref s, 3);
            Unsafe.Add(ref destination, 16) = Unsafe.Add(ref source, 16);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CopyChar18(ref char source, ref char destination)
        {
            ref var s = ref Unsafe.As<char, long>(ref source);
            ref var d = ref Unsafe.As<char, long>(ref destination);
            d = s;
            Unsafe.Add(ref d, 1) = Unsafe.Add(ref s, 1);
            Unsafe.Add(ref d, 2) = Unsafe.Add(ref s, 2);
            Unsafe.Add(ref d, 3) = Unsafe.Add(ref s, 3);
            s = ref Unsafe.As<char, long>(ref Unsafe.Add(ref source, 14));
            d = ref Unsafe.As<char, long>(ref Unsafe.Add(ref destination, 14));
            d = s;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CopyChar19(ref char source, ref char destination)
        {
            ref var s = ref Unsafe.As<char, long>(ref source);
            ref var d = ref Unsafe.As<char, long>(ref destination);
            d = s;
            Unsafe.Add(ref d, 1) = Unsafe.Add(ref s, 1);
            Unsafe.Add(ref d, 2) = Unsafe.Add(ref s, 2);
            Unsafe.Add(ref d, 3) = Unsafe.Add(ref s, 3);
            s = ref Unsafe.As<char, long>(ref Unsafe.Add(ref source, 15));
            d = ref Unsafe.As<char, long>(ref Unsafe.Add(ref destination, 15));
            d = s;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CopyChar20(ref char source, ref char destination)
        {
            ref var s = ref Unsafe.As<char, long>(ref source);
            ref var d = ref Unsafe.As<char, long>(ref destination);
            d = s;
            Unsafe.Add(ref d, 1) = Unsafe.Add(ref s, 1);
            Unsafe.Add(ref d, 2) = Unsafe.Add(ref s, 2);
            Unsafe.Add(ref d, 3) = Unsafe.Add(ref s, 3);
            Unsafe.Add(ref d, 4) = Unsafe.Add(ref s, 4);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CopyChar21(ref char source, ref char destination)
        {
            ref var s = ref Unsafe.As<char, long>(ref source);
            ref var d = ref Unsafe.As<char, long>(ref destination);
            d = s;
            Unsafe.Add(ref d, 1) = Unsafe.Add(ref s, 1);
            Unsafe.Add(ref d, 2) = Unsafe.Add(ref s, 2);
            Unsafe.Add(ref d, 3) = Unsafe.Add(ref s, 3);
            Unsafe.Add(ref d, 4) = Unsafe.Add(ref s, 4);
            Unsafe.Add(ref destination, 20) = Unsafe.Add(ref source, 20);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CopyChar22(ref char source, ref char destination)
        {
            ref var s = ref Unsafe.As<char, long>(ref source);
            ref var d = ref Unsafe.As<char, long>(ref destination);
            d = s;
            Unsafe.Add(ref d, 1) = Unsafe.Add(ref s, 1);
            Unsafe.Add(ref d, 2) = Unsafe.Add(ref s, 2);
            Unsafe.Add(ref d, 3) = Unsafe.Add(ref s, 3);
            Unsafe.Add(ref d, 4) = Unsafe.Add(ref s, 4);
            s = ref Unsafe.As<char, long>(ref Unsafe.Add(ref source, 18));
            d = ref Unsafe.As<char, long>(ref Unsafe.Add(ref destination, 18));
            d = s;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CopyChar23(ref char source, ref char destination)
        {
            ref var s = ref Unsafe.As<char, long>(ref source);
            ref var d = ref Unsafe.As<char, long>(ref destination);
            d = s;
            Unsafe.Add(ref d, 1) = Unsafe.Add(ref s, 1);
            Unsafe.Add(ref d, 2) = Unsafe.Add(ref s, 2);
            Unsafe.Add(ref d, 3) = Unsafe.Add(ref s, 3);
            Unsafe.Add(ref d, 4) = Unsafe.Add(ref s, 4);
            s = ref Unsafe.As<char, long>(ref Unsafe.Add(ref source, 19));
            d = ref Unsafe.As<char, long>(ref Unsafe.Add(ref destination, 19));
            d = s;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CopyChar24(ref char source, ref char destination)
        {
            ref var s = ref Unsafe.As<char, long>(ref source);
            ref var d = ref Unsafe.As<char, long>(ref destination);
            d = s;
            Unsafe.Add(ref d, 1) = Unsafe.Add(ref s, 1);
            Unsafe.Add(ref d, 2) = Unsafe.Add(ref s, 2);
            Unsafe.Add(ref d, 3) = Unsafe.Add(ref s, 3);
            Unsafe.Add(ref d, 4) = Unsafe.Add(ref s, 4);
            Unsafe.Add(ref d, 5) = Unsafe.Add(ref s, 5);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CopyChar25(ref char source, ref char destination)
        {
            ref var s = ref Unsafe.As<char, long>(ref source);
            ref var d = ref Unsafe.As<char, long>(ref destination);
            d = s;
            Unsafe.Add(ref d, 1) = Unsafe.Add(ref s, 1);
            Unsafe.Add(ref d, 2) = Unsafe.Add(ref s, 2);
            Unsafe.Add(ref d, 3) = Unsafe.Add(ref s, 3);
            Unsafe.Add(ref d, 4) = Unsafe.Add(ref s, 4);
            Unsafe.Add(ref d, 5) = Unsafe.Add(ref s, 5);
            Unsafe.Add(ref destination, 24) = Unsafe.Add(ref source, 24);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CopyChar26(ref char source, ref char destination)
        {
            ref var s = ref Unsafe.As<char, long>(ref source);
            ref var d = ref Unsafe.As<char, long>(ref destination);
            d = s;
            Unsafe.Add(ref d, 1) = Unsafe.Add(ref s, 1);
            Unsafe.Add(ref d, 2) = Unsafe.Add(ref s, 2);
            Unsafe.Add(ref d, 3) = Unsafe.Add(ref s, 3);
            Unsafe.Add(ref d, 4) = Unsafe.Add(ref s, 4);
            Unsafe.Add(ref d, 5) = Unsafe.Add(ref s, 5);
            s = ref Unsafe.As<char, long>(ref Unsafe.Add(ref source, 22));
            d = ref Unsafe.As<char, long>(ref Unsafe.Add(ref destination, 22));
            d = s;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CopyChar27(ref char source, ref char destination)
        {
            ref var s = ref Unsafe.As<char, long>(ref source);
            ref var d = ref Unsafe.As<char, long>(ref destination);
            d = s;
            Unsafe.Add(ref d, 1) = Unsafe.Add(ref s, 1);
            Unsafe.Add(ref d, 2) = Unsafe.Add(ref s, 2);
            Unsafe.Add(ref d, 3) = Unsafe.Add(ref s, 3);
            Unsafe.Add(ref d, 4) = Unsafe.Add(ref s, 4);
            Unsafe.Add(ref d, 5) = Unsafe.Add(ref s, 5);
            s = ref Unsafe.As<char, long>(ref Unsafe.Add(ref source, 23));
            d = ref Unsafe.As<char, long>(ref Unsafe.Add(ref destination, 23));
            d = s;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CopyChar28(ref char source, ref char destination)
        {
            ref var s = ref Unsafe.As<char, long>(ref source);
            ref var d = ref Unsafe.As<char, long>(ref destination);
            d = s;
            Unsafe.Add(ref d, 1) = Unsafe.Add(ref s, 1);
            Unsafe.Add(ref d, 2) = Unsafe.Add(ref s, 2);
            Unsafe.Add(ref d, 3) = Unsafe.Add(ref s, 3);
            Unsafe.Add(ref d, 4) = Unsafe.Add(ref s, 4);
            Unsafe.Add(ref d, 5) = Unsafe.Add(ref s, 5);
            Unsafe.Add(ref d, 6) = Unsafe.Add(ref s, 6);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CopyChar29(ref char source, ref char destination)
        {
            ref var s = ref Unsafe.As<char, long>(ref source);
            ref var d = ref Unsafe.As<char, long>(ref destination);
            d = s;
            Unsafe.Add(ref d, 1) = Unsafe.Add(ref s, 1);
            Unsafe.Add(ref d, 2) = Unsafe.Add(ref s, 2);
            Unsafe.Add(ref d, 3) = Unsafe.Add(ref s, 3);
            Unsafe.Add(ref d, 4) = Unsafe.Add(ref s, 4);
            Unsafe.Add(ref d, 5) = Unsafe.Add(ref s, 5);
            Unsafe.Add(ref d, 6) = Unsafe.Add(ref s, 6);
            Unsafe.Add(ref destination, 28) = Unsafe.Add(ref source, 28);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CopyChar30(ref char source, ref char destination)
        {
            ref var s = ref Unsafe.As<char, long>(ref source);
            ref var d = ref Unsafe.As<char, long>(ref destination);
            d = s;
            Unsafe.Add(ref d, 1) = Unsafe.Add(ref s, 1);
            Unsafe.Add(ref d, 2) = Unsafe.Add(ref s, 2);
            Unsafe.Add(ref d, 3) = Unsafe.Add(ref s, 3);
            Unsafe.Add(ref d, 4) = Unsafe.Add(ref s, 4);
            Unsafe.Add(ref d, 5) = Unsafe.Add(ref s, 5);
            Unsafe.Add(ref d, 6) = Unsafe.Add(ref s, 6);
            s = ref Unsafe.As<char, long>(ref Unsafe.Add(ref source, 26));
            d = ref Unsafe.As<char, long>(ref Unsafe.Add(ref destination, 26));
            d = s;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CopyChar31(ref char source, ref char destination)
        {
            ref var s = ref Unsafe.As<char, long>(ref source);
            ref var d = ref Unsafe.As<char, long>(ref destination);
            d = s;
            Unsafe.Add(ref d, 1) = Unsafe.Add(ref s, 1);
            Unsafe.Add(ref d, 2) = Unsafe.Add(ref s, 2);
            Unsafe.Add(ref d, 3) = Unsafe.Add(ref s, 3);
            Unsafe.Add(ref d, 4) = Unsafe.Add(ref s, 4);
            Unsafe.Add(ref d, 5) = Unsafe.Add(ref s, 5);
            Unsafe.Add(ref d, 6) = Unsafe.Add(ref s, 6);
            s = ref Unsafe.As<char, long>(ref Unsafe.Add(ref source, 27));
            d = ref Unsafe.As<char, long>(ref Unsafe.Add(ref destination, 27));
            d = s;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CopyChar32(ref char source, ref char destination)
        {
            ref var s = ref Unsafe.As<char, long>(ref source);
            ref var d = ref Unsafe.As<char, long>(ref destination);
            d = s;
            Unsafe.Add(ref d, 1) = Unsafe.Add(ref s, 1);
            Unsafe.Add(ref d, 2) = Unsafe.Add(ref s, 2);
            Unsafe.Add(ref d, 3) = Unsafe.Add(ref s, 3);
            Unsafe.Add(ref d, 4) = Unsafe.Add(ref s, 4);
            Unsafe.Add(ref d, 5) = Unsafe.Add(ref s, 5);
            Unsafe.Add(ref d, 6) = Unsafe.Add(ref s, 6);
            Unsafe.Add(ref d, 7) = Unsafe.Add(ref s, 7);
        }
    }
}