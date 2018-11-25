using BitbankDotNet.Entities;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
namespace BitbankDotNet
{
    partial class BitbankRestApiClient
    {
        const string DepthPath = "/depth";

        /// <summary>
        /// [Public API]板情報を返します。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <returns>板情報</returns>
        /// <exception cref="BitbankDotNetException">APIリクエストでエラーが発生しました。</exception>
        public Task<Depth> GetDepthAsync(CurrencyPair pair)
            => PublicApiGetAsync<Depth>(DepthPath, pair);
    }
}