using BitbankDotNet.Entities;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
namespace BitbankDotNet
{
    partial class BitbankClient
    {
        /// <summary>
        /// [PublicAPI]取引所ステータスを返します。
        /// </summary>
        /// <returns>取引所ステータス</returns>
        public async Task<HealthStatus[]> GetStatus()
            => (await PublicApiGetAsync<HealthStatusList>("spot/status").ConfigureAwait(false)).Statuses;
    }
}