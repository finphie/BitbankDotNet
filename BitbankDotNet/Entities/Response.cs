using BitbankDotNet.Resolvers;
using SpanJson;

namespace BitbankDotNet.Entities
{
    interface IResponse
    {
        int Success { get; set; }
    }

    class Response<T> : IResponse
        where T : class
    {
        public int Success { get; set; }
        public T Data { get; set; }

        public override string ToString()
            => JsonSerializer.Generic.Utf16.Serialize<Response<T>, BitbankResolver<char>>(this);
    }
}