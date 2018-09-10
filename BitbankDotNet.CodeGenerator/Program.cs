using System.IO;

namespace BitbankDotNet.CodeGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            const string methodName = nameof(BitbankClient.GetTickerAsync);

            var tt = new BitbankClientTestTemplate(methodName);
            var text = tt.TransformText();
            File.WriteAllText(nameof(BitbankClient) + methodName + "Test.cs", text);
        }
    }
}