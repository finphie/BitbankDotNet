using BenchmarkDotNet.Running;

namespace BitbankDotNet.Benchmarks
{
    class Program
    {
        static void Main(string[] args)
            => BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
    }
}