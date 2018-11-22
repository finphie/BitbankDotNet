using BitbankDotNet.Shared.Helpers;
using Moq;
using Moq.Protected;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace BitbankDotNet.Tests.PublicApis
{
    public class BitbankRestApiClientGetCandlesticksAsyncTest
    {
        const string Json =
            "{\"success\":1,\"data\":{\"candlestick\":[{\"type\":\"1min\",\"ohlcv\":[[\"1.2\",\"1.2\",\"1.2\",\"1.2\",\"1.2\",1514862245678],[\"1.2\",\"1.2\",\"1.2\",\"1.2\",\"1.2\",1514862245678]]},{\"type\":\"1min\",\"ohlcv\":[[\"1.2\",\"1.2\",\"1.2\",\"1.2\",\"1.2\",1514862245678],[\"1.2\",\"1.2\",\"1.2\",\"1.2\",\"1.2\",1514862245678]]}]}}";

        [Fact]
        public void HTTPステータスが200かつSuccessが1_Ohlcvを返す()
        {
            var mockHttpHandler = new Mock<HttpMessageHandler>();
            mockHttpHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .Callback<HttpRequestMessage, CancellationToken>((request, _) =>
                {
                    Assert.StartsWith("https://public.bitbank.cc/", request.RequestUri.AbsoluteUri);
                })
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(Json)
                });
            
            using (var client = new HttpClient(mockHttpHandler.Object))
            {
                var bitbank = new BitbankRestApiClient(client);
                var result = bitbank.GetCandlesticksAsync(default, default, default, default, default).GetAwaiter().GetResult();

                Assert.NotNull(result);
                Assert.All(result, entity =>
                {
                    Assert.Equal(EntityHelper.GetTestValue<double>(), entity.Close);
                    Assert.Equal(EntityHelper.GetTestValue<DateTime>(), entity.Date);
                    Assert.Equal(EntityHelper.GetTestValue<double>(), entity.High);
                    Assert.Equal(EntityHelper.GetTestValue<double>(), entity.Low);
                    Assert.Equal(EntityHelper.GetTestValue<double>(), entity.Open);
                    Assert.Equal(EntityHelper.GetTestValue<double>(), entity.Volume);
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
                var bitbank = new BitbankRestApiClient(client);
                var exception = Assert.Throws<BitbankException>(() =>
                    bitbank.GetCandlesticksAsync(default, default, default, default, default).GetAwaiter().GetResult());
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
                var bitbank = new BitbankRestApiClient(client, TimeSpan.FromMilliseconds(1));
                var exception = Assert.Throws<BitbankException>(() =>
                    bitbank.GetCandlesticksAsync(default, default, default, default, default).GetAwaiter().GetResult());
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
                var bitbank = new BitbankRestApiClient(client);
                Assert.Throws<BitbankException>(() =>
                    bitbank.GetCandlesticksAsync(default, default, default, default, default).GetAwaiter().GetResult());
            }
        }
    }
}