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
                return 76543210.12345678;
            if (type == typeof(int))
                return int.MaxValue;
            if (type == typeof(long))
                return long.MaxValue;
            if (type == typeof(string))
                return "abc";
            if (type == typeof(DateTime))
                return new DateTime(2018, 1, 1, 1, 1, 1, 111);

            // BitbankDotNetで定義したenumの場合
            if (type.IsEnum && type.Namespace == nameof(BitbankDotNet))
                // Activator.CreateInstance(type)では、enumの値が0のメンバーを返す。
                // 0に相当するメンバーがない場合は0を返してしまうので使えない。             
                return type.GetFields(BindingFlags.Public | BindingFlags.Static).First().GetValue(null);

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