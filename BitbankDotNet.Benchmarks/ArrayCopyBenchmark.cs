using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using BenchmarkDotNet.Attributes;

namespace BitbankDotNet.Benchmarks
{
    /// <summary>
    /// byte配列をコピーする処理のベンチマーク
    /// </summary>
    /// <remarks>
    /// .NET Core 2.1.5 (CoreCLR 4.6.26919.02, CoreFX 4.6.26919.02), 64bit RyuJIT
    ///                      Method | ArraySize |       Mean |     Error |    StdDev | Gen 0/1k Op | Allocated Memory/Op |
    /// --------------------------- |---------- |-----------:|----------:|----------:|------------:|--------------------:|
    ///                   ArrayCopy |        10 |  11.164 ns | 0.0400 ns | 0.0374 ns |           - |                   - |
    ///                  SpanCopyTo |        10 |  11.446 ns | 0.0346 ns | 0.0307 ns |           - |                   - |
    ///                MemoryCopyTo |        10 |  20.576 ns | 0.1022 ns | 0.0853 ns |           - |                   - |
    ///             BufferBlockCopy |        10 |   5.374 ns | 0.0347 ns | 0.0325 ns |           - |                   - |
    ///            BufferMemoryCopy |        10 |   2.703 ns | 0.0219 ns | 0.0205 ns |           - |                   - |
    ///                 MarshalCopy |        10 |  12.667 ns | 0.1168 ns | 0.1093 ns |           - |                   - |
    ///             UnsafeCopyBlock |        10 |   1.396 ns | 0.0091 ns | 0.0086 ns |           - |                   - |
    ///    UnsafeCopyBlockUnaligned |        10 |   1.394 ns | 0.0074 ns | 0.0069 ns |           - |                   - |
    /// UnmanagedMemoryStreamCopyTo |        10 | 167.240 ns | 0.5569 ns | 0.5209 ns |      0.1218 |               192 B |
    ///   UnmanagedMemoryStreamRead |        10 |  46.143 ns | 0.1477 ns | 0.1309 ns |      0.0610 |                96 B |
    ///  UnmanagedMemoryStreamWrite |        10 |  47.202 ns | 0.1418 ns | 0.1326 ns |      0.0610 |                96 B |
    ///                   ArrayCopy |       100 |  14.807 ns | 0.0481 ns | 0.0402 ns |           - |                   - |
    ///                  SpanCopyTo |       100 |  13.346 ns | 0.0469 ns | 0.0416 ns |           - |                   - |
    ///                MemoryCopyTo |       100 |  22.091 ns | 0.0832 ns | 0.0738 ns |           - |                   - |
    ///             BufferBlockCopy |       100 |   8.269 ns | 0.0267 ns | 0.0250 ns |           - |                   - |
    ///            BufferMemoryCopy |       100 |   4.923 ns | 0.0268 ns | 0.0251 ns |           - |                   - |
    ///                 MarshalCopy |       100 |  15.199 ns | 0.0748 ns | 0.0624 ns |           - |                   - |
    ///             UnsafeCopyBlock |       100 |   3.238 ns | 0.0245 ns | 0.0217 ns |           - |                   - |
    ///    UnsafeCopyBlockUnaligned |       100 |   3.288 ns | 0.0197 ns | 0.0184 ns |           - |                   - |
    /// UnmanagedMemoryStreamCopyTo |       100 | 171.076 ns | 0.9193 ns | 0.8599 ns |      0.1218 |               192 B |
    ///   UnmanagedMemoryStreamRead |       100 |  48.327 ns | 0.1803 ns | 0.1686 ns |      0.0610 |                96 B |
    ///  UnmanagedMemoryStreamWrite |       100 |  49.177 ns | 0.1275 ns | 0.1193 ns |      0.0610 |                96 B |
    ///                   ArrayCopy |       512 |  25.699 ns | 0.1039 ns | 0.0972 ns |           - |                   - |
    ///                  SpanCopyTo |       512 |  18.186 ns | 0.0886 ns | 0.0786 ns |           - |                   - |
    ///                MemoryCopyTo |       512 |  28.494 ns | 0.0968 ns | 0.0905 ns |           - |                   - |
    ///             BufferBlockCopy |       512 |  16.204 ns | 0.0437 ns | 0.0387 ns |           - |                   - |
    ///            BufferMemoryCopy |       512 |  11.685 ns | 0.0469 ns | 0.0439 ns |           - |                   - |
    ///                 MarshalCopy |       512 |  27.357 ns | 0.0974 ns | 0.0911 ns |           - |                   - |
    ///             UnsafeCopyBlock |       512 |  10.197 ns | 0.0392 ns | 0.0367 ns |           - |                   - |
    ///    UnsafeCopyBlockUnaligned |       512 |   9.539 ns | 0.0484 ns | 0.0429 ns |           - |                   - |
    /// UnmanagedMemoryStreamCopyTo |       512 | 175.777 ns | 0.6582 ns | 0.6157 ns |      0.1218 |               192 B |
    ///   UnmanagedMemoryStreamRead |       512 |  54.675 ns | 0.1817 ns | 0.1611 ns |      0.0610 |                96 B |
    ///  UnmanagedMemoryStreamWrite |       512 |  55.433 ns | 0.1336 ns | 0.1250 ns |      0.0610 |                96 B |
    ///                   ArrayCopy |      1024 |  30.031 ns | 0.0219 ns | 0.0194 ns |           - |                   - |
    ///                  SpanCopyTo |      1024 |  24.974 ns | 0.1305 ns | 0.1220 ns |           - |                   - |
    ///                MemoryCopyTo |      1024 |  36.700 ns | 0.3673 ns | 0.3436 ns |           - |                   - |
    ///             BufferBlockCopy |      1024 |  25.774 ns | 0.0912 ns | 0.0712 ns |           - |                   - |
    ///            BufferMemoryCopy |      1024 |  21.763 ns | 0.1196 ns | 0.1118 ns |           - |                   - |
    ///                 MarshalCopy |      1024 |  36.451 ns | 0.2883 ns | 0.2697 ns |           - |                   - |
    ///             UnsafeCopyBlock |      1024 |  23.771 ns | 0.0689 ns | 0.0645 ns |           - |                   - |
    ///    UnsafeCopyBlockUnaligned |      1024 |  23.646 ns | 0.0764 ns | 0.0715 ns |           - |                   - |
    /// UnmanagedMemoryStreamCopyTo |      1024 | 197.271 ns | 0.6174 ns | 0.5775 ns |      0.1218 |               192 B |
    ///   UnmanagedMemoryStreamRead |      1024 |  63.506 ns | 0.4353 ns | 0.3859 ns |      0.0609 |                96 B |
    ///  UnmanagedMemoryStreamWrite |      1024 |  64.249 ns | 0.2965 ns | 0.2773 ns |      0.0609 |                96 B |
    ///                   ArrayCopy |      2048 |  38.882 ns | 0.2240 ns | 0.2095 ns |           - |                   - |
    ///                  SpanCopyTo |      2048 |  42.010 ns | 0.2576 ns | 0.2283 ns |           - |                   - |
    ///                MemoryCopyTo |      2048 |  51.960 ns | 0.1898 ns | 0.1585 ns |           - |                   - |
    ///             BufferBlockCopy |      2048 |  34.502 ns | 0.1737 ns | 0.1625 ns |           - |                   - |
    ///            BufferMemoryCopy |      2048 |  40.164 ns | 0.1620 ns | 0.1516 ns |           - |                   - |
    ///                 MarshalCopy |      2048 |  45.575 ns | 0.0413 ns | 0.0322 ns |           - |                   - |
    ///             UnsafeCopyBlock |      2048 |  30.587 ns | 0.1511 ns | 0.1414 ns |           - |                   - |
    ///    UnsafeCopyBlockUnaligned |      2048 |  30.451 ns | 0.1462 ns | 0.1367 ns |           - |                   - |
    /// UnmanagedMemoryStreamCopyTo |      2048 | 232.804 ns | 4.6042 ns | 4.9264 ns |      0.1216 |               192 B |
    ///   UnmanagedMemoryStreamRead |      2048 |  80.761 ns | 0.2769 ns | 0.2162 ns |      0.0609 |                96 B |
    ///  UnmanagedMemoryStreamWrite |      2048 |  82.180 ns | 0.6484 ns | 0.6065 ns |      0.0609 |                96 B |
    ///                   ArrayCopy |     10000 | 156.595 ns | 0.6902 ns | 0.6456 ns |           - |                   - |
    ///                  SpanCopyTo |     10000 | 114.318 ns | 0.3935 ns | 0.3286 ns |           - |                   - |
    ///                MemoryCopyTo |     10000 | 122.700 ns | 0.2064 ns | 0.1723 ns |           - |                   - |
    ///             BufferBlockCopy |     10000 | 152.926 ns | 0.6126 ns | 0.5730 ns |           - |                   - |
    ///            BufferMemoryCopy |     10000 | 156.016 ns | 0.4628 ns | 0.4103 ns |           - |                   - |
    ///                 MarshalCopy |     10000 | 113.328 ns | 0.3792 ns | 0.3362 ns |           - |                   - |
    ///             UnsafeCopyBlock |     10000 | 148.841 ns | 0.5570 ns | 0.5210 ns |           - |                   - |
    ///    UnsafeCopyBlockUnaligned |     10000 | 147.681 ns | 0.5931 ns | 0.5548 ns |           - |                   - |
    /// UnmanagedMemoryStreamCopyTo |     10000 | 489.231 ns | 1.7102 ns | 1.5161 ns |      0.1211 |               192 B |
    ///   UnmanagedMemoryStreamRead |     10000 | 207.219 ns | 0.5111 ns | 0.4781 ns |      0.0608 |                96 B |
    ///  UnmanagedMemoryStreamWrite |     10000 | 153.135 ns | 0.5225 ns | 0.4632 ns |      0.0608 |                96 B |
    /// </remarks>
    [Config(typeof(BenchmarkConfig))]
    public class ArrayCopyBenchmark
    {
        byte[] _source;
        byte[] _destination;

