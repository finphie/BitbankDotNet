# BitbankDotNet

.Net Core用の[Bitbank.cc](https://bitbank.cc) APIライブラリです。

## 説明

BitbankDotNetは、C#で開発している.Net Core用の[Bitbank.cc](https://bitbank.cc) APIライブラリです。

パフォーマンスを重視して開発しています。

## 依存ライブラリ

- [.NET Core](https://dotnet.microsoft.com/download) 2.2.0+
- System.Runtime.CompilerServices.Unsafe 4.5.2
- [SpanJson](https://github.com/Tornhoof/SpanJson) 2.0.0

## 使い方

### Public API

```cs
using System.Net.Http;
using BitbankDotNet;

var client = new HttpClient();
var restApi = new BitbankRestApiClient(client);
```

### Private API

```cs
using System.Net.Http;
using BitbankDotNet;

var apiKey = "API Key";
var apiSecret = "API Secret";

var client = new HttpClient();
var restApi = new BitbankRestApiClient(client, apiKey, apiSecret);
```

## 使用言語

- C# 7.3
- [PowerShell Core](https://github.com/PowerShell/PowerShell) 6.1

## 開発環境

- Visual Studio 2017 version 15.9
- Visual Studio Code

## 作者

finphie

## ライセンス

MIT

## 参考

- Bitbank.cc API - <https://docs.bitbank.cc>