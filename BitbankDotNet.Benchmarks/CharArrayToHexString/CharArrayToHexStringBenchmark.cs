using BenchmarkDotNet.Attributes;
using System;
using System.Linq;
using System.Runtime.InteropServices;

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
            => ByteArrayHelperXmlSerializationWriter.ToHexString(SourceBytes).ToLowerInvariant();      
       
        //[Benchmark]
        public string ArrayConvertAll()
            => string.Concat(Array.ConvertAll(SourceBytes, b => b.ToString("x2")));

        public string LinqSelect()
            => string.Concat(SourceBytes.Select(b => b.ToString("x2")));

        //[Benchmark]
        public string StringCreate()
            => string.Create(SourceBytes.Length * 2, SourceBytes, (span, sourceBytes) =>
            {
                var i = 0;
                foreach (var sourceByte in sourceBytes)
                {
                    sourceByte.TryFormat(span.Slice(i), out _, "x2");
                    i += 2;
                }
            });

        //[Benchmark]
        public string MemoryMarshalCreateSpan()
        {
            var length = SourceBytes.Length * 2;
            var buffer = new string(default, length);
            var span = MemoryMarshal.CreateSpan(ref MemoryMarshal.GetReference(buffer.AsSpan()), length);
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
        public string StringTable() => ByteArrayHelperStringTable.ToHexString(SourceBytes);

        //[Benchmark]
        public string Lookup() => ByteArrayHelperLookup.ToHexString(SourceBytes);

        //[Benchmark]
        public string LookupUnsafe() => ByteArrayHelperLookup.ToHexStringUnsafe(SourceBytes);
    }
}