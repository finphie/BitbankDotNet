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
        const string WithdrawalAccountPath = "/v1/user/withdrawal_account?";
        const string RequestWithdrawalPath = "/v1/user/request_withdrawal";
        const int WithdrawalAccountPathLength = 28;

        static readonly byte[] WithdrawalAccountUtf8Path =
        {
            0x2F, 0x76, 0x31, 0x2F, 0x75, 0x73, 0x65, 0x72, 0x2F, 0x77,
            0x69, 0x74, 0x68, 0x64, 0x72, 0x61, 0x77, 0x61, 0x6C, 0x5F,
            0x61, 0x63, 0x63, 0x6F, 0x75, 0x6E, 0x74, 0x3F
        };

        /// <summary>
        /// [PrivateAPI]出金アカウントを取得します。
        /// </summary>
        /// <param name="asset">通貨名</param>
        /// <returns>出金アカウント情報</returns>
        public async Task<WithdrawalAccount[]> GetWithdrawalAccountsAsync(AssetName asset)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["asset"] = asset.GetEnumMemberValue();

            var result = await GetWithdrawalAccountsAsync(query.ToString()).ConfigureAwait(false);
            return result.Accounts;
        }

        /// <summary>
        /// [PrivateAPI]出金リクエストを行います。
        /// </summary>
        /// <param name="asset">アセット名</param>
        /// <param name="amount">引き出し量</param>
        /// <param name="uuid">出金アカウントのUUID</param>
        /// <returns></returns>
        public Task<Withdrawal> RequestWithdrawalAsync(AssetName asset, double amount, string uuid)
        {
            var body = new WithdrawalBody
            {
                Asset = asset,
                Amount = amount,
                Uuid = uuid
            };

            return PrivateApiPostAsync<Withdrawal, WithdrawalBody>(RequestWithdrawalPath, body);
        }

        /// <summary>
        /// [PrivateAPI]出金リクエストを行います。
        /// </summary>
        /// <param name="asset">アセット名</param>
        /// <param name="amount">引き出し量</param>
        /// <param name="uuid">出金アカウントのUUID</param>
        /// <param name="otpToken">二段階認証トークン</param>
        /// <param name="smsToken">SMS認証トークン</param>
        /// <returns></returns>
        public Task<Withdrawal> RequestWithdrawalAsync(AssetName asset, double amount, string uuid, int? otpToken, int? smsToken)
        {
            var body = new WithdrawalBody
            {
                Asset = asset,
                Amount = amount,
                Uuid = uuid,
                OtpToken = otpToken,
                SmsToken = smsToken
            };

            return PrivateApiPostAsync<Withdrawal, WithdrawalBody>(RequestWithdrawalPath, body);
        }

        Task<WithdrawalAccountList> GetWithdrawalAccountsAsync(string query)
        {
            Span<byte> buffer = stackalloc byte[WithdrawalAccountPathLength + query.Length];
            ref var bufferStart = ref MemoryMarshal.GetReference(buffer);

            Unsafe.CopyBlockUnaligned(ref bufferStart, ref WithdrawalAccountUtf8Path[0], WithdrawalAccountPathLength);
            query.FromAsciiStringToUtf8Bytes(buffer.Slice(WithdrawalAccountPathLength));

            var path = WithdrawalAccountPath + query;

            return PrivateApiGetAsync<WithdrawalAccountList>(path, buffer);
        }
    }
}