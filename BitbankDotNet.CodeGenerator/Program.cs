using System;
using System.IO;
using System.Linq;

namespace BitbankDotNet.CodeGenerator
{
    class Program
    {
        static void Main()
        {
            // メソッド一覧を取得
            var methods = typeof(BitbankClient).GetMethods()
                .Where(m => m.IsPublic && !m.IsVirtual)
                .Where(m => m.Name != "GetType");

            foreach (var group in methods.GroupBy(m => m.Name))
            {
                Console.WriteLine(group.Key);
                var tt = new BitbankClientTestTemplate(group.First());
                var text = tt.TransformText();
                File.WriteAllText(nameof(BitbankClient) + group.Key + "Test.cs", text);
            }
        }
    }
}