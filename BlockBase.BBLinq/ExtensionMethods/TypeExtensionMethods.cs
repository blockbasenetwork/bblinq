using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using BlockBase.BBLinq.Annotations;
using BlockBase.BBLinq.Enums;

namespace BlockBase.BBLinq.ExtensionMethods
{
    /// <summary>
    /// Static methods for types
    /// </summary>
    public static class TypeExtensionMethods
    {
        /// <summary>
        /// checks if a type is based on a certain type
        /// </summary>
        /// <param name="type">the type</param>
        /// <param name="secondType">the other type</param>
        /// <returns>true if the type is based on the second type</returns>
        public static bool Is(this Type type, Type secondType)
        {
            while (type.BaseType != null)
            {
                if (type == secondType)
                {
                    return true;
                }
                type = type.BaseType;
            }
            return false;
        }

        /// <summary>
        /// Retrieves the table's name if the type has a table attribute
        /// </summary>
        /// <param name="type">the class type</param>
        /// <returns>a table's name or an empty string if there's no table attribute</returns>
        public static string GetTableName(this Type type)
        {
            var tableAttributes = type.GetCustomAttributes(typeof(TableAttribute), false);
            if (tableAttributes.Length != 0 && tableAttributes[0] is TableAttribute table)
            {
                return table.Name;
            }
            return type.Name;
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
        /// Checks if the type is generated dynamically
        /// </summary>
        /// <param name="type">A type</param>
        /// <returns>true if the object was dynamically generated. False otherwise</returns>
        public static bool IsGenerated(this Type type)
        {
            var customAttributes = type.GetCustomAttributes(typeof(CompilerGeneratedAttribute), false);
            return customAttributes.Length > 0;
        }

        /// <summary>
        /// Retrieves the table's name if the type has a table attribute
        /// </summary>
        /// <param name="type">the class type</param>
        /// <returns>a table's name or an empty string if there's no table attribute</returns>
        public static PropertyInfo GetPrimaryKey(this Type type)
        {
            var properties = type.GetProperties();
            foreach (var property in properties)
            {
                var attributes = property.GetCustomAttributes((typeof(PrimaryKeyAttribute)), false);
                if (attributes.Length != 0 && attributes[0] is PrimaryKeyAttribute)
                {
                    return property;
                }
            }
            return default;
        }

        /// <summary>
        /// Retrieves the table's foreign key
        /// </summary>
        /// <param name="type">the class type</param>
        /// <param name="foreignType"></param>
        /// <returns>a table's name or an empty string if there's no table attribute</returns>
        public static PropertyInfo GetForeignKey(this Type type, Type foreignType)
        {
            var properties = type.GetProperties();
            var tableName = foreignType.GetTableName();

            foreach (var property in properties)
            {
                var attributes = property.GetCustomAttributes((typeof(ForeignKeyAttribute)), false);
                if (attributes.Length != 0 && attributes[0] is ForeignKeyAttribute fk && fk.Name == tableName)
                {
                    return property;
                }
            }
            return default;
        }

        /// <summary>
        /// Retrieves the table's foreign key
        /// </summary>
        /// <param name="type">the class type</param>
        /// <param name="foreignType"></param>
        /// <returns>a table's name or an empty string if there's no table attribute</returns>
        public static ForeignKeyAttribute[] GetForeignKeys(this Type type)
        {
            var properties = type.GetProperties();
            var keys = new List<ForeignKeyAttribute>();
            foreach (var property in properties)
            {
                var attributes = property.GetCustomAttributes((typeof(ForeignKeyAttribute)), false);
                if (attributes.Length != 0 && attributes[0] is ForeignKeyAttribute fk)
                {
                    keys.Add(fk);
                }
            }
            return keys.ToArray();
        }

        /// <summary>
        /// Converts a type to a BBSqlType
        /// </summary>
        /// <param name="type">the type</param>
        /// <returns>the returning BbSQL type</returns>
        public static BbSqlType ToBbSqlType(this Type type)
        {
            if (type == typeof(bool)) return BbSqlType.Bool;
            if (type == typeof(int)) return BbSqlType.Int;
            if (type == typeof(decimal)) return BbSqlType.Decimal;
            if (type == typeof(DateTime)) return BbSqlType.DateTime;
            if (type == typeof(double)) return BbSqlType.Double;
            return type == typeof(TimeSpan) ? BbSqlType.Duration : BbSqlType.Text;
        }
    }
}
