using System.Threading.Tasks;
using BitbankDotNet.Entities;

// ReSharper disable once CheckNamespace
namespace BitbankDotNet
{
    partial class BitbankRestApiClient
    {
        const string SettingPath = "/v1/spot/pairs";

        /// <summary>
        /// [Private API]通貨ペア詳細一覧を返します。
        /// </summary>
        /// <returns>通貨ペア詳細一覧</returns>
        /// <exception cref="BitbankDotNetException">APIリクエストでエラーが発生しました。</exception>
        public async Task<CurrencyPairSetting[]> GetCurrencyPairSettingsAsync()
        {
            var result = await PrivateApiGetAsync<CurrencyPairSettingList>(SettingPath).ConfigureAwait(false);
            return result.Pairs;
        }
    }
}