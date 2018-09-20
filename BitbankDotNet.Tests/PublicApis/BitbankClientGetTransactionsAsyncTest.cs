using BitbankDotNet.Entities;
using BitbankDotNet.Shared.Helpers;
using Moq;
using Moq.Protected;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace BitbankDotNet.Tests.PublicApis
{
    public class BitbankClientGetTransactionsAsyncTest
    {
        const string Json =
            "{\"success\":1,\"data\":{\"transactions\":[{\"transaction_id\":2147483647,\"side\":\"buy\",\"price\":\"76543210.1234568\",\"amount\":\"76543210.1234568\",\"executed_at\":1514768461111},{\"transaction_id\":2147483647,\"side\":\"buy\",\"price\":\"76543210.1234568\",\"amount\":\"76543210.1234568\",\"executed_at\":1514768461111}]}}";

        [Fact]
        public void HTTPステータスが200かつSuccessが1_Transactionを返す()
        {
            var mockHttpHandler = new Mock<HttpMessageHandler>();
            mockHttpHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .Callback<HttpRequestMessage, CancellationToken>((request, _) =>
                {
                    Assert.StartsWith("https://public.bitbank.cc/btc_jpy/", request.RequestUri.AbsoluteUri);
                })
                .Returns(Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(Json)
                }));
            
            using (var client = new HttpClient(mockHttpHandler.Object))
            {
                var bitbank = new BitbankClient(client);
                var result = bitbank.GetTransactionsAsync(default, default, default, default).GetAwaiter().GetResult();

                Assert.NotNull(result);
				
				var entity = new Transaction();
				EntityHelper.SetValue(entity);
				Assert.Equal(Enumerable.Repeat(entity, 2).ToArray(), result, new PublicPropertyComparer<Transaction[]>());
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
                    bitbank.GetTransactionsAsync(default, default, default, default).GetAwaiter().GetResult());
            }
        }
    }
}