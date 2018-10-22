using BenchmarkDotNet.Attributes;
using System;
using System.Security.Cryptography;
using System.Text;

namespace BitbankDotNet.Benchmarks
{
    [Config(typeof(BenchmarkConfig))]
    public class HmacHha256Benchmark
    {
        // キーの長さは64文字固定
        const int KeyLength = 64;

        readonly HMACSHA256 _hmac;
        readonly IncrementalHash _incrementalHash;

        byte[] _message;

        [Params(32, 64, 128)]
        public int MessageLength { get; set; }

        public HmacHha256Benchmark()
        {
            var key = CreateUtf8Bytes(KeyLength);
            _hmac = new HMACSHA256(key);
            _incrementalHash = IncrementalHash.CreateHMAC(HashAlgorithmName.SHA256, key);
        }

        [GlobalSetup]
        public void Setup() => _message = CreateUtf8Bytes(MessageLength);

        [GlobalCleanup]
        public void Cleanup()
        {
            _hmac?.Dispose();
            _incrementalHash?.Dispose();
        }

        [Benchmark]
        public byte[] HmacHha256ComputeHash() => _hmac.ComputeHash(_message);

        [Benchmark]
        public byte[] IncrementalHashCreateHmac()
        {
            _incrementalHash.AppendData(_message);
            return _incrementalHash.GetHashAndReset();
        }

        // UTF-8 byte配列作成
        static byte[] CreateUtf8Bytes(int length)
        {
            // UTF-16文字列作成
            string CreateUtf16String() => Guid.NewGuid().ToString("N");

            var sb = new StringBuilder(64);
            sb.Append(CreateUtf16String());

            var count = length / sb.Length;
            for (var i = 0; i < count; i++)
                sb.Append(CreateUtf16String());

            return Encoding.UTF8.GetBytes(sb.ToString().Substring(0, length));
        }
    }
}