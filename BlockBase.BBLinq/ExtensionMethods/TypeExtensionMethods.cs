using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using BlockBase.BBLinq.DataAnnotations;
using BlockBase.BBLinq.Pocos.Components;

namespace BlockBase.BBLinq.ExtensionMethods
{
    /// <summary>
    /// Extension methods for Types
    /// </summary>
    public static class TypeExtensionMethods
    {
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
        /// Checks if the type is dynamic
        /// </summary>
        /// <param name="type">A type</param>
        /// <returns>true if the object was dynamically generated. False otherwise</returns>
        public static bool IsDynamic(this Type type)
        {
            var customAttributes = type.GetCustomAttributes(typeof(CompilerGeneratedAttribute), false);
            return customAttributes.Length > 0;
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
        /// Lists all the field name,value pairings
        /// </summary>
        /// <param name="object">an object</param>
        /// <returns>the list of field name,value pairing</returns>
        public static FieldValuePairing[] GetFieldsAndValues(this object @object)
        {
            var type = @object.GetType();
            var result = new List<FieldValuePairing>();
            var properties = type.GetProperties();

            foreach (var property in properties)
            {
                var fieldName = property.GetFieldName();
                var value = property.GetValue(@object);
                result.Add(new FieldValuePairing(fieldName, value));
            }

            return result.ToArray();
        }

        /// <summary>
        /// Lists all the fields name
        /// </summary>
        /// <param name="type">the type</param>
        /// <returns>the list of field name,value pairing</returns>
        public static string[] GetFieldNames(this Type type)
        {
            var result = new List<string>();
            var properties = type.GetProperties();

            foreach (var property in properties)
            {
                var fieldName = property.GetFieldName();
                result.Add(fieldName);
            }

            return result.ToArray();
        }

    }

}
