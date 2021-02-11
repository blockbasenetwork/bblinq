using BlockBase.BBLinq.Annotations;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace BlockBase.BBLinq.ExtensionMethods
{
    public static class TypeExtensionMethods
    {
        

        #region Properties
        /// <summary>
        /// Fetches all properties on a class with a specific type 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <param name="stopOnFirst"></param>
        /// <returns></returns>
        public static IEnumerable<PropertyInfo> GetPropertiesWithAttribute<T>(this Type type, bool stopOnFirst) where T : Attribute
        {
            var properties = type.GetProperties();
            var propertyInfoList = new List<PropertyInfo>();
            foreach (var property in properties)
            {
                var primaryKeys = property.GetAttributes<T>();
                if (primaryKeys != default)
                {
                    propertyInfoList.Add(property);
                    if (stopOnFirst)
                    {
                        return propertyInfoList;
                    }
                }
            }
            return propertyInfoList;
        }

        #endregion

        #region Primary Keys
        /// <summary>
        /// Fetches a list of primary keys of a type.
        /// </summary>
        /// <param name="type">the wrapping type</param>
        /// <param name="stopOnFirst">only fetches the first primary key</param>
        /// <returns>An enumerable of PropertyInfo</returns>
        public static IEnumerable<PropertyInfo> GetPrimaryKeys(this Type type, bool stopOnFirst = false)
        {
            return GetPropertiesWithAttribute<KeyAttribute>(type, stopOnFirst);
        }

        /// <summary>
        /// Fetches the primary key on a type
        /// </summary>
        /// <param name="type">the wrapping type</param>
        /// <returns></returns>
        public static PropertyInfo GetPrimaryKey(this Type type)
        {
            var primaryKeys = GetPrimaryKeys(type, true) as List<PropertyInfo>;
            if (primaryKeys.Count > 0)
            {
                return primaryKeys[0];
            }
            return default;
        }

        #endregion

        #region Foreign Keys
        /// <summary>
        /// Fetches a list of foreign keys of a type.
        /// </summary>
        /// <param name="type">the wrapping type</param>
        /// <param name="stopOnFirst">only fetches the first foreign key</param>
        /// <returns>An enumerable of PropertyInfo</returns>
        public static IEnumerable<PropertyInfo> GetForeignKeys(this Type type, bool stopOnFirst = false)
        {
            return GetPropertiesWithAttribute<KeyAttribute>(type, stopOnFirst);
        }

        /// <summary>
        /// Fetches the primary key on a type
        /// </summary>
        /// <param name="type">the wrapping type</param>
        /// <returns></returns>
        public static PropertyInfo GetForeignKey(this Type type)
        {
            var primaryKeys = GetForeignKeys(type, true) as List<PropertyInfo>;
            if (primaryKeys.Count > 0)
            {
                return primaryKeys[0];
            }
            return default;
        }
        #endregion

        #region Tables 

        /// <summary>
        /// Retrieves the table's name if the type has a table attribute
        /// </summary>
        /// <param name="type">the class type</param>
        /// <returns>a table's name or an empty string if there's no table attribute</returns>
        public static TableAttribute GetTable(this Type type)
        {
            var tableAttributes = type.GetAttributes<TableAttribute>();
            if(tableAttributes == default || tableAttributes.Length == 0)
            {
                return default;
            }
            return tableAttributes[0];
        }

        /// <summary>
        /// Fetches the table's name
        /// </summary>
        /// <param name="type">the type associated to type table</param>
        /// <returns></returns>
        public static string GetTableName(this Type type)
        {
            var table = GetTable(type);
            if(table == default)
            {
                return type.Name;
            }
            return table.Name;
        }
        #endregion

        #region Type Checkers
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
        #endregion
        //TODO  
        #region Type Convertion
        /// <summary>
        /// Converts a type to a BBSqlType
        /// </summary>
        /// <param name="type">the type</param>
        /// <returns>the returning BbSQL type</returns>
        //public static BbSqlType ToBbSqlType(this Type type)
        //{
        //    if (type == typeof(bool)) return BbSqlType.Bool;
        //    if (type == typeof(int)) return BbSqlType.Int;
        //    if (type == typeof(decimal)) return BbSqlType.Decimal;
        //    if (type == typeof(DateTime)) return BbSqlType.DateTime;
        //    if (type == typeof(double)) return BbSqlType.Double;
        //    return type == typeof(TimeSpan) ? BbSqlType.Duration : BbSqlType.Text;
        //}
        #endregion
    }
}
