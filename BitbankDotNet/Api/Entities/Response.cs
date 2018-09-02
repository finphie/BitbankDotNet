namespace BitbankDotNet.Api.Entities
{
    class Response<T>
    {
        public int Success { get; set; }
        public T Data { get; set; }
    }
}