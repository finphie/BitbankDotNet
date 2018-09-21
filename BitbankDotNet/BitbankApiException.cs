using System;
using System.Net.Http;

namespace BitbankDotNet
{
    /// <summary>
    /// <see cref="BitbankClient"/>例外クラス
    /// </summary>
    public class BitbankApiException : Exception
    {
        /// <summary>
        /// HTTPメッセージ
        /// </summary>
        public HttpResponseMessage Response { get; }

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

        public BitbankApiException(string message, Exception inner, HttpResponseMessage response)
            : base(message, inner)
            => Response = response;

        public BitbankApiException(string message, HttpResponseMessage response, int apiErrorCode)
            : this(message, null, response)
            => ApiErrorCode = apiErrorCode;
    }
}