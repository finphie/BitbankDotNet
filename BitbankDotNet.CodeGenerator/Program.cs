using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

[assembly: CLSCompliant(true)]

namespace BitbankDotNet.CodeGenerator
{
    class Program
    {
        static void Main()
        {
            var path = $"../../../../{nameof(BitbankDotNet)}";
            var files = Directory.EnumerateFiles(path + "/PublicApis")
                .Concat(Directory.EnumerateFiles(path + "/PrivateApis"))
                .Select(s => File.ReadAllText(s));

            // Roslynによる構文解析
            var tree = CSharpSyntaxTree.ParseText(string.Concat(files));
            var methodDeclarations = tree.GetRoot().DescendantNodes()
                .OfType<MethodDeclarationSyntax>()
                .GroupBy(m => m.Identifier.ValueText);
            var compilation = CSharpCompilation.Create("compilation", new[] {tree});
            var semanticModel = compilation.GetSemanticModel(compilation.SyntaxTrees[0], true);

            // コメント取得
            var dic = new Dictionary<string, bool>();
            foreach (var group in methodDeclarations)
            {
                var symbol = semanticModel.GetDeclaredSymbol(group.First());
                var comment = symbol.GetDocumentationCommentXml();
                var summary = XDocument.Parse(comment).Descendants("summary").First().Value;

                dic.Add(group.Key, Regex.Match(summary, @"\[.*?\]").Value.Contains("Public API"));
            }

            // メソッド一覧を取得
            var methods = typeof(BitbankRestApiClient).GetMethods()
                .Where(mi => mi.IsPublic && !mi.IsVirtual)
                .Where(mi => mi.Name != "GetType");

            foreach (var group in methods.GroupBy(mi => mi.Name))
            {
                Console.WriteLine(group.Key);
                var method = group.OrderByDescending(mi => mi.GetParameters().Length);
                var isPublicApi = dic[group.Key];
                var tt = new BitbankRestApiClientTestTemplate(method.First(), isPublicApi);
                var text = tt.TransformText();
                var outDirectoryPath = path + ".Tests/" + (isPublicApi ? "Public" : "Private") + "Apis/";
                var outPath = Path.GetFullPath($"{outDirectoryPath}{nameof(BitbankRestApiClient)}{group.Key}Test.cs");
                File.WriteAllText(outPath, text, Encoding.UTF8);
            }
        }
    }
}