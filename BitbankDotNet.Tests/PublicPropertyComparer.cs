using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BitbankDotNet.Tests
{
    class PublicPropertyComparer<T> : IEqualityComparer<T>
    {
        public bool Equals(T expected, T actual)
            => PublicPropertyEquals(expected, actual);

        static bool PublicPropertyEquals(object expected, object actual)
            => expected.GetType().GetProperties(
                    BindingFlags.DeclaredOnly |
                    BindingFlags.Instance |
                    BindingFlags.Public |
                    BindingFlags.Static)
                .All(p => PublicPropertyEqualsCore(p.GetValue(expected), p.GetValue(actual)));

        static bool PublicPropertyEqualsCore(object expected, object actual)
        {
            // double型のまま比較すると精度の関係でfalseになる。
            // また、expectedとactualの差の絶対値で許容範囲を決めようにも、
            // Entityクラス毎に有効桁数が変わるため、対応が難しい。
            // 従って、stringかdecimalを利用して比較する必要がある。
            // stringでは、CultureInfoを考慮する必要があるため大変。
            if (expected is double e && actual is double a)
                return (decimal)e == (decimal)a;

            return expected.GetType().IsArray
                ? ((object[]) expected, (object[]) actual).Zip().All(t => PublicPropertyEquals(t.first, t.second))
                : expected.Equals(actual);
        }

        public int GetHashCode(T obj)
            => obj.GetHashCode();
    }

    static class ValueTupleExtensions
    {
        public static IEnumerable<(TFirst first, TSecond second)> Zip<TFirst, TSecond>(this (IEnumerable<TFirst> first, IEnumerable<TSecond> second) source)
            => source.first.Zip(source.second, (x1, x2) => (x1, x2));
    }
}