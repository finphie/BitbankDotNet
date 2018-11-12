using SpanJson;
using System;
using System.Net;
using System.Runtime.CompilerServices;

namespace BitbankDotNet.Helpers
{
    static class ThrowHelper
    {
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void ThrowBitbankApiException(HttpStatusCode statusCode, int errorCode)
            => throw new BitbankApiException(statusCode, errorCode);

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void ThrowBitbankJsonDeserializeException(Exception inner, HttpStatusCode statusCode)
            => throw new BitbankApiException("JSONデシリアライズでエラーが発生しました。", inner, statusCode);

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void ThrowBitbankRequestTimeoutException(Exception inner)
            => throw new BitbankApiException("リクエストがタイムアウトしました。", inner);

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void ThrowJsonParserException(JsonParserException.ParserError error, int position)
            => throw new JsonParserException(error, position);

        // ReSharper disable once IdentifierTypo
        public static Exception ThrowBigEndianNotSupported()
            => new PlatformNotSupportedException("ビックエンディアンプロセッサーは対応していません。");
    }
}