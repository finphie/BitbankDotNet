using BenchmarkDotNet.Attributes;
using System;
using System.Buffers.Binary;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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
    public class CharArrayToHexStringBenchmark
    {
        // HMAC-SHA256は256bit
        const int ArraySize = 32;
        static readonly byte[] SourceBytes;

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

        //[Benchmark]
        public string LinqSelect()
            => string.Concat(SourceBytes.Select(b => b.ToString("x2")));

        //[Benchmark]
        public string ForEach()
        {
            var length = SourceBytes.Length * 2;
            var result = new string(default, length);
            var span = MemoryMarshal.CreateSpan(ref MemoryMarshal.GetReference(result.AsSpan()), length);
            var i = 0;
            foreach (var sourceByte in SourceBytes)
            {
                sourceByte.TryFormat(span.Slice(i), out _, "x2");
                i += 2;
            }

            return result;
        }

        //[Benchmark]
        public string UnsafeAsLong()
        {
            var length = SourceBytes.Length * 2;
            var result = new string(default, length);
            var resultSpan = MemoryMarshal.CreateSpan(ref MemoryMarshal.GetReference(result.AsSpan()), length);
            ref var sourceStart = ref Unsafe.As<byte, long>(ref SourceBytes[0]);

            const int size = sizeof(long);
            const string format = "x16";

            BinaryPrimitives.ReverseEndianness(Unsafe.Add(ref sourceStart, 0))
                .TryFormat(resultSpan, out _, format);
            BinaryPrimitives.ReverseEndianness(Unsafe.Add(ref sourceStart, 1))
                .TryFormat(resultSpan.Slice(size * 2 * 1), out _, format);
            BinaryPrimitives.ReverseEndianness(Unsafe.Add(ref sourceStart, 2))
                .TryFormat(resultSpan.Slice(size * 2 * 2), out _, format);
            BinaryPrimitives.ReverseEndianness(Unsafe.Add(ref sourceStart, 3))
                .TryFormat(resultSpan.Slice(size * 2 * 3), out _, format);

            return result;
        }

        //[Benchmark]
        public string UnsafeReadUnalignedLong()
        {
            var length = SourceBytes.Length * 2;
            var result = new string(default, length);
            var resultSpan = MemoryMarshal.CreateSpan(ref MemoryMarshal.GetReference(result.AsSpan()), length);
            ref var sourceStart = ref SourceBytes[0];

            const int size = sizeof(long);
            const string format = "x16";

            // BitConverter.ToInt64やMemoryMarshal.Read内部では、Unsafe.ReadUnalignedを使用している。
            // cf. https://github.com/dotnet/corefx/blob/v2.1.5/src/Common/src/CoreLib/System/BitConverter.cs#L293
            // cf. https://github.com/dotnet/corefx/blob/v2.1.5/src/Common/src/CoreLib/System/Runtime/InteropServices/MemoryMarshal.cs#L165
            BinaryPrimitives.ReverseEndianness(Unsafe.ReadUnaligned<long>(ref Unsafe.Add(ref sourceStart, size * 0)))
                .TryFormat(resultSpan, out _, format);
            BinaryPrimitives.ReverseEndianness(Unsafe.ReadUnaligned<long>(ref Unsafe.Add(ref sourceStart, size * 1)))
                .TryFormat(resultSpan.Slice(size * 2 * 1), out _, format);
            BinaryPrimitives.ReverseEndianness(Unsafe.ReadUnaligned<long>(ref Unsafe.Add(ref sourceStart, size * 2)))
                .TryFormat(resultSpan.Slice(size * 2 * 2), out _, format);
            BinaryPrimitives.ReverseEndianness(Unsafe.ReadUnaligned<long>(ref Unsafe.Add(ref sourceStart, size * 3)))
                .TryFormat(resultSpan.Slice(size * 2 * 3), out _, format);

            return result;
        }

        //[Benchmark]
        public string BinaryPrimitivesReadInt64()
        {
            var length = SourceBytes.Length * 2;
            var result = new string(default, length);
            var resultSpan = MemoryMarshal.CreateSpan(ref MemoryMarshal.GetReference(result.AsSpan()), length);
            var sourceSpan = SourceBytes.AsSpan();

            const int size = sizeof(long);
            const string format = "x16";

            BinaryPrimitives.ReadInt64BigEndian(sourceSpan)
                .TryFormat(resultSpan, out _, format);
            BinaryPrimitives.ReadInt64BigEndian(sourceSpan.Slice(size * 1))
                .TryFormat(resultSpan.Slice(size * 2 * 1), out _, format);
            BinaryPrimitives.ReadInt64BigEndian(sourceSpan.Slice(size * 2))
                .TryFormat(resultSpan.Slice(size * 2 * 2), out _, format);
            BinaryPrimitives.ReadInt64BigEndian(sourceSpan.Slice(size * 3))
                .TryFormat(resultSpan.Slice(size * 2 * 3), out _, format);

            return result;
        }

        //[Benchmark]
        public string BinaryReader()
        {
            var length = SourceBytes.Length * 2;
            var result = new string(default, length);
            var resultSpan = MemoryMarshal.CreateSpan(ref MemoryMarshal.GetReference(result.AsSpan()), length);

            using (var ms = new MemoryStream(SourceBytes))
            using (var br = new BinaryReader(ms))
            {
                const int size = sizeof(long);
                const string format = "x16";

                BinaryPrimitives.ReverseEndianness(br.ReadInt64())
                    .TryFormat(resultSpan, out _, format);
                BinaryPrimitives.ReverseEndianness(br.ReadInt64())
                    .TryFormat(resultSpan.Slice(size * 2 * 1), out _, format);
                BinaryPrimitives.ReverseEndianness(br.ReadInt64())
                    .TryFormat(resultSpan.Slice(size * 2 * 2), out _, format);
                BinaryPrimitives.ReverseEndianness(br.ReadInt64())
                    .TryFormat(resultSpan.Slice(size * 2 * 3), out _, format);
            }

            return result;
        }

        //[Benchmark]
        public string LookupShift()
        {
            const string table = "0123456789abcdef";
            ref var tableStart = ref MemoryMarshal.GetReference(table.AsSpan());
            var result = new string(default, SourceBytes.Length * 2);
            ref var resultStart = ref MemoryMarshal.GetReference(result.AsSpan());
            var i = 0;
            foreach (var sourceByte in SourceBytes)
            {
                Unsafe.Add(ref resultStart, i++) = Unsafe.Add(ref tableStart, sourceByte >> 0b0100);
                Unsafe.Add(ref resultStart, i++) = Unsafe.Add(ref tableStart, sourceByte & 0b1111);
            }

            return result;
        }

        //[Benchmark]
        public unsafe string LookupShiftUnsafe()
        {
            const string table = "0123456789abcdef";
            var result = new string(default, SourceBytes.Length * 2);
            fixed (char* resultPointer = result, tablePointer = table)
            {
                var pointer = resultPointer;
                foreach (var sourceByte in SourceBytes)
                {
                    *pointer++ = tablePointer[sourceByte >> 0b0100];
                    *pointer++ = tablePointer[sourceByte & 0b1111];
                }
            }

            return result;
        }

        //[Benchmark]
        public string Manipulation1()
        {
            var result = new string(default, SourceBytes.Length * 2);
            ref var resultStart = ref MemoryMarshal.GetReference(result.AsSpan());
            var i = 0;
            foreach (var sourceByte in SourceBytes)
            {
                var b = (byte)(sourceByte >> 0b0100);
                Unsafe.Add(ref resultStart, i++) = (char)(b > 9 ? 'a' - 10 + b : b + '0');
                b = (byte)(sourceByte & 0b1111);
                Unsafe.Add(ref resultStart, i++) = (char)(b > 9 ? 'a' - 10 + b : b + '0');
            }

            return result;
        }

        //[Benchmark]
        public unsafe string Manipulation1Unsafe()
        {
            var result = new string(default, SourceBytes.Length * 2);
            fixed (char* resultPointer = result)
            {
                var pointer = resultPointer;
                foreach (var sourceByte in SourceBytes)
                {
                    var b = (byte)(sourceByte >> 0b0100);
                    *pointer++ = (char)(b > 9 ? 'a' - 10 + b : b + '0');
                    b = (byte)(sourceByte & 0b1111);
                    *pointer++ = (char)(b > 9 ? 'a' - 10 + b : b + '0');
                }
            }

            return result;
        }

        //[Benchmark]
        public string Manipulation2()
        {
            var result = new string(default, SourceBytes.Length * 2);
            ref var resultStart = ref MemoryMarshal.GetReference(result.AsSpan());
            var i = 0;
            foreach (var sourceByte in SourceBytes)
            {
                var b = sourceByte >> 0b0100;
                Unsafe.Add(ref resultStart, i++) =
                    (char)('a' - 10 + b + (((b - 10) >> (sizeof(int) * 8 - 1)) & -('a' - 10 - '0')));
                b = sourceByte & 0b1111;
                Unsafe.Add(ref resultStart, i++) =
                    (char)('a' - 10 + b + (((b - 10) >> (sizeof(int) * 8 - 1)) & -('a' - 10 - '0')));
            }

            return result;
        }

        //[Benchmark]
        public unsafe string Manipulation2Unsafe()
        {
            var result = new string(default, SourceBytes.Length * 2);
            fixed (char* resultPointer = result)
            {
                var pointer = resultPointer;
                foreach (var sourceByte in SourceBytes)
                {
                    var b = sourceByte >> 0b0100;
                    *pointer++ = (char)('a' - 10 + b + (((b - 10) >> (sizeof(int) * 8 - 1)) & -('a' - 10 - '0')));
                    b = sourceByte & 0b1111;
                    *pointer++ = (char)('a' - 10 + b + (((b - 10) >> (sizeof(int) * 8 - 1)) & -('a' - 10 - '0')));
                }
            }

            return result;
        }

        //[Benchmark]
        public string StringTable() => ByteArrayHelperStringTable.ToHexString(SourceBytes);

        //[Benchmark]
        public string Lookup() => ByteArrayHelperLookup.ToHexString(SourceBytes);

        //[Benchmark]
        public string LookupUnsafe() => ByteArrayHelperLookup.ToHexStringUnsafe(SourceBytes);
    }
}