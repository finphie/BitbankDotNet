using System.Runtime.Serialization;

namespace BitbankDotNet
{
    public enum OrderSide
    {
        [EnumMember(Value = "buy")]
        Buy,
        [EnumMember(Value = "sell")]
        Sell
    }
}