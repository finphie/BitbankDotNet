using BitbankDotNet.Entities;
using BitbankDotNet.Extensions;
using System.Threading.Tasks;
using System.Web;

// ReSharper disable once CheckNamespace
namespace BitbankDotNet
{
    partial class BitbankClient
    {
        const string WithdrawalAccountPath = "/v1/user/withdrawal_account?";
        const string RequestWithdrawalPath = "/v1/user/request_withdrawal";

        /// <summary>
        /// [PrivateAPI]出金アカウントを取得します。
        /// </summary>
        /// <param name="asset">通貨名</param>
        /// <returns>出金アカウント情報</returns>
        public async Task<WithdrawalAccount[]> GetWithdrawalAccountsAsync(AssetName asset)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["asset"] = asset.GetEnumMemberValue();
            var path = WithdrawalAccountPath + query;
            var result = await PrivateApiGetAsync<WithdrawalAccountList>(path).ConfigureAwait(false);

            return result.Accounts;
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
        public Task<Withdrawal> RequestWithdrawalAsync(AssetName asset, double amount, string uuid, int otpToken, int smsToken)
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
    }
}