using System;
using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace BitbankDotNet.Benchmarks.EnumGetEnumMember
{
    static class EnumHelperHashtable
    {
        static readonly Hashtable Dic = new Hashtable();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetEnumMemberValue<T>(T value)
            where T : struct, Enum
        {
            if (Dic.ContainsKey(value))
                return Dic[value] as string;
            var memberValue = typeof(T)
                .GetField(value.ToString())
                .GetCustomAttribute<EnumMemberAttribute>().Value;
            Dic[value] = memberValue;
            return memberValue;
        }
    }
}