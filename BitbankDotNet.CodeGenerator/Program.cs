using BitbankDotNet.Entities;
using System;
using System.IO;

namespace BitbankDotNet.CodeGenerator
{
    class Program
    {
        static void SetValue<T>(T target)
        {
            var properties = typeof(T).GetProperties();

            foreach (var property in properties)
            {
                switch (property.GetValue(target))
                {
                    case double _:
                        property.SetValue(target, 76543210.12345678);
                        break;
                    case string _:
                        property.SetValue(target, "abc");
                        break;
                    case DateTime _:
                        property.SetValue(target, new DateTime(2018, 1, 1, 1, 1, 1, 111));
                        break;
                    default:
                        throw new NotImplementedException(property.PropertyType.Name);
                }
            }
        }

        static void Main()
        {
            const string methodName = nameof(BitbankClient.GetTickerAsync);

            var ticker = new Ticker();
            SetValue(ticker);

            var json = ticker.ToString().Replace("\"", @"\""");

            var tt = new BitbankClientTestTemplate(json, methodName);
            var text = tt.TransformText();
            File.WriteAllText(nameof(BitbankClient) + methodName + "Test.cs", text);
        }
    }
}