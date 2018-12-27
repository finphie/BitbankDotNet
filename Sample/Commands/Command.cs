using System;
using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Logging;

namespace Sample.Commands
{
    /// <summary>
    /// コマンド
    /// </summary>
    [Command(OptionsComparison = StringComparison.OrdinalIgnoreCase)]
    [Subcommand(typeof(TickerCommand))]
    [Subcommand(typeof(DepthCommand))]
    [Subcommand(typeof(TransactionCommand))]
    [Subcommand(typeof(CandlestickCommand))]
    [Subcommand(typeof(AssetCommand))]
    [Subcommand(typeof(OrderCommand))]
    [Subcommand(typeof(TradeCommand))]
    [Subcommand(typeof(WithdrawalCommand))]
    [Subcommand(typeof(StatusCommand))]
    public class Command : BaseCommand
    {
        /// <summary>
        /// <see cref="Command"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="sampleService">service</param>
        /// <param name="logger">logger</param>
        public Command(ISampleService sampleService, ILogger<Command> logger)
            : base(sampleService, logger)
        {
        }

        /// <inheritdoc/>
        protected override async Task OnExecuteAsync(CommandLineApplication application)
            => await base.OnExecuteAsync(application).ConfigureAwait(false);
    }
}