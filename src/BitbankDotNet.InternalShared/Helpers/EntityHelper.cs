﻿using System;
using System.Linq;

namespace BitbankDotNet.InternalShared.Helpers
{
    /// <summary>
    /// Entityクラスのヘルパークラス
    /// </summary>
    public static class EntityHelper
    {
        /// <summary>
        /// テスト値を取得します。
        /// </summary>
        /// <param name="type">対象の型</param>
        /// <returns>テスト値</returns>
        public static object GetTestValue(Type type)
        {
            if (type == typeof(decimal))
                return 1.2M;
            if (type == typeof(double))
                return 1.2;
            if (type == typeof(int))
                return 3;
            if (type == typeof(long))
                return 4L;
            if (type == typeof(string))
                return "a";
            if (type == typeof(bool))
                return false;

            // タイムゾーンを明示的に指定しないと、ローカル時間と認識されてしまう。
            // 対応策は主に2つ
            // 1. new DateTimeOffset()でTimeSpan.Zeroを指定
            // 2. DateTimeOffset.ParseでISO8601形式を利用（DateTime.Parseは不可）
            if (type == typeof(DateTime))
                return DateTimeOffset.Parse("2018-01-02T03:04:05.678Z").UtcDateTime;

            // BitbankDotNetで定義したenumの場合
            // Activator.CreateInstance(type)では、enumの値が0のメンバーを返す。
            // 0に相当するメンバーがない場合は0を返してしまうので使えない。
            // また、type.GetFieldsは順序が不定なので注意
            if (type.IsEnum && type.Namespace == nameof(BitbankDotNet))
                return Enum.GetValues(type).Cast<object>().Min();

            if (type.IsArray)
            {
                var value = GetTestValue(type.GetElementType());

                var entityArray = (object[])Activator.CreateInstance(type, 2);
                for (var i = 0; i < entityArray.Length; i++)
                    entityArray[i] = value;
                return entityArray;
            }

            // Entityクラス以外で未実装の型の場合
            if (type.Namespace != $"{nameof(BitbankDotNet)}.Entities")
                throw new NotImplementedException(type.Name);

            var entity = Activator.CreateInstance(type);
            foreach (var property in type.GetProperties())
                property.SetValue(entity, GetTestValue(property.PropertyType));

            return entity;
        }

        /// <summary>
        /// テスト値を取得します。
        /// </summary>
        /// <typeparam name="T">対象の型</typeparam>
        /// <returns>テスト値</returns>
        public static T GetTestValue<T>()
            => (T)GetTestValue(typeof(T));

        /// <summary>
        /// 値を設定します。
        /// </summary>
        /// <param name="target">対象の変数</param>
        public static void SetValue(object target)
        {
            foreach (var property in target.GetType().GetProperties())
                property.SetValue(target, GetTestValue(property.PropertyType));
        }
    }
}