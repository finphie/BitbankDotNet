using System.Runtime.Serialization;
using BitbankDotNet.Resolvers;
using SpanJson;

namespace BitbankDotNet.Entities
{
    /// <summary>
    /// 通貨ペア詳細一覧
    /// </summary>
    public class CurrencyPairSetting
    {
        /// <summary>
        /// 通貨ペア
        /// </summary>
        public CurrencyPair Name { get; set; }

        /// <summary>
        /// 原資産
        /// </summary>
        [DataMember(Name = "base_asset")]
        public AssetName BaseAsset { get; set; }

        /// <summary>
        /// クオート資産
        /// </summary>
        [DataMember(Name = "quote_asset")]
        public AssetName QuoteAsset { get; set; }

        /// <summary>
        /// メイカー手数料率（原資産）
        /// </summary>
        [DataMember(Name = "maker_fee_rate_base")]
        public decimal MakerFeeRateBase { get; set; }

        /// <summary>
        /// テイカー手数料率（原資産）
        /// </summary>
        [DataMember(Name = "taker_fee_rate_base")]
        public decimal TakerFeeRateBase { get; set; }

        /// <summary>
        /// メイカー手数料率（クオート資産）
        /// </summary>
        [DataMember(Name = "maker_fee_rate_quote")]
        public decimal MakerFeeRateQuote { get; set; }

        /// <summary>
        /// テイカー手数料率（クオート資産）
        /// </summary>
        [DataMember(Name = "taker_fee_rate_quote")]
        public decimal TakerFeeRateQuote { get; set; }

        /// <summary>
        /// 最小注文数量
        /// </summary>
        [DataMember(Name = "unit_amount")]
        public decimal UnitAmount { get; set; }

        /// <summary>
        /// 最大注文数量
        /// </summary>
        [DataMember(Name = "limit_max_amount")]
        public decimal LimitMaxAmount { get; set; }

        /// <summary>
        /// 成行注文時の最大数量
        /// </summary>
        [DataMember(Name = "market_max_amount")]
        public decimal MarketMaxAmount { get; set; }

        /// <summary>
        /// 成行買い注文時の余裕率
        /// </summary>
        [DataMember(Name = "market_allowance_rate")]
        public decimal MarketAllowanceRate { get; set; }

        /// <summary>
        /// 価格切り捨て対象桁数（0起点）
        /// </summary>
        [DataMember(Name = "price_digits")]
        public int PriceDigits { get; set; }

        /// <summary>
        /// 数量切り捨て対象桁数（0起点）
        /// </summary>
        [DataMember(Name = "amount_digits")]
        public int AmountDigits { get; set; }

        /// <summary>
        /// 買い注文停止ステータス
        /// </summary>
        [DataMember(Name = "is_stop_buy")]
        public bool IsStopBuy { get; set; }

        /// <summary>
        /// 売り注文停止ステータス
        /// </summary>
        [DataMember(Name = "is_stop_sell")]
        public bool IsStopSell { get; set; }

        /// <inheritdoc/>
        public override string ToString()
            => JsonSerializer.Generic.Utf16.Serialize<CurrencyPairSetting, BitbankResolver<char>>(this);
    }

    /// <summary>
    /// 通貨ペア詳細一覧のリスト
    /// </summary>
    class CurrencyPairSettingList
    {
        /// <summary>
        /// 通貨ペア詳細一覧のリスト
        /// </summary>
        public CurrencyPairSetting[] Pairs { get; set; }
    }
}