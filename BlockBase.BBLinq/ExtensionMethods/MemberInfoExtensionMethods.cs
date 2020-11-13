using System.Reflection;
using BlockBase.BBLinq.DataAnnotations;

namespace BlockBase.BBLinq.ExtensionMethods
{
    /// <summary>
    /// Extension methods for Members and its derivatives
    /// </summary>
    public static class MemberInfoExtensionMethods
    {
        /// <summary>
        /// Retrieves a field's name based on a property
        /// </summary>
        /// <param name="property">a property</param>
        /// <returns></returns>
        public static string GetFieldName(this MemberInfo property)
        {
            var fieldAttributes = property.GetCustomAttributes(typeof(FieldAttribute), false);
            return fieldAttributes.Length == 0 ? string.Empty : (fieldAttributes[0] as FieldAttribute)?.Name;
        }
    }
}
