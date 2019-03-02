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
    public class BitbankRestApiClientRequestWithdrawalAsyncTest
    {
        const string Json =
            "{\"success\":1,\"data\":{\"uuid\":\"a\",\"asset\":\"jpy\",\"account_uuid\":\"a\",\"amount\":\"1.2\",\"fee\":\"1.2\",\"label\":\"a\",\"address\":\"a\",\"txId\":\"a\",\"status\":\"CONFIRMING\",\"requested_at\":1514862245678}}";

        [Fact]
        public async Task HTTPステータスが200かつSuccessが1_Withdrawalを返す()
        {
            var handler = new Mock<HttpMessageHandler>();
            handler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Callback<HttpRequestMessage, CancellationToken>((request, _) =>
                {
                    Assert.StartsWith("https://api.bitbank.cc/v1/", request.RequestUri.AbsoluteUri, StringComparison.Ordinal);
                })
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(Json)
                });

            using var client = new HttpClient(handler.Object);
            using var restApi = new BitbankRestApiClient(client, " ", " ");
            var result = await restApi.RequestWithdrawalAsync(default, default, default, default, default).ConfigureAwait(false);

            Assert.NotNull(result);
            Assert.Equal(EntityHelper.GetTestValue<string>(), result.AccountUuid);
            Assert.Equal(EntityHelper.GetTestValue<string>(), result.Address);
            Assert.Equal(EntityHelper.GetTestValue<decimal>(), result.Amount);
            Assert.Equal(EntityHelper.GetTestValue<AssetName>(), result.Asset);
            Assert.Equal(EntityHelper.GetTestValue<decimal>(), result.Fee);
            Assert.Equal(EntityHelper.GetTestValue<string>(), result.Label);
            Assert.Equal(EntityHelper.GetTestValue<DateTime>(), result.RequestedAt);
            Assert.Equal(EntityHelper.GetTestValue<WithdrawalStatus>(), result.Status);
            Assert.Equal(EntityHelper.GetTestValue<string>(), result.TxId);
            Assert.Equal(EntityHelper.GetTestValue<string>(), result.Uuid);
        }

        [Theory]
        [InlineData(HttpStatusCode.NotFound, 0, 10000)]
        [InlineData(HttpStatusCode.NotFound, 1, 60003)]
        [InlineData(HttpStatusCode.OK, 0, 70001)]
        public async Task HTTPステータスが404またはSuccessが0_BitbankDotNetExceptionをスローする(HttpStatusCode statusCode, int success, int apiErrorCode)
        {
            var handler = new Mock<HttpMessageHandler>();
            handler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage(statusCode)
                {
                    Content = new StringContent($"{{\"success\":{success},\"data\":{{\"code\":{apiErrorCode}}}}}")
                });

            using var client = new HttpClient(handler.Object);
            using var restApi = new BitbankRestApiClient(client, " ", " ");
            var result = restApi.RequestWithdrawalAsync(default, default, default, default, default);

            var exception = await Assert.ThrowsAsync<BitbankDotNetException>(() => result).ConfigureAwait(false);
            Assert.Equal(apiErrorCode, exception.ApiErrorCode);
        }

        [Fact]
        public async Task タイムアウト_BitbankDotNetExceptionをスローする()
        {
            var handler = new Mock<HttpMessageHandler>();
            handler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Throws<TaskCanceledException>();

            using var client = new HttpClient(handler.Object);
            using var restApi = new BitbankRestApiClient(client, " ", " ");
            var result = restApi.RequestWithdrawalAsync(default, default, default, default, default);

            var exception = await Assert.ThrowsAsync<BitbankDotNetException>(() => result).ConfigureAwait(false);
            Assert.IsType<TaskCanceledException>(exception.InnerException);
        }

        [Theory]
        [InlineData("")]
        [InlineData("{}")]
        [InlineData("{\"data\":\"\"}")]
        [InlineData("{\"data\":{}")]
        [InlineData("{\"data\":\"a\"}")]
        public async Task 不正なJSONを取得_BitbankDotNetExceptionをスローする(string content)
        {
            var handler = new Mock<HttpMessageHandler>();
            handler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(content)
                });

            using var client = new HttpClient(handler.Object);
            using var restApi = new BitbankRestApiClient(client, " ", " ");
            var result = restApi.RequestWithdrawalAsync(default, default, default, default, default);

            await Assert.ThrowsAsync<BitbankDotNetException>(() => result).ConfigureAwait(false);
        }
    }
}