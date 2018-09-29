using BitbankDotNet.Entities;
using BitbankDotNet.Resolvers;
using BitbankDotNet.Shared.Extensions;
using BitbankDotNet.Shared.Helpers;
using Microsoft.CSharp;
using SpanJson;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BitbankDotNet.CodeGenerator
{
    partial class BitbankClientTestTemplate
    {
        // Entityの名前空間名
        const string EntityNamespace = nameof(BitbankDotNet) + "." + nameof(Entities);

        // BitbankClientがあるアセンブリ
        static readonly Assembly LibraryAssembly = typeof(BitbankClient).Assembly;

        // Entityの型情報リスト
        static readonly TypeInfo[] EntityTypeInfos = LibraryAssembly.DefinedTypes
            .Where(ti=> ti.Namespace == EntityNamespace)
            .ToArray();

        // Response<T>の型情報
        static readonly TypeInfo ResponseTypeInfo = EntityTypeInfos.First(ti => ti.Name == typeof(Response<>).Name);

        public SortedList<string, (string TypeName, SortedList<string, string> Element)> EntityProperties { get; }
        public string Json { get; }
        public string MethodName { get; }
        public string ApiName { get; }

        public bool IsArray { get; }
        public bool IsPublicApi { get; }

        public (string Name, string Type)[] Parameters { get; }

        public BitbankClientTestTemplate(MethodInfo method, bool isPublicApi)
        {
            IsPublicApi = isPublicApi;
            MethodName = method.Name;

            var entityType = method.ReturnType.GenericTypeArguments[0];
            var entityElementType = entityType;

            ApiName = entityType.Name;

            // EntityResponseクラスが配列の場合
            if (entityType.IsArray)
            {
                entityElementType = entityType.GetElementType();
                var apiName = ApiName = entityElementType.Name;
                if (apiName == nameof(Ohlcv))
                    apiName = nameof(Candlestick);
                entityType = EntityTypeInfos.First(ti => ti.Name == $"{apiName}List");
                IsArray = true;
            }

            EntityProperties = entityElementType.GetProperties()
                .ToSortedList(pi => pi.Name, pi =>
                {
                    var type = pi.PropertyType;
                    var elementTypeName = type.IsArray && type.Namespace == EntityNamespace
                        ? type.GetElementType()
                        : null;
                    var elementProperties = elementTypeName?.GetProperties()
                        .ToSortedList(pi2 => pi2.Name, pi2 => GetTypeOutput(pi2.PropertyType));
                    return (GetTypeOutput(pi.PropertyType), elementProperties);
                });

            var entity = Activator.CreateInstance(entityType);
            EntityHelper.SetValue(entity);

            var responseType = ResponseTypeInfo.MakeGenericType(entityType);
            var entityResponse = Activator.CreateInstance(responseType);
            responseType.GetProperty("Success").SetValue(entityResponse, 1);
            responseType.GetProperty("Data").SetValue(entityResponse, entity);

            Json = JsonSerializer.NonGeneric.Utf16.Serialize<BitbankResolver<char>>(entityResponse)
                .Replace("\"", @"\""");

            Parameters = method.GetParameters().Select(pi => (pi.Name, GetTypeOutput(pi.ParameterType))).ToArray();
        }

        string GetDefaultParametersString()
            => string.Join(", ", Enumerable.Repeat("default", Parameters.Length));

        // 指定した型のエイリアスを取得する
        static string GetTypeOutput(Type type)
        {
            using (var provider = new CSharpCodeProvider())
            {
                var typeRef = new CodeTypeReference(type);
                var typeName = provider.GetTypeOutput(typeRef);

                // エイリアスがない型だと、名前空間付きで出力されてしまうので削除
                return typeName.Split('.').Last();
            }
        }
    }
}