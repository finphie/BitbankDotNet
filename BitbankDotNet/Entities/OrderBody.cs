namespace BitbankDotNet.Entities
{
    /// <summary>
    /// 新規指値注文を行う際のリクエストボディ
    /// </summary>
    class LimitOrderBody
    {
        /// <summary>
        /// 通貨ペア
        /// </summary>
        public CurrencyPair Pair { get; set; }

        /// <summary>
        /// 価格
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public double Amount { get; set; }

        /// <summary>
        /// 注文の方向
        /// </summary>
        public OrderSide Side { get; set; }

        /// <summary>
        /// 注文の種類
        /// </summary>
        public OrderType Type { get; set; }
    }

    /// <summary>
    /// 新規成行注文を行う際のリクエストボディ
    /// </summary>
    class MarketOrderBody
    {
        /// <summary>
        /// 通貨ペア
        /// </summary>
        public CurrencyPair Pair { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public double Amount { get; set; }

        /// <summary>
        /// 注文の方向
        /// </summary>
        public OrderSide Side { get; set; }

        /// <summary>
        /// 注文の種類
        /// </summary>
        public OrderType Type { get; set; }
    }
}