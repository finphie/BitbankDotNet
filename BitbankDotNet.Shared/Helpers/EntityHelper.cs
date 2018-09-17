using System;

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
            if (type.IsArray)
            {
                var value = GetTestValue(type.GetElementType());

                if (!(Activator.CreateInstance(type, 2) is object[] entityArray))
                    throw new InvalidCastException(type.Name);
                for (var i = 0; i < entityArray.Length; i++)
                    entityArray[i] = value;
                return entityArray;
            }

            // TODO: enum

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