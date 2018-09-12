using System;
using System.Reflection;
using System.Runtime.Serialization;

namespace BitbankDotNet.Extensions
{
    public static class EnumExtensions
    {
        public static string GetEnumMemberValue<T>(this T value)
            where T : struct, Enum
        {
            var name = value.ToString();
            return typeof(T)
                .GetField(value.ToString())
                .GetCustomAttribute<EnumMemberAttribute>()?.Value ?? name;
        }
    }
}