using System;
using System.Reflection;

namespace BitbankDotNet.CodeGenerator
{
    partial class BitbankClientTestTemplate
    {
        public dynamic Entity { get; set; }
        public PropertyInfo[] Properties { get; set; }

        public string Json { get; set; }
        public string MethodName { get; set; }

        public string ApiName1 { get; set; }
        public string ApiName2 { get; set; }

        public BitbankClientTestTemplate(object entity, string methodName)
        {
            SetValue(entity);
            Entity = entity;

            Properties = Entity.GetType().GetProperties();

            Json = Entity.ToString().Replace("\"", @"\""");
            MethodName = methodName;

            var name = methodName.Replace("Get", "").Replace("Async", "");
            ApiName1 = name;
            ApiName2 = name.ToLower();
        }

        static object GetTestValue<T>(T property)
        {
            switch (property)
            {
                case double _:
                    return 76543210.12345678;
                case string _:
                    return "abc";
                case DateTime _:
                    return new DateTime(2018, 1, 1, 1, 1, 1, 111);
                default:
                    throw new NotImplementedException(typeof(T).Name);
            }
        }

        static string GetTestValueString<T>(T property)
        {
            string F(string s) => $"\"{s}\"";

            switch (property)
            {
                case double _:
                    return property.ToString();
                case string s:
                    return F(s);
                case DateTime date:
                    return $"{nameof(DateTime)}.{nameof(DateTime.Parse)}({F($"{date:O}")})";
                default:
                    throw new NotImplementedException(typeof(T).Name);
            }
        }

        static void SetValue<T>(T target)
        {
            foreach (var property in typeof(T).GetProperties())
                property.SetValue(target, GetTestValue(property.GetValue(target)));
        }
    }
}