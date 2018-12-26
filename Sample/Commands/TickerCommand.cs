using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using BitbankDotNet;
using BitbankDotNet.Entities;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Logging;

namespace Sample.Commands
{
    /// <summary>
    /// <see cref="Ticker"/>に関するコマンド
    /// </summary>
    public class TickerCommand : BaseCommand
    {
        /// <summary>
        /// 通貨ペア
        /// </summary>
        [Option]
        [Required]
        public CurrencyPair Pair { get; }

        /// <summary>
        /// <see cref="TickerCommand"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="sampleService">service</param>
        /// <param name="logger">logger</param>
        public TickerCommand(ISampleService sampleService, ILogger<TickerCommand> logger)
            : base(sampleService, logger)
        {
        }

        /// <inheritdoc/>
        protected override async Task OnExecuteAsync(CommandLineApplication application)
        {
            try
            {
                var json = await Service.GetTickerAsync(Pair).ConfigureAwait(false);
                Logger.LogInformation(json.ToString());
            }
            catch (BitbankDotNetException ex)
            {
                Logger.LogError(ex.Message);
            }
        }
    }
}