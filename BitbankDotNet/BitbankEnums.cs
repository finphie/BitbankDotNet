using System.Runtime.Serialization;

namespace BitbankDotNet
{
    /// <summary>
    /// ロウソク足の期間
    /// </summary>
    public enum CandleType
    {
        /// <summary>
        /// 1分
        /// </summary>
        [EnumMember(Value = "1min")]
        OneMin,

        /// <summary>
        /// 5分
        /// </summary>
        [EnumMember(Value = "5min")]
        FiveMin,

        /// <summary>
        /// 15分
        /// </summary>
        [EnumMember(Value = "15min")]
        FifteenMin,

        /// <summary>
        /// 30分
        /// </summary>
        [EnumMember(Value = "30min")]
        ThirtyMin,

        /// <summary>
        /// 1時間
        /// </summary>
        [EnumMember(Value = "1hour")]
        OneHour,

        /// <summary>
        /// 4時間
        /// </summary>
        [EnumMember(Value = "4hour")]
        FourHour,

        /// <summary>
        /// 12時間
        /// </summary>
        [EnumMember(Value = "12hour")]
        TwelveHour,

        /// <summary>
        /// 1日
        /// </summary>
        [EnumMember(Value = "1day")]
        OneDay,

        /// <summary>
        /// 1週間
        /// </summary>
        [EnumMember(Value = "1week")]
        OneWeek
    }

    /// <summary>
    /// Orderの種類
    /// </summary>
    public enum OrderSide
    {
        /// <summary>
        /// 買い
        /// </summary>
        [EnumMember(Value = "buy")]
        Buy,

        /// <summary>
        /// 売り
        /// </summary>
        [EnumMember(Value = "sell")]
        Sell
    }
}