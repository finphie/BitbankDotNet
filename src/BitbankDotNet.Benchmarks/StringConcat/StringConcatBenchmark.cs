using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using BenchmarkDotNet.Attributes;
using BitbankDotNet.InternalShared.Extensions;
using BitbankDotNet.InternalShared.Helpers;

namespace BitbankDotNet.Benchmarks.StringConcat
{
    /// <summary>
    /// 文字列連結処理のベンチマーク
    /// </summary>
    /// <remarks>
    /// .NET Core 2.1.5 (CoreCLR 4.6.26919.02, CoreFX 4.6.26919.02), 64bit RyuJIT
    ///                     Method | Categories | Length |      Mean |     Error |    StdDev | Gen 0/1k Op | Allocated Memory/Op |
    /// -------------------------- |------------|--------|-----------|-----------|-----------|-------------|---------------------|
    ///             StringConcat04 |         04 |      5 |  25.26 ns | 0.0417 ns | 0.0391 ns |      0.0457 |                72 B |
    ///            StringBuilder04 |         04 |      5 |  52.61 ns | 0.1751 ns | 0.1638 ns |      0.1169 |               184 B |
    ///                     Span04 |         04 |      5 |  37.41 ns | 0.1119 ns | 0.1047 ns |      0.0457 |                72 B |
    /// UnsafeCopyBlockUnaligned04 |         04 |      5 |  17.95 ns | 0.0642 ns | 0.0601 ns |      0.0457 |                72 B |
    ///             StringConcat04 |         04 |     10 |  28.39 ns | 0.0656 ns | 0.0613 ns |      0.0712 |               112 B |
    ///            StringBuilder04 |         04 |     10 |  59.51 ns | 0.2547 ns | 0.1989 ns |      0.1677 |               264 B |
    ///                     Span04 |         04 |     10 |  40.44 ns | 0.0928 ns | 0.0823 ns |      0.0712 |               112 B |
    /// UnsafeCopyBlockUnaligned04 |         04 |     10 |  21.68 ns | 0.0825 ns | 0.0771 ns |      0.0712 |               112 B |
    ///                            |            |        |           |           |           |             |                     |
    ///             StringConcat08 |         08 |      5 |  77.38 ns | 0.3934 ns | 0.3071 ns |      0.1271 |               200 B |
    ///            StringBuilder08 |         08 |      5 |  79.03 ns | 0.2369 ns | 0.2216 ns |      0.1677 |               264 B |
    ///                     Span08 |         08 |      5 |  60.21 ns | 0.1718 ns | 0.1607 ns |      0.0712 |               112 B |
    /// UnsafeCopyBlockUnaligned08 |         08 |      5 |  30.48 ns | 0.1067 ns | 0.0998 ns |      0.0712 |               112 B |
    ///             StringConcat08 |         08 |     10 |  82.63 ns | 0.4420 ns | 0.3918 ns |      0.1780 |               280 B |
    ///            StringBuilder08 |         08 |     10 |  95.28 ns | 0.3558 ns | 0.3154 ns |      0.2695 |               424 B |
    ///                     Span08 |         08 |     10 |  66.12 ns | 0.1831 ns | 0.1713 ns |      0.1220 |               192 B |
    /// UnsafeCopyBlockUnaligned08 |         08 |     10 |  37.15 ns | 0.2163 ns | 0.2023 ns |      0.1220 |               192 B |
    ///                            |            |        |           |           |           |             |                     |
    ///             StringConcat12 |         12 |      5 | 111.08 ns | 0.3497 ns | 0.3271 ns |      0.1729 |               272 B |
    ///            StringBuilder12 |         12 |      5 | 106.81 ns | 0.3034 ns | 0.2838 ns |      0.2186 |               344 B |
    ///                     Span12 |         12 |      5 |  82.66 ns | 0.3092 ns | 0.2741 ns |      0.0966 |               152 B |
    /// UnsafeCopyBlockUnaligned12 |         12 |      5 |  43.44 ns | 0.0859 ns | 0.0762 ns |      0.0966 |               152 B |
    ///             StringConcat12 |         12 |     10 | 125.41 ns | 0.3513 ns | 0.3286 ns |      0.2491 |               392 B |
    ///            StringBuilder12 |         12 |     10 | 127.76 ns | 0.4600 ns | 0.4078 ns |      0.3712 |               584 B |
    ///                     Span12 |         12 |     10 |  92.31 ns | 0.1660 ns | 0.1472 ns |      0.1729 |               272 B |
    /// UnsafeCopyBlockUnaligned12 |         12 |     10 |  53.03 ns | 0.2643 ns | 0.2473 ns |      0.1729 |               272 B |
    /// </remarks>
    [Config(typeof(BenchmarkConfig))]
    [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1132:Do not combine fields", Justification = "ベンチマーク")]
    public class StringConcatBenchmark
    {
        const string Count04 = "04";
        const string Count08 = "08";
        const string Count12 = "12";

        string _source00, _source01, _source02, _source03;
        string _source04, _source05, _source06, _source07;
        string _source08, _source09, _source10, _source11;

        [Params(5, 10, 16, 32)]
        public int Length { get; set; }

        [GlobalSetup]
        public void Setup()
        {
            string[] CreateUtf16Strings() => StringHelper.CreateUtf16Strings(4, Length);

            (_source00, _source01, _source02, _source03) = CreateUtf16Strings();
            (_source04, _source05, _source06, _source07) = CreateUtf16Strings();
            (_source08, _source09, _source10, _source11) = CreateUtf16Strings();
        }

        [Benchmark]
        [BenchmarkCategory(Count04)]
        public string StringConcat04() => string.Concat(_source00, _source01, _source02, _source03);

        [Benchmark]
        [BenchmarkCategory(Count08)]
        public string StringConcat08()
            => string.Concat(_source00, _source01, _source02, _source03, _source04, _source05, _source06, _source07);

        [Benchmark]
        [BenchmarkCategory(Count12)]
        public string StringConcat12()
            => string.Concat(_source00, _source01, _source02, _source03, _source04, _source05, _source06, _source07, _source08, _source09, _source10, _source11);

        [Benchmark]
        [BenchmarkCategory(Count04)]
        public string StringBuilder04()
        {
            var length = _source00.Length + _source01.Length + _source03.Length + _source04.Length;

            var sb = new StringBuilder(length);
            sb.Append(_source00);
            sb.Append(_source01);
            sb.Append(_source02);
            sb.Append(_source03);

            return sb.ToString();
        }

        [Benchmark]
        [BenchmarkCategory(Count08)]
        public string StringBuilder08()
        {
            var length = _source00.Length + _source01.Length + _source02.Length + _source03.Length +
                         _source04.Length + _source05.Length + _source06.Length + _source07.Length;

            var sb = new StringBuilder(length);
            sb.Append(_source00);
            sb.Append(_source01);
            sb.Append(_source02);
            sb.Append(_source03);
            sb.Append(_source04);
            sb.Append(_source05);
            sb.Append(_source06);
            sb.Append(_source07);

            return sb.ToString();
        }

        [Benchmark]
        [BenchmarkCategory(Count12)]
        public string StringBuilder12()
        {
            var length = _source00.Length + _source01.Length + _source02.Length + _source03.Length +
                         _source04.Length + _source05.Length + _source06.Length + _source07.Length +
                         _source08.Length + _source09.Length + _source10.Length + _source11.Length;

            var sb = new StringBuilder(length);
            sb.Append(_source00);
            sb.Append(_source01);
            sb.Append(_source02);
            sb.Append(_source03);
            sb.Append(_source04);
            sb.Append(_source05);
            sb.Append(_source06);
            sb.Append(_source07);
            sb.Append(_source08);
            sb.Append(_source09);
            sb.Append(_source10);
            sb.Append(_source11);

            return sb.ToString();
        }

        [Benchmark]
        [BenchmarkCategory(Count04)]
        public string Span04()
        {
            var length = _source00.Length + _source01.Length + _source03.Length + _source04.Length;

            var result = new string(default, length);
            var span = MemoryMarshal.CreateSpan(ref MemoryMarshal.GetReference(result.AsSpan()), length);

            _source00.AsSpan().CopyTo(span);
            var pos = _source00.Length;
            _source01.AsSpan().CopyTo(span.Slice(pos));
            pos += _source01.Length;
            _source02.AsSpan().CopyTo(span.Slice(pos));
            pos += _source02.Length;
            _source03.AsSpan().CopyTo(span.Slice(pos));

            return result;
        }

        [Benchmark]
        [BenchmarkCategory(Count08)]
        public string Span08()
        {
            var length = _source00.Length + _source01.Length + _source02.Length + _source03.Length +
                         _source04.Length + _source05.Length + _source06.Length + _source07.Length;

            var result = new string(default, length);
            var span = MemoryMarshal.CreateSpan(ref MemoryMarshal.GetReference(result.AsSpan()), length);

            _source00.AsSpan().CopyTo(span);
            var pos = _source00.Length;
            _source01.AsSpan().CopyTo(span.Slice(pos));
            pos += _source01.Length;
            _source02.AsSpan().CopyTo(span.Slice(pos));
            pos += _source02.Length;
            _source03.AsSpan().CopyTo(span.Slice(pos));
            pos += _source03.Length;
            _source04.AsSpan().CopyTo(span.Slice(pos));
            pos += _source04.Length;
            _source05.AsSpan().CopyTo(span.Slice(pos));
            pos += _source05.Length;
            _source06.AsSpan().CopyTo(span.Slice(pos));
            pos += _source06.Length;
            _source07.AsSpan().CopyTo(span.Slice(pos));

            return result;
        }

        [Benchmark]
        [BenchmarkCategory(Count12)]
        public string Span12()
        {
            var length = _source00.Length + _source01.Length + _source02.Length + _source03.Length +
                         _source04.Length + _source05.Length + _source06.Length + _source07.Length +
                         _source08.Length + _source09.Length + _source10.Length + _source11.Length;

            var result = new string(default, length);
            var span = MemoryMarshal.CreateSpan(ref MemoryMarshal.GetReference(result.AsSpan()), length);

            _source00.AsSpan().CopyTo(span);
            var pos = _source00.Length;
            _source01.AsSpan().CopyTo(span.Slice(pos));
            pos += _source01.Length;
            _source02.AsSpan().CopyTo(span.Slice(pos));
            pos += _source02.Length;
            _source03.AsSpan().CopyTo(span.Slice(pos));
            pos += _source03.Length;
            _source04.AsSpan().CopyTo(span.Slice(pos));
            pos += _source04.Length;
            _source05.AsSpan().CopyTo(span.Slice(pos));
            pos += _source05.Length;
            _source06.AsSpan().CopyTo(span.Slice(pos));
            pos += _source06.Length;
            _source07.AsSpan().CopyTo(span.Slice(pos));
            pos += _source07.Length;
            _source08.AsSpan().CopyTo(span.Slice(pos));
            pos += _source08.Length;
            _source09.AsSpan().CopyTo(span.Slice(pos));
            pos += _source09.Length;
            _source10.AsSpan().CopyTo(span.Slice(pos));
            pos += _source10.Length;
            _source11.AsSpan().CopyTo(span.Slice(pos));

            return result;
        }

        [Benchmark]
        [BenchmarkCategory(Count04)]
        public string UnsafeCopyBlockUnaligned04()
        {
            var length = _source00.Length + _source01.Length + _source02.Length + _source03.Length;

            var result = new string(default, length);
            ref var resultStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(result.AsSpan()));

            var pos = _source00.Length * sizeof(char);
            BinaryHelper.Copy(_source00, ref resultStart, pos);

            var byteCount = _source01.Length * sizeof(char);
            BinaryHelper.Copy(_source01, ref Unsafe.Add(ref resultStart, pos), byteCount);
            pos += byteCount;

            byteCount = _source02.Length * sizeof(char);
            BinaryHelper.Copy(_source02, ref Unsafe.Add(ref resultStart, pos), byteCount);
            pos += byteCount;

            byteCount = _source03.Length * sizeof(char);
            BinaryHelper.Copy(_source03, ref Unsafe.Add(ref resultStart, pos), byteCount);

            return result;
        }

        [Benchmark]
        [BenchmarkCategory(Count08)]
        public string UnsafeCopyBlockUnaligned08()
        {
            var length = _source00.Length + _source01.Length + _source02.Length + _source03.Length +
                         _source04.Length + _source05.Length + _source06.Length + _source07.Length;

            var result = new string(default, length);
            ref var resultStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(result.AsSpan()));

            var pos = _source00.Length * sizeof(char);
            BinaryHelper.Copy(_source00, ref resultStart, pos);

            var byteCount = _source01.Length * sizeof(char);
            BinaryHelper.Copy(_source01, ref Unsafe.Add(ref resultStart, pos), byteCount);
            pos += byteCount;

            byteCount = _source02.Length * sizeof(char);
            BinaryHelper.Copy(_source02, ref Unsafe.Add(ref resultStart, pos), byteCount);
            pos += byteCount;

            byteCount = _source03.Length * sizeof(char);
            BinaryHelper.Copy(_source03, ref Unsafe.Add(ref resultStart, pos), byteCount);
            pos += byteCount;

            byteCount = _source04.Length * sizeof(char);
            BinaryHelper.Copy(_source04, ref Unsafe.Add(ref resultStart, pos), byteCount);
            pos += byteCount;

            byteCount = _source05.Length * sizeof(char);
            BinaryHelper.Copy(_source05, ref Unsafe.Add(ref resultStart, pos), byteCount);
            pos += byteCount;

            byteCount = _source06.Length * sizeof(char);
            BinaryHelper.Copy(_source06, ref Unsafe.Add(ref resultStart, pos), byteCount);
            pos += byteCount;

            byteCount = _source07.Length * sizeof(char);
            BinaryHelper.Copy(_source07, ref Unsafe.Add(ref resultStart, pos), byteCount);

            return result;
        }

