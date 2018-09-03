namespace BitbankDotNet.Entities
{
    class Error
    {
        public int Code { get; set; }
    }

    class ErrorResponse : Response<Error>
    {       
    }
}