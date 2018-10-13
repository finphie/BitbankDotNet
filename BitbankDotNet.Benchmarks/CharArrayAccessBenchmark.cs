using BenchmarkDotNet.Attributes;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace BitbankDotNet.Benchmarks
{
    /// <summary>
    /// stringやchar配列から、特定の要素2つにアクセスする処理のベンチマークです。
    /// </summary>
    [Config(typeof(BenchmarkConfig))]
    public class CharArrayAccessBenchmark
    {
        const int Count = 32;
        const string SourceConstString = "0123456789abcdef";
        // ReSharper disable once ConvertToConstant.Local
        static readonly string SourceStaticString = SourceConstString;

        static readonly char[] SourceCharArray =
        {
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
            'a', 'b', 'c', 'd', 'e', 'f'
        };

        [Params(3)]
        public int Index1 { get; set; }

        [Params(7)]
        public int Index2 { get; set; }

        [Benchmark]
        public int ConstString()
        {
            var result = 0;
            for (var i = 0; i < Count; i++)
                result += SourceConstString[Index1] + SourceConstString[Index2];
            return result;
        }

        [Benchmark]
        public int StaticString()
        {
            var result = 0;
            for (var i = 0; i < Count; i++)
                result += SourceStaticString[Index1] + SourceStaticString[Index2];
            return result;
        }

        [Benchmark]
        public int CharArray()
        {
            var result = 0;
            for (var i = 0; i < Count; i++)
                result += SourceCharArray[Index1] + SourceCharArray[Index2];
            return result;
        }

        [Benchmark]
        public unsafe int PointerConstString()
        {
            var result = 0;
            fixed (char* sourcePtr = SourceConstString)
            for (var i = 0; i < Count; i++)
                result += sourcePtr[Index1] + sourcePtr[Index2];
            return result;
        }

        [Benchmark]
        public unsafe int PointerStaticString()
        {
            var result = 0;
            fixed (char* sourcePtr = SourceStaticString)
                for (var i = 0; i < Count; i++)
                    result += sourcePtr[Index1] + sourcePtr[Index2];
            return result;
        }

        [Benchmark]
        public unsafe int PointerCharArray()
        {
            var result = 0;
            fixed (char* sourcePtr = SourceCharArray)
                for (var i = 0; i < Count; i++)
                    result += sourcePtr[Index1] + sourcePtr[Index2];
            return result;
        }

        [Benchmark]
        public unsafe int UnsafeAsPointerStaticString()
        {
            var handle = GCHandle.Alloc(SourceStaticString, GCHandleType.Pinned);
            var pointer = (char*) Unsafe.AsPointer(ref MemoryMarshal.GetReference(SourceStaticString.AsSpan()));
            var result = 0;
            for (var i = 0; i < Count; i++)
                result += pointer[Index1] + pointer[Index2];
            handle.Free();
            return result;
        }

        [Benchmark]
        public unsafe int UnsafeAsPointerCharArray()
        {
            var handle = GCHandle.Alloc(SourceCharArray, GCHandleType.Pinned);
            var pointer = (char*) Unsafe.AsPointer(ref MemoryMarshal.GetReference(SourceCharArray.AsSpan()));
            var result = 0;
            for (var i = 0; i < Count; i++)
                result += pointer[Index1] + pointer[Index2];
            handle.Free();
            return result;
        }

        [Benchmark]
        public unsafe int MemoryPinConstString()
        {
            var sourceMemory = SourceConstString.AsMemory();
            var result = 0;
            using (var handle = sourceMemory.Pin())
            {
                var pointer = (char*) handle.Pointer;
                for (var i = 0; i < Count; i++)
                    result += pointer[Index1] + pointer[Index2];
            }
            return result;
        }

        [Benchmark]
        public unsafe int MemoryPinStaticString()
        {
            var sourceMemory = SourceStaticString.AsMemory();
            var result = 0;
            using (var handle = sourceMemory.Pin())
            {
                var pointer = (char*) handle.Pointer;
                for (var i = 0; i < Count; i++)
                    result += pointer[Index1] + pointer[Index2];
            }
            return result;
        }

        [Benchmark]
        public unsafe int MemoryPinCharArray()
        {
            var sourceMemory = SourceCharArray.AsMemory();
            using (var handle = sourceMemory.Pin())
            {
                var sourcePtr = (char*) handle.Pointer;
                var result = 0;
                for (var i = 0; i < Count; i++)
                    result += sourcePtr[Index1] + sourcePtr[Index2];
                return result;
            }         
        }

        [Benchmark]
        public int SpanConstString()
        {
            var sourceSpan = SourceConstString.AsSpan();
            var result = 0;
            for (var i = 0; i < Count; i++)
                result += sourceSpan[Index1] + sourceSpan[Index2];
            return result;
        }

        [Benchmark]
        public int SpanStaticString()
        {
            var sourceSpan = SourceStaticString.AsSpan();
            var result = 0;
            for (var i = 0; i < Count; i++)
                result += sourceSpan[Index1] + sourceSpan[Index2];
            return result;
        }

        [Benchmark]
        public int SpanCharArray()
        {
            var sourceSpan = SourceCharArray.AsSpan();
            var result = 0;
            for (var i = 0; i < Count; i++)
                result += sourceSpan[Index1] + sourceSpan[Index2];
            return result;
        }

        [Benchmark]
        public int UnsafeAddConstString()
        {
            ref var sourceStart = ref MemoryMarshal.GetReference(SourceConstString.AsSpan());
            var result = 0;
            for (var i = 0; i < Count; i++)
                result += Unsafe.Add(ref sourceStart, Index1) + Unsafe.Add(ref sourceStart, Index2);
            return result;
        }

        [Benchmark]
        public int UnsafeAddStaticString()
        {
            ref var sourceStart = ref MemoryMarshal.GetReference(SourceStaticString.AsSpan());
            var result = 0;
            for (var i = 0; i < Count; i++)
                result += Unsafe.Add(ref sourceStart, Index1) + Unsafe.Add(ref sourceStart, Index2);
            return result;
        }

        [Benchmark]
        public int UnsafeAddCharArray()
        {
            ref var sourceStart = ref SourceCharArray.AsSpan().GetPinnableReference();
            var result = 0;
            for (var i = 0; i < Count; i++)
                result += Unsafe.Add(ref sourceStart, Index1) + Unsafe.Add(ref sourceStart, Index2);
            return result;
        }
    }
}