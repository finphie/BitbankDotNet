using System;
using System.Buffers;
using System.Text;
using BenchmarkDotNet.Attributes;

namespace BitbankDotNet.Benchmarks
{
    /// <summary>
    /// UTF-16文字列からUTF-8Byte配列に変換
    /// </summary>
    /// <remarks>
    /// .Net Core 2.1ではSpanベースのEncoding.GetBytesは遅い（.Net Core 3.0で改善）
    /// cf. https://github.com/dotnet/corefx/issues/30382
    /// </remarks>
    [Config(typeof(BenchmarkConfig))]
    public class GetBytesBenchmark
    {
        const int BufferSize = DataLength * 3;
        const int DataLength = 64;
        readonly string _data = new string('a', DataLength);

        [Benchmark]
        public byte[] Array()
            => Encoding.UTF8.GetBytes(_data);

        [Benchmark]
        public byte[] SpanStackAlloc()
        {
            Span<byte> buffer = stackalloc byte[BufferSize];
            var length = Encoding.UTF8.GetBytes(_data, buffer);
            return buffer.Slice(0, length).ToArray();
        }

        [Benchmark]
        public byte[] SpanArrayPool()
        {
            var buffer = ArrayPool<byte>.Shared.Rent(BufferSize);
            try
            {
                var span = buffer.AsSpan();
                var length = Encoding.UTF8.GetBytes(_data, span);
                return span.Slice(0, length).ToArray();
            }
            finally
            {
                if (buffer != null)
                    ArrayPool<byte>.Shared.Return(buffer);
            }
        }

        [Benchmark]
        public unsafe byte[] UnsafeStackAlloc()
        {
            Span<byte> buffer = stackalloc byte[BufferSize];
            fixed (char* chars = _data)
            fixed (byte* bytes = buffer)
            {
                var length = Encoding.UTF8.GetBytes(chars, _data.Length, bytes, BufferSize);
                return buffer.Slice(0, length).ToArray();
            }
        }

        [Benchmark]
        public unsafe byte[] UnsafeArrayPool()
        {
            var buffer = ArrayPool<byte>.Shared.Rent(BufferSize);
            try
            {
                var span = buffer.AsSpan();
                fixed (char* chars = _data)
                fixed (byte* bytes = span)
                {
                    var length = Encoding.UTF8.GetBytes(chars, _data.Length, bytes, BufferSize);
                    return span.Slice(0, length).ToArray();
                }
            }
            finally
            {
                if (buffer != null)
                    ArrayPool<byte>.Shared.Return(buffer);
            }
        }
    }
}
