﻿using Moq;
using Moq.Protected;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace BitbankDotNet.Tests.PublicApis
{
    public class BitbankClientGetTickerAsyncTest
    {
        const string Json =
            "{\"success\":1,\"data\":{\"sell\":\"76543210.12345678\",\"buy\":\"76543210.12345678\",\"high\":\"76543210.12345678\",\"low\":\"76543210.12345678\",\"last\":\"76543210.12345678\",\"vol\":\"76543210.12345678\",\"timestamp\":1514768461111}}";

        [Theory]
        [InlineData(CurrencyPair.BtcJpy, "btc_jpy")]
        [InlineData(CurrencyPair.LtcBtc, "ltc_btc")]
        [InlineData(CurrencyPair.XrpJpy, "xrp_jpy")]
        [InlineData(CurrencyPair.EthBtc, "eth_btc")]
        [InlineData(CurrencyPair.MonaJpy, "mona_jpy")]
        [InlineData(CurrencyPair.MonaBtc, "mona_btc")]
        [InlineData(CurrencyPair.BccJpy, "bcc_jpy")]
        [InlineData(CurrencyPair.BccBtc, "bcc_btc")]
        public void HTTPステータスが200かつSuccessが1_Tickerを返す(CurrencyPair pair, string pairName)
        {
            var mockHttpHandler = new Mock<HttpMessageHandler>();
            mockHttpHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .Callback<HttpRequestMessage, CancellationToken>((request, _) =>
                {
                    Assert.Equal(HttpMethod.Get, request.Method);
                    Assert.Equal($"https://public.bitbank.cc/{pairName}/ticker", request.RequestUri.AbsoluteUri);
                })
                .Returns(Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(Json)
                }));
            
            using (var client = new HttpClient(mockHttpHandler.Object))
            {
                var bitbank = new BitbankClient(client);
                var ticker = bitbank.GetTickerAsync(pair).GetAwaiter().GetResult();

                Assert.NotNull(ticker);
                Assert.Equal(76543210.12345678, ticker.Sell);
                Assert.Equal(76543210.12345678, ticker.Buy);
                Assert.Equal(76543210.12345678, ticker.High);
                Assert.Equal(76543210.12345678, ticker.Low);
                Assert.Equal(76543210.12345678, ticker.Last);
                Assert.Equal(76543210.12345678, ticker.Vol);
                Assert.Equal(new DateTime(2018, 1, 1, 1, 1, 1, 111), ticker.Timestamp);
            }
        }

        [Theory]
        [InlineData(HttpStatusCode.NotFound, 1)]
        [InlineData(HttpStatusCode.OK, 0)]
        public void HTTPステータスが404またはSuccessが0_BitbankApiExceptionをスローする(HttpStatusCode statusCode, int success)
        {
            var mockHttpHandler = new Mock<HttpMessageHandler>();
            mockHttpHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .Returns(Task.FromResult(new HttpResponseMessage(statusCode)
                {
                    Content = new StringContent($"{{\"success\":{success},\"data\":{{\"code\":10000}}}}")
                }));

            using (var client = new HttpClient(mockHttpHandler.Object))
            {
                var bitbank = new BitbankClient(client);
                Assert.Throws<BitbankApiException>(() =>
                    bitbank.GetTickerAsync(CurrencyPair.BtcJpy).GetAwaiter().GetResult());
            }
        }
    }
}