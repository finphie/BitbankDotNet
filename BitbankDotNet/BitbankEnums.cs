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
    /// ローソク足の期間
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
        /// 8時間
        /// </summary>
        [EnumMember(Value = "8hour")]
        EightHour,

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
        OneWeek,

        /// <summary>
        /// 1か月
        /// </summary>
        [EnumMember(Value = "1month")]
        OneMonth
    }

    /// <summary>
    /// 通貨ペア
    /// </summary>
    public enum CurrencyPair
    {
        /// <summary>
        /// BTC/JPY
        /// </summary>
        [EnumMember(Value = "btc_jpy")]
        BtcJpy,

        /// <summary>
        /// LTC/BTC
        /// </summary>
        [EnumMember(Value = "ltc_btc")]
        LtcBtc,

        /// <summary>
        /// XRP/JPY
        /// </summary>
        [EnumMember(Value = "xrp_jpy")]
        XrpJpy,

        /// <summary>
        /// ETH/BTC
        /// </summary>
        [EnumMember(Value = "eth_btc")]
        EthBtc,

        /// <summary>
        /// MONA/JPY
        /// </summary>
        [EnumMember(Value = "mona_jpy")]
        MonaJpy,

        /// <summary>
        /// MONA/BTC
        /// </summary>
        [EnumMember(Value = "mona_btc")]
        MonaBtc,

        /// <summary>
        /// BCC/JPY
        /// </summary>
        [EnumMember(Value = "bcc_jpy")]
        BccJpy,

        /// <summary>
        /// BCC/BTC
        /// </summary>
        [EnumMember(Value = "bcc_btc")]
        BccBtc
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

    /// <summary>
    /// ソート順序
    /// </summary>
    public enum SortOrder
    {
        /// <summary>
        /// 昇順
        /// </summary>
        [EnumMember(Value = "asc")]
        Asc,

        /// <summary>
        /// 降順
        /// </summary>
        [EnumMember(Value = "desc")]
        Desc
    }

    /// <summary>
    /// 取引所ステータス
    /// </summary>
    public enum SystemStatus
    {
        /// <summary>
        /// 通常
        /// </summary>
        [EnumMember(Value = "NORMAL")]
        Normal,

        /// <summary>
        /// 負荷状態
        /// </summary>
        [EnumMember(Value = "BUSY")]
        Busy,

        /// <summary>
        /// 高負荷状態
        /// </summary>
        [EnumMember(Value = "VERY_BUSY")]
        VeryBusy
    }

    /// <summary>
    /// 出金ステータス
    /// </summary>
    public enum WithdrawalStatus
    {
        /// <summary>
        /// メール認証待ち
        /// </summary>
        [EnumMember(Value = "CONFIRMING")]
        Confirming,

        /// <summary>
        /// 審査中
        /// </summary>
        [EnumMember(Value = "EXAMINING")]
        Examining,

        /// <summary>
        /// 送金待ち
        /// </summary>
        [EnumMember(Value = "SENDING")]
        Sending,

        /// <summary>
        /// 送金完了
        /// </summary>
        [EnumMember(Value = "DONE")]
        Done,

        /// <summary>
        /// 否認
        /// </summary>
        [EnumMember(Value = "REJECTED")]
        Rejected,

        /// <summary>
        /// キャンセル済み
        /// </summary>
        [EnumMember(Value = "CANCELED")]
        Canceled,

        /// <summary>
        /// メール認証タイムアウト
        /// </summary>
        [EnumMember(Value = "CONFIRM_TIMEOUT")]
        ConfirmTimeout
    }
}