﻿using BitbankDotNet.Entities;
using BitbankDotNet.Extensions;
using BitbankDotNet.Resolvers;
using SpanJson;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BitbankDotNet
{
    /// <summary>
    /// Bitbank REST API Client
    /// </summary>
    public partial class BitbankClient
    {
        // Public API
        const string PublicUrl = "https://public.bitbank.cc/";

        // Private API
        const string PrivateUrl = "https://api.bitbank.cc/v1/";

        readonly HttpClient _client;

        readonly string _apiKey;
        readonly byte[] _apiSecret;

        public BitbankClient(HttpClient client) : this(client, string.Empty, string.Empty)
        {
        }

        public BitbankClient(HttpClient client, string apiKey, string apiSecret)
        {
            _client = client;
            _client.Timeout = TimeSpan.FromSeconds(10);
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // APIキーとAPIシークレットが設定されている場合
            if (!string.IsNullOrEmpty(apiKey) && !string.IsNullOrEmpty(apiSecret))
            {
                _apiKey = apiKey;
                _apiSecret = Encoding.UTF8.GetBytes(apiSecret);
            }
        }

        async Task<T> GetAsync<T>(HttpRequestMessage request)
            where T : class, IResponse
        {
            try
            {
                var response = await _client.SendAsync(request).ConfigureAwait(false);
                var json = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonSerializer.Generic.Utf8.Deserialize<T, BitbankResolver<byte>>(json);
                    if (result.Success == 1)
                        return result;
                }

                Error error;
                try
                {
                    error = JsonSerializer.Generic.Utf8.Deserialize<ErrorResponse, BitbankResolver<byte>>(json).Data;
                }
                catch
                {
                    throw new BitbankApiException(
                        $"JSONデシリアライズでエラーが発生しました。Response StatusCode:{response.StatusCode} ReasonPhrase:{response.ReasonPhrase}");
                }

                throw new BitbankApiException($"ErrorCode:{error.Code}");
            }
            catch (TaskCanceledException ex)
            {
                throw new BitbankApiException("リクエストがタイムアウトしました。", ex);
            }
            catch (HttpRequestException ex)
            {
                throw new BitbankApiException("リクエストに失敗しました。", ex);
            }
        }

        // Public API Getリクエスト
        async Task<T> GetAsync<T>(string path, CurrencyPair pair)
            where T : class, IResponse =>
            await GetAsync<T>(new HttpRequestMessage(HttpMethod.Get, PublicUrl + pair.GetEnumMemberValue() + "/" + path))
            .ConfigureAwait(false);

        // Private API Getリクエスト
        async Task<T> GetAsync<T>(string path)
            where T : class, IResponse
        {
            var timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();

            var request = new HttpRequestMessage(HttpMethod.Get, PrivateUrl + path);
            request.Headers.Add("ACCESS-KEY", _apiKey);
            request.Headers.Add("ACCESS-NONCE", timestamp);
            request.Headers.Add("ACCESS-SIGNATURE", CreateSign(timestamp + "/v1/" + path));

            return await GetAsync<T>(request).ConfigureAwait(false);
        }

        async Task<T> PostAsync<T>(string path)
            where T : class, IResponse
        {
            throw new NotImplementedException();
        }

        string CreateSign(string message)
        {
            using (var hmac = new HMACSHA256(_apiSecret))
            {
                var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(message));

                // TODO: 後で高速化する
                return BitConverter.ToString(hash).ToLower().Replace("-", "");
            }
        }
    }
}