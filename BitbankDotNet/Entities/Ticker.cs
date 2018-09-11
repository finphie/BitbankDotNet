using BitbankDotNet.Resolvers;
using SpanJson;
using System;

namespace BitbankDotNet.Entities
{
    /// <summary>
    /// ティッカー情報
    /// </summary>
    public class Ticker
    {
        /// <summary>
        /// 現在の売り注文の最安値
        /// </summary>
        public double Sell { get; set; }

        /// <summary>
        /// 現在の買い注文の最高値
        /// </summary>
        public double Buy { get; set; }

        /// <summary>
        /// 過去24時間の最高値取引価格
        /// </summary>
        public double High { get; set; }

        /// <summary>
        /// 過去24時間の最安値取引価格
        /// </summary>
        public double Low { get; set; }

        /// <summary>
        /// 最新取引価格
        /// </summary>
        public double Last { get; set; }

        /// <summary>
        /// 過去24時間の出来高
        /// </summary>
        public double Vol { get; set; }

        /// <summary>
        /// 日時
        /// </summary>
        public DateTime Timestamp { get; set; }

        public override string ToString()
            => JsonSerializer.Generic.Utf16.Serialize<Ticker, BitbankResolver<char>>(this);
    }

    class TickerResponse : Response<Ticker>
    {
    }
}