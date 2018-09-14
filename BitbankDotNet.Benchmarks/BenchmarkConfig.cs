using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Toolchains.CsProj;
using BenchmarkDotNet.Toolchains.DotNetCli;

namespace BitbankDotNet.Benchmarks
{
    class BenchmarkConfig : ManualConfig
    {
        public BenchmarkConfig()
        {
            Add(MarkdownExporter.GitHub);
            Add(MemoryDiagnoser.Default);

            Add(Job.Default.With(CsProjCoreToolchain.NetCoreApp21));
            //Add(Job.Default.With(CsProjCoreToolchain.NetCoreApp22));

            Add(Job.Default.With(
                CsProjCoreToolchain.From(
                    new NetCoreAppSettings("netcoreapp3.0", "3.0.0-*", ".NET Core 3.0")))
            );

            // CoreRT（RyuJIT利用）
            // cf. https://benchmarkdotnet.org/articles/configs/toolchains.html
            // cf. https://github.com/dotnet/corert/blob/master/Documentation/how-to-build-and-run-ilcompiler-in-console-shell-prompt.md
            Add(Job.Default
                .With(CsProjCoreToolchain.NetCoreApp21)
                .With(Runtime.CoreRT));

            // CoreRT（CPP Code Generator利用）
            //Add(Job.Default
            //    .With(CsProjCoreToolchain.NetCoreApp21)
            //    .With(Runtime.CoreRT)
            //    .With(CoreRtToolchain.CreateBuilder()
            //        .UseCoreRtLocal(@"\corert\bin\Windows_NT.x64.Release")
            //        .UseCppCodeGenerator()
            //        .ToToolchain()));
        }
    }
}