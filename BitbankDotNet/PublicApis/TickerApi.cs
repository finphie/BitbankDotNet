using BitbankDotNet.Entities;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
namespace BitbankDotNet
{
    partial class BitbankClient
    {
        /// <summary>
        /// [PublicAPI]ティッカー情報を返します。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <returns>ティッカー情報</returns>
        public Task<Ticker> GetTickerAsync(CurrencyPair pair)
            => PublicApiGetAsync<Ticker>("/ticker", pair);
    }
}