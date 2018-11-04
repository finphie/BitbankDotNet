using BenchmarkDotNet.Attributes;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Web;

namespace BitbankDotNet.Benchmarks
{
    /// <summary>
    /// URI文字列連結処理のベンチマーク
    /// </summary>
    /// <remarks>
    ///                      Method |        Mean |     Error |    StdDev | Gen 0/1k Op | Allocated Memory/Op |
    /// --------------------------- |-------------|-----------|-----------|-------------|---------------------|
    /// HttpUtilityParseQueryString | 1,344.51 ns | 5.8619 ns | 5.4833 ns |      0.7057 |              1112 B |
    ///               StringConcat1 |    78.80 ns | 0.5835 ns | 0.5458 ns |      0.1067 |               168 B |
    ///               StringConcat2 |    68.91 ns | 0.2437 ns | 0.2161 ns |      0.1220 |               192 B |
    ///               StringBuilder |    67.91 ns | 0.4094 ns | 0.3629 ns |      0.1322 |               208 B |
    ///                        Span |    44.06 ns | 0.1720 ns | 0.1609 ns |      0.0508 |                80 B |
    ///    UnsafeCopyBlockUnaligned |    21.48 ns | 0.0829 ns | 0.0776 ns |      0.0508 |                80 B |
    /// </remarks>
    [Config(typeof(BenchmarkConfig))]
    public class ConcatUriStringBenchmark
    {
        const string AndSign = "&";
        const string EqualsSign = "=";

        const char CharAndSign = '&';
        const char CharEqualsSign = '=';

        string _uri;

        string _key1;
        string _key2;

        string _value1;
        string _value2;

        [GlobalSetup]
        public void Setup()
        {
            _uri = "uri?";

            _key1 = "key1";
            _key2 = "key2";

            _value1 = "value1";
            _value2 = "value2";
        }

        [Benchmark]
        public string HttpUtilityParseQueryString()
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query[_key1] = _value1;
            query[_key2] = _value2;

            return _uri + query;
        }

        [Benchmark]
        public string StringConcat1()
            => _uri +
               _key1 + EqualsSign + _value1 + AndSign +
               _key2 + EqualsSign + _value2;

        [Benchmark]
        public string StringConcat2()
        {
            var s1 = _uri + _key1 + EqualsSign + _value1;
            var s2 = AndSign + _key2 + EqualsSign + _value2;
            return s1 + s2;
        }

        [Benchmark]
        public string StringBuilder()
        {
            var length = _uri.Length +
                         _key1.Length + _value1.Length +
                         _key2.Length + _value2.Length +
                         3;

            var sb = new StringBuilder(length);
            sb.Append(_uri);
            sb.Append(_key1);
            sb.Append(EqualsSign);
            sb.Append(_value1);
            sb.Append(AndSign);
            sb.Append(_key2);
            sb.Append(EqualsSign);
            sb.Append(_value2);

            return sb.ToString();
        }

        [Benchmark]
        public string Span()
        {
            var length = _uri.Length +
                         _key1.Length + _value1.Length +
                         _key2.Length + _value2.Length +
                         3;

            var result = new string(default, length);
            var span = MemoryMarshal.CreateSpan(ref MemoryMarshal.GetReference(result.AsSpan()), length);

            _uri.AsSpan().CopyTo(span);
            var pos = _uri.Length;
            _key1.AsSpan().CopyTo(span.Slice(pos));
            pos += _key1.Length;
            span[pos++] = CharEqualsSign;
            _value1.AsSpan().CopyTo(span.Slice(pos));
            pos += _value1.Length;
            span[pos++] = CharAndSign;
            _key2.AsSpan().CopyTo(span.Slice(pos));
            pos += _key2.Length;
            span[pos++] = CharEqualsSign;
            _value2.AsSpan().CopyTo(span.Slice(pos));

            return result;
        }

        [Benchmark]
        public string UnsafeCopyBlockUnaligned()
        {
            var length = _uri.Length +
                         _key1.Length + _value1.Length +
                         _key2.Length + _value2.Length +
                         3;

            var result = new string(default, length);
            ref var resultStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(result.AsSpan()));

            var pos = _uri.Length * sizeof(char);

            ref var sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_uri.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref resultStart, ref sourceStart, (uint)pos);

            var size = _key1.Length * sizeof(char);
            sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_key1.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref Unsafe.Add(ref resultStart, pos), ref sourceStart, (uint)size);
            pos += size;

            Unsafe.Add(ref resultStart, pos) = (byte)CharEqualsSign;
            pos += sizeof(char);

            size = _value1.Length * sizeof(char);
            sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_value1.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref Unsafe.Add(ref resultStart, pos), ref sourceStart, (uint)size);
            pos += size;

            Unsafe.Add(ref resultStart, pos) = (byte)CharAndSign;
            pos += sizeof(char);

            size = _key2.Length * sizeof(char);
            sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_key2.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref Unsafe.Add(ref resultStart, pos), ref sourceStart, (uint)size);
            pos += size;

            Unsafe.Add(ref resultStart, pos) = (byte)CharEqualsSign;
            pos += sizeof(char);

            size = _value2.Length * sizeof(char);
            sourceStart = ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(_value2.AsSpan()));
            Unsafe.CopyBlockUnaligned(ref Unsafe.Add(ref resultStart, pos), ref sourceStart, (uint)size);

            return result;
        }
    }
}