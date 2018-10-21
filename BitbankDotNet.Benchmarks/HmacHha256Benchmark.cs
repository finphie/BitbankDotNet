using BenchmarkDotNet.Attributes;
using System;
using System.Security.Cryptography;
using System.Text;

namespace BitbankDotNet.Benchmarks
{
    [Config(typeof(BenchmarkConfig))]
    public class HmacHha256Benchmark
    {
        byte[] _key;
        byte[] _message;

        [Params(32, 64, 128)]
        public int MessageLength;

        [GlobalSetup]
        public void Setup()
        {
            // UTF-8 byte配列作成
            byte[] CreateUtf8Bytes(int length)
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

            // キーは64文字固定
            _key = CreateUtf8Bytes(64);

            _message = CreateUtf8Bytes(MessageLength);
        }

        [Benchmark]
        public byte[] HmacHha256ComputeHash()
        {
            using (var hmac = new HMACSHA256(_key))
                return hmac.ComputeHash(_message);
        }

        [Benchmark]
        public byte[] IncrementalHashCreateHmac()
        {            
            using (var incrementalHash = IncrementalHash.CreateHMAC(HashAlgorithmName.SHA256, _key))
            {
                incrementalHash.AppendData(_message);
                return incrementalHash.GetHashAndReset();
            }            
        }
    }
}