using BitbankDotNet.Entities;
using BitbankDotNet.Shared.Helpers;
using Moq;
using Moq.Protected;
using System;
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
				
				var entity = new Withdrawal
                {
                    AccountUuid = EntityHelper.GetTestValue<string>(),
                    Address = EntityHelper.GetTestValue<string>(),
                    Amount = EntityHelper.GetTestValue<double>(),
                    Asset = EntityHelper.GetTestValue<AssetName>(),
                    Fee = EntityHelper.GetTestValue<double>(),
                    Label = EntityHelper.GetTestValue<string>(),
                    RequestedAt = EntityHelper.GetTestValue<DateTime>(),
                    Status = EntityHelper.GetTestValue<WithdrawalStatus>(),
                    TxId = EntityHelper.GetTestValue<string>(),
                    Uuid = EntityHelper.GetTestValue<string>()
                };
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
                var exception = Assert.Throws<BitbankApiException>(() =>
                    bitbank.RequestWithdrawalAsync(default, default, default, default, default).GetAwaiter().GetResult());
                Assert.Equal(statusCode, exception.StatusCode);
            }
        }

		[Fact]
		public void タイムアウト_BitbankApiExceptionをスローする()
        {
            var mockHttpHandler = new Mock<HttpMessageHandler>();
            mockHttpHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .Returns<HttpRequestMessage, CancellationToken>(async (_, cancellationToken) =>
                {
                    await Task.Delay(50, cancellationToken).ConfigureAwait(false);
                    return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                    {
                        Content = new StringContent(Json)
                    };
                });

            using (var client = new HttpClient(mockHttpHandler.Object))
            {
				var bitbank = new BitbankClient(client, " ", " ", TimeSpan.FromMilliseconds(1));
                var exception = Assert.Throws<BitbankApiException>(() =>
                    bitbank.RequestWithdrawalAsync(default, default, default, default, default).GetAwaiter().GetResult());
                Assert.IsType<TaskCanceledException>(exception.InnerException);
            }
        }

        [Theory]
        [InlineData("")]
        [InlineData("{}")]
        [InlineData("{\"data\":\"\"}")]
        [InlineData("{\"data\":{}")]
        [InlineData("{\"data\":\"a\"}")]
        public void 不正なJSONを取得_BitbankApiExceptionをスローする(string content)
        {
            var mockHttpHandler = new Mock<HttpMessageHandler>();
            mockHttpHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(content)
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