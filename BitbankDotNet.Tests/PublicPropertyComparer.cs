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
                .All(p => p.GetValue(expected).Equals(p.GetValue(actual)));

        public int GetHashCode(T obj)
            => obj.GetHashCode();
    }
}