
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using BlockBase.BBLinq.Annotations;
using BlockBase.BBLinq.Enumerables;

namespace BlockBase.BBLinq.ExtensionMethods
{
    public static class MemberExtensionMethods
    {
        /// <summary>
        /// Fetches all the attributes of a certain type on a property
        /// </summary>
        public static T[] GetAttributes<T>(this MemberInfo property) where T : Attribute
        {
            var attributes = property.GetCustomAttributes(typeof(T), false);
            if (attributes.Length == 0)
            {
                return null;
            }
            var attributeList = new T[attributes.Length];
            for (var attributeListCounter = 0; attributeListCounter < attributeList.Length; attributeListCounter++)
            {
                attributeList[attributeListCounter] = attributes[attributeListCounter] as T;
            }
            return attributeList;
        }

        /// <summary>
        /// Fetches all properties on a type that have a certain attribute
        /// </summary>
        private static PropertyInfo[] GetPropertiesWithAttribute<T>(this Type type) where T:Attribute
        {
            var properties = type.GetProperties();
            var propertyList = new List<PropertyInfo>();
            foreach (var property in properties)
            {
                var attributes = property.GetAttributes<T>();
                if (attributes != null && attributes.Length > 0)
                {
                    propertyList.Add(property);
                }
            }

            return propertyList.Count == 0 ? null : propertyList.ToArray();
        }

        /// <summary>
        /// Fetches all foreign keys from a type
        /// </summary>
        public static PropertyInfo[] GetForeignKeyProperties(this Type type)
        {
            return GetPropertiesWithAttribute<ForeignKeyAttribute>(type);
        }

        /// <summary>
        /// Fetches all foreign keys from a property
        /// </summary>
        public static ForeignKeyAttribute[] GetForeignKeys(this PropertyInfo property)
        {
            return property.GetAttributes<ForeignKeyAttribute>();
        }


        /// <summary>
        /// Fetches all primary keys from a type
        /// </summary>
        public static PropertyInfo[] GetPrimaryKeyProperties(this Type type)
        {
            return GetPropertiesWithAttribute<PrimaryKeyAttribute>(type);
        }

        /// <summary>
        /// Fetches all primary keys from a property
        /// </summary>
        public static PrimaryKeyAttribute[] GetPrimaryKeys(this PropertyInfo property)
        {
            return property.GetAttributes<PrimaryKeyAttribute>();
        }

        /// <summary>
        /// Validates if the property is not null
        /// </summary>
        public static bool IsNotNull(this MemberInfo property)
        {
            var attributes = property.GetAttributes<NotNullAttribute>();
            return attributes != null && attributes.Length > 0;
        }

        /// <summary>
        /// Fetches all non-mapped properties from a type
        /// </summary>
        public static PropertyInfo[] GetNonMappedProperties(this Type type)
        {
            return GetPropertiesWithAttribute<NotMappedAttribute>(type);
        }

        public static bool IsNotMapped(this PropertyInfo property)
        {
            var nonMappedProperties = property.GetAttributes<NotMappedAttribute>();
            return nonMappedProperties != null && nonMappedProperties.Length > 0;
        }

        /// <summary>
        /// Fetches all range properties from a type
        /// </summary>
        public static PropertyInfo[] GetRangeProperties(this Type type)
        {
            return GetPropertiesWithAttribute<RangeAttribute>(type);
        }

        /// <summary>
        /// Fetches all ranges from a property
        /// </summary>
        public static RangeAttribute[] GetRanges(this PropertyInfo property)
        {
            return property.GetAttributes<RangeAttribute>();
        }

        /// <summary>
        /// Fetches all encrypted properties from a type
        /// </summary>
        public static PropertyInfo[] GetEncryptedProperties(this Type type)
        {
            return GetPropertiesWithAttribute<EncryptedAttribute>(type);
        }

        /// <summary>
        /// Fetches all encrypted from a property
        /// </summary>
        public static EncryptedAttribute[] GetEncrypted(this PropertyInfo property)
        {
            return property.GetAttributes<EncryptedAttribute>();
        }

        /// <summary>
        /// Checks if the object is a numeric value
        /// </summary>
        /// <param name="value">the value to check</param>
        /// <returns>true if the object is a number. False otherwise</returns>
        public static bool IsNumber(this object value)
        {
            return value is sbyte
                   || value is byte
                   || value is short
                   || value is ushort
                   || value is int
                   || value is uint
                   || value is long
                   || value is ulong
                   || value is float
                   || value is double
                   || value is decimal;
        }

        /// <summary>
        /// Fetches the table's name
        /// </summary>
        public static string GetTableName(this Type type)
        {
            var tableAttributes = type.GetAttributes<TableAttribute>();
            if (tableAttributes == null || tableAttributes.Length == 0)
            {
                return type.Name;
            }
            return tableAttributes[0].Name;
        }

        /// <summary>
        /// Fetches the column's name
        /// </summary>
        public static string GetColumnName(this MemberInfo property)
        {
            var columnAttributes = property.GetAttributes<ColumnAttribute>();
            if (columnAttributes == null || columnAttributes.Length == 0)
            {
                return property.Name;
            }
            return columnAttributes[0].Name;
        }

        /// <summary>
        /// Checks if a property is virtual, static, or abstract
        /// </summary>
        public static bool IsVirtualOrStaticOrAbstract(this PropertyInfo property)
        {
            var getGetMethod = property.GetGetMethod();
            if (getGetMethod == null)
            {
                return false;
            }
            return getGetMethod.IsStatic || getGetMethod.IsVirtual || getGetMethod.IsAbstract;
        }

        public static bool IsAcceptableType(this PropertyInfo property)
        {
            return property.PropertyType == typeof(bool) ||
                   property.PropertyType == typeof(int) ||
                   property.PropertyType == typeof(decimal) ||
                   property.PropertyType == typeof(double) ||
                   property.PropertyType == typeof(TimeSpan) ||
                   property.PropertyType == typeof(string) ||
                   property.PropertyType == typeof(DateTime) ||
                   property.PropertyType == typeof(Guid);
        }

        /// <summary>
        /// Converts a type to a BBSqlType
        /// </summary>
        public static BbSqlDataTypeEnum ToBbSqlType(this Type type)
        {
            if (type == typeof(bool))
                return BbSqlDataTypeEnum.Bool;
            if (type == typeof(int))
                return BbSqlDataTypeEnum.Int;
            if (type == typeof(decimal))
                return BbSqlDataTypeEnum.Decimal;
            if (type == typeof(DateTime))
                return BbSqlDataTypeEnum.DateTime;
            if (type == typeof(double))
                return BbSqlDataTypeEnum.Double;
            return type == typeof(TimeSpan) ?
                BbSqlDataTypeEnum.Duration :
                BbSqlDataTypeEnum.Text;
        }

    }
}