        [Benchmark]
        [BenchmarkCategory(Count12)]
        public string UnsafeCopyBlockUnaligned12()
        {
            var length = _source00.Length + _source01.Length + _source02.Length + _source03.Length +
                         _source04.Length + _source05.Length + _source06.Length + _source07.Length +
                         _source08.Length + _source09.Length + _source10.Length + _source11.Length;

            var result = new string(default, length);
            ref var resultStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(result.AsSpan()));

            var pos = _source00.Length * sizeof(char);
            BinaryHelper.Copy(_source00, ref resultStart, pos);

            var byteCount = _source01.Length * sizeof(char);
            BinaryHelper.Copy(_source01, ref Unsafe.Add(ref resultStart, pos), byteCount);
            pos += byteCount;

            byteCount = _source02.Length * sizeof(char);
            BinaryHelper.Copy(_source02, ref Unsafe.Add(ref resultStart, pos), byteCount);
            pos += byteCount;

            byteCount = _source03.Length * sizeof(char);
            BinaryHelper.Copy(_source03, ref Unsafe.Add(ref resultStart, pos), byteCount);
            pos += byteCount;

            byteCount = _source04.Length * sizeof(char);
            BinaryHelper.Copy(_source04, ref Unsafe.Add(ref resultStart, pos), byteCount);
            pos += byteCount;

