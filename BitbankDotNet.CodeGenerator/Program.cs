using BitbankDotNet.Entities;
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

            // とりあえずGetTickerAsyncのみ
            var method = methods.First(m => m.Name == nameof(BitbankClient.GetTickerAsync));

            var tt = new BitbankClientTestTemplate(typeof(Ticker), method);
            var text = tt.TransformText();
            File.WriteAllText(nameof(BitbankClient) + method.Name + "Test.cs", text);

        }
    }
}