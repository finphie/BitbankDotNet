using BitbankDotNet.Shared.Helpers;
using Microsoft.CSharp;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace BitbankDotNet.CodeGenerator
{
    partial class BitbankClientTestTemplate
    {
        static readonly Assembly LibraryAssembly = typeof(BitbankClient).Assembly;

        // GetType()は名前空間付きの型名が必要。こちらの方が簡潔。
        static readonly IEnumerable<TypeInfo> EntityTypes = LibraryAssembly.DefinedTypes;

        public string Json { get; set; }
        public string MethodName { get; set; }

        public string ApiName1 { get; set; }
        public string ApiName2 { get; set; }

        public string ParameterString { get; set; }

        public BitbankClientTestTemplate(MethodInfo method)
        {
            MethodName = method.Name;
            var split = Regex.Split(MethodName, "(?<!^)(?=[A-Z])").AsSpan();
            ApiName1 = string.Concat(split.Slice(1, split.Length - 2).ToArray());
            ApiName2 = ApiName1.ToLower();

            var entityType = method.ReturnType.GenericTypeArguments[0];
            // EntityResponseクラスが配列の場合
            if (entityType.IsArray)
                entityType = EntityTypes.First(t => t.Name == $"{ApiName1}List");       

            var entity = Activator.CreateInstance(entityType);
            EntityHelper.SetValue(entity);
            
            var responseType = EntityTypes.First(t => t.Name == $"{ApiName1}Response");
            var entityResponse = Activator.CreateInstance(responseType);
            responseType.GetProperty("Success").SetValue(entityResponse, 1);
            responseType.GetProperty("Data").SetValue(entityResponse, entity);

            Json = entityResponse.ToString().Replace("\"", @"\""");

            ParameterString = GetParameterString(method);
        }

        // メソッドの引数を文字列として取得する
        static string GetParameterString(MethodBase method)
            => string.Join(", ", method.GetParameters().Select(p => $"{GetTypeOutput(p.ParameterType)} {p.Name}"));

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