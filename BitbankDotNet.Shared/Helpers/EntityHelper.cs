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
            //if (type == typeof(BoardOrder[]))
            //    return new[] { new BoardOrder { Price = 1.1, Amount = 1.2 }, new BoardOrder { Price = 1.3, Amount = 1.4 } };
            //if (type == typeof(AssetName))
            //    return AssetName.Jpy;

            throw new NotImplementedException(type.Name);
        }

        public static void SetValue(object target)
        {
            foreach (var property in target.GetType().GetProperties())
                property.SetValue(target, GetTestValue(property.PropertyType));
        }
    }
}