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
    public class BitbankRestApiClientGetAssetsAsyncTest
    {
        const string Json =
            "{\"success\":1,\"data\":{\"assets\":[{\"asset\":\"jpy\",\"amount_precision\":3,\"onhand_amount\":\"1.2\",\"locked_amount\":\"1.2\",\"free_amount\":\"1.2\",\"withdrawal_fee\":{\"threshold\":\"1.2\",\"under\":\"1.2\",\"over\":\"1.2\"}},{\"asset\":\"jpy\",\"amount_precision\":3,\"onhand_amount\":\"1.2\",\"locked_amount\":\"1.2\",\"free_amount\":\"1.2\",\"withdrawal_fee\":{\"threshold\":\"1.2\",\"under\":\"1.2\",\"over\":\"1.2\"}}]}}";

        [Fact]
        public void HTTPステータスが200かつSuccessが1_Assetを返す()
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
                var result = bitbank.GetAssetsAsync().GetAwaiter().GetResult();

                Assert.NotNull(result);
                Assert.All(result, entity =>
                {
                    Assert.Equal(EntityHelper.GetTestValue<int>(), entity.AmountPrecision);
                    Assert.Equal(EntityHelper.GetTestValue<double>(), entity.FreeAmount);
                    Assert.Equal(EntityHelper.GetTestValue<double>(), entity.LockedAmount);
                    Assert.Equal(EntityHelper.GetTestValue<AssetName>(), entity.Name);
                    Assert.Equal(EntityHelper.GetTestValue<double>(), entity.OnhandAmount);
                    Assert.Equal(EntityHelper.GetTestValue<WithdrawalFeeObject>(), entity.WithdrawalFee);
                });
            }
        }

        [Theory]
        [InlineData(HttpStatusCode.NotFound, 0, 10000)]
        [InlineData(HttpStatusCode.NotFound, 1, 60003)]
        [InlineData(HttpStatusCode.OK, 0, 70001)]
        public void HTTPステータスが404またはSuccessが0_BitbankExceptionをスローする(HttpStatusCode statusCode, int success, int apiErrorCode)
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
                var exception = Assert.Throws<BitbankException>(() =>
                    bitbank.GetAssetsAsync().GetAwaiter().GetResult());
                Assert.Equal(apiErrorCode, exception.ApiErrorCode);
            }
        }

		[Fact]
		public void タイムアウト_BitbankExceptionをスローする()
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
                var exception = Assert.Throws<BitbankException>(() =>
                    bitbank.GetAssetsAsync().GetAwaiter().GetResult());
                Assert.IsType<TaskCanceledException>(exception.InnerException);
            }
        }

        [Theory]
        [InlineData("")]
        [InlineData("{}")]
        [InlineData("{\"data\":\"\"}")]
        [InlineData("{\"data\":{}")]
        [InlineData("{\"data\":\"a\"}")]
        public void 不正なJSONを取得_BitbankExceptionをスローする(string content)
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
                Assert.Throws<BitbankException>(() =>
                    bitbank.GetAssetsAsync().GetAwaiter().GetResult());
            }
        }
    }
}