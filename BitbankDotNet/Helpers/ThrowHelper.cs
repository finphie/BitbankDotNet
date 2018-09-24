﻿using SpanJson;
using System;
using System.Net.Http;
using System.Runtime.CompilerServices;

namespace BitbankDotNet.Helpers
{
    static class ThrowHelper
    {
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void ThrowBitbankApiException(HttpResponseMessage response, int errorCode)
            => throw new BitbankApiException("APIでエラーが発生しました。", response, errorCode);

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void ThrowBitbankJsonDeserializeException(Exception inner, HttpResponseMessage response)
            => throw new BitbankApiException("JSONデシリアライズでエラーが発生しました。", inner, response);

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void ThrowBitbankRequestTimeoutException(Exception inner)
            => throw new BitbankApiException("リクエストがタイムアウトしました。", inner);

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void ThrowJsonParserException(JsonParserException.ParserError error, int position)
            => throw new JsonParserException(error, position);
    }
}