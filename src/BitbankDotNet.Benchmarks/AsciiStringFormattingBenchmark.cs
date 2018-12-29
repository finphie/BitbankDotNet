using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using BenchmarkDotNet.Attributes;

namespace BitbankDotNet.Benchmarks
{
    /// <summary>
    /// ASCII文字列をUTF-8のbyte配列に変換する処理のベンチマーク
    /// </summary>
    [Config(typeof(BenchmarkConfig))]
    [SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "ベンチマーク")]
    public class AsciiStringFormattingBenchmark
    {
        public static IEnumerable<string> Values => new[]
        {
            DateTimeOffset.Parse("2018/01/01T00:00:00Z").ToUnixTimeMilliseconds().ToString()
        };

        [Benchmark]
        [ArgumentsSource(nameof(Values))]
        public unsafe byte[] EncodingGetBytes(string value)
        {
            var byteBuffer = new byte[value.Length];
            fixed (char* chars = value)
            fixed (byte* bytes = byteBuffer)
                Encoding.ASCII.GetBytes(chars, value.Length, bytes, byteBuffer.Length);

            return byteBuffer;
        }

        [Benchmark]
        [ArgumentsSource(nameof(Values))]
        public byte[] Cast(string value)
        {
            ref var valueStart = ref MemoryMarshal.GetReference(value.AsSpan());
            var byteBuffer = new byte[value.Length];
            for (var i = 0; i < byteBuffer.Length; i++)
                byteBuffer[i] = (byte)Unsafe.Add(ref valueStart, i);

            return byteBuffer;
        }
    }
}