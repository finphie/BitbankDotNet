using System;
using System.Linq;
using System.Reflection;

namespace BitbankDotNet.Shared.Helpers
{
    public static class EntityHelper
    {
        public static object GetTestValue(Type type)
        {
            if (type == typeof(double))
                return 1.2;
            if (type == typeof(int))
                return 3;
            if (type == typeof(long))
                return 4L;
            if (type == typeof(string))
                return "a";

            // タイムゾーンを明示的に指定しないと、ローカル時間と認識されてしまう。
            // 対応策は主に2つ
            // 1. new DateTimeOffset()でTimeSpan.Zeroを指定
            // 2. DateTimeOffset.ParseでISO8601形式を利用（DateTime.Parseは不可）
            if (type == typeof(DateTime))
                return DateTimeOffset.Parse("2018-01-02T03:04:05.678Z").UtcDateTime;

            // BitbankDotNetで定義したenumの場合
            if (type.IsEnum && type.Namespace == nameof(BitbankDotNet))
                // Activator.CreateInstance(type)では、enumの値が0のメンバーを返す。
                // 0に相当するメンバーがない場合は0を返してしまうので使えない。
                // また、type.GetFieldsは順序が不定なので注意
                return type.GetFields(BindingFlags.Public | BindingFlags.Static).Min(x => x.GetValue(null));

            if (type.IsArray)
            {
                var value = GetTestValue(type.GetElementType());

                var entityArray = (object[]) Activator.CreateInstance(type, 2);
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

        public static void SetValue(object target)
        {
            foreach (var property in target.GetType().GetProperties())
                property.SetValue(target, GetTestValue(property.PropertyType));
        }
    }
}