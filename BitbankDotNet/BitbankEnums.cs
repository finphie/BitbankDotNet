using System.Runtime.Serialization;

namespace BitbankDotNet
{
    /// <summary>
    /// アセット名
    /// </summary>
    public enum AssetName
    {
        /// <summary>
        /// JPY
        /// </summary>
        [EnumMember(Value = "jpy")]
        Jpy,

        /// <summary>
        /// BTC
        /// </summary>
        [EnumMember(Value = "btc")]
        Btc,

        /// <summary>
        /// LTC
        /// </summary>
        [EnumMember(Value = "ltc")]
        Ltc,

        /// <summary>
        /// XRP
        /// </summary>
        [EnumMember(Value = "xrp")]
        Xrp,

        /// <summary>
        /// ETH
        /// </summary>
        [EnumMember(Value = "eth")]
        Eth,

        /// <summary>
        /// MONA
        /// </summary>
        [EnumMember(Value = "mona")]
        Mona,

        /// <summary>
        /// BCC
        /// </summary>
        [EnumMember(Value = "bcc")]
        Bcc
    }

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
    /// 注文の方向
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

    /// <summary>
    /// 注文の種類
    /// </summary>
    public enum OrderType
    {
        /// <summary>
        /// 指値
        /// </summary>
        [EnumMember(Value = "limit")]
        Limit,

        /// <summary>
        /// 成行
        /// </summary>
        [EnumMember(Value = "market")]
        Market
    }

    /// <summary>
    /// 注文のステータス
    /// </summary>
    public enum OrderStatus
    {
        /// <summary>
        /// 注文中
        /// </summary>
        [EnumMember(Value = "UNFILLED")]
        Unfilled,

        /// <summary>
        /// 注文中（一部約定）
        /// </summary>
        [EnumMember(Value = "PARTIALLY_FILLED")]
        PartiallyFilled,

        /// <summary>
        /// 約定済み
        /// </summary>
        [EnumMember(Value = "FULLY_FILLED")]
        FullyFilled,

        /// <summary>
        /// 取消済
        /// </summary>
        [EnumMember(Value = "CANCELED_UNFILLED")]
        CanceledUnfilled,

        /// <summary>
        /// 取消済（一部約定）
        /// </summary>
        [EnumMember(Value = "CANCELED_PARTIALLY_FILLED")]
        CanceledPartiallyFilled
    }
}