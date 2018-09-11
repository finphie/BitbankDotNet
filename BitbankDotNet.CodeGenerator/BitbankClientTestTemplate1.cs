namespace BitbankDotNet.CodeGenerator
{
    partial class BitbankClientTestTemplate
    {
        public string Json { get; set; }

        public string MethodName { get; set; }

        public string ApiName1 { get; set; }
        public string ApiName2 { get; set; }

        public BitbankClientTestTemplate(string json, string methodName)
        {
            Json = json;

            MethodName = methodName;

            var name = methodName.Replace("Get", "").Replace("Async", "");
            ApiName1 = name;
            ApiName2 = name.ToLower();
        }
    }
}