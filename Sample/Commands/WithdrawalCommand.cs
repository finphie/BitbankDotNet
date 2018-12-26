using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using BitbankDotNet;
using BitbankDotNet.Entities;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Logging;

namespace Sample.Commands
{
    /// <summary>
    /// <see cref="Withdrawal"/>に関するコマンド
    /// </summary>
    public class WithdrawalCommand : BaseCommand
    {
        /// <summary>
        /// 通貨名
        /// </summary>
        [Option]
        [Required]
        public AssetName Asset { get; }

        /// <summary>
        /// <see cref="WithdrawalCommand"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="sampleService">service</param>
        /// <param name="logger">logger</param>
        public WithdrawalCommand(ISampleService sampleService, ILogger<WithdrawalCommand> logger)
            : base(sampleService, logger)
        {
        }

        /// <inheritdoc/>
        protected override async Task OnExecuteAsync(CommandLineApplication application)
        {
            try
            {
                var json = await Service.GetWithdrawalAccountsAsync(Asset).ConfigureAwait(false);
                Logger.LogInformation(json.ToString());
            }
            catch (BitbankDotNetException ex)
            {
                Logger.LogError(ex.Message);
            }
        }
    }
}