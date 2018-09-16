using BitbankDotNet.Shared.Helpers;
using Microsoft.CSharp;
using System;
using System.CodeDom;
using System.Linq;
using System.Reflection;

namespace BitbankDotNet.CodeGenerator
{
    partial class BitbankClientTestTemplate
    {
        public object Entity { get; set; }
        public PropertyInfo[] Properties { get; set; }

        public string Json { get; set; }
        public string MethodName { get; set; }

        public string ApiName1 { get; set; }
        public string ApiName2 { get; set; }
        public string VariableName { get; set; }

        public string ParameterString { get; set; }

        public BitbankClientTestTemplate(MethodInfo method)
        {
            var entityType = method.ReturnType.GenericTypeArguments[0];
            if (entityType.IsArray)
            {
                entityType = entityType.GetElementType();
                VariableName = "[0]";
            }

            Entity = Activator.CreateInstance(entityType);
            EntityHelper.SetValue(Entity);

            Properties = Entity.GetType().GetProperties();

            Json = Entity.ToString().Replace("\"", @"\""");
            MethodName = method.Name;

            ApiName1 = entityType.Name;
            ApiName2 = ApiName1.ToLower();
            VariableName = ApiName2 + VariableName;

            ParameterString = GetParameterString(method);
        }

        // メソッドの引数を文字列として取得する
        static string GetParameterString(MethodBase method)
            => string.Join(", ", method.GetParameters().Select(p => $"{GetTypeOutput(p.ParameterType)} {p.Name}"));

        static string GetTestValueString(object property)
        {
            string F(string s) => $"\"{s}\"";

            switch (property)
            {
                case double _:
                case int _:
                case long _:
                    return property.ToString();
                case string s:
                    return F(s);
                case DateTime date:
                    return $"{nameof(DateTime)}.{nameof(DateTime.Parse)}({F($"{date:O}")})";
                case AssetName _:
                    return $"{nameof(AssetName)}.{nameof(AssetName.Jpy)}";
                default:
                    throw new NotImplementedException(property.GetType().Name);
            }
        }

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