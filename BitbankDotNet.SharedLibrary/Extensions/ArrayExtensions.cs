﻿using System;

namespace BitbankDotNet.SharedLibrary.Extensions
{
    /// <summary>
    /// 配列の拡張メソッド
    /// </summary>
    public static class ArrayExtensions
    {
        public static void Deconstruct<T>(this T[] array, out T first, out T second)
        {
            if (array is null)
                throw new ArgumentNullException(nameof(array));
            if (array.Length < 2)
                throw new ArgumentOutOfRangeException(nameof(array));

            (first, second) = (array[0], array[1]);
        }

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