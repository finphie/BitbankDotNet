using System;

namespace BitbankDotNet.SharedLibrary.Extensions
{
    /// <summary>
    /// 配列の拡張メソッド
    /// </summary>
    public static class ArrayExtensions
    {
        /// <summary>
        /// 指定された配列を分解します。
        /// </summary>
        /// <typeparam name="T">対象の型</typeparam>
        /// <param name="array">対象の配列</param>
        /// <param name="first">配列の1番目の要素</param>
        /// <param name="second">配列の2番目の要素</param>
        public static void Deconstruct<T>(this T[] array, out T first, out T second)
        {
            if (array is null)
                throw new ArgumentNullException(nameof(array));
            if (array.Length < 2)
                throw new ArgumentOutOfRangeException(nameof(array));

            (first, second) = (array[0], array[1]);
        }

        /// <summary>
        /// 指定された配列を分解します。
        /// </summary>
        /// <typeparam name="T">対象の型</typeparam>
        /// <param name="array">対象の配列</param>
        /// <param name="first">配列の1番目の要素</param>
        /// <param name="second">配列の2番目の要素</param>
        /// <param name="third">配列の3番目の要素</param>
        /// <param name="fourth">配列の4番目の要素></param>
        public static void Deconstruct<T>(this T[] array, out T first, out T second, out T third, out T fourth)
        {
            if (array is null)
                throw new ArgumentNullException(nameof(array));
            if (array.Length < 4)
                throw new ArgumentOutOfRangeException(nameof(array));

            (first, second, third, fourth) = (array[0], array[1], array[2], array[3]);
        }
    }
}