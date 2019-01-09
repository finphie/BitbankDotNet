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

namespace BitbankDotNet.Tests.PrivateApis
{
    [SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "ユニットテスト")]
    public class BitbankRestApiClientCancelOrderAsyncTest
    {
        const string Json =
            "{\"success\":1,\"data\":{\"order_id\":4,\"pair\":\"btc_jpy\",\"side\":\"buy\",\"type\":\"limit\",\"start_amount\":\"1.2\",\"remaining_amount\":\"1.2\",\"executed_amount\":\"1.2\",\"price\":\"1.2\",\"average_price\":\"1.2\",\"ordered_at\":1514862245678,\"status\":\"UNFILLED\"}}";

        [Fact]
        public void HTTPステータスが200かつSuccessが1_Orderを返す()
        {
            var mockHttpHandler = new Mock<HttpMessageHandler>();
            mockHttpHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Callback<HttpRequestMessage, CancellationToken>((request, _) =>
                {
                    Assert.StartsWith("https://api.bitbank.cc/v1/", request.RequestUri.AbsoluteUri, StringComparison.Ordinal);
                })
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(Json)
                });

            using (var client = new HttpClient(mockHttpHandler.Object))
            {
                var bitbank = new BitbankRestApiClient(client, " ", " ");
                var result = bitbank.CancelOrderAsync(default, default).GetAwaiter().GetResult();

                Assert.NotNull(result);
                Assert.Equal(EntityHelper.GetTestValue<decimal>(), result.AveragePrice);
                Assert.Equal(EntityHelper.GetTestValue<decimal>(), result.ExecutedAmount);
                Assert.Equal(EntityHelper.GetTestValue<DateTime>(), result.OrderedAt);
                Assert.Equal(EntityHelper.GetTestValue<long>(), result.OrderId);
                Assert.Equal(EntityHelper.GetTestValue<CurrencyPair>(), result.Pair);
                Assert.Equal(EntityHelper.GetTestValue<decimal>(), result.Price);
                Assert.Equal(EntityHelper.GetTestValue<decimal>(), result.RemainingAmount);
                Assert.Equal(EntityHelper.GetTestValue<OrderSide>(), result.Side);
                Assert.Equal(EntityHelper.GetTestValue<decimal>(), result.StartAmount);
                Assert.Equal(EntityHelper.GetTestValue<OrderStatus>(), result.Status);
                Assert.Equal(EntityHelper.GetTestValue<OrderType>(), result.Type);
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
            {
                var bitbank = new BitbankRestApiClient(client, " ", " ");
                var exception = Assert.Throws<BitbankDotNetException>(() =>
                    bitbank.CancelOrderAsync(default, default).GetAwaiter().GetResult());
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
                var bitbank = new BitbankRestApiClient(client, " ", " ");
                var exception = Assert.Throws<BitbankDotNetException>(() =>
                    bitbank.CancelOrderAsync(default, default).GetAwaiter().GetResult());
                Assert.IsType<TaskCanceledException>(exception.InnerException);
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
            {
                var bitbank = new BitbankRestApiClient(client, " ", " ");
                Assert.Throws<BitbankDotNetException>(() =>
                    bitbank.CancelOrderAsync(default, default).GetAwaiter().GetResult());
            }
        }
    }
}