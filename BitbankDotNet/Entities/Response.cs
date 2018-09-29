﻿namespace BitbankDotNet.Entities
{
    class Response<T>
        where T : class, IEntityResponse
    {
        public int Success { get; set; }
        public T Data { get; set; }
    }
}