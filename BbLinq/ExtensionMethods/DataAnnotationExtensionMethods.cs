using System;
using System.Collections.Generic;
using System.Reflection;
using BlockBase.BBLinq.DataAnnotations;
using BlockBase.BBLinq.Enumerables;

namespace BlockBase.BBLinq.ExtensionMethods
{
    public static class DataAnnotationExtensionMethods
    {

        /// <summary>
        /// Fetches all foreign keys from a type
        /// </summary>
        public static PropertyInfo[] GetForeignKeyProperties(this Type type)
        {
            return type.GetPropertiesWithAttribute<ForeignKeyAttribute>();
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
            return type.GetPropertiesWithAttribute<PrimaryKeyAttribute>();
        }

        /// <summary>
        /// Fetches all primary keys from a property
        /// </summary>
        public static PrimaryKeyAttribute[] GetPrimaryKeys(this PropertyInfo property)
        {
            return property.GetAttributes<PrimaryKeyAttribute>();
        }

        /// <summary>
        /// Fetches all primary keys from a type
        /// </summary>
        public static PropertyInfo GetPrimaryKeyProperty(this Type type)
        {
            var properties = type.GetPropertiesWithAttribute<PrimaryKeyAttribute>();
            return properties != null && properties.Length > 0 ? properties[0] : null;
        }

        public static bool HasPrimaryKey(this Type type)
        {
            var primaryKey = type.GetPrimaryKeyProperty();
            return primaryKey != null;
        }

        public static bool IsPrimaryKey(this PropertyInfo property)
        {
            var primaryKeys = property.GetPrimaryKeys();
            return primaryKeys != null && primaryKeys.Length != 0;
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
            return type.GetPropertiesWithAttribute<NotMappedAttribute>();
        }

        public static bool IsNotMapped(this PropertyInfo property)
        {
            var nonMappedProperties = property.GetAttributes<NotMappedAttribute>();
            return nonMappedProperties != null && nonMappedProperties.Length > 0;
        }

        public static bool IsColumnEncrypted(this PropertyInfo property)
        {
            var nonMappedProperties = property.GetAttributes<EncryptedColumnAttribute>();
            return nonMappedProperties != null && nonMappedProperties.Length > 0;
        }

        public static bool IsRange(this PropertyInfo property)
        {
            var nonMappedProperties = property.GetAttributes<RangeAttribute>();
            return nonMappedProperties != null && nonMappedProperties.Length > 0;
        }

        public static RangeAttribute GetRange(this PropertyInfo propertyInfo)
        {
            var attributes = propertyInfo.GetAttributes<RangeAttribute>();
            return attributes != null && attributes.Length > 0 ? attributes[0] : null;
        }

        public static bool IsForeignKey(this PropertyInfo property)
        {
            var nonMappedProperties = property.GetAttributes<ForeignKeyAttribute>();
            return nonMappedProperties != null && nonMappedProperties.Length > 0;
        }

        public static bool IsValueEncrypted(this PropertyInfo property)
        {
            var nonMappedProperties = property.GetAttributes<EncryptedValueAttribute>();
            return nonMappedProperties != null && nonMappedProperties.Length > 0;
        }

        /// <summary>
        /// Fetches all range properties from a type
        /// </summary>
        public static PropertyInfo[] GetRangeProperties(this Type type)
        {
            return type.GetPropertiesWithAttribute<RangeAttribute>();
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
        public static PropertyInfo[] GetEncryptedColumnProperties(this Type type)
        {
            return type.GetPropertiesWithAttribute<EncryptedColumnAttribute>();
        }

        /// <summary>
        /// Fetches all encrypted properties from a type
        /// </summary>
        public static PropertyInfo[] GetEncryptedValueProperties(this Type type)
        {
            return type.GetPropertiesWithAttribute<EncryptedValueAttribute>();
        }

        /// <summary>
        /// Fetches all encrypted from a property
        /// </summary>
        public static EncryptedColumnAttribute[] GetColumnEncryptedProperties(this PropertyInfo property)
        {
            return property.GetAttributes<EncryptedColumnAttribute>();
        }

        /// <summary>
        /// Fetches all encrypted from a property
        /// </summary>
        public static EncryptedValueAttribute[] GetValueEncryptedProperties(this PropertyInfo property)
        {
            return property.GetAttributes<EncryptedValueAttribute>();
        }

        /// <summary>
        /// Fetches all encrypted from a property
        /// </summary>
        public static NotNullAttribute[] GetNotNull(this PropertyInfo property)
        {
            return property.GetAttributes<NotNullAttribute>();
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

        public static bool IsValidDataType(this Type type, List<Type> types)
        {
            return types.Contains(type) || (type.IsEnum && types.Contains(typeof(int)));
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
