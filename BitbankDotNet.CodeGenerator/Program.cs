using BitbankDotNet.Entities;
using System;
using System.IO;

namespace BitbankDotNet.CodeGenerator
{
    class Program
    {
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

        static void SetValue<T>(T target)
        {
            foreach (var property in typeof(T).GetProperties())
                property.SetValue(target, GetTestValue(property.GetValue(target)));
        }

        static void Main()
        {
            const string methodName = nameof(BitbankClient.GetTickerAsync);

            var tt = new BitbankClientTestTemplate(new Ticker(), methodName);
            var text = tt.TransformText();
            File.WriteAllText(nameof(BitbankClient) + methodName + "Test.cs", text);
        }
    }
}