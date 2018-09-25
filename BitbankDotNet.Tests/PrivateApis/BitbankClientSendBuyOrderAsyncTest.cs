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
    public class BitbankClientSendBuyOrderAsyncTest
    {
        const string Json =
            "{\"success\":1,\"data\":{\"order_id\":4,\"pair\":\"btc_jpy\",\"side\":\"buy\",\"type\":\"limit\",\"start_amount\":\"1.2\",\"remaining_amount\":\"1.2\",\"executed_amount\":\"1.2\",\"price\":\"1.2\",\"average_price\":\"1.2\",\"ordered_at\":1514862245678,\"status\":\"UNFILLED\"}}";

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
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(Json)
                });
            
            using (var client = new HttpClient(mockHttpHandler.Object))
            {
				var bitbank = new BitbankClient(client, " ", " ");
                var result = bitbank.SendBuyOrderAsync(default, default, default).GetAwaiter().GetResult();

                Assert.NotNull(result);
				
				var entity = new Order
                {
                    OrderId = EntityHelper.GetTestValue<long>(),
                    Pair = EntityHelper.GetTestValue<CurrencyPair>(),
                    Side = EntityHelper.GetTestValue<OrderSide>(),
                    Type = EntityHelper.GetTestValue<OrderType>(),
                    StartAmount = EntityHelper.GetTestValue<double>(),
                    RemainingAmount = EntityHelper.GetTestValue<double>(),
                    ExecutedAmount = EntityHelper.GetTestValue<double>(),
                    Price = EntityHelper.GetTestValue<double>(),
                    AveragePrice = EntityHelper.GetTestValue<double>(),
                    OrderedAt = EntityHelper.GetTestValue<DateTime>(),
                    Status = EntityHelper.GetTestValue<OrderStatus>(),
                };
				Assert.Equal(entity, result, new PublicPropertyComparer<Order>());
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
                    bitbank.SendBuyOrderAsync(default, default, default).GetAwaiter().GetResult());
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
                    bitbank.SendBuyOrderAsync(default, default, default).GetAwaiter().GetResult());
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
                    bitbank.SendBuyOrderAsync(default, default, default).GetAwaiter().GetResult());
            }
        }
    }
}