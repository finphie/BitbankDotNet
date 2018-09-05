using BitbankDotNet.Entities;
using BitbankDotNet.Extensions;
using System.Threading.Tasks;
using System.Web;

namespace BitbankDotNet
{
    public partial class BitbankClient
    {
        /// <summary>
        /// [PrivateAPI]アセット一覧を返します。
        /// </summary>
        /// <returns>アセット一覧</returns>
        public async Task<Asset[]> GetAssetAsync()
            => (await GetAsync<AssetResponse>("user/assets").ConfigureAwait(false)).Data.Assets;

        /// <summary>
        /// [PrivateAPI]注文情報を取得します。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <param name="orderId">注文ID</param>
        /// <returns>注文情報</returns>
        public async Task<Order> GetOrderAsync(CurrencyPair pair, int orderId)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["pair"] = pair.GetEnumMemberValue();
            query["order_id"] = orderId.ToString();

            return (await GetAsync<OrderResponse>("user/spot/order?" + query).ConfigureAwait(false)).Data;
        }
    }
}