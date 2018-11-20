using SpanJson;
using System;
using System.Runtime.CompilerServices;

namespace BitbankDotNet.Helpers
{
    static class ThrowHelper
    {
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void ThrowBitbankApiException(int errorCode)
            => throw new BitbankException(errorCode);

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void ThrowBitbankJsonDeserializeException(Exception inner)
            => throw new BitbankException("JSONデシリアライズでエラーが発生しました。", inner);

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void ThrowBitbankRequestTimeoutException(Exception inner)
            => throw new BitbankException("リクエストがタイムアウトしました。", inner);

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void ThrowJsonParserException(JsonParserException.ParserError error, int position)
            => throw new JsonParserException(error, position);

        // ReSharper disable once IdentifierTypo
        public static Exception ThrowBigEndianNotSupported()
            => new PlatformNotSupportedException("ビックエンディアンプロセッサーは対応していません。");
    }
}