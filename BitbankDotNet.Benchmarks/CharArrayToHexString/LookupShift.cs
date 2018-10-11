//#define IsPartial

#if IsAll || IsBest || IsPartial
using BenchmarkDotNet.Attributes;
#endif
using System;
using System.Text;

namespace BitbankDotNet.Benchmarks.CharArrayToHexString
{
    /*
     * byte配列を16進数stringに変換
     * - ルックアップテーブル（string）とシフト演算を利用
     * - メソッド名は、LookupShift_Table利用方法_Buffer種別_Buffer利用方法
     * ベンチマーク結果
     * - ルックアップテーブルを直接またはSpan経由で利用するより、ポインターの方が速い。
     */
    public partial class CharArrayToHexStringBenchmark
    {
#if IsAll || IsPartial
        [Benchmark]
#endif
        [Obsolete]
        public string LookupShift_Direct_StringBuilder_Append()
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
        public string LookupShift_Span_StringBuilder_Append()
        {
            const string hexString = "0123456789abcdef";
            var hexSpan = hexString.AsSpan();
            var sb = new StringBuilder(SourceBytes.Length * 2);
            foreach (var sourceByte in SourceBytes)
            {
                sb.Append(hexSpan[sourceByte >> 0b0100]);
                sb.Append(hexSpan[sourceByte & 0b1111]);
            }
            return sb.ToString();
        }

#if IsAll || IsPartial
        [Benchmark]
#endif
        [Obsolete]
        public unsafe string LookupShift_Pointer_StringBuilder_Append()
        {
            const string hexString = "0123456789abcdef";
            var sb = new StringBuilder(SourceBytes.Length * 2);
            fixed (char* hexPtr = hexString)
                foreach (var sourceByte in SourceBytes)
                {
                    sb.Append(hexPtr[sourceByte >> 0b0100]);
                    sb.Append(hexPtr[sourceByte & 0b1111]);
                }
            return sb.ToString();
        }

#if IsAll || IsPartial
        [Benchmark]
#endif
        [Obsolete]
        public string LookupShift_Direct_NewCharArray_Direct()
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
        public unsafe string LookupShift_Direct_NewCharArray_Pointer()
        {
            const string hexString = "0123456789abcdef";
            var buffer = new char[SourceBytes.Length * 2];
            fixed (char* bufferPtr = buffer)
            {
                var ptr = bufferPtr;
                foreach (var sourceByte in SourceBytes)
                {
                    *ptr++ = hexString[sourceByte >> 0b0100];
                    *ptr++ = hexString[sourceByte & 0b1111];
                }
            }
            return new string(buffer);
        }

#if IsAll || IsPartial
        [Benchmark]
#endif
        [Obsolete]
        public string LookupShift_Span_NewCharArray_Direct()
        {
            const string hexString = "0123456789abcdef";
            var hexSpan = hexString.AsSpan();
            var buffer = new char[SourceBytes.Length * 2];
            for (var i = 0; i < SourceBytes.Length; i++)
            {
                buffer[i * 2] = hexSpan[SourceBytes[i] >> 0b0100];
                buffer[i * 2 + 1] = hexSpan[SourceBytes[i] & 0b1111];
            }
            return new string(buffer);
        }

#if IsAll || IsPartial
        [Benchmark]
#endif
        [Obsolete]
        public unsafe string LookupShift_Span_NewCharArray_Pointer()
        {
            const string hexString = "0123456789abcdef";
            var hexSpan = hexString.AsSpan();
            var buffer = new char[SourceBytes.Length * 2];
            fixed (char* bufferPtr = buffer)
            {
                var ptr = bufferPtr;
                foreach (var sourceByte in SourceBytes)
                {
                    *ptr++ = hexSpan[sourceByte >> 0b0100];
                    *ptr++ = hexSpan[sourceByte & 0b1111];
                }
            }
            return new string(buffer);
        }

#if IsAll || IsPartial
        [Benchmark]
#endif
        [Obsolete]
        public unsafe string LookupShift_Pointer_NewCharArray_Direct()
        {
            const string hexString = "0123456789abcdef";
            var buffer = new char[SourceBytes.Length * 2];
            fixed (char* hexPtr = hexString)
            {
                for (var i = 0; i < SourceBytes.Length; i++)
                {
                    buffer[i * 2] = hexPtr[SourceBytes[i] >> 0b0100];
                    buffer[i * 2 + 1] = hexPtr[SourceBytes[i] & 0b1111];
                }
            }
            return new string(buffer);
        }

#if IsAll || IsPartial
        [Benchmark]
#endif
        [Obsolete]
        public unsafe string LookupShift_Pointer_NewCharArray_Pointer()
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
        public string LookupShift_Direct_StackallocCharArray_Direct()
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
        public unsafe string LookupShift_Direct_StackallocCharArray_Pointer()
        {
            const string hexString = "0123456789abcdef";
            Span<char> buffer = stackalloc char[SourceBytes.Length * 2];
            fixed (char* bufferPtr = buffer)
            {
                var ptr = bufferPtr;
                foreach (var sourceByte in SourceBytes)
                {
                    *ptr++ = hexString[sourceByte >> 0b0100];
                    *ptr++ = hexString[sourceByte & 0b1111];
                }
            }
            return new string(buffer);
        }

#if IsAll || IsPartial
        [Benchmark]
#endif
        [Obsolete]
        public string LookupShift_Span_StackallocCharArray_Direct()
        {
            const string hexString = "0123456789abcdef";
            var hexSpan = hexString.AsSpan();
            Span<char> buffer = stackalloc char[SourceBytes.Length * 2];
            for (var i = 0; i < SourceBytes.Length; i++)
            {
                buffer[i * 2] = hexSpan[SourceBytes[i] >> 0b0100];
                buffer[i * 2 + 1] = hexSpan[SourceBytes[i] & 0b1111];
            }
            return new string(buffer);
        }

#if IsAll || IsPartial
        [Benchmark]
#endif
        [Obsolete]
        public unsafe string LookupShift_Span_StackallocCharArray_Pointer()
        {
            const string hexString = "0123456789abcdef";
            var hexSpan = hexString.AsSpan();
            Span<char> buffer = stackalloc char[SourceBytes.Length * 2];
            fixed (char* bufferPtr = buffer)
            {
                var ptr = bufferPtr;
                foreach (var sourceByte in SourceBytes)
                {
                    *ptr++ = hexSpan[sourceByte >> 0b0100];
                    *ptr++ = hexSpan[sourceByte & 0b1111];
                }
            }
            return new string(buffer);
        }

#if IsAll || IsPartial
        [Benchmark]
#endif
        [Obsolete]
        public unsafe string LookupShift_Pointer_StackallocCharArray_Direct()
        {
            const string hexString = "0123456789abcdef";
            Span<char> buffer = stackalloc char[SourceBytes.Length * 2];
            fixed (char* hexPtr = hexString)
            {
                for (var i = 0; i < SourceBytes.Length; i++)
                {
                    buffer[i * 2] = hexPtr[SourceBytes[i] >> 0b0100];
                    buffer[i * 2 + 1] = hexPtr[SourceBytes[i] & 0b1111];
                }
            }
            return new string(buffer);
        }

#if IsAll || IsPartial
        [Benchmark]
#endif
        [Obsolete]
        public unsafe string LookupShift_Pointer_StackallocCharArray_Pointer()
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
        public unsafe string LookupShift_Direct_String_Pointer()
        {
            const string hexString = "0123456789abcdef";
            var buffer = new string(default, SourceBytes.Length * 2);
            fixed (char* bufferPtr = buffer)
            {
                var ptr = bufferPtr;
                foreach (var sourceByte in SourceBytes)
                {
                    *ptr++ = hexString[sourceByte >> 0b0100];
                    *ptr++ = hexString[sourceByte & 0b1111];
                }
            }
            return buffer;
        }

#if IsAll || IsPartial
        [Benchmark]
#endif
        [Obsolete]
        public unsafe string LookupShift_Span_String_Pointer()
        {
            const string hexString = "0123456789abcdef";
            var hexSpan = hexString.AsSpan();
            var buffer = new string(default, SourceBytes.Length * 2);
            fixed (char* bufferPtr = buffer)
            {
                var ptr = bufferPtr;
                foreach (var sourceByte in SourceBytes)
                {
                    *ptr++ = hexSpan[sourceByte >> 0b0100];
                    *ptr++ = hexSpan[sourceByte & 0b1111];
                }
            }
            return buffer;
        }

#if IsAll || IsBest || IsPartial
        [Benchmark]
#endif
        public unsafe string LookupShift_Pointer_String_Pointer()
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