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
    public class BitbankClientRequestWithdrawalAsyncTest
    {
        const string Json =
            "{\"success\":1,\"data\":{\"uuid\":\"a\",\"asset\":\"jpy\",\"account_uuid\":\"a\",\"amount\":\"1.2\",\"fee\":\"1.2\",\"label\":\"a\",\"address\":\"a\",\"txId\":\"a\",\"status\":\"CONFIRMING\",\"requested_at\":1514862245678}}";

        [Fact]
        public void HTTPステータスが200かつSuccessが1_Withdrawalを返す()
        {
            var mockHttpHandler = new Mock<HttpMessageHandler>();
            mockHttpHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .Callback<HttpRequestMessage, CancellationToken>((request, _) =>
                {
					Assert.StartsWith("https://api.bitbank.cc/v1/user/", request.RequestUri.AbsoluteUri);
                })
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(Json)
                });
            
            using (var client = new HttpClient(mockHttpHandler.Object))
            {
				var bitbank = new BitbankClient(client, " ", " ");
                var result = bitbank.RequestWithdrawalAsync(default, default, default, default, default).GetAwaiter().GetResult();

                Assert.NotNull(result);
				
				var entity = new Withdrawal();
				EntityHelper.SetValue(entity);
				Assert.Equal(entity, result, new PublicPropertyComparer<Withdrawal>());
            }
        }

        [Theory]
        [InlineData(HttpStatusCode.NotFound, 0)]
        [InlineData(HttpStatusCode.NotFound, 1)]
        [InlineData(HttpStatusCode.OK, 0)]
        public void HTTPステータスが404またはSuccessが0_BitbankApiExceptionをスローする(HttpStatusCode statusCode, int success)
        {
            var mockHttpHandler = new Mock<HttpMessageHandler>();
            mockHttpHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage(statusCode)
                {
                    Content = new StringContent($"{{\"success\":{success},\"data\":{{\"code\":10000}}}}")
                });

            using (var client = new HttpClient(mockHttpHandler.Object))
            {
				var bitbank = new BitbankClient(client, " ", " ");
                Assert.Throws<BitbankApiException>(() =>
                    bitbank.RequestWithdrawalAsync(default, default, default, default, default).GetAwaiter().GetResult());
            }
        }
    }
}