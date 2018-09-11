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
                var type = property.PropertyType;          
                if (type == typeof(double))
                    property.SetValue(target, 76543210.12345678);
                else if (type == typeof(string))
                    property.SetValue(target, "abc");
                else if (type == typeof(DateTime))
                    property.SetValue(target, new DateTime(2018, 1, 1, 1, 1, 1, 111));
                else
                    throw new NotImplementedException(type.Name);
            }
        }

        static void Main(string[] args)
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