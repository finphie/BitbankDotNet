using BitbankDotNet.Resolvers;
using SpanJson;

namespace BitbankDotNet.Entities
{
    class Response<T>
        where T : class, IEntity
    {
        public int Success { get; set; }
        public T Data { get; set; }

        public override string ToString()
            => JsonSerializer.Generic.Utf16.Serialize<Response<T>, BitbankResolver<char>>(this);
    }
}