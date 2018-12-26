using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using BitbankDotNet;
using BitbankDotNet.Entities;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Logging;

namespace Sample.Commands
{
    /// <summary>
    /// <see cref="Candlestick"/>に関するコマンド
    /// </summary>
    public class CandlestickCommand : BaseCommand
    {
        /// <summary>
        /// 通貨ペア
        /// </summary>
        [Option]
        [Required]
        public CurrencyPair Pair { get; }

        /// <summary>
        /// ローソク足の期間
        /// </summary>
        [Option]
        [Required]
        public CandleType Type { get; }

        /// <summary>
        /// 日時
        /// </summary>
        [Option]
        [Required]
        public DateTimeOffset Date { get; }

        /// <summary>
        /// <see cref="CandlestickCommand"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="sampleService">service</param>
        /// <param name="logger">logger</param>
        public CandlestickCommand(ISampleService sampleService, ILogger<CandlestickCommand> logger)
            : base(sampleService, logger)
        {
        }

        /// <inheritdoc/>
        protected override async Task OnExecuteAsync(CommandLineApplication application)
        {
            try
            {
                var json = await Service.GetCandlesticksAsync(Pair, Type, Date).ConfigureAwait(false);
                Logger.LogInformation(json.ToString());
            }
            catch (BitbankDotNetException ex)
            {
                Logger.LogError(ex.Message);
            }
        }
    }
}