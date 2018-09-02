namespace BitbankDotNet.Api.Entities
{
    public class Error
    {
        public int Code { get; set; }
    }

    class ErrorResponse : Response<Error>
    {       
    }
}