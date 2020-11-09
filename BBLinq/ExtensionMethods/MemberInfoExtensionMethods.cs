using System.Reflection;
using agap2IT.Labs.BlockBase.BBLinq.DataAnnotations;

namespace agap2IT.Labs.BlockBase.BBLinq.ExtensionMethods
{
    /// <summary>
    /// Extension methods for Members and its derivatives
    /// </summary>
    internal static class MemberInfoExtensionMethods
    {
        /// <summary>
        /// Retrieves a field's name based on a property
        /// </summary>
        /// <param name="property">a property</param>
        /// <returns></returns>
        internal static string GetFieldName(this MemberInfo property)
        {
            var fieldAttributes = property.GetCustomAttributes(typeof(FieldAttribute), false);
            return fieldAttributes.Length == 0 ? string.Empty : (fieldAttributes[0] as FieldAttribute)?.Name;
        }
    }
}
