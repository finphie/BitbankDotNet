using BitbankDotNet.Entities;
using BitbankDotNet.Shared.Helpers;
using Moq;
using Moq.Protected;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace BitbankDotNet.Tests.PrivateApis
{
    public class BitbankClientCancelOrderAsyncTest
    {
        const string Json =
            "{\"success\":1,\"data\":{\"order_id\":9223372036854775807,\"pair\":\"btc_jpy\",\"side\":\"buy\",\"type\":\"limit\",\"start_amount\":\"76543210.1234568\",\"remaining_amount\":\"76543210.1234568\",\"executed_amount\":\"76543210.1234568\",\"price\":\"76543210.1234568\",\"average_price\":\"76543210.1234568\",\"ordered_at\":1514768461111,\"status\":\"UNFILLED\"}}";

        [Fact]
        public void HTTPステータスが200かつSuccessが1_Orderを返す()
        {
            var mockHttpHandler = new Mock<HttpMessageHandler>();
            mockHttpHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .Callback<HttpRequestMessage, CancellationToken>((request, _) =>
                {
					Assert.StartsWith("https://api.bitbank.cc/v1/user/", request.RequestUri.AbsoluteUri);
                })
                .Returns(Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(Json)
                }));
            
            using (var client = new HttpClient(mockHttpHandler.Object))
            {
				var bitbank = new BitbankClient(client, " ", " ");
                var result = bitbank.CancelOrderAsync(default, default).GetAwaiter().GetResult();

                Assert.NotNull(result);
				
				var entity = new Order();
				EntityHelper.SetValue(entity);
				Assert.Equal(entity, result, new PublicPropertyComparer<Order>());
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
				var bitbank = new BitbankClient(client, " ", " ");
                Assert.Throws<BitbankApiException>(() =>
                    bitbank.CancelOrderAsync(default, default).GetAwaiter().GetResult());
            }
        }
    }
}