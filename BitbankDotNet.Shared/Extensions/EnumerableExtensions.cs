using System;
using System.Collections.Generic;
using System.Linq;

namespace BitbankDotNet.Shared.Extensions
{
    public static class EnumerableExtensions
    {
        public static SortedDictionary<TKey, TElement> ToSortedDictionary<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
            => new SortedDictionary<TKey, TElement>(source.ToDictionary(keySelector, elementSelector));
    }
}