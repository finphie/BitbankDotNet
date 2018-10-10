//#define IsPartial

#if IsAll || IsBest || IsPartial
using BenchmarkDotNet.Attributes;
#endif
using System;
using System.Text;

namespace BitbankDotNet.Benchmarks.CharArrayToHexString
{
    public partial class CharArrayToHexStringBenchmark
    {
#if IsAll || IsPartial
        [Benchmark]
#endif
        [Obsolete]
        public string LookupShiftStringBuilder()
        {
            const string hexString = "0123456789abcdef";
            var sb = new StringBuilder(SourceBytes.Length * 2);
            foreach (var sourceByte in SourceBytes)
            {
                sb.Append(hexString[sourceByte >> 0b0100]);
                sb.Append(hexString[sourceByte & 0b1111]);
            }
            return sb.ToString();
        }

#if IsAll || IsPartial
        [Benchmark]
#endif
        [Obsolete]
        public string LookupShiftBufferNew()
        {
            const string hexString = "0123456789abcdef";
            var buffer = new char[SourceBytes.Length * 2];
            for (var i = 0; i < SourceBytes.Length; i++)
            {
                buffer[i * 2] = hexString[SourceBytes[i] >> 0b0100];
                buffer[i * 2 + 1] = hexString[SourceBytes[i] & 0b1111];
            }
            return new string(buffer);
        }

#if IsAll || IsPartial
        [Benchmark]
#endif
        [Obsolete]
        public unsafe string LookupShiftBufferNewUnsafe()
        {
            const string hexString = "0123456789abcdef";
            var buffer = new char[SourceBytes.Length * 2];
            fixed (char* hexPtr = hexString)
            fixed (char* bufferPtr = buffer)
            {
                var ptr = bufferPtr;
                foreach (var sourceByte in SourceBytes)
                {
                    *ptr++ = hexPtr[sourceByte >> 0b0100];
                    *ptr++ = hexPtr[sourceByte & 0b1111];
                }
            }
            return new string(buffer);
        }

#if IsAll || IsPartial
        [Benchmark]
#endif
        [Obsolete]
        public string LookupShiftBufferStackalloc()
        {
            const string hexString = "0123456789abcdef";
            Span<char> buffer = stackalloc char[SourceBytes.Length * 2];
            for (var i = 0; i < SourceBytes.Length; i++)
            {
                buffer[i * 2] = hexString[SourceBytes[i] >> 0b0100];
                buffer[i * 2 + 1] = hexString[SourceBytes[i] & 0b1111];
            }
            return new string(buffer);
        }

#if IsAll || IsPartial
        [Benchmark]
#endif
        [Obsolete]
        public unsafe string LookupShiftBufferStackallocUnsafe()
        {
            const string hexString = "0123456789abcdef";
            Span<char> buffer = stackalloc char[SourceBytes.Length * 2];
            fixed (char* hexPtr = hexString)
            fixed (char* bufferPtr = buffer)
            {
                var ptr = bufferPtr;
                foreach (var sourceByte in SourceBytes)
                {
                    *ptr++ = hexPtr[sourceByte >> 0b0100];
                    *ptr++ = hexPtr[sourceByte & 0b1111];
                }
            }
            return new string(buffer);
        }

#if IsAll || IsPartial
        [Benchmark]
#endif
        [Obsolete]
        public unsafe string LookupShiftStringDirect()
        {
            const string hexString = "0123456789abcdef";
            var buffer = new string(default, SourceBytes.Length * 2);
            fixed (char* bufferPtr = buffer)
            {
                for (var i = 0; i < SourceBytes.Length; i++)
                {
                    bufferPtr[i * 2] = hexString[SourceBytes[i] >> 0b0100];
                    bufferPtr[i * 2 + 1] = hexString[SourceBytes[i] & 0b1111];
                }
            }
            return buffer;
        }

#if IsAll || IsBest || IsPartial
        [Benchmark]
#endif
        public unsafe string LookupShiftStringDirectUnsafe()
        {
            const string hexString = "0123456789abcdef";
            var buffer = new string(default, SourceBytes.Length * 2);
            fixed (char* hexPtr = hexString)
            fixed (char* bufferPtr = buffer)
            {
                var ptr = bufferPtr;
                foreach (var sourceByte in SourceBytes)
                {
                    *ptr++ = hexPtr[sourceByte >> 0b0100];
                    *ptr++ = hexPtr[sourceByte & 0b1111];
                }
            }
            return buffer;
        }
    }
}