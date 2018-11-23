﻿using BitbankDotNet.Entities;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
namespace BitbankDotNet
{
    partial class BitbankRestApiClient
    {
        const string TickerPath = "/ticker";

        /// <summary>
        /// [Public API]ティッカー情報を返します。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <returns>ティッカー情報</returns>
        public Task<Ticker> GetTickerAsync(CurrencyPair pair)
            => PublicApiGetAsync<Ticker>(TickerPath, pair);
    }
}