using System;
using System.Collections.Generic;
using System.Linq;

namespace BitbankDotNet.InternalShared.Extensions
{
    /// <summary>
    /// <see cref="IEnumerable{T}"/>の拡張メソッド
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// <see cref="SortedList{TKey, TValue}"/>に変換
        /// </summary>
        /// <typeparam name="TSource">対象コレクションの型</typeparam>
        /// <typeparam name="TKey">キーの型</typeparam>
        /// <typeparam name="TElement">elementSelectorの型</typeparam>
        /// <param name="source">対象のコレクション</param>
        /// <param name="keySelector">keySelector</param>
        /// <param name="elementSelector">elementSelector</param>
        /// <returns><see cref="SortedList{TKey, TValue}"/>を返します。</returns>
        public static SortedList<TKey, TElement> ToSortedList<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
            => new SortedList<TKey, TElement>(source.ToDictionary(keySelector, elementSelector));
    }
}