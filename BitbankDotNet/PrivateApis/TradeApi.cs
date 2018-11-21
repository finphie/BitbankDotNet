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
    partial class BitbankRestApiClient
    {
        const string TradeHistoryPath = "/v1/user/spot/trade_history?";
        const int TradeHistoryPathLength = 28;

        static readonly byte[] TradeHistoryUtf8Path =
        {
            0x2F, 0x76, 0x31, 0x2F, 0x75, 0x73, 0x65, 0x72, 0x2F, 0x73,
            0x70, 0x6F, 0x74, 0x2F, 0x74, 0x72, 0x61, 0x64, 0x65, 0x5F,
            0x68, 0x69, 0x73, 0x74, 0x6F, 0x72, 0x79, 0x3F
        };

        /// <summary>
        /// [PrivateAPI]約定履歴を取得します。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <returns>約定履歴</returns>
        public async Task<Trade[]> GetTradeHistoryAsync(CurrencyPair pair)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["pair"] = pair.GetEnumMemberValue();

            var result = await GetTradeHistoryAsync(query.ToString()).ConfigureAwait(false);
            return result.Trades;
        }

        /// <summary>
        /// [PrivateAPI]約定履歴を取得します。
        /// </summary>
        /// <param name="pair">通貨ペア</param>
        /// <param name="count">取得する注文数</param>
        /// <param name="orderId">注文ID</param>
        /// <param name="since">開始時間</param>
        /// <param name="end">終了時間</param>
        /// <param name="sort">順序</param>
        /// <returns>約定履歴</returns>
        public async Task<Trade[]> GetTradeHistoryAsync(CurrencyPair pair, long? count, long? orderId, DateTimeOffset? since, DateTimeOffset? end, SortOrder? sort)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["pair"] = pair.GetEnumMemberValue();
            if (count.HasValue)
                query["count"] = count.ToString();
            if (orderId.HasValue)
                query["order_id"] = orderId.ToString();
            if (since.HasValue)
                query["since"] = since.Value.ToUnixTimeMilliseconds().ToString();
            if (end.HasValue)
                query["end"] = end.Value.ToUnixTimeMilliseconds().ToString();
            if (sort.HasValue)
                query["order"] = sort.Value.GetEnumMemberValue();

            var result = await GetTradeHistoryAsync(query.ToString()).ConfigureAwait(false);
            return result.Trades;
        }

        Task<TradeList> GetTradeHistoryAsync(string query)
        {
            Span<byte> buffer = stackalloc byte[TradeHistoryPathLength + query.Length];
            ref var bufferStart = ref MemoryMarshal.GetReference(buffer);

            Unsafe.CopyBlockUnaligned(ref bufferStart, ref TradeHistoryUtf8Path[0], TradeHistoryPathLength);
            query.FromAsciiStringToUtf8Bytes(buffer.Slice(TradeHistoryPathLength));

            var path = TradeHistoryPath + query;

            return PrivateApiGetAsync<TradeList>(path, buffer);
        }
    }
}