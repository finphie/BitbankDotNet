using System.Collections.Generic;

namespace BitbankDotNet.Extensions
{
    static class DictionaryExtensions
    {
        public static string GetOrDefault<T>(this Dictionary<T, string> dic, T key)
        {
            dic.TryGetValue(key, out var value);
            return value;
        }
    }
}