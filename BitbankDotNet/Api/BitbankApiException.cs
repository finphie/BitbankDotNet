using System;

namespace BitbankDotNet.Api
{
    public class BitbankApiException : Exception
    {
        public BitbankApiException()
        {
        }

        public BitbankApiException(string message)
            : base(message)
        {
        }

        public BitbankApiException(string message, Exception inner)
            : base(message)
        {
        }
    }
}