using BitbankDotNet.Entities;
using System.IO;

namespace BitbankDotNet.CodeGenerator
{
    class Program
    {
        static void Main()
        {
            const string methodName = nameof(BitbankClient.GetTickerAsync);

            var tt = new BitbankClientTestTemplate(typeof(Ticker), methodName);
            var text = tt.TransformText();
            File.WriteAllText(nameof(BitbankClient) + methodName + "Test.cs", text);
        }
    }
}