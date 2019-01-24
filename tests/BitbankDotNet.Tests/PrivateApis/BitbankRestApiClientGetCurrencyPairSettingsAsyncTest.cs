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
    public class BitbankRestApiClientGetCurrencyPairSettingsAsyncTest
    {
        const string Json =
            "{\"success\":1,\"data\":{\"pairs\":[{\"name\":\"btc_jpy\",\"base_asset\":\"jpy\",\"quote_asset\":\"jpy\",\"maker_fee_rate_base\":\"1.2\",\"taker_fee_rate_base\":\"1.2\",\"maker_fee_rate_quote\":\"1.2\",\"taker_fee_rate_quote\":\"1.2\",\"unit_amount\":\"1.2\",\"limit_max_amount\":\"1.2\",\"market_max_amount\":\"1.2\",\"market_allowance_rate\":\"1.2\",\"price_digits\":3,\"amount_digits\":3,\"is_stop_buy\":false,\"is_stop_sell\":false},{\"name\":\"btc_jpy\",\"base_asset\":\"jpy\",\"quote_asset\":\"jpy\",\"maker_fee_rate_base\":\"1.2\",\"taker_fee_rate_base\":\"1.2\",\"maker_fee_rate_quote\":\"1.2\",\"taker_fee_rate_quote\":\"1.2\",\"unit_amount\":\"1.2\",\"limit_max_amount\":\"1.2\",\"market_max_amount\":\"1.2\",\"market_allowance_rate\":\"1.2\",\"price_digits\":3,\"amount_digits\":3,\"is_stop_buy\":false,\"is_stop_sell\":false}]}}";

        [Fact]
        public void HTTPステータスが200かつSuccessが1_CurrencyPairSettingを返す()
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
                var result = bitbank.GetCurrencyPairSettingsAsync().GetAwaiter().GetResult();

                Assert.NotNull(result);
                Assert.All(result, entity =>
                {
                    Assert.Equal(EntityHelper.GetTestValue<int>(), entity.AmountDigits);
                    Assert.Equal(EntityHelper.GetTestValue<AssetName>(), entity.BaseAsset);
                    Assert.Equal(EntityHelper.GetTestValue<bool>(), entity.IsStopBuy);
                    Assert.Equal(EntityHelper.GetTestValue<bool>(), entity.IsStopSell);
                    Assert.Equal(EntityHelper.GetTestValue<decimal>(), entity.LimitMaxAmount);
                    Assert.Equal(EntityHelper.GetTestValue<decimal>(), entity.MakerFeeRateBase);
                    Assert.Equal(EntityHelper.GetTestValue<decimal>(), entity.MakerFeeRateQuote);
                    Assert.Equal(EntityHelper.GetTestValue<decimal>(), entity.MarketAllowanceRate);
                    Assert.Equal(EntityHelper.GetTestValue<decimal>(), entity.MarketMaxAmount);
                    Assert.Equal(EntityHelper.GetTestValue<CurrencyPair>(), entity.Name);
                    Assert.Equal(EntityHelper.GetTestValue<int>(), entity.PriceDigits);
                    Assert.Equal(EntityHelper.GetTestValue<AssetName>(), entity.QuoteAsset);
                    Assert.Equal(EntityHelper.GetTestValue<decimal>(), entity.TakerFeeRateBase);
                    Assert.Equal(EntityHelper.GetTestValue<decimal>(), entity.TakerFeeRateQuote);
                    Assert.Equal(EntityHelper.GetTestValue<decimal>(), entity.UnitAmount);
                });
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
                    bitbank.GetCurrencyPairSettingsAsync().GetAwaiter().GetResult());
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
                    bitbank.GetCurrencyPairSettingsAsync().GetAwaiter().GetResult());
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
                    bitbank.GetCurrencyPairSettingsAsync().GetAwaiter().GetResult());
            }
        }
    }
}