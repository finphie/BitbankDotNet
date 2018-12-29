using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using BenchmarkDotNet.Attributes;
using BitbankDotNet.InternalShared.Extensions;
using BitbankDotNet.InternalShared.Helpers;

namespace BitbankDotNet.Benchmarks.StringConcat
{
    [Config(typeof(BenchmarkConfig))]
    [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1132:Do not combine fields", Justification = "自動生成コード")]
    public class StringConcat2Benchmark
    {
        string _source1A, _source1B;
        uint _length1;
        string _source2A, _source2B;
        uint _length2;
        string _source3A, _source3B;
        uint _length3;
        string _source4A, _source4B;
        uint _length4;
        string _source5A, _source5B;
        uint _length5;
        string _source6A, _source6B;
        uint _length6;
        string _source7A, _source7B;
        uint _length7;
        string _source8A, _source8B;
        uint _length8;
        string _source9A, _source9B;
        uint _length9;
        string _source10A, _source10B;
        uint _length10;
        string _source11A, _source11B;
        uint _length11;
        string _source12A, _source12B;
        uint _length12;
        string _source13A, _source13B;
        uint _length13;
        string _source14A, _source14B;
        uint _length14;
        string _source15A, _source15B;
        uint _length15;
        string _source16A, _source16B;
        uint _length16;
        string _source17A, _source17B;
        uint _length17;
        string _source18A, _source18B;
        uint _length18;
        string _source19A, _source19B;
        uint _length19;
        string _source20A, _source20B;
        uint _length20;
        string _source21A, _source21B;
        uint _length21;
        string _source22A, _source22B;
        uint _length22;
        string _source23A, _source23B;
        uint _length23;
        string _source24A, _source24B;
        uint _length24;
        string _source25A, _source25B;
        uint _length25;
        string _source26A, _source26B;
        uint _length26;
        string _source27A, _source27B;
        uint _length27;
        string _source28A, _source28B;
        uint _length28;
        string _source29A, _source29B;
        uint _length29;
        string _source30A, _source30B;
        uint _length30;
        string _source31A, _source31B;
        uint _length31;
        string _source32A, _source32B;
        uint _length32;

        [GlobalSetup]
        public void Setup()
        {
            string[] CreateUtf16Strings(int length) => StringHelper.CreateUtf16Strings(2, length);

            (_source1A, _source1B) = CreateUtf16Strings(1);
            _length1 = (uint)_source1A.Length * 2;
            (_source2A, _source2B) = CreateUtf16Strings(2);
            _length2 = (uint)_source2A.Length * 2;
            (_source3A, _source3B) = CreateUtf16Strings(3);
            _length3 = (uint)_source3A.Length * 2;
            (_source4A, _source4B) = CreateUtf16Strings(4);
            _length4 = (uint)_source4A.Length * 2;
            (_source5A, _source5B) = CreateUtf16Strings(5);
            _length5 = (uint)_source5A.Length * 2;
            (_source6A, _source6B) = CreateUtf16Strings(6);
            _length6 = (uint)_source6A.Length * 2;
            (_source7A, _source7B) = CreateUtf16Strings(7);
            _length7 = (uint)_source7A.Length * 2;
            (_source8A, _source8B) = CreateUtf16Strings(8);
            _length8 = (uint)_source8A.Length * 2;
            (_source9A, _source9B) = CreateUtf16Strings(9);
            _length9 = (uint)_source9A.Length * 2;
            (_source10A, _source10B) = CreateUtf16Strings(10);
            _length10 = (uint)_source10A.Length * 2;
            (_source11A, _source11B) = CreateUtf16Strings(11);
            _length11 = (uint)_source11A.Length * 2;
            (_source12A, _source12B) = CreateUtf16Strings(12);
            _length12 = (uint)_source12A.Length * 2;
            (_source13A, _source13B) = CreateUtf16Strings(13);
            _length13 = (uint)_source13A.Length * 2;
            (_source14A, _source14B) = CreateUtf16Strings(14);
            _length14 = (uint)_source14A.Length * 2;
            (_source15A, _source15B) = CreateUtf16Strings(15);
            _length15 = (uint)_source15A.Length * 2;
            (_source16A, _source16B) = CreateUtf16Strings(16);
            _length16 = (uint)_source16A.Length * 2;
            (_source17A, _source17B) = CreateUtf16Strings(17);
            _length17 = (uint)_source17A.Length * 2;
            (_source18A, _source18B) = CreateUtf16Strings(18);
            _length18 = (uint)_source18A.Length * 2;
            (_source19A, _source19B) = CreateUtf16Strings(19);
            _length19 = (uint)_source19A.Length * 2;
            (_source20A, _source20B) = CreateUtf16Strings(20);
            _length20 = (uint)_source20A.Length * 2;
            (_source21A, _source21B) = CreateUtf16Strings(21);
            _length21 = (uint)_source21A.Length * 2;
            (_source22A, _source22B) = CreateUtf16Strings(22);
            _length22 = (uint)_source22A.Length * 2;
            (_source23A, _source23B) = CreateUtf16Strings(23);
            _length23 = (uint)_source23A.Length * 2;
            (_source24A, _source24B) = CreateUtf16Strings(24);
            _length24 = (uint)_source24A.Length * 2;
            (_source25A, _source25B) = CreateUtf16Strings(25);
            _length25 = (uint)_source25A.Length * 2;
            (_source26A, _source26B) = CreateUtf16Strings(26);
            _length26 = (uint)_source26A.Length * 2;
            (_source27A, _source27B) = CreateUtf16Strings(27);
            _length27 = (uint)_source27A.Length * 2;
            (_source28A, _source28B) = CreateUtf16Strings(28);
            _length28 = (uint)_source28A.Length * 2;
            (_source29A, _source29B) = CreateUtf16Strings(29);
            _length29 = (uint)_source29A.Length * 2;
            (_source30A, _source30B) = CreateUtf16Strings(30);
            _length30 = (uint)_source30A.Length * 2;
            (_source31A, _source31B) = CreateUtf16Strings(31);
            _length31 = (uint)_source31A.Length * 2;
            (_source32A, _source32B) = CreateUtf16Strings(32);
            _length32 = (uint)_source32A.Length * 2;
        }

        [Benchmark]
        [BenchmarkCategory("1")]
        public string UnsafeCopyBlockUnaligned1A()
        {
            var result = new string(default, 2);
            ref var resultStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(result.AsSpan()));

            ref var sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source1A.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref resultStart, ref sourceStart, 2);

            sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source1B.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref Unsafe.Add(ref resultStart, 2), ref sourceStart, 2);

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("1")]
        public string UnsafeCopyBlockUnaligned1B()
        {
            var result = new string(default, 2);
            ref var resultStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(result.AsSpan()));

            ref var sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source1A.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref resultStart, ref sourceStart, _length1);

            sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source1B.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref Unsafe.Add(ref resultStart, 2), ref sourceStart, _length1);

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("1")]
        public string CopyChar1()
        {
            var result = new string(default, 2);
            ref var resultStart = ref MemoryMarshal.GetReference(result.AsSpan());

            ref var sourceStart = ref MemoryMarshal.GetReference(_source1A.AsSpan());
            BinaryHelper.CopyChar1(ref sourceStart, ref resultStart);

            sourceStart = ref MemoryMarshal.GetReference(_source1B.AsSpan());
            BinaryHelper.CopyChar1(ref sourceStart, ref Unsafe.Add(ref resultStart, 1));

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("2")]
        public string UnsafeCopyBlockUnaligned2A()
        {
            var result = new string(default, 4);
            ref var resultStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(result.AsSpan()));

            ref var sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source2A.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref resultStart, ref sourceStart, 4);

            sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source2B.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref Unsafe.Add(ref resultStart, 4), ref sourceStart, 4);

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("2")]
        public string UnsafeCopyBlockUnaligned2B()
        {
            var result = new string(default, 4);
            ref var resultStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(result.AsSpan()));

            ref var sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source2A.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref resultStart, ref sourceStart, _length2);

            sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source2B.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref Unsafe.Add(ref resultStart, 4), ref sourceStart, _length2);

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("2")]
        public string CopyChar2()
        {
            var result = new string(default, 4);
            ref var resultStart = ref MemoryMarshal.GetReference(result.AsSpan());

            ref var sourceStart = ref MemoryMarshal.GetReference(_source2A.AsSpan());
            BinaryHelper.CopyChar2(ref sourceStart, ref resultStart);

            sourceStart = ref MemoryMarshal.GetReference(_source2B.AsSpan());
            BinaryHelper.CopyChar2(ref sourceStart, ref Unsafe.Add(ref resultStart, 2));

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("3")]
        public string UnsafeCopyBlockUnaligned3A()
        {
            var result = new string(default, 6);
            ref var resultStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(result.AsSpan()));

            ref var sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source3A.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref resultStart, ref sourceStart, 6);

            sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source3B.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref Unsafe.Add(ref resultStart, 6), ref sourceStart, 6);

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("3")]
        public string UnsafeCopyBlockUnaligned3B()
        {
            var result = new string(default, 6);
            ref var resultStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(result.AsSpan()));

            ref var sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source3A.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref resultStart, ref sourceStart, _length3);

            sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source3B.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref Unsafe.Add(ref resultStart, 6), ref sourceStart, _length3);

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("3")]
        public string CopyChar3()
        {
            var result = new string(default, 6);
            ref var resultStart = ref MemoryMarshal.GetReference(result.AsSpan());

            ref var sourceStart = ref MemoryMarshal.GetReference(_source3A.AsSpan());
            BinaryHelper.CopyChar3(ref sourceStart, ref resultStart);

            sourceStart = ref MemoryMarshal.GetReference(_source3B.AsSpan());
            BinaryHelper.CopyChar3(ref sourceStart, ref Unsafe.Add(ref resultStart, 3));

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("4")]
        public string UnsafeCopyBlockUnaligned4A()
        {
            var result = new string(default, 8);
            ref var resultStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(result.AsSpan()));

            ref var sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source4A.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref resultStart, ref sourceStart, 8);

            sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source4B.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref Unsafe.Add(ref resultStart, 8), ref sourceStart, 8);

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("4")]
        public string UnsafeCopyBlockUnaligned4B()
        {
            var result = new string(default, 8);
            ref var resultStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(result.AsSpan()));

            ref var sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source4A.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref resultStart, ref sourceStart, _length4);

            sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source4B.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref Unsafe.Add(ref resultStart, 8), ref sourceStart, _length4);

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("4")]
        public string CopyChar4()
        {
            var result = new string(default, 8);
            ref var resultStart = ref MemoryMarshal.GetReference(result.AsSpan());

            ref var sourceStart = ref MemoryMarshal.GetReference(_source4A.AsSpan());
            BinaryHelper.CopyChar4(ref sourceStart, ref resultStart);

            sourceStart = ref MemoryMarshal.GetReference(_source4B.AsSpan());
            BinaryHelper.CopyChar4(ref sourceStart, ref Unsafe.Add(ref resultStart, 4));

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("5")]
        public string UnsafeCopyBlockUnaligned5A()
        {
            var result = new string(default, 10);
            ref var resultStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(result.AsSpan()));

            ref var sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source5A.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref resultStart, ref sourceStart, 10);

            sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source5B.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref Unsafe.Add(ref resultStart, 10), ref sourceStart, 10);

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("5")]
        public string UnsafeCopyBlockUnaligned5B()
        {
            var result = new string(default, 10);
            ref var resultStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(result.AsSpan()));

            ref var sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source5A.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref resultStart, ref sourceStart, _length5);

            sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source5B.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref Unsafe.Add(ref resultStart, 10), ref sourceStart, _length5);

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("5")]
        public string CopyChar5()
        {
            var result = new string(default, 10);
            ref var resultStart = ref MemoryMarshal.GetReference(result.AsSpan());

            ref var sourceStart = ref MemoryMarshal.GetReference(_source5A.AsSpan());
            BinaryHelper.CopyChar5(ref sourceStart, ref resultStart);

            sourceStart = ref MemoryMarshal.GetReference(_source5B.AsSpan());
            BinaryHelper.CopyChar5(ref sourceStart, ref Unsafe.Add(ref resultStart, 5));

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("6")]
        public string UnsafeCopyBlockUnaligned6A()
        {
            var result = new string(default, 12);
            ref var resultStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(result.AsSpan()));

            ref var sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source6A.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref resultStart, ref sourceStart, 12);

            sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source6B.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref Unsafe.Add(ref resultStart, 12), ref sourceStart, 12);

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("6")]
        public string UnsafeCopyBlockUnaligned6B()
        {
            var result = new string(default, 12);
            ref var resultStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(result.AsSpan()));

            ref var sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source6A.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref resultStart, ref sourceStart, _length6);

            sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source6B.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref Unsafe.Add(ref resultStart, 12), ref sourceStart, _length6);

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("6")]
        public string CopyChar6()
        {
            var result = new string(default, 12);
            ref var resultStart = ref MemoryMarshal.GetReference(result.AsSpan());

            ref var sourceStart = ref MemoryMarshal.GetReference(_source6A.AsSpan());
            BinaryHelper.CopyChar6(ref sourceStart, ref resultStart);

            sourceStart = ref MemoryMarshal.GetReference(_source6B.AsSpan());
            BinaryHelper.CopyChar6(ref sourceStart, ref Unsafe.Add(ref resultStart, 6));

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("7")]
        public string UnsafeCopyBlockUnaligned7A()
        {
            var result = new string(default, 14);
            ref var resultStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(result.AsSpan()));

            ref var sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source7A.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref resultStart, ref sourceStart, 14);

            sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source7B.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref Unsafe.Add(ref resultStart, 14), ref sourceStart, 14);

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("7")]
        public string UnsafeCopyBlockUnaligned7B()
        {
            var result = new string(default, 14);
            ref var resultStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(result.AsSpan()));

            ref var sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source7A.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref resultStart, ref sourceStart, _length7);

            sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source7B.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref Unsafe.Add(ref resultStart, 14), ref sourceStart, _length7);

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("7")]
        public string CopyChar7()
        {
            var result = new string(default, 14);
            ref var resultStart = ref MemoryMarshal.GetReference(result.AsSpan());

            ref var sourceStart = ref MemoryMarshal.GetReference(_source7A.AsSpan());
            BinaryHelper.CopyChar7(ref sourceStart, ref resultStart);

            sourceStart = ref MemoryMarshal.GetReference(_source7B.AsSpan());
            BinaryHelper.CopyChar7(ref sourceStart, ref Unsafe.Add(ref resultStart, 7));

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("8")]
        public string UnsafeCopyBlockUnaligned8A()
        {
            var result = new string(default, 16);
            ref var resultStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(result.AsSpan()));

            ref var sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source8A.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref resultStart, ref sourceStart, 16);

            sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source8B.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref Unsafe.Add(ref resultStart, 16), ref sourceStart, 16);

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("8")]
        public string UnsafeCopyBlockUnaligned8B()
        {
            var result = new string(default, 16);
            ref var resultStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(result.AsSpan()));

            ref var sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source8A.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref resultStart, ref sourceStart, _length8);

            sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source8B.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref Unsafe.Add(ref resultStart, 16), ref sourceStart, _length8);

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("8")]
        public string CopyChar8()
        {
            var result = new string(default, 16);
            ref var resultStart = ref MemoryMarshal.GetReference(result.AsSpan());

            ref var sourceStart = ref MemoryMarshal.GetReference(_source8A.AsSpan());
            BinaryHelper.CopyChar8(ref sourceStart, ref resultStart);

            sourceStart = ref MemoryMarshal.GetReference(_source8B.AsSpan());
            BinaryHelper.CopyChar8(ref sourceStart, ref Unsafe.Add(ref resultStart, 8));

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("9")]
        public string UnsafeCopyBlockUnaligned9A()
        {
            var result = new string(default, 18);
            ref var resultStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(result.AsSpan()));

            ref var sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source9A.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref resultStart, ref sourceStart, 18);

            sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source9B.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref Unsafe.Add(ref resultStart, 18), ref sourceStart, 18);

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("9")]
        public string UnsafeCopyBlockUnaligned9B()
        {
            var result = new string(default, 18);
            ref var resultStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(result.AsSpan()));

            ref var sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source9A.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref resultStart, ref sourceStart, _length9);

            sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source9B.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref Unsafe.Add(ref resultStart, 18), ref sourceStart, _length9);

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("9")]
        public string CopyChar9()
        {
            var result = new string(default, 18);
            ref var resultStart = ref MemoryMarshal.GetReference(result.AsSpan());

            ref var sourceStart = ref MemoryMarshal.GetReference(_source9A.AsSpan());
            BinaryHelper.CopyChar9(ref sourceStart, ref resultStart);

            sourceStart = ref MemoryMarshal.GetReference(_source9B.AsSpan());
            BinaryHelper.CopyChar9(ref sourceStart, ref Unsafe.Add(ref resultStart, 9));

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("10")]
        public string UnsafeCopyBlockUnaligned10A()
        {
            var result = new string(default, 20);
            ref var resultStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(result.AsSpan()));

            ref var sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source10A.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref resultStart, ref sourceStart, 20);

            sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source10B.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref Unsafe.Add(ref resultStart, 20), ref sourceStart, 20);

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("10")]
        public string UnsafeCopyBlockUnaligned10B()
        {
            var result = new string(default, 20);
            ref var resultStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(result.AsSpan()));

            ref var sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source10A.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref resultStart, ref sourceStart, _length10);

            sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source10B.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref Unsafe.Add(ref resultStart, 20), ref sourceStart, _length10);

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("10")]
        public string CopyChar10()
        {
            var result = new string(default, 20);
            ref var resultStart = ref MemoryMarshal.GetReference(result.AsSpan());

            ref var sourceStart = ref MemoryMarshal.GetReference(_source10A.AsSpan());
            BinaryHelper.CopyChar10(ref sourceStart, ref resultStart);

            sourceStart = ref MemoryMarshal.GetReference(_source10B.AsSpan());
            BinaryHelper.CopyChar10(ref sourceStart, ref Unsafe.Add(ref resultStart, 10));

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("11")]
        public string UnsafeCopyBlockUnaligned11A()
        {
            var result = new string(default, 22);
            ref var resultStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(result.AsSpan()));

            ref var sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source11A.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref resultStart, ref sourceStart, 22);

            sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source11B.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref Unsafe.Add(ref resultStart, 22), ref sourceStart, 22);

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("11")]
        public string UnsafeCopyBlockUnaligned11B()
        {
            var result = new string(default, 22);
            ref var resultStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(result.AsSpan()));

            ref var sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source11A.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref resultStart, ref sourceStart, _length11);

            sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source11B.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref Unsafe.Add(ref resultStart, 22), ref sourceStart, _length11);

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("11")]
        public string CopyChar11()
        {
            var result = new string(default, 22);
            ref var resultStart = ref MemoryMarshal.GetReference(result.AsSpan());

            ref var sourceStart = ref MemoryMarshal.GetReference(_source11A.AsSpan());
            BinaryHelper.CopyChar11(ref sourceStart, ref resultStart);

            sourceStart = ref MemoryMarshal.GetReference(_source11B.AsSpan());
            BinaryHelper.CopyChar11(ref sourceStart, ref Unsafe.Add(ref resultStart, 11));

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("12")]
        public string UnsafeCopyBlockUnaligned12A()
        {
            var result = new string(default, 24);
            ref var resultStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(result.AsSpan()));

            ref var sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source12A.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref resultStart, ref sourceStart, 24);

            sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source12B.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref Unsafe.Add(ref resultStart, 24), ref sourceStart, 24);

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("12")]
        public string UnsafeCopyBlockUnaligned12B()
        {
            var result = new string(default, 24);
            ref var resultStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(result.AsSpan()));

            ref var sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source12A.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref resultStart, ref sourceStart, _length12);

            sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source12B.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref Unsafe.Add(ref resultStart, 24), ref sourceStart, _length12);

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("12")]
        public string CopyChar12()
        {
            var result = new string(default, 24);
            ref var resultStart = ref MemoryMarshal.GetReference(result.AsSpan());

            ref var sourceStart = ref MemoryMarshal.GetReference(_source12A.AsSpan());
            BinaryHelper.CopyChar12(ref sourceStart, ref resultStart);

            sourceStart = ref MemoryMarshal.GetReference(_source12B.AsSpan());
            BinaryHelper.CopyChar12(ref sourceStart, ref Unsafe.Add(ref resultStart, 12));

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("13")]
        public string UnsafeCopyBlockUnaligned13A()
        {
            var result = new string(default, 26);
            ref var resultStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(result.AsSpan()));

            ref var sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source13A.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref resultStart, ref sourceStart, 26);

            sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source13B.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref Unsafe.Add(ref resultStart, 26), ref sourceStart, 26);

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("13")]
        public string UnsafeCopyBlockUnaligned13B()
        {
            var result = new string(default, 26);
            ref var resultStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(result.AsSpan()));

            ref var sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source13A.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref resultStart, ref sourceStart, _length13);

            sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source13B.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref Unsafe.Add(ref resultStart, 26), ref sourceStart, _length13);

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("13")]
        public string CopyChar13()
        {
            var result = new string(default, 26);
            ref var resultStart = ref MemoryMarshal.GetReference(result.AsSpan());

            ref var sourceStart = ref MemoryMarshal.GetReference(_source13A.AsSpan());
            BinaryHelper.CopyChar13(ref sourceStart, ref resultStart);

            sourceStart = ref MemoryMarshal.GetReference(_source13B.AsSpan());
            BinaryHelper.CopyChar13(ref sourceStart, ref Unsafe.Add(ref resultStart, 13));

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("14")]
        public string UnsafeCopyBlockUnaligned14A()
        {
            var result = new string(default, 28);
            ref var resultStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(result.AsSpan()));

            ref var sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source14A.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref resultStart, ref sourceStart, 28);

            sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source14B.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref Unsafe.Add(ref resultStart, 28), ref sourceStart, 28);

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("14")]
        public string UnsafeCopyBlockUnaligned14B()
        {
            var result = new string(default, 28);
            ref var resultStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(result.AsSpan()));

            ref var sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source14A.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref resultStart, ref sourceStart, _length14);

            sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source14B.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref Unsafe.Add(ref resultStart, 28), ref sourceStart, _length14);

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("14")]
        public string CopyChar14()
        {
            var result = new string(default, 28);
            ref var resultStart = ref MemoryMarshal.GetReference(result.AsSpan());

            ref var sourceStart = ref MemoryMarshal.GetReference(_source14A.AsSpan());
            BinaryHelper.CopyChar14(ref sourceStart, ref resultStart);

            sourceStart = ref MemoryMarshal.GetReference(_source14B.AsSpan());
            BinaryHelper.CopyChar14(ref sourceStart, ref Unsafe.Add(ref resultStart, 14));

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("15")]
        public string UnsafeCopyBlockUnaligned15A()
        {
            var result = new string(default, 30);
            ref var resultStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(result.AsSpan()));

            ref var sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source15A.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref resultStart, ref sourceStart, 30);

            sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source15B.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref Unsafe.Add(ref resultStart, 30), ref sourceStart, 30);

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("15")]
        public string UnsafeCopyBlockUnaligned15B()
        {
            var result = new string(default, 30);
            ref var resultStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(result.AsSpan()));

            ref var sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source15A.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref resultStart, ref sourceStart, _length15);

            sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source15B.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref Unsafe.Add(ref resultStart, 30), ref sourceStart, _length15);

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("15")]
        public string CopyChar15()
        {
            var result = new string(default, 30);
            ref var resultStart = ref MemoryMarshal.GetReference(result.AsSpan());

            ref var sourceStart = ref MemoryMarshal.GetReference(_source15A.AsSpan());
            BinaryHelper.CopyChar15(ref sourceStart, ref resultStart);

            sourceStart = ref MemoryMarshal.GetReference(_source15B.AsSpan());
            BinaryHelper.CopyChar15(ref sourceStart, ref Unsafe.Add(ref resultStart, 15));

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("16")]
        public string UnsafeCopyBlockUnaligned16A()
        {
            var result = new string(default, 32);
            ref var resultStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(result.AsSpan()));

            ref var sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source16A.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref resultStart, ref sourceStart, 32);

            sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source16B.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref Unsafe.Add(ref resultStart, 32), ref sourceStart, 32);

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("16")]
        public string UnsafeCopyBlockUnaligned16B()
        {
            var result = new string(default, 32);
            ref var resultStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(result.AsSpan()));

            ref var sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source16A.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref resultStart, ref sourceStart, _length16);

            sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source16B.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref Unsafe.Add(ref resultStart, 32), ref sourceStart, _length16);

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("16")]
        public string CopyChar16()
        {
            var result = new string(default, 32);
            ref var resultStart = ref MemoryMarshal.GetReference(result.AsSpan());

            ref var sourceStart = ref MemoryMarshal.GetReference(_source16A.AsSpan());
            BinaryHelper.CopyChar16(ref sourceStart, ref resultStart);

            sourceStart = ref MemoryMarshal.GetReference(_source16B.AsSpan());
            BinaryHelper.CopyChar16(ref sourceStart, ref Unsafe.Add(ref resultStart, 16));

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("17")]
        public string UnsafeCopyBlockUnaligned17A()
        {
            var result = new string(default, 34);
            ref var resultStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(result.AsSpan()));

            ref var sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source17A.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref resultStart, ref sourceStart, 34);

            sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source17B.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref Unsafe.Add(ref resultStart, 34), ref sourceStart, 34);

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("17")]
        public string UnsafeCopyBlockUnaligned17B()
        {
            var result = new string(default, 34);
            ref var resultStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(result.AsSpan()));

            ref var sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source17A.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref resultStart, ref sourceStart, _length17);

            sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source17B.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref Unsafe.Add(ref resultStart, 34), ref sourceStart, _length17);

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("17")]
        public string CopyChar17()
        {
            var result = new string(default, 34);
            ref var resultStart = ref MemoryMarshal.GetReference(result.AsSpan());

            ref var sourceStart = ref MemoryMarshal.GetReference(_source17A.AsSpan());
            BinaryHelper.CopyChar17(ref sourceStart, ref resultStart);

            sourceStart = ref MemoryMarshal.GetReference(_source17B.AsSpan());
            BinaryHelper.CopyChar17(ref sourceStart, ref Unsafe.Add(ref resultStart, 17));

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("18")]
        public string UnsafeCopyBlockUnaligned18A()
        {
            var result = new string(default, 36);
            ref var resultStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(result.AsSpan()));

            ref var sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source18A.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref resultStart, ref sourceStart, 36);

            sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source18B.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref Unsafe.Add(ref resultStart, 36), ref sourceStart, 36);

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("18")]
        public string UnsafeCopyBlockUnaligned18B()
        {
            var result = new string(default, 36);
            ref var resultStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(result.AsSpan()));

            ref var sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source18A.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref resultStart, ref sourceStart, _length18);

            sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source18B.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref Unsafe.Add(ref resultStart, 36), ref sourceStart, _length18);

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("18")]
        public string CopyChar18()
        {
            var result = new string(default, 36);
            ref var resultStart = ref MemoryMarshal.GetReference(result.AsSpan());

            ref var sourceStart = ref MemoryMarshal.GetReference(_source18A.AsSpan());
            BinaryHelper.CopyChar18(ref sourceStart, ref resultStart);

            sourceStart = ref MemoryMarshal.GetReference(_source18B.AsSpan());
            BinaryHelper.CopyChar18(ref sourceStart, ref Unsafe.Add(ref resultStart, 18));

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("19")]
        public string UnsafeCopyBlockUnaligned19A()
        {
            var result = new string(default, 38);
            ref var resultStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(result.AsSpan()));

            ref var sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source19A.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref resultStart, ref sourceStart, 38);

            sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source19B.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref Unsafe.Add(ref resultStart, 38), ref sourceStart, 38);

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("19")]
        public string UnsafeCopyBlockUnaligned19B()
        {
            var result = new string(default, 38);
            ref var resultStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(result.AsSpan()));

            ref var sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source19A.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref resultStart, ref sourceStart, _length19);

            sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source19B.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref Unsafe.Add(ref resultStart, 38), ref sourceStart, _length19);

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("19")]
        public string CopyChar19()
        {
            var result = new string(default, 38);
            ref var resultStart = ref MemoryMarshal.GetReference(result.AsSpan());

            ref var sourceStart = ref MemoryMarshal.GetReference(_source19A.AsSpan());
            BinaryHelper.CopyChar19(ref sourceStart, ref resultStart);

            sourceStart = ref MemoryMarshal.GetReference(_source19B.AsSpan());
            BinaryHelper.CopyChar19(ref sourceStart, ref Unsafe.Add(ref resultStart, 19));

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("20")]
        public string UnsafeCopyBlockUnaligned20A()
        {
            var result = new string(default, 40);
            ref var resultStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(result.AsSpan()));

            ref var sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source20A.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref resultStart, ref sourceStart, 40);

            sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source20B.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref Unsafe.Add(ref resultStart, 40), ref sourceStart, 40);

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("20")]
        public string UnsafeCopyBlockUnaligned20B()
        {
            var result = new string(default, 40);
            ref var resultStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(result.AsSpan()));

            ref var sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source20A.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref resultStart, ref sourceStart, _length20);

            sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source20B.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref Unsafe.Add(ref resultStart, 40), ref sourceStart, _length20);

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("20")]
        public string CopyChar20()
        {
            var result = new string(default, 40);
            ref var resultStart = ref MemoryMarshal.GetReference(result.AsSpan());

            ref var sourceStart = ref MemoryMarshal.GetReference(_source20A.AsSpan());
            BinaryHelper.CopyChar20(ref sourceStart, ref resultStart);

            sourceStart = ref MemoryMarshal.GetReference(_source20B.AsSpan());
            BinaryHelper.CopyChar20(ref sourceStart, ref Unsafe.Add(ref resultStart, 20));

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("21")]
        public string UnsafeCopyBlockUnaligned21A()
        {
            var result = new string(default, 42);
            ref var resultStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(result.AsSpan()));

            ref var sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source21A.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref resultStart, ref sourceStart, 42);

            sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source21B.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref Unsafe.Add(ref resultStart, 42), ref sourceStart, 42);

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("21")]
        public string UnsafeCopyBlockUnaligned21B()
        {
            var result = new string(default, 42);
            ref var resultStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(result.AsSpan()));

            ref var sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source21A.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref resultStart, ref sourceStart, _length21);

            sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source21B.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref Unsafe.Add(ref resultStart, 42), ref sourceStart, _length21);

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("21")]
        public string CopyChar21()
        {
            var result = new string(default, 42);
            ref var resultStart = ref MemoryMarshal.GetReference(result.AsSpan());

            ref var sourceStart = ref MemoryMarshal.GetReference(_source21A.AsSpan());
            BinaryHelper.CopyChar21(ref sourceStart, ref resultStart);

            sourceStart = ref MemoryMarshal.GetReference(_source21B.AsSpan());
            BinaryHelper.CopyChar21(ref sourceStart, ref Unsafe.Add(ref resultStart, 21));

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("22")]
        public string UnsafeCopyBlockUnaligned22A()
        {
            var result = new string(default, 44);
            ref var resultStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(result.AsSpan()));

            ref var sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source22A.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref resultStart, ref sourceStart, 44);

            sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source22B.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref Unsafe.Add(ref resultStart, 44), ref sourceStart, 44);

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("22")]
        public string UnsafeCopyBlockUnaligned22B()
        {
            var result = new string(default, 44);
            ref var resultStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(result.AsSpan()));

            ref var sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source22A.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref resultStart, ref sourceStart, _length22);

            sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source22B.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref Unsafe.Add(ref resultStart, 44), ref sourceStart, _length22);

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("22")]
        public string CopyChar22()
        {
            var result = new string(default, 44);
            ref var resultStart = ref MemoryMarshal.GetReference(result.AsSpan());

            ref var sourceStart = ref MemoryMarshal.GetReference(_source22A.AsSpan());
            BinaryHelper.CopyChar22(ref sourceStart, ref resultStart);

            sourceStart = ref MemoryMarshal.GetReference(_source22B.AsSpan());
            BinaryHelper.CopyChar22(ref sourceStart, ref Unsafe.Add(ref resultStart, 22));

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("23")]
        public string UnsafeCopyBlockUnaligned23A()
        {
            var result = new string(default, 46);
            ref var resultStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(result.AsSpan()));

            ref var sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source23A.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref resultStart, ref sourceStart, 46);

            sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source23B.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref Unsafe.Add(ref resultStart, 46), ref sourceStart, 46);

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("23")]
        public string UnsafeCopyBlockUnaligned23B()
        {
            var result = new string(default, 46);
            ref var resultStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(result.AsSpan()));

            ref var sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source23A.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref resultStart, ref sourceStart, _length23);

            sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source23B.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref Unsafe.Add(ref resultStart, 46), ref sourceStart, _length23);

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("23")]
        public string CopyChar23()
        {
            var result = new string(default, 46);
            ref var resultStart = ref MemoryMarshal.GetReference(result.AsSpan());

            ref var sourceStart = ref MemoryMarshal.GetReference(_source23A.AsSpan());
            BinaryHelper.CopyChar23(ref sourceStart, ref resultStart);

            sourceStart = ref MemoryMarshal.GetReference(_source23B.AsSpan());
            BinaryHelper.CopyChar23(ref sourceStart, ref Unsafe.Add(ref resultStart, 23));

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("24")]
        public string UnsafeCopyBlockUnaligned24A()
        {
            var result = new string(default, 48);
            ref var resultStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(result.AsSpan()));

            ref var sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source24A.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref resultStart, ref sourceStart, 48);

            sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source24B.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref Unsafe.Add(ref resultStart, 48), ref sourceStart, 48);

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("24")]
        public string UnsafeCopyBlockUnaligned24B()
        {
            var result = new string(default, 48);
            ref var resultStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(result.AsSpan()));

            ref var sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source24A.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref resultStart, ref sourceStart, _length24);

            sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source24B.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref Unsafe.Add(ref resultStart, 48), ref sourceStart, _length24);

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("24")]
        public string CopyChar24()
        {
            var result = new string(default, 48);
            ref var resultStart = ref MemoryMarshal.GetReference(result.AsSpan());

            ref var sourceStart = ref MemoryMarshal.GetReference(_source24A.AsSpan());
            BinaryHelper.CopyChar24(ref sourceStart, ref resultStart);

            sourceStart = ref MemoryMarshal.GetReference(_source24B.AsSpan());
            BinaryHelper.CopyChar24(ref sourceStart, ref Unsafe.Add(ref resultStart, 24));

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("25")]
        public string UnsafeCopyBlockUnaligned25A()
        {
            var result = new string(default, 50);
            ref var resultStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(result.AsSpan()));

            ref var sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source25A.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref resultStart, ref sourceStart, 50);

            sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source25B.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref Unsafe.Add(ref resultStart, 50), ref sourceStart, 50);

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("25")]
        public string UnsafeCopyBlockUnaligned25B()
        {
            var result = new string(default, 50);
            ref var resultStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(result.AsSpan()));

            ref var sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source25A.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref resultStart, ref sourceStart, _length25);

            sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source25B.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref Unsafe.Add(ref resultStart, 50), ref sourceStart, _length25);

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("25")]
        public string CopyChar25()
        {
            var result = new string(default, 50);
            ref var resultStart = ref MemoryMarshal.GetReference(result.AsSpan());

            ref var sourceStart = ref MemoryMarshal.GetReference(_source25A.AsSpan());
            BinaryHelper.CopyChar25(ref sourceStart, ref resultStart);

            sourceStart = ref MemoryMarshal.GetReference(_source25B.AsSpan());
            BinaryHelper.CopyChar25(ref sourceStart, ref Unsafe.Add(ref resultStart, 25));

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("26")]
        public string UnsafeCopyBlockUnaligned26A()
        {
            var result = new string(default, 52);
            ref var resultStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(result.AsSpan()));

            ref var sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source26A.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref resultStart, ref sourceStart, 52);

            sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source26B.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref Unsafe.Add(ref resultStart, 52), ref sourceStart, 52);

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("26")]
        public string UnsafeCopyBlockUnaligned26B()
        {
            var result = new string(default, 52);
            ref var resultStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(result.AsSpan()));

            ref var sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source26A.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref resultStart, ref sourceStart, _length26);

            sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source26B.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref Unsafe.Add(ref resultStart, 52), ref sourceStart, _length26);

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("26")]
        public string CopyChar26()
        {
            var result = new string(default, 52);
            ref var resultStart = ref MemoryMarshal.GetReference(result.AsSpan());

            ref var sourceStart = ref MemoryMarshal.GetReference(_source26A.AsSpan());
            BinaryHelper.CopyChar26(ref sourceStart, ref resultStart);

            sourceStart = ref MemoryMarshal.GetReference(_source26B.AsSpan());
            BinaryHelper.CopyChar26(ref sourceStart, ref Unsafe.Add(ref resultStart, 26));

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("27")]
        public string UnsafeCopyBlockUnaligned27A()
        {
            var result = new string(default, 54);
            ref var resultStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(result.AsSpan()));

            ref var sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source27A.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref resultStart, ref sourceStart, 54);

            sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source27B.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref Unsafe.Add(ref resultStart, 54), ref sourceStart, 54);

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("27")]
        public string UnsafeCopyBlockUnaligned27B()
        {
            var result = new string(default, 54);
            ref var resultStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(result.AsSpan()));

            ref var sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source27A.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref resultStart, ref sourceStart, _length27);

            sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source27B.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref Unsafe.Add(ref resultStart, 54), ref sourceStart, _length27);

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("27")]
        public string CopyChar27()
        {
            var result = new string(default, 54);
            ref var resultStart = ref MemoryMarshal.GetReference(result.AsSpan());

            ref var sourceStart = ref MemoryMarshal.GetReference(_source27A.AsSpan());
            BinaryHelper.CopyChar27(ref sourceStart, ref resultStart);

            sourceStart = ref MemoryMarshal.GetReference(_source27B.AsSpan());
            BinaryHelper.CopyChar27(ref sourceStart, ref Unsafe.Add(ref resultStart, 27));

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("28")]
        public string UnsafeCopyBlockUnaligned28A()
        {
            var result = new string(default, 56);
            ref var resultStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(result.AsSpan()));

            ref var sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source28A.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref resultStart, ref sourceStart, 56);

            sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source28B.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref Unsafe.Add(ref resultStart, 56), ref sourceStart, 56);

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("28")]
        public string UnsafeCopyBlockUnaligned28B()
        {
            var result = new string(default, 56);
            ref var resultStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(result.AsSpan()));

            ref var sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source28A.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref resultStart, ref sourceStart, _length28);

            sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source28B.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref Unsafe.Add(ref resultStart, 56), ref sourceStart, _length28);

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("28")]
        public string CopyChar28()
        {
            var result = new string(default, 56);
            ref var resultStart = ref MemoryMarshal.GetReference(result.AsSpan());

            ref var sourceStart = ref MemoryMarshal.GetReference(_source28A.AsSpan());
            BinaryHelper.CopyChar28(ref sourceStart, ref resultStart);

            sourceStart = ref MemoryMarshal.GetReference(_source28B.AsSpan());
            BinaryHelper.CopyChar28(ref sourceStart, ref Unsafe.Add(ref resultStart, 28));

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("29")]
        public string UnsafeCopyBlockUnaligned29A()
        {
            var result = new string(default, 58);
            ref var resultStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(result.AsSpan()));

            ref var sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source29A.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref resultStart, ref sourceStart, 58);

            sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source29B.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref Unsafe.Add(ref resultStart, 58), ref sourceStart, 58);

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("29")]
        public string UnsafeCopyBlockUnaligned29B()
        {
            var result = new string(default, 58);
            ref var resultStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(result.AsSpan()));

            ref var sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source29A.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref resultStart, ref sourceStart, _length29);

            sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source29B.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref Unsafe.Add(ref resultStart, 58), ref sourceStart, _length29);

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("29")]
        public string CopyChar29()
        {
            var result = new string(default, 58);
            ref var resultStart = ref MemoryMarshal.GetReference(result.AsSpan());

            ref var sourceStart = ref MemoryMarshal.GetReference(_source29A.AsSpan());
            BinaryHelper.CopyChar29(ref sourceStart, ref resultStart);

            sourceStart = ref MemoryMarshal.GetReference(_source29B.AsSpan());
            BinaryHelper.CopyChar29(ref sourceStart, ref Unsafe.Add(ref resultStart, 29));

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("30")]
        public string UnsafeCopyBlockUnaligned30A()
        {
            var result = new string(default, 60);
            ref var resultStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(result.AsSpan()));

            ref var sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source30A.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref resultStart, ref sourceStart, 60);

            sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source30B.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref Unsafe.Add(ref resultStart, 60), ref sourceStart, 60);

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("30")]
        public string UnsafeCopyBlockUnaligned30B()
        {
            var result = new string(default, 60);
            ref var resultStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(result.AsSpan()));

            ref var sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source30A.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref resultStart, ref sourceStart, _length30);

            sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source30B.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref Unsafe.Add(ref resultStart, 60), ref sourceStart, _length30);

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("30")]
        public string CopyChar30()
        {
            var result = new string(default, 60);
            ref var resultStart = ref MemoryMarshal.GetReference(result.AsSpan());

            ref var sourceStart = ref MemoryMarshal.GetReference(_source30A.AsSpan());
            BinaryHelper.CopyChar30(ref sourceStart, ref resultStart);

            sourceStart = ref MemoryMarshal.GetReference(_source30B.AsSpan());
            BinaryHelper.CopyChar30(ref sourceStart, ref Unsafe.Add(ref resultStart, 30));

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("31")]
        public string UnsafeCopyBlockUnaligned31A()
        {
            var result = new string(default, 62);
            ref var resultStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(result.AsSpan()));

            ref var sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source31A.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref resultStart, ref sourceStart, 62);

            sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source31B.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref Unsafe.Add(ref resultStart, 62), ref sourceStart, 62);

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("31")]
        public string UnsafeCopyBlockUnaligned31B()
        {
            var result = new string(default, 62);
            ref var resultStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(result.AsSpan()));

            ref var sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source31A.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref resultStart, ref sourceStart, _length31);

            sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source31B.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref Unsafe.Add(ref resultStart, 62), ref sourceStart, _length31);

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("31")]
        public string CopyChar31()
        {
            var result = new string(default, 62);
            ref var resultStart = ref MemoryMarshal.GetReference(result.AsSpan());

            ref var sourceStart = ref MemoryMarshal.GetReference(_source31A.AsSpan());
            BinaryHelper.CopyChar31(ref sourceStart, ref resultStart);

            sourceStart = ref MemoryMarshal.GetReference(_source31B.AsSpan());
            BinaryHelper.CopyChar31(ref sourceStart, ref Unsafe.Add(ref resultStart, 31));

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("32")]
        public string UnsafeCopyBlockUnaligned32A()
        {
            var result = new string(default, 64);
            ref var resultStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(result.AsSpan()));

            ref var sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source32A.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref resultStart, ref sourceStart, 64);

            sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source32B.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref Unsafe.Add(ref resultStart, 64), ref sourceStart, 64);

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("32")]
        public string UnsafeCopyBlockUnaligned32B()
        {
            var result = new string(default, 64);
            ref var resultStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(result.AsSpan()));

            ref var sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source32A.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref resultStart, ref sourceStart, _length32);

            sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_source32B.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref Unsafe.Add(ref resultStart, 64), ref sourceStart, _length32);

            return result;
        }

        [Benchmark]
        [BenchmarkCategory("32")]
        public string CopyChar32()
        {
            var result = new string(default, 64);
            ref var resultStart = ref MemoryMarshal.GetReference(result.AsSpan());

            ref var sourceStart = ref MemoryMarshal.GetReference(_source32A.AsSpan());
            BinaryHelper.CopyChar32(ref sourceStart, ref resultStart);

            sourceStart = ref MemoryMarshal.GetReference(_source32B.AsSpan());
            BinaryHelper.CopyChar32(ref sourceStart, ref Unsafe.Add(ref resultStart, 32));

            return result;
        }
    }
}