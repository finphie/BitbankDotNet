using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Web;
using BitbankDotNet.Entities;
using BitbankDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace BitbankDotNet
{
    partial class BitbankRestApiClient
    {
        const string OrderInfoPath = "/v1/user/spot/order?";
        const string OrdersInfoPath = "/v1/user/spot/orders_info";
        const int OrderInfoPathLength = 20;

        static readonly byte[] OrderInfoUtf8Path =
        {
            0x2F, 0x76, 0x31, 0x2F, 0x75, 0x73, 0x65, 0x72, 0x2F, 0x73,
            0x70, 0x6F, 0x74, 0x2F, 0x6F, 0x72, 0x64, 0x65, 0x72, 0x3F
        };

        /// <summary>
        /// [Private API]注文情報を取得します。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <param name="orderId">注文ID</param>
        /// <returns>注文情報</returns>
        /// <exception cref="BitbankDotNetException">APIリクエストでエラーが発生しました。</exception>
        public Task<Order> GetOrderAsync(CurrencyPair pair, long orderId)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["pair"] = pair.GetEnumMemberValue();
            query["order_id"] = orderId.ToString();
            var utf16Query = query.ToString();

            Span<byte> buffer = stackalloc byte[OrderInfoPathLength + utf16Query.Length];
            ref var bufferStart = ref MemoryMarshal.GetReference(buffer);

            Unsafe.CopyBlockUnaligned(ref bufferStart, ref OrderInfoUtf8Path[0], OrderInfoPathLength);
            utf16Query.FromAsciiStringToUtf8Bytes(buffer.Slice(OrderInfoPathLength));

            var path = OrderInfoPath + query;

            return PrivateApiGetAsync<Order>(path, buffer);
        }

        /// <summary>
        /// [Private API]複数の注文情報を取得します。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <param name="orderIds">複数の注文ID</param>
        /// <returns>注文情報</returns>
        /// <exception cref="BitbankDotNetException">APIリクエストでエラーが発生しました。</exception>
        public async Task<Order[]> GetOrdersAsync(CurrencyPair pair, long[] orderIds)
        {
            var body = new OrdersInfoBody
            {
                Pair = pair,
                OrderIds = orderIds
            };
            var result = await PrivateApiPostAsync<OrderList, OrdersInfoBody>(OrdersInfoPath, body)
                .ConfigureAwait(false);

            return result.Orders;
        }
    }
}