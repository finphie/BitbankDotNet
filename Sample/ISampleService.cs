using System;
using System.Threading.Tasks;
using BitbankDotNet;
using BitbankDotNet.Entities;

namespace Sample
{
    /// <summary>
    /// サンプルサービスのインターフェース
    /// </summary>
    public interface ISampleService
    {
        /// <summary>
        /// [Public API]ティッカー情報を返します。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <returns>ティッカー情報</returns>
        Task<Ticker> GetTickerAsync(CurrencyPair pair);

        /// <summary>
        /// [Public API]板情報を返します。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <returns>板情報</returns>
        Task<Depth> GetDepthAsync(CurrencyPair pair);

        /// <summary>
        /// [Public API]最新の約定履歴を返します。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <returns>約定履歴</returns>
        Task<Transaction[]> GetTransactionsAsync(CurrencyPair pair);

        /// <summary>
        /// [Public API]指定された日付のローソク足データを返します。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <param name="type">ローソク足の期間</param>
        /// <param name="date">日付</param>
        /// <returns>ローソク足データ</returns>
        Task<Ohlcv[]> GetCandlesticksAsync(CurrencyPair pair, CandleType type, DateTimeOffset date);

        /// <summary>
        /// [Private API]アセット一覧を返します。
        /// </summary>
        /// <returns>アセット一覧</returns>
        Task<Asset[]> GetAssetsAsync();

        /// <summary>
        /// [Private API]新規指値買い注文を行います。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <param name="amount">数量</param>
        /// <returns>注文情報</returns>
        Task<Order> SendBuyOrderAsync(CurrencyPair pair, decimal amount);

        /// <summary>
        /// [Private API]約定履歴を取得します。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <returns>約定履歴</returns>
        Task<Trade[]> GetTradeHistoryAsync(CurrencyPair pair);

        /// <summary>
        /// [Private API]出金アカウントを取得します。
        /// </summary>
        /// <param name="asset">通貨名</param>
        /// <returns>出金アカウント情報</returns>
        Task<WithdrawalAccount[]> GetWithdrawalAccountsAsync(AssetName asset);

        /// <summary>
        /// [Private API]取引所ステータスを返します。
        /// </summary>
        /// <returns>取引所ステータス</returns>
        Task<HealthStatus[]> GetStatusesAsync();
    }
}