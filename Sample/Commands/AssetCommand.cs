using System.Threading.Tasks;
using BitbankDotNet;
using BitbankDotNet.Entities;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Logging;

namespace Sample.Commands
{
    /// <summary>
    /// <see cref="Asset"/>に関するコマンド
    /// </summary>
    public class AssetCommand : BaseCommand
    {
        /// <summary>
        /// <see cref="AssetCommand"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="sampleService">service</param>
        /// <param name="logger">logger</param>
        public AssetCommand(ISampleService sampleService, ILogger<AssetCommand> logger)
            : base(sampleService, logger)
        {
        }

        /// <inheritdoc/>
        protected override async Task OnExecuteAsync(CommandLineApplication application)
        {
            try
            {
                var json = await Service.GetAssetsAsync().ConfigureAwait(false);
                Logger.LogInformation(json.ToString());
            }
            catch (BitbankDotNetException ex)
            {
                Logger.LogError(ex.Message);
            }
        }
    }
}