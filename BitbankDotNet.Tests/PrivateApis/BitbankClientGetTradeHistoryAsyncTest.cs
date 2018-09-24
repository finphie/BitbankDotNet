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

namespace BitbankDotNet.Tests.PrivateApis
{
    public class BitbankClientGetTradeHistoryAsyncTest
    {
        const string Json =
            "{\"success\":1,\"data\":{\"trades\":[{\"trade_id\":4,\"pair\":\"btc_jpy\",\"order_id\":4,\"side\":\"buy\",\"type\":\"limit\",\"amount\":\"1.2\",\"price\":\"1.2\",\"maker_taker\":\"a\",\"fee_amount_base\":\"a\",\"fee_amount_quote\":\"a\",\"executed_at\":1514862245678},{\"trade_id\":4,\"pair\":\"btc_jpy\",\"order_id\":4,\"side\":\"buy\",\"type\":\"limit\",\"amount\":\"1.2\",\"price\":\"1.2\",\"maker_taker\":\"a\",\"fee_amount_base\":\"a\",\"fee_amount_quote\":\"a\",\"executed_at\":1514862245678}]}}";

        [Fact]
        public void HTTPステータスが200かつSuccessが1_Tradeを返す()
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
                var result = bitbank.GetTradeHistoryAsync(default, default, default, default, default, default).GetAwaiter().GetResult();

                Assert.NotNull(result);
				
				var entity = new Trade();
				EntityHelper.SetValue(entity);
				Assert.Equal(Enumerable.Repeat(entity, 2).ToArray(), result, new PublicPropertyComparer<Trade[]>());
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
                    bitbank.GetTradeHistoryAsync(default, default, default, default, default, default).GetAwaiter().GetResult());
            }
        }
    }
}