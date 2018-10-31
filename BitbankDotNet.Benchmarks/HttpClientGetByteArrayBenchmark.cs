using BenchmarkDotNet.Attributes;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BitbankDotNet.Benchmarks
{
    /// <summary>
    /// 
    /// </summary>
    [Config(typeof(BenchmarkConfig))]
    public class HttpClientGetByteArrayBenchmark
    {
        static readonly Uri Url = new Uri("http://localhost:3000/depth");
        static readonly HttpClient Client = new HttpClient();

        [Benchmark]
        public async Task<byte[]> GetByteArrayAsync()
            => await Client.GetByteArrayAsync(Url);

        [Benchmark]
        public async Task<byte[]> ReadAsByteArrayAsync()
        {
            var response = await Client.SendAsync(new HttpRequestMessage(HttpMethod.Get, Url), HttpCompletionOption.ResponseHeadersRead);
            return await response.Content.ReadAsByteArrayAsync();
        }
    }
}