        [Params(10, 100, 512, 1024, 2048, 10000)]
        public int ArraySize { get; set; }

        [GlobalSetup]
        public void Setup()
        {
            _source = Enumerable.Repeat<byte>(1, ArraySize).ToArray();
            _destination = new byte[_source.Length];
        }

        [Benchmark]
        public void ArrayCopy()
            => Array.Copy(_source, 0, _destination, 0, _destination.Length);

        [Benchmark]
        public void SpanCopyTo()
            => _source.AsSpan().CopyTo(_destination);

        [Benchmark]
        public void MemoryCopyTo()
            => _source.AsMemory().CopyTo(_destination);

        [Benchmark]
        public void BufferBlockCopy()
            => Buffer.BlockCopy(_source, 0, _destination, 0, _destination.Length);

        [Benchmark]
        public unsafe void BufferMemoryCopy()
        {
            fixed (void* source = &_source[0], destination = &_destination[0])
                Buffer.MemoryCopy(source, destination, _destination.Length, _source.Length);
        }

        [Benchmark]
        public unsafe void MarshalCopy()
        {
            fixed (byte* source = &_source[0])
                Marshal.Copy((IntPtr)source, _destination, 0, _destination.Length);
        }

        [Benchmark]
        public void UnsafeCopyBlock()
            => Unsafe.CopyBlock(ref _destination[0], ref _source[0], (uint)_destination.Length);

        [Benchmark]
        public void UnsafeCopyBlockUnaligned()
            => Unsafe.CopyBlockUnaligned(ref _destination[0], ref _source[0], (uint)_destination.Length);

        [Benchmark]
        public unsafe void UnmanagedMemoryStreamCopyTo()
        {
            var length = _destination.Length;
            fixed (byte* source = &_source[0], destination = &_destination[0])
            using (var streamSource = new UnmanagedMemoryStream(source, _source.Length))
            using (var streamDestination =
                new UnmanagedMemoryStream(destination, length, length, FileAccess.Write))
                streamSource.CopyTo(streamDestination);
        }

        [Benchmark]
        public unsafe void UnmanagedMemoryStreamRead()
        {
            fixed (byte* source = &_source[0])
            using (var streamSource = new UnmanagedMemoryStream(source, _source.Length))
                streamSource.Read(_destination);
        }

        [Benchmark]
        public unsafe void UnmanagedMemoryStreamWrite()
        {
            var length = _destination.Length;
            fixed (byte* destination = &_destination[0])
            using (var streamDestination =
                new UnmanagedMemoryStream(destination, length, length, FileAccess.Write))
                streamDestination.Write(_source);
        }
    }
}