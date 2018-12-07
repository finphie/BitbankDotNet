using System;
using System.Collections.Generic;
using System.Linq;

namespace BitbankDotNet.SharedLibrary.Extensions
{
    public static class EnumerableExtensions
    {
        public static SortedList<TKey, TElement> ToSortedList<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
            => new SortedList<TKey, TElement>(source.ToDictionary(keySelector, elementSelector));
    }
}