            byteCount = _source05.Length * sizeof(char);
            BinaryHelper.Copy(_source05, ref Unsafe.Add(ref resultStart, pos), byteCount);
            pos += byteCount;

            byteCount = _source06.Length * sizeof(char);
            BinaryHelper.Copy(_source06, ref Unsafe.Add(ref resultStart, pos), byteCount);
            pos += byteCount;

            byteCount = _source07.Length * sizeof(char);
            BinaryHelper.Copy(_source07, ref Unsafe.Add(ref resultStart, pos), byteCount);
            pos += byteCount;

            byteCount = _source08.Length * sizeof(char);
            BinaryHelper.Copy(_source08, ref Unsafe.Add(ref resultStart, pos), byteCount);
            pos += byteCount;

            byteCount = _source09.Length * sizeof(char);
            BinaryHelper.Copy(_source09, ref Unsafe.Add(ref resultStart, pos), byteCount);
            pos += byteCount;

            byteCount = _source10.Length * sizeof(char);
            BinaryHelper.Copy(_source10, ref Unsafe.Add(ref resultStart, pos), byteCount);
            pos += byteCount;

            byteCount = _source11.Length * sizeof(char);
            BinaryHelper.Copy(_source11, ref Unsafe.Add(ref resultStart, pos), byteCount);

