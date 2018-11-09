using BitbankDotNet.Entities;
using BitbankDotNet.Extensions;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Web;

// ReSharper disable once CheckNamespace
namespace BitbankDotNet
{
    partial class BitbankClient
    {
        const string ActiveOrderPath = "/v1/user/spot/active_orders?";
        const int ActiveOrderPathLength = 28;

        static readonly byte[] ActiveOrderUtf8Path =
        {
            0x2F, 0x76, 0x31, 0x2F, 0x75, 0x73, 0x65, 0x72, 0x2F, 0x73,
            0x70, 0x6F, 0x74, 0x2F, 0x61, 0x63, 0x74, 0x69, 0x76, 0x65,
            0x5F, 0x6F, 0x72, 0x64, 0x65, 0x72, 0x73, 0x3F
        };

        /// <summary>
        /// [PrivateAPI]アクティブな注文を取得します。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <param name="count">取得する注文数</param>
        /// <param name="fromId">取得開始注文ID</param>
        /// <param name="endId">取得終了注文ID</param>
        /// <param name="since">開始時間</param>
        /// <param name="end">終了時間</param>
        /// <returns>注文情報</returns>
        public async Task<Order[]> GetActiveOrdersAsync(CurrencyPair pair, long count, long fromId, long endId, DateTimeOffset since, DateTimeOffset end)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["pair"] = pair.GetEnumMemberValue();
            query["from_id"] = fromId.ToString();
            query["end_id"] = fromId.ToString();
            query["since"] = since.ToUnixTimeMilliseconds().ToString();
            query["end"] = end.ToUnixTimeMilliseconds().ToString();
            
            var result = await GetActiveOrdersAsync(query.ToString()).ConfigureAwait(false);
            return result.Orders;
        }

        Task<OrderList> GetActiveOrdersAsync(string query)
        {
            Span<byte> buffer = stackalloc byte[ActiveOrderPathLength + query.Length];
            ref var bufferStart = ref MemoryMarshal.GetReference(buffer);

            Unsafe.CopyBlockUnaligned(ref bufferStart, ref ActiveOrderUtf8Path[0], ActiveOrderPathLength);
            query.FromAsciiStringToUtf8Bytes(buffer.Slice(ActiveOrderPathLength));

            var path = ActiveOrderPath + query;

            return PrivateApiGetAsync<OrderList>(path, buffer);
        }
    }
}