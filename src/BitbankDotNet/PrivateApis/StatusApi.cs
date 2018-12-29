using System.Threading.Tasks;
using BitbankDotNet.Entities;

// ReSharper disable once CheckNamespace
namespace BitbankDotNet
{
    partial class BitbankRestApiClient
    {
        const string StatusPath = "/v1/spot/status";

        /// <summary>
        /// [Private API]取引所ステータスを返します。
        /// </summary>
        /// <returns>取引所ステータス</returns>
        /// <exception cref="BitbankDotNetException">APIリクエストでエラーが発生しました。</exception>
        public async Task<HealthStatus[]> GetStatusesAsync()
        {
            var result = await PrivateApiGetAsync<HealthStatusList>(StatusPath).ConfigureAwait(false);
            return result.Statuses;
        }
    }
}