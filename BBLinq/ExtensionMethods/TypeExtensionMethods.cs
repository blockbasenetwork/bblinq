using System;
using System.Linq;
using System.Reflection;
using agap2IT.Labs.BlockBase.BBLinq.DataAnnotations;

namespace agap2IT.Labs.BlockBase.BBLinq.ExtensionMethods
{
    public static class TypeExtensionMethods
    {
        public static string GetTableName(this Type type)
        {
            var tableAttributes = type.GetCustomAttributes(typeof(TableAttribute), false);
            return tableAttributes.Length == 0 ? string.Empty : (tableAttributes[0] as TableAttribute)?.Name;
        }

        public static bool IsPrimaryKey(this PropertyInfo property)
        {
            return property.GetCustomAttributes<PrimaryKeyAttribute>().Any();
        }
    }
}
