using BitbankDotNet.Entities;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
namespace BitbankDotNet
{
    partial class BitbankRestApiClient
    {
        const string StatusPath = "spot/status";

        /// <summary>
        /// [Public API]取引所ステータスを返します。
        /// </summary>
        /// <returns>取引所ステータス</returns>
        /// <exception cref="BitbankDotNetException">APIリクエストでエラーが発生しました。</exception>
        public async Task<HealthStatus[]> GetStatusAsync()
        {
            var result = await PublicApiGetAsync<HealthStatusList>(StatusPath).ConfigureAwait(false);
            return result.Statuses;
        }
    }
}