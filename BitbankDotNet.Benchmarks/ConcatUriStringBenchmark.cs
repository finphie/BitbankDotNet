using BenchmarkDotNet.Attributes;
using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Web;

namespace BitbankDotNet.Benchmarks
{
    /// <summary>
    /// URI文字列連結処理のベンチマーク
    /// </summary>
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
            var sb = new StringBuilder(32);
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
    }
}