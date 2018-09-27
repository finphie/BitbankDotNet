namespace BitbankDotNet.Entities
{
    class Error : IEntity, IEntityResponse
    {
        public int Code { get; set; }
    }
}