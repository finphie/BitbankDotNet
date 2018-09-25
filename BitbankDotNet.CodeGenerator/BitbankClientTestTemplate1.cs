using BitbankDotNet.Shared.Extensions;
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

        readonly string _responseName = "Response";

        public SortedDictionary<string, string> EntityProperties { get; }
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

            var apiName = ApiName = entityType.Name;

            // EntityResponseクラスが配列の場合
            if (entityType.IsArray)
            {
                _responseName = "s" + _responseName;
                entityElementType = entityType.GetElementType();
                ApiName = apiName = entityElementType.Name;
                if (apiName == "Ohlcv")
                    apiName = "Candlestick";
                entityType = EntityTypes.First(t => t.Name == $"{apiName}List");
                IsArray = true;
            }

            EntityProperties = entityElementType.GetProperties()
                .ToSortedDictionary(pi => pi.Name, pi => GetTypeOutput(pi.PropertyType));

            var entity = Activator.CreateInstance(entityType);
            EntityHelper.SetValue(entity);
            
            var responseType = EntityTypes.First(t => t.Name == $"{apiName}{_responseName}");
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