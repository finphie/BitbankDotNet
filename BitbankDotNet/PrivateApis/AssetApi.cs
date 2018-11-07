using BitbankDotNet.Entities;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
namespace BitbankDotNet
{
    partial class BitbankClient
    {
        const string AssetPath = "/v1/user/assets";

        /// <summary>
        /// [PrivateAPI]アセット一覧を返します。
        /// </summary>
        /// <returns>アセット一覧</returns>
        public async Task<Asset[]> GetAssetsAsync()
        {
            var result = await PrivateApiGetAsync<AssetList>(AssetPath).ConfigureAwait(false);
            return result.Assets;
        }
    }
}