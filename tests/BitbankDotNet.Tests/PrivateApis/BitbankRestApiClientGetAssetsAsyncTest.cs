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
    public class BitbankRestApiClientGetAssetsAsyncTest
    {
        const string Json =
            "{\"success\":1,\"data\":{\"assets\":[{\"asset\":\"jpy\",\"amount_precision\":3,\"onhand_amount\":\"1.2\",\"locked_amount\":\"1.2\",\"free_amount\":\"1.2\",\"withdrawal_fee\":{\"threshold\":\"1.2\",\"under\":\"1.2\",\"over\":\"1.2\"}},{\"asset\":\"jpy\",\"amount_precision\":3,\"onhand_amount\":\"1.2\",\"locked_amount\":\"1.2\",\"free_amount\":\"1.2\",\"withdrawal_fee\":{\"threshold\":\"1.2\",\"under\":\"1.2\",\"over\":\"1.2\"}}]}}";

        [Fact]
        public async Task HTTPステータスが200かつSuccessが1_Assetを返す()
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
            using (var restApi = new BitbankRestApiClient(client, " ", " "))
            {
                var result = await restApi.GetAssetsAsync().ConfigureAwait(false);

                Assert.NotNull(result);
                Assert.All(result, entity =>
                {
                    Assert.Equal(EntityHelper.GetTestValue<int>(), entity.AmountPrecision);
                    Assert.Equal(EntityHelper.GetTestValue<decimal>(), entity.FreeAmount);
                    Assert.Equal(EntityHelper.GetTestValue<decimal>(), entity.LockedAmount);
                    Assert.Equal(EntityHelper.GetTestValue<AssetName>(), entity.Name);
                    Assert.Equal(EntityHelper.GetTestValue<decimal>(), entity.OnhandAmount);
                    Assert.Equal(EntityHelper.GetTestValue<decimal>(), entity.WithdrawalFee.Over);
                    Assert.Equal(EntityHelper.GetTestValue<decimal>(), entity.WithdrawalFee.Threshold);
                    Assert.Equal(EntityHelper.GetTestValue<decimal>(), entity.WithdrawalFee.Under);
                });
            }
        }

        [Theory]
        [InlineData(HttpStatusCode.NotFound, 0, 10000)]
        [InlineData(HttpStatusCode.NotFound, 1, 60003)]
        [InlineData(HttpStatusCode.OK, 0, 70001)]
        public async Task HTTPステータスが404またはSuccessが0_BitbankDotNetExceptionをスローする(HttpStatusCode statusCode, int success, int apiErrorCode)
        {
            var mockHttpHandler = new Mock<HttpMessageHandler>();
            mockHttpHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage(statusCode)
                {
                    Content = new StringContent($"{{\"success\":{success},\"data\":{{\"code\":{apiErrorCode}}}}}")
                });

            using (var client = new HttpClient(mockHttpHandler.Object))
            using (var restApi = new BitbankRestApiClient(client, " ", " "))
            {
                var result = restApi.GetAssetsAsync();
                var exception = await Assert.ThrowsAsync<BitbankDotNetException>(() => result).ConfigureAwait(false);
                Assert.Equal(apiErrorCode, exception.ApiErrorCode);
            }
        }

        [Fact]
        public async Task タイムアウト_BitbankDotNetExceptionをスローする()
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
                using (var restApi = new BitbankRestApiClient(client, " ", " "))
                {
                    var result = restApi.GetAssetsAsync();
                    var exception = await Assert.ThrowsAsync<BitbankDotNetException>(() => result).ConfigureAwait(false);
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
        public async Task 不正なJSONを取得_BitbankDotNetExceptionをスローする(string content)
        {
            var mockHttpHandler = new Mock<HttpMessageHandler>();
            mockHttpHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(content)
                });

            using (var client = new HttpClient(mockHttpHandler.Object))
            using (var restApi = new BitbankRestApiClient(client, " ", " "))
            {
                var result = restApi.GetAssetsAsync();
                await Assert.ThrowsAsync<BitbankDotNetException>(() => result).ConfigureAwait(false);
            }
        }
    }
}