using BitbankDotNet.Entities;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
namespace BitbankDotNet
{
    partial class BitbankClient
    {
        const string StatusPath = "spot/status";

        /// <summary>
        /// [PublicAPI]取引所ステータスを返します。
        /// </summary>
        /// <returns>取引所ステータス</returns>
        public async Task<HealthStatus[]> GetStatusAsync()
        {
            var result = await PublicApiGetAsync<HealthStatusList>(StatusPath).ConfigureAwait(false);
            return result.Statuses;
        }
    }
}