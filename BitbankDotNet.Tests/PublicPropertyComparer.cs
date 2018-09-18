using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BitbankDotNet.Tests
{
    class PublicPropertyComparer<T> : IEqualityComparer<T>
    {
        public bool Equals(T expected, T actual)
            => typeof(T)
                .GetProperties(
                    BindingFlags.DeclaredOnly |
                    BindingFlags.Instance |
                    BindingFlags.Public |
                    BindingFlags.Static)
                .All(p =>
                {
                    var expectedValue = p.GetValue(expected);
                    var actualValue = p.GetValue(actual);

                    // double型のまま比較すると精度の関係でfalseになる。
                    // また、expectedとactualの差の絶対値で許容範囲を決めようにも、
                    // Entityクラス毎に有効桁数が変わるため、対応が難しい。
                    // 従って、stringかdecimalを利用して比較する必要がある。
                    // stringでは、CultureInfoを考慮する必要があるため大変。
                    if (expectedValue is double e && actualValue is double a)
                        return (decimal) e == (decimal) a;

                    return expectedValue.GetType().IsArray
                        ? ((object[]) expectedValue).ToString() == ((object[]) actualValue).ToString()
                        : p.GetValue(expected).Equals(p.GetValue(actual));
                });

        public int GetHashCode(T obj)
            => obj.GetHashCode();
    }
}