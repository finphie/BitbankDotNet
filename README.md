# BitbankDotNet

.NET Core用の[Bitbank.cc](https://bitbank.cc) APIライブラリです。

## 説明

BitbankDotNetは、C#で開発している.NET Core用の[Bitbank.cc](https://bitbank.cc) APIライブラリです。

パフォーマンスを重視して開発しています。

## 依存ライブラリ

### BitbankDotNet（ライブラリ本体のプロジェクト）

- [.NET Core](https://dotnet.microsoft.com/download) 2.2.0+
- [System.Runtime.CompilerServices.Unsafe](https://www.nuget.org/packages/System.Runtime.CompilerServices.Unsafe) 4.5.2
- [SpanJson](https://github.com/Tornhoof/SpanJson) 2.0.4

### BitbankDotNet.CodeGenerator（コード生成のプロジェクト）

- [.NET Core](https://dotnet.microsoft.com/download) 2.2.0+
- [Microsoft.CodeAnalysis.CSharp](https://github.com/dotnet/roslyn) 2.10.0
- [System.CodeDom](https://www.nuget.org/packages/System.CodeDom) 4.5.0

### BitbankDotNet.InternalShared（ライブラリ本体以外との共有プロジェクト）

- [.NET Standard](https://github.com/dotnet/standard) 2.0+

### BitbankDotNet.Tests（ライブラリ本体のユニットテストプロジェクト）

- [.NET Core](https://dotnet.microsoft.com/download) 2.2.0+
- [Microsoft.NET.Test.Sdk](https://github.com/microsoft/vstest) 15.9.0
- [Moq](https://github.com/moq/moq4) 4.10.1
- [xUnit.net](https://github.com/xunit/xunit) 2.4.1

## インストール

コマンドラインやNuGetパッケージマネージャーを使用してインストールできます。

### コマンドライン

```shell
dotnet add package BitbankDotNet
```

### NuGetパッケージマネージャー

[BitbankDotNet](https://www.nuget.org/packages/BitbankDotNet)をインストールしてください。

## 使い方

### Public API

```csharp
using System.Net.Http;
using BitbankDotNet;

var client = new HttpClient();
var restApi = new BitbankRestApiClient(client);
```

### Private API

```csharp
using System.Net.Http;
using BitbankDotNet;

var apiKey = "API Key";
var apiSecret = "API Secret";

var client = new HttpClient();
var restApi = new BitbankRestApiClient(client, apiKey, apiSecret);
```

## サンプル

- [BitbankApiTool](https://github.com/finphie/BitbankApiTool)

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