            return result;
        }

        [Benchmark]
        [BenchmarkCategory(Count04)]
        public string CopyChar04A()
        {
            var length = _source00.Length + _source01.Length + _source02.Length + _source03.Length;

            var result = new string(default, length);
            ref var resultStart = ref MemoryMarshal.GetReference(result.AsSpan());

            ref var sourceStart = ref MemoryMarshal.GetReference(_source00.AsSpan());
            var pos = _source00.Length;
            BinaryHelper.Copy(ref sourceStart, ref resultStart, pos);

            sourceStart = ref MemoryMarshal.GetReference(_source01.AsSpan());
            var charCount = _source01.Length;
            BinaryHelper.Copy(ref sourceStart, ref Unsafe.Add(ref resultStart, pos), charCount);
            pos += charCount;

            sourceStart = ref MemoryMarshal.GetReference(_source02.AsSpan());
            charCount = _source02.Length;
            BinaryHelper.Copy(ref sourceStart, ref Unsafe.Add(ref resultStart, pos), charCount);
            pos += charCount;

            sourceStart = ref MemoryMarshal.GetReference(_source03.AsSpan());
            charCount = _source03.Length;
            BinaryHelper.Copy(ref sourceStart, ref Unsafe.Add(ref resultStart, pos), charCount);

            return result;
        }

        [Benchmark]
        [BenchmarkCategory(Count08)]
        public string CopyChar08A()
        {
            var length = _source00.Length + _source01.Length + _source02.Length + _source03.Length +
                         _source04.Length + _source05.Length + _source06.Length + _source07.Length;

            var result = new string(default, length);
            ref var resultStart = ref MemoryMarshal.GetReference(result.AsSpan());

            ref var sourceStart = ref MemoryMarshal.GetReference(_source00.AsSpan());
            var pos = _source00.Length;
            BinaryHelper.Copy(ref sourceStart, ref resultStart, pos);

            sourceStart = ref MemoryMarshal.GetReference(_source01.AsSpan());
            var charCount = _source01.Length;
            BinaryHelper.Copy(ref sourceStart, ref Unsafe.Add(ref resultStart, pos), charCount);
            pos += charCount;

            sourceStart = ref MemoryMarshal.GetReference(_source02.AsSpan());
            charCount = _source02.Length;
            BinaryHelper.Copy(ref sourceStart, ref Unsafe.Add(ref resultStart, pos), charCount);
            pos += charCount;

            sourceStart = ref MemoryMarshal.GetReference(_source03.AsSpan());
            charCount = _source03.Length;
            BinaryHelper.Copy(ref sourceStart, ref Unsafe.Add(ref resultStart, pos), charCount);
            pos += charCount;

            sourceStart = ref MemoryMarshal.GetReference(_source04.AsSpan());
            charCount = _source04.Length;
            BinaryHelper.Copy(ref sourceStart, ref Unsafe.Add(ref resultStart, pos), charCount);
            pos += charCount;

            sourceStart = ref MemoryMarshal.GetReference(_source05.AsSpan());
            charCount = _source05.Length;
            BinaryHelper.Copy(ref sourceStart, ref Unsafe.Add(ref resultStart, pos), charCount);
            pos += charCount;

            sourceStart = ref MemoryMarshal.GetReference(_source06.AsSpan());
            charCount = _source06.Length;
            BinaryHelper.Copy(ref sourceStart, ref Unsafe.Add(ref resultStart, pos), charCount);
            pos += charCount;

            sourceStart = ref MemoryMarshal.GetReference(_source07.AsSpan());
            charCount = _source07.Length;
            BinaryHelper.Copy(ref sourceStart, ref Unsafe.Add(ref resultStart, pos), charCount);

            return result;
        }

        [Benchmark]
        [BenchmarkCategory(Count12)]
        public string CopyChar12A()
        {
            var length = _source00.Length + _source01.Length + _source02.Length + _source03.Length +
                         _source04.Length + _source05.Length + _source06.Length + _source07.Length +
                         _source08.Length + _source09.Length + _source10.Length + _source11.Length;

            var result = new string(default, length);
            ref var resultStart = ref MemoryMarshal.GetReference(result.AsSpan());

            ref var sourceStart = ref MemoryMarshal.GetReference(_source00.AsSpan());
            var pos = _source00.Length;
            BinaryHelper.Copy(ref sourceStart, ref resultStart, pos);

            sourceStart = ref MemoryMarshal.GetReference(_source01.AsSpan());
            var charCount = _source01.Length;
            BinaryHelper.Copy(ref sourceStart, ref Unsafe.Add(ref resultStart, pos), charCount);
            pos += charCount;

            sourceStart = ref MemoryMarshal.GetReference(_source02.AsSpan());
            charCount = _source02.Length;
            BinaryHelper.Copy(ref sourceStart, ref Unsafe.Add(ref resultStart, pos), charCount);
            pos += charCount;

            sourceStart = ref MemoryMarshal.GetReference(_source03.AsSpan());
            charCount = _source03.Length;
            BinaryHelper.Copy(ref sourceStart, ref Unsafe.Add(ref resultStart, pos), charCount);
            pos += charCount;

            sourceStart = ref MemoryMarshal.GetReference(_source04.AsSpan());
            charCount = _source04.Length;
            BinaryHelper.Copy(ref sourceStart, ref Unsafe.Add(ref resultStart, pos), charCount);
            pos += charCount;

            sourceStart = ref MemoryMarshal.GetReference(_source05.AsSpan());
            charCount = _source05.Length;
            BinaryHelper.Copy(ref sourceStart, ref Unsafe.Add(ref resultStart, pos), charCount);
            pos += charCount;

            sourceStart = ref MemoryMarshal.GetReference(_source06.AsSpan());
            charCount = _source06.Length;
            BinaryHelper.Copy(ref sourceStart, ref Unsafe.Add(ref resultStart, pos), charCount);
            pos += charCount;

            sourceStart = ref MemoryMarshal.GetReference(_source07.AsSpan());
            charCount = _source07.Length;
            BinaryHelper.Copy(ref sourceStart, ref Unsafe.Add(ref resultStart, pos), charCount);
            pos += charCount;

            sourceStart = ref MemoryMarshal.GetReference(_source08.AsSpan());
            charCount = _source08.Length;
            BinaryHelper.Copy(ref sourceStart, ref Unsafe.Add(ref resultStart, pos), charCount);
            pos += charCount;

            sourceStart = ref MemoryMarshal.GetReference(_source09.AsSpan());
            charCount = _source09.Length;
            BinaryHelper.Copy(ref sourceStart, ref Unsafe.Add(ref resultStart, pos), charCount);
            pos += charCount;

            sourceStart = ref MemoryMarshal.GetReference(_source10.AsSpan());
            charCount = _source10.Length;
            BinaryHelper.Copy(ref sourceStart, ref Unsafe.Add(ref resultStart, pos), charCount);
            pos += charCount;

            sourceStart = ref MemoryMarshal.GetReference(_source11.AsSpan());
            charCount = _source11.Length;
            BinaryHelper.Copy(ref sourceStart, ref Unsafe.Add(ref resultStart, pos), charCount);

            return result;
        }

        [Benchmark]
        [BenchmarkCategory(Count04)]
        public string CopyChar04B()
        {
            var length = _source00.Length + _source01.Length + _source02.Length + _source03.Length;

            var result = new string(default, length);
            ref var resultStart = ref MemoryMarshal.GetReference(result.AsSpan());

            ref var sourceStart = ref MemoryMarshal.GetReference(_source00.AsSpan());
            var pos = _source00.Length;
            BinaryHelper.CopyChar(ref sourceStart, ref resultStart, pos);

            sourceStart = ref MemoryMarshal.GetReference(_source01.AsSpan());
            var charCount = _source01.Length;
            BinaryHelper.CopyChar(ref sourceStart, ref Unsafe.Add(ref resultStart, pos), charCount);
            pos += charCount;

            sourceStart = ref MemoryMarshal.GetReference(_source02.AsSpan());
            charCount = _source02.Length;
            BinaryHelper.CopyChar(ref sourceStart, ref Unsafe.Add(ref resultStart, pos), charCount);
            pos += charCount;

            sourceStart = ref MemoryMarshal.GetReference(_source03.AsSpan());
            charCount = _source03.Length;
            BinaryHelper.CopyChar(ref sourceStart, ref Unsafe.Add(ref resultStart, pos), charCount);

            return result;
        }

        [Benchmark]
        [BenchmarkCategory(Count08)]
        public string CopyChar08B()
        {
            var length = _source00.Length + _source01.Length + _source02.Length + _source03.Length +
                         _source04.Length + _source05.Length + _source06.Length + _source07.Length;

            var result = new string(default, length);
            ref var resultStart = ref MemoryMarshal.GetReference(result.AsSpan());

            ref var sourceStart = ref MemoryMarshal.GetReference(_source00.AsSpan());
            var pos = _source00.Length;
            BinaryHelper.CopyChar(ref sourceStart, ref resultStart, pos);

            sourceStart = ref MemoryMarshal.GetReference(_source01.AsSpan());
            var charCount = _source01.Length;
            BinaryHelper.CopyChar(ref sourceStart, ref Unsafe.Add(ref resultStart, pos), charCount);
            pos += charCount;

            sourceStart = ref MemoryMarshal.GetReference(_source02.AsSpan());
            charCount = _source02.Length;
            BinaryHelper.CopyChar(ref sourceStart, ref Unsafe.Add(ref resultStart, pos), charCount);
            pos += charCount;

            sourceStart = ref MemoryMarshal.GetReference(_source03.AsSpan());
            charCount = _source03.Length;
            BinaryHelper.CopyChar(ref sourceStart, ref Unsafe.Add(ref resultStart, pos), charCount);
            pos += charCount;

            sourceStart = ref MemoryMarshal.GetReference(_source04.AsSpan());
            charCount = _source04.Length;
            BinaryHelper.CopyChar(ref sourceStart, ref Unsafe.Add(ref resultStart, pos), charCount);
            pos += charCount;

            sourceStart = ref MemoryMarshal.GetReference(_source05.AsSpan());
            charCount = _source05.Length;
            BinaryHelper.CopyChar(ref sourceStart, ref Unsafe.Add(ref resultStart, pos), charCount);
            pos += charCount;

            sourceStart = ref MemoryMarshal.GetReference(_source06.AsSpan());
            charCount = _source06.Length;
            BinaryHelper.CopyChar(ref sourceStart, ref Unsafe.Add(ref resultStart, pos), charCount);
            pos += charCount;

            sourceStart = ref MemoryMarshal.GetReference(_source07.AsSpan());
            charCount = _source07.Length;
            BinaryHelper.CopyChar(ref sourceStart, ref Unsafe.Add(ref resultStart, pos), charCount);

            return result;
        }

        [Benchmark]
        [BenchmarkCategory(Count12)]
        public string CopyChar12B()
        {
            var length = _source00.Length + _source01.Length + _source02.Length + _source03.Length +
                         _source04.Length + _source05.Length + _source06.Length + _source07.Length +
                         _source08.Length + _source09.Length + _source10.Length + _source11.Length;

            var result = new string(default, length);
            ref var resultStart = ref MemoryMarshal.GetReference(result.AsSpan());

            ref var sourceStart = ref MemoryMarshal.GetReference(_source00.AsSpan());
            var pos = _source00.Length;
            BinaryHelper.CopyChar(ref sourceStart, ref resultStart, pos);

            sourceStart = ref MemoryMarshal.GetReference(_source01.AsSpan());
            var charCount = _source01.Length;
            BinaryHelper.CopyChar(ref sourceStart, ref Unsafe.Add(ref resultStart, pos), charCount);
            pos += charCount;

            sourceStart = ref MemoryMarshal.GetReference(_source02.AsSpan());
            charCount = _source02.Length;
            BinaryHelper.CopyChar(ref sourceStart, ref Unsafe.Add(ref resultStart, pos), charCount);
            pos += charCount;

            sourceStart = ref MemoryMarshal.GetReference(_source03.AsSpan());
            charCount = _source03.Length;
            BinaryHelper.CopyChar(ref sourceStart, ref Unsafe.Add(ref resultStart, pos), charCount);
            pos += charCount;

            sourceStart = ref MemoryMarshal.GetReference(_source04.AsSpan());
            charCount = _source04.Length;
            BinaryHelper.CopyChar(ref sourceStart, ref Unsafe.Add(ref resultStart, pos), charCount);
            pos += charCount;

            sourceStart = ref MemoryMarshal.GetReference(_source05.AsSpan());
            charCount = _source05.Length;
            BinaryHelper.CopyChar(ref sourceStart, ref Unsafe.Add(ref resultStart, pos), charCount);
            pos += charCount;

            sourceStart = ref MemoryMarshal.GetReference(_source06.AsSpan());
            charCount = _source06.Length;
            BinaryHelper.CopyChar(ref sourceStart, ref Unsafe.Add(ref resultStart, pos), charCount);
            pos += charCount;

            sourceStart = ref MemoryMarshal.GetReference(_source07.AsSpan());
            charCount = _source07.Length;
            BinaryHelper.CopyChar(ref sourceStart, ref Unsafe.Add(ref resultStart, pos), charCount);
            pos += charCount;

            sourceStart = ref MemoryMarshal.GetReference(_source08.AsSpan());
            charCount = _source08.Length;
            BinaryHelper.CopyChar(ref sourceStart, ref Unsafe.Add(ref resultStart, pos), charCount);
            pos += charCount;

            sourceStart = ref MemoryMarshal.GetReference(_source09.AsSpan());
            charCount = _source09.Length;
            BinaryHelper.CopyChar(ref sourceStart, ref Unsafe.Add(ref resultStart, pos), charCount);
            pos += charCount;

            sourceStart = ref MemoryMarshal.GetReference(_source10.AsSpan());
            charCount = _source10.Length;
            BinaryHelper.CopyChar(ref sourceStart, ref Unsafe.Add(ref resultStart, pos), charCount);
            pos += charCount;

            sourceStart = ref MemoryMarshal.GetReference(_source11.AsSpan());
            charCount = _source11.Length;
            BinaryHelper.CopyChar(ref sourceStart, ref Unsafe.Add(ref resultStart, pos), charCount);

            return result;
        }
    }
}