using System;
using System.Net.Http;
using System.Threading.Tasks;
using BitbankDotNet;
using BitbankDotNet.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Sample
{
    /// <summary>
    /// サンプルサービス
    /// </summary>
    public sealed class SampleService : ISampleService, IDisposable
    {
        static readonly HttpClient Client = new HttpClient()
        {
            Timeout = TimeSpan.FromSeconds(10)
        };

        /// <summary>
        /// <see cref="BitbankRestApiClient"/>クラスのインスタンス
        /// </summary>
        readonly BitbankRestApiClient _restApi;

        /// <summary>
        /// 設定
        /// </summary>
        readonly Config _config;

        /// <summary>
        /// logger
        /// </summary>
        readonly ILogger<SampleService> _logger;

        /// <summary>
        /// <see cref="SampleService"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="config">設定</param>
        /// <param name="logger">logger</param>
        public SampleService(IOptions<Config> config, ILogger<SampleService> logger)
        {
            _config = config.Value;
            _logger = logger;

            _restApi = new BitbankRestApiClient(Client, _config.Key, _config.Secret);
        }

        /// <inheritdoc/>
        public void Dispose()
            => _restApi.Dispose();

        /// <inheritdoc/>
        public Task<Ticker> GetTickerAsync(CurrencyPair pair)
            => _restApi.GetTickerAsync(pair);

        /// <inheritdoc/>
        public Task<Depth> GetDepthAsync(CurrencyPair pair)
            => _restApi.GetDepthAsync(pair);

        /// <inheritdoc/>
        public Task<Transaction[]> GetTransactionsAsync(CurrencyPair pair)
            => _restApi.GetTransactionsAsync(pair);

        /// <inheritdoc/>
        public Task<Ohlcv[]> GetCandlesticksAsync(CurrencyPair pair, CandleType type, DateTimeOffset date)
            => _restApi.GetCandlesticksAsync(pair, type, date);

        /// <inheritdoc/>
        public Task<Asset[]> GetAssetsAsync()
            => _restApi.GetAssetsAsync();

        /// <inheritdoc/>
        public Task<Order> SendBuyOrderAsync(CurrencyPair pair, decimal amount)
            => _restApi.SendBuyOrderAsync(pair, amount);

        /// <inheritdoc/>
        public Task<Trade[]> GetTradeHistoryAsync(CurrencyPair pair)
            => _restApi.GetTradeHistoryAsync(pair);

        /// <inheritdoc/>
        public Task<WithdrawalAccount[]> GetWithdrawalAccountsAsync(AssetName asset)
            => _restApi.GetWithdrawalAccountsAsync(asset);

        /// <inheritdoc/>
        public Task<HealthStatus[]> GetStatusesAsync()
            => _restApi.GetStatusesAsync();
    }
}