#define Core22

using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
#if CoreRt || CoreRtCpp
using BenchmarkDotNet.Environments;
#endif
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Jobs;
#if CoreRtCpp
using BenchmarkDotNet.Toolchains.CoreRt;
#endif
using BenchmarkDotNet.Toolchains.CsProj;
#if Core30
using BenchmarkDotNet.Toolchains.DotNetCli;
#endif
using System.Diagnostics.CodeAnalysis;

namespace BitbankDotNet.Benchmarks
{
    [SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "BenchmarkDotNetで使用される")]
    class BenchmarkConfig : ManualConfig
    {
        public BenchmarkConfig()
        {
            Add(MarkdownExporter.GitHub);
            Add(MemoryDiagnoser.Default);
            Add(BenchmarkLogicalGroupRule.ByCategory);
            Add(CategoriesColumn.Default);
            Add(DisassemblyDiagnoser.Create(new DisassemblyDiagnoserConfig(printSource: true)));

#if Core22
            Add(Job.Default.With(CsProjCoreToolchain.NetCoreApp22));
#endif
#if Core30
            Add(Job.Default.With(
                CsProjCoreToolchain.From(
                    new NetCoreAppSettings("netcoreapp3.0", "3.0.0-*", ".NET Core 3.0"))));
#endif
#if CoreRt
            // CoreRT（RyuJIT利用）
            // cf. https://benchmarkdotnet.org/articles/configs/toolchains.html
            // cf. https://github.com/dotnet/corert/blob/master/Documentation/how-to-build-and-run-ilcompiler-in-console-shell-prompt.md
            Add(Job.Default
                .With(CsProjCoreToolchain.NetCoreApp22)
                .With(Runtime.CoreRT));
#endif
#if CoreRtCpp
            // CoreRT（CPP Code Generator利用）
            Add(Job.Default
                .With(CsProjCoreToolchain.NetCoreApp22)
                .With(Runtime.CoreRT)
                .With(CoreRtToolchain.CreateBuilder()
                    // ReSharper disable once StringLiteralTypo
                    .UseCoreRtLocal(@"\corert\bin\Windows_NT.x64.Release")
                    .UseCppCodeGenerator()
                    .ToToolchain()));
#endif
        }
    }
}