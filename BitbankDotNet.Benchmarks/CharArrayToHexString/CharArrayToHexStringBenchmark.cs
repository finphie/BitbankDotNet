using BenchmarkDotNet.Attributes;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml.Serialization;

namespace BitbankDotNet.Benchmarks.CharArrayToHexString
{
    // cf. https://stackoverflow.com/questions/311165/how-do-you-convert-a-byte-array-to-a-hexadecimal-string-and-vice-versa
    /// <summary>
    /// byte配列を16進数stringに変換
    /// </summary>
    /// <remarks>
    /// ベンチマーク結果（左から速い順）
    /// 1. {byte}.TryFormat, {byte}.ToString, string.Format
    /// 2. {StringBuilder}.Append, {StringBuilder}.AppendFormat, {IEnumerable{char}}.Append
    /// 3. foreach, {IEnumerable{char}}.Aggregate
    /// 4. string.Concat({string[]}), string.Join
    /// 5. string直接書き換え, Buffer + new string({char[]}or{Span{char}}), Buffer + {Span{char}}.ToString, {StringBuilder}.Append, string.Concat({char[]})
    /// </remarks>
    [Config(typeof(BenchmarkConfig))]
    public partial class CharArrayToHexStringBenchmark
    {
        // HMAC-SHA256は256bit
        const int ArraySize = 32;
        static readonly byte[] SourceBytes;// = Enumerable.Repeat<byte>(1, 32).ToArray();

        static CharArrayToHexStringBenchmark()
        {
            SourceBytes = new byte[ArraySize];
            var random = new Random();
            random.NextBytes(SourceBytes);
        }

        //[Benchmark]
        public string BitConverterToString()
            => BitConverter.ToString(SourceBytes).ToLowerInvariant().Replace("-", "", StringComparison.Ordinal);

        //[Benchmark]
        public string XmlSerializationWriterFromByteArrayHex()
            => ByteArrayHelperUseXmlSerializationWriter.ToHexString(SourceBytes).ToLowerInvariant();      
       
        //[Benchmark]
        public string ArrayConvertAll()
            => string.Concat(Array.ConvertAll(SourceBytes, b => b.ToString("x2")));

        public string LinqSelect()
            => string.Concat(SourceBytes.Select(b => b.ToString("x2")));

        //[Benchmark]
        public string StringDirect()
        {
            var buffer = new string(default, SourceBytes.Length * 2);
            var span = MemoryMarshal.CreateSpan(ref MemoryMarshal.GetReference(buffer.AsSpan()), SourceBytes.Length * 2);
            var i = 0;
            foreach (var sourceByte in SourceBytes)
            {
                sourceByte.TryFormat(span.Slice(i), out _, "x2");
                i += 2;
            }
            return buffer;
        }

        //[Benchmark]
        public unsafe string LookupShift()
        {
            const string hexString = "0123456789abcdef";
            var buffer = new string(default, SourceBytes.Length * 2);
            fixed (char* hexPtr = hexString)
            fixed (char* bufferPtr = buffer)
            {
                var ptr = bufferPtr;
                foreach (var sourceByte in SourceBytes)
                {
                    *ptr++ = hexPtr[sourceByte >> 0b0100];
                    *ptr++ = hexPtr[sourceByte & 0b1111];
                }
            }
            return buffer;
        }

        //[Benchmark]
        public string StringTable()
            => ByteArrayHelperUseStringTable.ToHexString(SourceBytes);

        //[Benchmark]
        public string Lookup() => ByteArrayHelperLookup.ToHexString(SourceBytes);

        //[Benchmark]
        public string LookupUnsafe() => ByteArrayHelperLookupUnsafe.ToHexString(SourceBytes);
    }

    // ReSharper disable once ClassNeverInstantiated.Local
    sealed class ByteArrayHelperUseXmlSerializationWriter : XmlSerializationWriter
    {
        public static string ToHexString(byte[] value) => FromByteArrayHex(value);
        protected override void InitCallbacks() => throw new NotSupportedException();
    }

    static class ByteArrayHelperUseStringTable
    {
        static readonly string[] HexStringTable =
        {
            "00", "01", "02", "03", "04", "05", "06", "07", "08", "09", "0a", "0b", "0c", "0d", "0e", "0f",
            "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "1a", "1b", "1c", "1d", "1e", "1f",
            "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "2a", "2b", "2c", "2d", "2e", "2f",
            "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "3a", "3b", "3c", "3d", "3e", "3f",
            "40", "41", "42", "43", "44", "45", "46", "47", "48", "49", "4a", "4b", "4c", "4d", "4e", "4f",
            "50", "51", "52", "53", "54", "55", "56", "57", "58", "59", "5a", "5b", "5c", "5d", "5e", "5f",
            "60", "61", "62", "63", "64", "65", "66", "67", "68", "69", "6a", "6b", "6c", "6d", "6e", "6f",
            "70", "71", "72", "73", "74", "75", "76", "77", "78", "79", "7a", "7b", "7c", "7d", "7e", "7f",
            "80", "81", "82", "83", "84", "85", "86", "87", "88", "89", "8a", "8b", "8c", "8d", "8e", "8f",
            "90", "91", "92", "93", "94", "95", "96", "97", "98", "99", "9a", "9b", "9c", "9d", "9e", "9f",
            "a0", "a1", "a2", "a3", "a4", "a5", "a6", "a7", "a8", "a9", "aa", "ab", "ac", "ad", "ae", "af",
            "b0", "b1", "b2", "b3", "b4", "b5", "b6", "b7", "b8", "b9", "ba", "bb", "bc", "bd", "be", "bf",
            "c0", "c1", "c2", "c3", "c4", "c5", "c6", "c7", "c8", "c9", "ca", "cb", "cc", "cd", "ce", "cf",
            "d0", "d1", "d2", "d3", "d4", "d5", "d6", "d7", "d8", "d9", "da", "db", "dc", "dd", "de", "df",
            "e0", "e1", "e2", "e3", "e4", "e5", "e6", "e7", "e8", "e9", "ea", "eb", "ec", "ed", "ee", "ef",
            "f0", "f1", "f2", "f3", "f4", "f5", "f6", "f7", "f8", "f9", "fa", "fb", "fc", "fd", "fe", "ff"
        };

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToHexString(byte[] value)
        {
            var sb = new StringBuilder(value.Length * 2);
            foreach (var b in value)
                sb.Append(HexStringTable[b]);
            return sb.ToString();
        }
    }

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
                    ? s[0] + ((uint) s[1] << 16)
                    : s[1] + ((uint) s[0] << 16);
            }

            TablePointer = (uint*) GCHandle.Alloc(Table, GCHandleType.Pinned).AddrOfPinnedObject();
        }

        public static string ToHexString(byte[] value)
        {
            var result = new string(default, value.Length * 2);
            fixed (byte* bytesPointer = value)
            fixed (char* resultPointer = result)
            {
                var resultPointer2 = (uint*) resultPointer;
                for (var i = 0; i < value.Length; i++)
                    resultPointer2[i] = TablePointer[bytesPointer[i]];
            }
            return result;
        }
    }
}
