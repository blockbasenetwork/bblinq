using System;
using System.Collections.Generic;
using System.Reflection;

namespace BlockBase.BBLinq.ExtensionMethods
{
    public static class MemberInfoExtensionMethods
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
        public static PropertyInfo[] GetPropertiesWithAttribute<T>(this Type type) where T : Attribute
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

        /// <summary>
        /// Checks if a type is nullable
        /// </summary>
        public static bool IsNullable(this PropertyInfo property)
        {
            var type = Nullable.GetUnderlyingType(property.PropertyType);
            return type != null;
        }

        /// <summary>
        /// Fetches the nullable type original value
        /// </summary>
        public static Type GetNullableType(this Type type)
        {
            return Nullable.GetUnderlyingType(type);
        }
    }
}
