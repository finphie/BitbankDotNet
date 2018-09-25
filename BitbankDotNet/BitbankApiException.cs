using System;
using System.Net;

namespace BitbankDotNet
{
    /// <summary>
    /// <see cref="BitbankClient"/>例外クラス
    /// </summary>
    public class BitbankApiException : Exception
    {
        /// <summary>
        /// HTTPステータス
        /// </summary>
        public HttpStatusCode StatusCode { get; }

        /// <summary>
        /// BitbankAPIのエラーコード
        /// </summary>
        public int ApiErrorCode { get; }

        public BitbankApiException(string message)
            : base(message)
        {
        }

        public BitbankApiException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public BitbankApiException(string message, Exception inner, HttpStatusCode statusCode)
            : base(message, inner)
            => StatusCode = statusCode;

        public BitbankApiException(string message, HttpStatusCode statusCode, int apiErrorCode)
            : this(message, null, statusCode)
            => ApiErrorCode = apiErrorCode;
    }
}