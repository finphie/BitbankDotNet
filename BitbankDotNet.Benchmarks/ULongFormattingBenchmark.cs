using BenchmarkDotNet.Attributes;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace BitbankDotNet.Benchmarks
{
    /// <summary>
    /// ulongをUTF-8のbyte配列に変換する処理のベンチマーク
    /// </summary>
    [Config(typeof(BenchmarkConfig))]
    public class ULongFormattingBenchmark
    {
        const int BufferLength = 20;

        public IEnumerable<ulong> Values => new[]
        {
            (ulong)DateTimeOffset.Parse("2018/01/01T00:00:00Z").ToUnixTimeMilliseconds()
        };

        [Benchmark]
        [ArgumentsSource(nameof(Values))]
        public unsafe byte[] TryFormatEncodingGetBytes(ulong value)
        {
            Span<char> charBuffer = stackalloc char[BufferLength];
            value.TryFormat(charBuffer, out var charLength, null, CultureInfo.InvariantCulture);
            Span<byte> byteBuffer = stackalloc byte[charLength];
            fixed (char* chars = charBuffer)
            fixed (byte* bytes = byteBuffer)
            {
                var byteLength = Encoding.UTF8.GetBytes(chars, charLength, bytes, BufferLength);
                return byteBuffer.Slice(0, byteLength).ToArray();
            }
        }

        [Benchmark]
        [ArgumentsSource(nameof(Values))]
        public byte[] Utf8FormatterTryFormat(ulong value)
        {
            Span<byte> byteBuffer = stackalloc byte[BufferLength];
            Utf8Formatter.TryFormat(value, byteBuffer, out var byteLength);
            return byteBuffer.Slice(0, byteLength).ToArray();
        }

        [Benchmark]
        [ArgumentsSource(nameof(Values))]
        public byte[] ToUtf8Bytes(ulong value)
        {
            Span<byte> byteBuffer = stackalloc byte[BufferLength];
            ref var byteBufferStart = ref MemoryMarshal.GetReference(byteBuffer);
            var digits = value < 10_000_000_000_000 ? 13
                : value < 100_000_000_000_000 ? 14
                : value < 1_000_000_000_000_000 ? 15
                : value < 10_000_000_000_000_000 ? 16
                : value < 100_000_000_000_000_000 ? 17
                : value < 1_000_000_000_000_000_000 ? 18
                : value < 10_000_000_000_000_000_000 ? 19
                : 20;
            for (var i = digits; i > 0; i--)
            {
                var temp = '0' + value;
                value /= 10;
                Unsafe.Add(ref byteBufferStart, i - 1) = (byte)(temp - value * 10);
            }

            return byteBuffer.Slice(0, digits).ToArray();
        }
    }
}
