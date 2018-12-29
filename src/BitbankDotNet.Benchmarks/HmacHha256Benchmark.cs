using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using BenchmarkDotNet.Attributes;
using BitbankDotNet.InternalShared.Helpers;

namespace BitbankDotNet.Benchmarks
{
    [Config(typeof(BenchmarkConfig))]
    public class HmacHha256Benchmark : IDisposable
    {
        // キーの長さは64文字固定
        const int KeyLength = 64;

        // 20文字固定（ulongの最大桁数）
        const int Source1Length = 20;

        readonly byte[] _hash;
        readonly HMACSHA256 _hmac;
        readonly IncrementalHash _incrementalHash;

        bool _disposed;

        byte[] _source1;
        byte[] _source2;

        [Params(32, 64, 128)]
        public int SourceLength { get; set; }

        public HmacHha256Benchmark()
        {
            var key = ByteArrayHelper.CreateUtf8Bytes(KeyLength);
            _hmac = new HMACSHA256(key);
            _hash = new byte[_hmac.HashSize / 8];
            _incrementalHash = IncrementalHash.CreateHMAC(HashAlgorithmName.SHA256, key);
        }

        ~HmacHha256Benchmark()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _hmac?.Dispose();
                    _incrementalHash?.Dispose();
                }

                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        [GlobalSetup]
        public void Setup()
        {
            _source1 = ByteArrayHelper.CreateUtf8Bytes(Source1Length);
            _source2 = ByteArrayHelper.CreateUtf8Bytes(SourceLength - Source1Length);
        }

        [GlobalCleanup]
        public void Cleanup() => Dispose();

        [Benchmark]
        public void HmacHha256TryComputeHash()
        {
            Span<byte> buffer = stackalloc byte[SourceLength];
            ref var bufferStart = ref MemoryMarshal.GetReference(buffer);

            Unsafe.CopyBlockUnaligned(ref bufferStart, ref _source1[0], (uint)_source1.Length);
            Unsafe.CopyBlockUnaligned(ref Unsafe.Add(ref bufferStart, _source1.Length), ref _source2[0], (uint)_source2.Length);

            _hmac.TryComputeHash(buffer, _hash, out _);
        }

        [Benchmark]
        public byte[] HmacHha256TransformBlock()
        {
            _hmac.TransformBlock(_source1, 0, _source1.Length, null, 0);
            _hmac.TransformFinalBlock(_source2, 0, _source2.Length);
            return _hmac.Hash;
        }

        [Benchmark]
        public void IncrementalHashTryGetHashAndReset()
        {
            _incrementalHash.AppendData(_source1);
            _incrementalHash.AppendData(_source2);
            _incrementalHash.TryGetHashAndReset(_hash, out _);
        }

        [Benchmark]
        public void IncrementalHashTryGetHashAndResetBuffer()
        {
            Span<byte> buffer = stackalloc byte[SourceLength];
            ref var bufferStart = ref MemoryMarshal.GetReference(buffer);

            Unsafe.CopyBlockUnaligned(ref bufferStart, ref _source1[0], (uint)_source1.Length);
            Unsafe.CopyBlockUnaligned(ref Unsafe.Add(ref bufferStart, _source1.Length), ref _source2[0], (uint)_source2.Length);

            _incrementalHash.AppendData(buffer);
            _incrementalHash.TryGetHashAndReset(_hash, out _);
        }
    }
}