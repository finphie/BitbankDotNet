﻿using BitbankDotNet.Shared.Extensions;
using BitbankDotNet.Shared.Helpers;
using Microsoft.CSharp;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BitbankDotNet.CodeGenerator
{
    partial class BitbankClientTestTemplate
    {
        static readonly Assembly LibraryAssembly = typeof(BitbankClient).Assembly;

        // GetType()は名前空間付きの型名が必要。こちらの方が簡潔。
        static readonly IEnumerable<TypeInfo> EntityTypes = LibraryAssembly.DefinedTypes;
        static readonly TypeInfo ResponseTypeInfo = EntityTypes.First(ti => ti.Name.StartsWith("Response"));

        public SortedList<string, (string TypeName, SortedList<string, string> Element)> EntityProperties { get; }
        public string Json { get; set; }
        public string MethodName { get; set; }
        public string ApiName { get; set; }

        public bool IsArray { get; set; }
        public bool IsPublicApi { get; }

        public (string Name, string Type)[] Parameters { get; set; }

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
                if (apiName == "Ohlcv")
                    apiName = "Candlestick";
                entityType = EntityTypes.First(t => t.Name == $"{apiName}List");
                IsArray = true;
            }

            EntityProperties = entityElementType.GetProperties()
                .ToSortedList(pi => pi.Name, pi =>
                {
                    var type = pi.PropertyType;
                    var elementTypeName =
                        type.IsArray && type.Namespace == $"{nameof(BitbankDotNet)}.{nameof(Entities)}"
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

            Json = entityResponse.ToString().Replace("\"", @"\""");

            Parameters = method.GetParameters().Select(p => (p.Name, GetTypeOutput(p.ParameterType))).ToArray();
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