using BlockBase.BBLinq.Annotations;
using System;
using System.Reflection;

namespace BlockBase.BBLinq.ExtensionMethods
{
    public static class MemberInfoExtensionMethods
    {
        #region Attributes
        internal static T[] GetAttributes<T>(this MemberInfo property) where T : Attribute
        {
            var attributes = property.GetCustomAttributes(typeof(T), false);
            if (attributes.Length != 0)
            {
                var attributeList = new T[attributes.Length];
                for (var attributeListCounter = 0; attributeListCounter < attributeList.Length; attributeListCounter++)
                {
                    attributeList[attributeListCounter] = attributes[attributeListCounter] as T;
                }
                return attributeList;
            }
            return default;
        }
        #endregion

        #region Columns 

        /// <summary>
        /// Retrieves the column's name if the type has a column attribute
        /// </summary>
        /// <param name="property">the property</param>
        /// <returns>a column if there's any, or null</returns>
        public static ColumnAttribute GetColumn(this PropertyInfo property)
        {
            var columnAttributes = property.GetAttributes<ColumnAttribute>();
            if (columnAttributes == default || columnAttributes.Length == 0)
            {
                return default;
            }
            return columnAttributes[0];
        }

        /// <summary>
        /// Fetches the columns's name
        /// </summary>
        /// <param name="property">the property associated to the column</param>
        /// <returns></returns>
        public static string GetColumnName(this PropertyInfo property)
        {
            var column = GetColumn(property);
            if (column == default)
            {
                return property.Name;
            }
            return column.Name;
        }
        #endregion

        #region Range
        /// <summary>
        /// Retrieves the range attribute if the property has one
        /// </summary>
        /// <param name="property">the property in use</param>
        /// <returns>a range attribute or none</returns>
        public static RangeAttribute GetRange(this PropertyInfo type)
        {
            var rangeAttributes = type.GetAttributes<RangeAttribute>();
            if (rangeAttributes == default || rangeAttributes.Length == 0)
            {
                return default;
            }
            return rangeAttributes[0];
        }
        #endregion

        #region Encrypted
        /// <summary>
        /// Retrieves the encryted attribute if the property has one
        /// </summary>
        /// <param name="property">the property in use</param>
        /// <returns>an encryption attribute or none</returns>
        public static EncryptedAttribute GetEncrypted(this PropertyInfo property)
        {
            var encryptionAttributes = property.GetAttributes<EncryptedAttribute>();
            if (encryptionAttributes == default || encryptionAttributes.Length == 0)
            {
                return default;
            }
            return encryptionAttributes[0];
        }
        #endregion
    }
}
