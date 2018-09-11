using BitbankDotNet.Entities;
using System;
using System.IO;

namespace BitbankDotNet.CodeGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            const string methodName = nameof(BitbankClient.GetTickerAsync);

            var json = new Ticker
            {
                Sell = 76543210.12345678,
                Buy = 76543210.12345678,
                High = 76543210.12345678,
                Low = 76543210.12345678,
                Last = 76543210.12345678,
                Vol = 76543210.12345678,
                Timestamp = new DateTime(2018, 1, 1, 1, 1, 1, 111)
            }.ToString().Replace("\"", @"\""");

            var tt = new BitbankClientTestTemplate(json, methodName);
            var text = tt.TransformText();
            File.WriteAllText(nameof(BitbankClient) + methodName + "Test.cs", text);
        }
    }
}