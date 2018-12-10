using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;

namespace BitbankDotNet.Benchmarks
{
    /// <summary>
    ///  指定したURLからbyte配列を取得する処理のベンチマーク
    /// </summary>
    [Config(typeof(BenchmarkConfig))]
    [SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "ベンチマーク")]
    public class HttpClientGetByteArrayBenchmark
    {
        static readonly Uri Url = new Uri("http://localhost:3000/depth");
        static readonly HttpClient Client = new HttpClient();

        [Benchmark]
        public async Task<byte[]> GetByteArrayAsync()
            => await Client.GetByteArrayAsync(Url).ConfigureAwait(false);

        [Benchmark]
        public async Task<byte[]> ReadAsByteArrayAsync()
        {
            var response = await Client.SendAsync(new HttpRequestMessage(HttpMethod.Get, Url), HttpCompletionOption.ResponseHeadersRead)
                .ConfigureAwait(false);
            return await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
        }
    }
}