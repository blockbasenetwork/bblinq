using System.Reflection;
using BlockBase.BBLinq.DataAnnotations;

namespace BlockBase.BBLinq.ExtensionMethods
{
    /// <summary>
    /// Extension methods for Properties its derivatives
    /// </summary>
    internal static class PropertyInfoExtensionMethods
    {
        /// <summary>
        /// Retrieves a field's name based on a property
        /// </summary>
        /// <param name="property">a property</param>
        /// <returns></returns>
        internal static string GetFieldName(this PropertyInfo property)
        {
            var fieldAttributes = property.GetCustomAttributes(typeof(FieldAttribute), false);
            return fieldAttributes.Length == 0 ? string.Empty : (fieldAttributes[0] as FieldAttribute)?.Name;
        }

        /// <summary>
        /// Validates if the property is a primary key
        /// </summary>
        /// <param name="property">A property</param>
        /// <returns>true if it is a primary key. False otherwise</returns>
        public static bool IsPrimaryKey(this PropertyInfo property)
        {
            var attributes = property.GetCustomAttributes<PrimaryKeyAttribute>();
            return !attributes.IsNullOrEmpty();
        }
    }
}
