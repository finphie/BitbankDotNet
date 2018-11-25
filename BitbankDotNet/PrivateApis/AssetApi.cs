using BitbankDotNet.Entities;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
namespace BitbankDotNet
{
    partial class BitbankRestApiClient
    {
        const string AssetPath = "/v1/user/assets";

        static readonly byte[] AssetUtf8Path =
        {
            0x2F, 0x76, 0x31, 0x2F, 0x75, 0x73, 0x65, 0x72, 0x2F, 0x61,
            0x73, 0x73, 0x65, 0x74, 0x73
        };

        /// <summary>
        /// [Private API]アセット一覧を返します。
        /// </summary>
        /// <returns>アセット一覧</returns>
        /// <exception cref="BitbankDotNetException">APIリクエストでエラーが発生しました。</exception>
        public async Task<Asset[]> GetAssetsAsync()
        {
            var result = await PrivateApiGetAsync<AssetList>(AssetPath, AssetUtf8Path).ConfigureAwait(false);
            return result.Assets;
        }
    }
}