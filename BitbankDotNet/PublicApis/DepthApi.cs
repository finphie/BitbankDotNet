using BitbankDotNet.Entities;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
namespace BitbankDotNet
{
    partial class BitbankClient
    {
        /// <summary>
        /// [PublicAPI]板情報を返します。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <returns>板情報</returns>
        public Task<Depth> GetDepthAsync(CurrencyPair pair)
            => PublicApiGetAsync<Depth>("/depth", pair);
    }
}