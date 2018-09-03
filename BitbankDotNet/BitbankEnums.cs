using System.Runtime.Serialization;

namespace BitbankDotNet
{
    public enum CandleType
    {
        [EnumMember(Value = "1min")]
        OneMin,

        [EnumMember(Value = "5min")]
        FiveMin,

        [EnumMember(Value = "15min")]
        FifteenMin,

        [EnumMember(Value = "30min")]
        ThirtyMin,

        [EnumMember(Value = "1hour")]
        OneHour,

        [EnumMember(Value = "4hour")]
        FourHour,

        [EnumMember(Value = "12hour")]
        TwelveHour,

        [EnumMember(Value = "1day")]
        OneDay,

        [EnumMember(Value = "1week")]
        OneWeek
    }

    public enum OrderSide
    {
        [EnumMember(Value = "buy")]
        Buy,

        [EnumMember(Value = "sell")]
        Sell
    }
}