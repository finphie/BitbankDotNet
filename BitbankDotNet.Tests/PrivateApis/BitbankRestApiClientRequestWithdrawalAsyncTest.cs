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
    public class BitbankRestApiClientRequestWithdrawalAsyncTest
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
				var bitbank = new BitbankRestApiClient(client, " ", " ");
                var result = bitbank.RequestWithdrawalAsync(default, default, default, default, default).GetAwaiter().GetResult();

                Assert.NotNull(result);
                Assert.Equal(EntityHelper.GetTestValue<string>(), result.AccountUuid);
                Assert.Equal(EntityHelper.GetTestValue<string>(), result.Address);
                Assert.Equal(EntityHelper.GetTestValue<double>(), result.Amount);
                Assert.Equal(EntityHelper.GetTestValue<AssetName>(), result.Asset);
                Assert.Equal(EntityHelper.GetTestValue<double>(), result.Fee);
                Assert.Equal(EntityHelper.GetTestValue<string>(), result.Label);
                Assert.Equal(EntityHelper.GetTestValue<DateTime>(), result.RequestedAt);
                Assert.Equal(EntityHelper.GetTestValue<WithdrawalStatus>(), result.Status);
                Assert.Equal(EntityHelper.GetTestValue<string>(), result.TxId);
                Assert.Equal(EntityHelper.GetTestValue<string>(), result.Uuid);
            }
        }

        [Theory]
        [InlineData(HttpStatusCode.NotFound, 0, 10000)]
        [InlineData(HttpStatusCode.NotFound, 1, 60003)]
        [InlineData(HttpStatusCode.OK, 0, 70001)]
        public void HTTPステータスが404またはSuccessが0_BitbankApiExceptionをスローする(HttpStatusCode statusCode, int success, int apiErrorCode)
        {
            var mockHttpHandler = new Mock<HttpMessageHandler>();
            mockHttpHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage(statusCode)
                {
                    Content = new StringContent($"{{\"success\":{success},\"data\":{{\"code\":{apiErrorCode}}}}}")
                });

            using (var client = new HttpClient(mockHttpHandler.Object))
            {
				var bitbank = new BitbankRestApiClient(client, " ", " ");
                var exception = Assert.Throws<BitbankApiException>(() =>
                    bitbank.RequestWithdrawalAsync(default, default, default, default, default).GetAwaiter().GetResult());
                Assert.Equal(statusCode, exception.StatusCode);
                Assert.Equal(apiErrorCode, exception.ApiErrorCode);
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
				var bitbank = new BitbankRestApiClient(client, " ", " ", TimeSpan.FromMilliseconds(1));
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
				var bitbank = new BitbankRestApiClient(client, " ", " ");
                Assert.Throws<BitbankApiException>(() =>
                    bitbank.RequestWithdrawalAsync(default, default, default, default, default).GetAwaiter().GetResult());
            }
        }
    }
}