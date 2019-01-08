using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BitbankDotNet.CodeGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
                return;

            var path = args.FirstOrDefault();
            var outDirectoryPath = args.ElementAtOrDefault(1);

            if (!Directory.Exists(path) || !Directory.Exists(outDirectoryPath))
                return;

            var files = Directory.EnumerateFiles(Path.Combine(path, "PublicApis"))
                .Concat(Directory.EnumerateFiles(Path.Combine(path, "PrivateApis")))
                .Select(s => File.ReadAllText(s));

            // コメント取得
            var dic = GetCommentSummary(string.Concat(files));

            // メソッド一覧を取得
            var methods = typeof(BitbankRestApiClient).GetMethods()
                .Where(mi => mi.IsPublic && !mi.IsVirtual)
                .Where(mi => mi.Name != "GetType");

            foreach (var group in methods.GroupBy(mi => mi.Name))
            {
                var key = group.Key;
                Console.WriteLine(key);
                var method = group.OrderByDescending(mi => mi.GetParameters().Length);
                var isPublicApi = dic[key];
                var tt = new BitbankRestApiClientTestTemplate(method.First(), isPublicApi);
                var text = tt.TransformText();
                var outPath = Path.Combine(outDirectoryPath, $"{(isPublicApi ? "Public" : "Private")}Apis", $"{nameof(BitbankRestApiClient)}{key}Test.cs");
                File.WriteAllText(Path.GetFullPath(outPath), text, Encoding.UTF8);
            }
        }

        /// <summary>
        /// コメントの概要を取得します。
        /// </summary>
        /// <param name="text">コード</param>
        /// <returns>コメントの概要</returns>
        static Dictionary<string, bool> GetCommentSummary(string text)
        {
            // Roslynによる構文解析
            var tree = CSharpSyntaxTree.ParseText(text);
            var methodDeclarations = tree.GetRoot().DescendantNodes()
                .OfType<MethodDeclarationSyntax>()
                .GroupBy(m => m.Identifier.ValueText);
            var compilation = CSharpCompilation.Create("compilation", new[] { tree });
            var semanticModel = compilation.GetSemanticModel(compilation.SyntaxTrees[0], true);

            // コメント取得
            var dic = new Dictionary<string, bool>();
            foreach (var group in methodDeclarations)
            {
                var symbol = semanticModel.GetDeclaredSymbol(group.First());
                var comment = symbol.GetDocumentationCommentXml();
                var summary = XDocument.Parse(comment).Descendants("summary").First().Value;

                dic.Add(group.Key, Regex.Match(summary, @"\[.*?\]").Value.Contains("Public API", StringComparison.Ordinal));
            }

            return dic;
        }
    }
}