using System;
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
                Table[i] = s[0] + ((uint) s[1] << 16);
            }
        }

        public static unsafe string ToHexString(byte[] value)
        {
            var result = new string(default, value.Length * 2);
            fixed (char* resultPointer = result)
            {
                for (var i = 0; i < value.Length; i++)
                {
                    var val = Table[value[i]];
                    resultPointer[i * 2] = (char) val;
                    resultPointer[i * 2 + 1] = (char) (val >> 16);
                }
                return result;
            }
        }
    }

    static unsafe class ByteArrayHelperLookupUnsafe
    {
        static readonly uint[] Table;
        static readonly uint* TablePointer;

        static ByteArrayHelperLookupUnsafe()
        {
            Table = new uint[256];
            for (var i = 0; i < Table.Length; i++)
            {
                var s = i.ToString("x2");
                Table[i] = BitConverter.IsLittleEndian
                    ? s[0] + ((uint)s[1] << 16)
                    : s[1] + ((uint)s[0] << 16);
            }

            TablePointer = (uint*)GCHandle.Alloc(Table, GCHandleType.Pinned).AddrOfPinnedObject();
        }

        public static string ToHexString(byte[] value)
        {
            var result = new string(default, value.Length * 2);
            fixed (byte* bytesPointer = value)
            fixed (char* resultPointer = result)
            {
                var resultPointer2 = (uint*)resultPointer;
                for (var i = 0; i < value.Length; i++)
                    resultPointer2[i] = TablePointer[bytesPointer[i]];
            }
            return result;
        }
    }
}