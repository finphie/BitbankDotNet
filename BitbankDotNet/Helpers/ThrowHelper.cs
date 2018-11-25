using SpanJson;
using System;
using System.Runtime.CompilerServices;

namespace BitbankDotNet.Helpers
{
    /// <summary>
    /// 例外処理関係のヘルパークラス
    /// </summary>
    static class ThrowHelper
    {
        /// <summary>
        /// 例外を送出します。（APIリクエストでエラー）
        /// </summary>
        /// <param name="apiErrorCode">APIのエラーコード</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void ThrowBitbankApiException(int apiErrorCode)
            => throw new BitbankDotNetException(apiErrorCode);

        /// <summary>
        /// 例外を送出します。（JSONデシリアライズでエラー）
        /// </summary>
        /// <param name="inner">内部のエラー</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void ThrowBitbankJsonDeserializeException(Exception inner)
            => throw new BitbankDotNetException("JSONデシリアライズでエラーが発生しました。", inner);

        /// <summary>
        /// 例外を送出します。（HTTPリクエストがタイムアウト）
        /// </summary>
        /// <param name="inner">内部のエラー</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void ThrowBitbankRequestTimeoutException(Exception inner)
            => throw new BitbankDotNetException("リクエストがタイムアウトしました。", inner);

        /// <summary>
        /// 例外を送出します。（<see cref="SpanJson"/>でのJSONシリアライズやデシリアライズでエラー）
        /// </summary>
        /// <param name="error">JSONの処理エラー</param>
        /// <param name="position">エラーが発生した位置</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void ThrowJsonParserException(JsonParserException.ParserError error, int position)
            => throw new JsonParserException(error, position);

        /// <summary>
        /// 例外を送出します。（ビッグエンディアンプロセッサー利用）
        /// </summary>
        /// <returns><see cref="PlatformNotSupportedException"/>クラスのインスタンス</returns>
        // ReSharper disable once IdentifierTypo
        public static Exception ThrowBigEndianNotSupported()
            => new PlatformNotSupportedException("ビックエンディアンプロセッサーは対応していません。");
    }
}