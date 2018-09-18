using BitbankDotNet.Entities;
using BitbankDotNet.Shared.Helpers;
using Moq;
using Moq.Protected;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace BitbankDotNet.Tests.PublicApis
{
    public class BitbankClientGetTickerAsyncTest
    {
        const string Json =
            "{\"success\":1,\"data\":{\"sell\":\"76543210.1234568\",\"buy\":\"76543210.1234568\",\"high\":\"76543210.1234568\",\"low\":\"76543210.1234568\",\"last\":\"76543210.1234568\",\"vol\":\"76543210.1234568\",\"timestamp\":1514800861111}}";

        [Fact]
        public void HTTPステータスが200かつSuccessが1_Tickerを返す()
        {
            var mockHttpHandler = new Mock<HttpMessageHandler>();
            mockHttpHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .Callback<HttpRequestMessage, CancellationToken>((request, _) =>
                {
                    Assert.Equal(HttpMethod.Get, request.Method);
                    Assert.Equal("https://public.bitbank.cc/btc_jpy/ticker", request.RequestUri.AbsoluteUri);
                })
                .Returns(Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(Json)
                }));
            
            using (var client = new HttpClient(mockHttpHandler.Object))
            {
                var bitbank = new BitbankClient(client);
                var ticker = bitbank.GetTickerAsync(default).GetAwaiter().GetResult();

                Assert.NotNull(ticker);
				
				var entity = new Ticker();
				EntityHelper.SetValue(entity);
				Assert.Equal(entity, ticker, new PublicPropertyComparer<Ticker>());
            }
        }

        [Theory]
        [InlineData(HttpStatusCode.NotFound, 1)]
        [InlineData(HttpStatusCode.OK, 0)]
        public void HTTPステータスが404またはSuccessが0_BitbankApiExceptionをスローする(HttpStatusCode statusCode, int success)
        {
            var mockHttpHandler = new Mock<HttpMessageHandler>();
            mockHttpHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .Returns(Task.FromResult(new HttpResponseMessage(statusCode)
                {
                    Content = new StringContent($"{{\"success\":{success},\"data\":{{\"code\":10000}}}}")
                }));

            using (var client = new HttpClient(mockHttpHandler.Object))
            {
                var bitbank = new BitbankClient(client);
                Assert.Throws<BitbankApiException>(() =>
                    bitbank.GetTickerAsync(default).GetAwaiter().GetResult());
            }
        }
    }
}