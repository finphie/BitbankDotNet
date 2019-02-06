using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using BitbankDotNet.InternalShared.Helpers;
using Moq;
using Moq.Protected;
using Xunit;

namespace BitbankDotNet.Tests.PublicApis
{
    [SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "ユニットテスト")]
    public class BitbankRestApiClientGetTickerAsyncTest
    {
        const string Json =
            "{\"success\":1,\"data\":{\"sell\":\"1.2\",\"buy\":\"1.2\",\"high\":\"1.2\",\"low\":\"1.2\",\"last\":\"1.2\",\"vol\":\"1.2\",\"timestamp\":1514862245678}}";

        [Fact]
        public void HTTPステータスが200かつSuccessが1_Tickerを返す()
        {
            var mockHttpHandler = new Mock<HttpMessageHandler>();
            mockHttpHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Callback<HttpRequestMessage, CancellationToken>((request, _) =>
                {
                    Assert.StartsWith("https://public.bitbank.cc/", request.RequestUri.AbsoluteUri, StringComparison.Ordinal);
                })
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(Json)
                });

            using (var client = new HttpClient(mockHttpHandler.Object))
            using (var restApi = new BitbankRestApiClient(client))
            {
                var result = restApi.GetTickerAsync(default).GetAwaiter().GetResult();

                Assert.NotNull(result);
                Assert.Equal(EntityHelper.GetTestValue<decimal>(), result.Buy);
                Assert.Equal(EntityHelper.GetTestValue<decimal>(), result.High);
                Assert.Equal(EntityHelper.GetTestValue<decimal>(), result.Last);
                Assert.Equal(EntityHelper.GetTestValue<decimal>(), result.Low);
                Assert.Equal(EntityHelper.GetTestValue<decimal>(), result.Sell);
                Assert.Equal(EntityHelper.GetTestValue<DateTime>(), result.Timestamp);
                Assert.Equal(EntityHelper.GetTestValue<decimal>(), result.Vol);
            }
        }

        [Theory]
        [InlineData(HttpStatusCode.NotFound, 0, 10000)]
        [InlineData(HttpStatusCode.NotFound, 1, 60003)]
        [InlineData(HttpStatusCode.OK, 0, 70001)]
        public void HTTPステータスが404またはSuccessが0_BitbankDotNetExceptionをスローする(HttpStatusCode statusCode, int success, int apiErrorCode)
        {
            var mockHttpHandler = new Mock<HttpMessageHandler>();
            mockHttpHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage(statusCode)
                {
                    Content = new StringContent($"{{\"success\":{success},\"data\":{{\"code\":{apiErrorCode}}}}}")
                });

            using (var client = new HttpClient(mockHttpHandler.Object))
            using (var restApi = new BitbankRestApiClient(client))
            {
                var exception = Assert.Throws<BitbankDotNetException>(() =>
                    restApi.GetTickerAsync(default).GetAwaiter().GetResult());
                Assert.Equal(apiErrorCode, exception.ApiErrorCode);
            }
        }

        [Fact]
        public void タイムアウト_BitbankDotNetExceptionをスローする()
        {
            var mockHttpHandler = new Mock<HttpMessageHandler>();
            mockHttpHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
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
                client.Timeout = TimeSpan.FromMilliseconds(1);
                using (var restApi = new BitbankRestApiClient(client))
                {
                    var exception = Assert.Throws<BitbankDotNetException>(() =>
                        restApi.GetTickerAsync(default).GetAwaiter().GetResult());
                    Assert.IsType<TaskCanceledException>(exception.InnerException);
                }
            }
        }

        [Theory]
        [InlineData("")]
        [InlineData("{}")]
        [InlineData("{\"data\":\"\"}")]
        [InlineData("{\"data\":{}")]
        [InlineData("{\"data\":\"a\"}")]
        public void 不正なJSONを取得_BitbankDotNetExceptionをスローする(string content)
        {
            var mockHttpHandler = new Mock<HttpMessageHandler>();
            mockHttpHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(content)
                });

            using (var client = new HttpClient(mockHttpHandler.Object))
            using (var restApi = new BitbankRestApiClient(client))
            {
                Assert.Throws<BitbankDotNetException>(() =>
                    restApi.GetTickerAsync(default).GetAwaiter().GetResult());
            }
        }
    }
}