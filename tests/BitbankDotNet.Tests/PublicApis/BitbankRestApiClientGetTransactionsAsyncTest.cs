﻿using System;
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
    public class BitbankRestApiClientGetTransactionsAsyncTest
    {
        const string Json =
            "{\"success\":1,\"data\":{\"transactions\":[{\"transaction_id\":3,\"side\":\"buy\",\"price\":\"1.2\",\"amount\":\"1.2\",\"executed_at\":1514862245678},{\"transaction_id\":3,\"side\":\"buy\",\"price\":\"1.2\",\"amount\":\"1.2\",\"executed_at\":1514862245678}]}}";

        [Fact]
        public async Task HTTPステータスが200かつSuccessが1_Transactionを返す()
        {
            var handler = new Mock<HttpMessageHandler>();
            handler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Callback<HttpRequestMessage, CancellationToken>((request, _) =>
                {
                    Assert.StartsWith("https://public.bitbank.cc/", request.RequestUri.AbsoluteUri, StringComparison.Ordinal);
                })
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(Json)
                });

            using var client = new HttpClient(handler.Object);
            using var restApi = new BitbankRestApiClient(client);
            var result = await restApi.GetTransactionsAsync(default, default, default, default).ConfigureAwait(false);

            Assert.NotNull(result);
            Assert.All(result, entity =>
            {
                Assert.Equal(EntityHelper.GetTestValue<decimal>(), entity.Amount);
                Assert.Equal(EntityHelper.GetTestValue<DateTime>(), entity.ExecutedAt);
                Assert.Equal(EntityHelper.GetTestValue<decimal>(), entity.Price);
                Assert.Equal(EntityHelper.GetTestValue<OrderSide>(), entity.Side);
                Assert.Equal(EntityHelper.GetTestValue<int>(), entity.TransactionId);
            });
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
            using var restApi = new BitbankRestApiClient(client);
            var result = restApi.GetTransactionsAsync(default, default, default, default);

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
            using var restApi = new BitbankRestApiClient(client);
            var result = restApi.GetTransactionsAsync(default, default, default, default);

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
            using var restApi = new BitbankRestApiClient(client);
            var result = restApi.GetTransactionsAsync(default, default, default, default);

            await Assert.ThrowsAsync<BitbankDotNetException>(() => result).ConfigureAwait(false);
        }
    }
}