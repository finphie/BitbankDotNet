using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using BenchmarkDotNet.Attributes;

namespace BitbankDotNet.Benchmarks
{
    /// <summary>
    /// char配列から2文字を抽出してstringに変換
    /// </summary>
    [Config(typeof(BenchmarkConfig))]
    [SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "ベンチマーク")]
    public class StringCreateBenchmark
    {
        static readonly char[] SourceChars = "0123456789abcdef".ToCharArray();

        [Benchmark]
        public string StringCreate()
            => string.Create(2, SourceChars, (span, c) =>
            {
                span[0] = c[1];
                span[1] = c[10];
            });

        [Benchmark]
        public string New() => new string(new[] { SourceChars[1], SourceChars[10] });

        [Benchmark]
        public unsafe string Stackalloc()
        {
            var array = stackalloc char[]
            {
                SourceChars[1], SourceChars[10]
            };
            return new string(array, 0, 2);
        }

        [Benchmark]
        public string SpanStackalloc()
        {
            Span<char> span = stackalloc char[]
            {
                SourceChars[1], SourceChars[10]
            };
            return new string(span);
        }

        [Benchmark]
        public unsafe string UnsafePointer()
        {
            var s = new string(default, 2);
            fixed (char* pointer = s)
            {
                pointer[0] = SourceChars[1];
                pointer[1] = SourceChars[10];
            }

            return s;
        }

        [Benchmark]
        public unsafe string UnsafeAsPointer()
        {
            var s = new string(default, 2);
            var handle = GCHandle.Alloc(s, GCHandleType.Pinned);
            var pointer = (char*)Unsafe.AsPointer(ref MemoryMarshal.GetReference(s.AsSpan()));
            pointer[0] = SourceChars[1];
            pointer[1] = SourceChars[10];
            handle.Free();
            return s;
        }

        [Benchmark]
        public unsafe string MemoryPin()
        {
            var s = new string(default, 2);
            var memory = s.AsMemory();
            using (var handle = memory.Pin())
            {
                var pointer = (char*)handle.Pointer;
                pointer[0] = SourceChars[1];
                pointer[1] = SourceChars[10];
            }

            return s;
        }

        // cf. https://github.com/dotnet/corefx/blob/v2.2.0/src/Common/src/CoreLib/System/Char.cs#L994-L998
        [Benchmark]
        public unsafe string CoreFx()
        {
            var temp = 0U;
            var array = (char*)&temp;
            array[0] = SourceChars[1];
            array[1] = SourceChars[10];
            return new string(array, 0, 2);
        }

        [Benchmark]
        public string MemoryMarshalCreateSpan()
        {
            var s = new string(default, 2);
            var span = MemoryMarshal.CreateSpan(ref MemoryMarshal.GetReference(s.AsSpan()), 2);
            span[0] = SourceChars[1];
            span[1] = SourceChars[10];
            return s;
        }

        [Benchmark]
        public string UnsafeAdd()
        {
            var s = new string(default, 2);
            ref var start = ref MemoryMarshal.GetReference(s.AsSpan());
            Unsafe.Add(ref start, 0) = SourceChars[1];
            Unsafe.Add(ref start, 1) = SourceChars[10];
            return s;
        }

        [Benchmark]
        public string StringBuilder()
            => new StringBuilder(2).Append(SourceChars[1]).Append(SourceChars[10]).ToString();
    }
}