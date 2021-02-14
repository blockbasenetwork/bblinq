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

        #region Primary Key
        /// <summary>
        /// Retrieves the primary key attribute if the property has one
        /// </summary>
        /// <param name="property">the property in use</param>
        /// <returns>an primary key attribute or none</returns>
        public static KeyAttribute GetPrimaryKey(this PropertyInfo property)
        {
            var attributes = property.GetAttributes<KeyAttribute>();
            if (attributes == default || attributes.Length == 0)
            {
                return default;
            }
            return attributes[0];
        }
        #endregion

        #region Foreign Key
        /// <summary>
        /// Retrieves the foreign key attribute if the property has one
        /// </summary>
        /// <param name="property">the property in use</param>
        /// <returns>an primary key attribute or none</returns>
        public static ForeignKeyAttribute GetForeignKey(this PropertyInfo property)
        {
            var attributes = property.GetAttributes<ForeignKeyAttribute>();
            if (attributes == default || attributes.Length == 0)
            {
                return default;
            }
            return attributes[0];
        }
        #endregion

        #region Mapped
        /// <summary>
        /// Checks if the property has a notmapped attribute
        /// </summary>
        /// <param name="property">the property in use</param>
        /// <returns>an primary key attribute or none</returns>
        public static bool IsMapped(this PropertyInfo property)
        {
            var attributes = property.GetAttributes<NotMappedAttribute>();
            if (attributes == default || attributes.Length == 0)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Other
        /// <summary>
        /// Checks if a property is virtual, static, or abstract
        /// </summary>
        /// <param name="property">the property</param>
        /// <returns>true if the property is virtual, static, or abstract. False otherwise.</returns>
        public static bool IsVirtualOrStaticOrAbstract(this PropertyInfo property)
        {
            var getGetMethod = property.GetGetMethod();
            return getGetMethod.IsStatic || getGetMethod.IsVirtual || getGetMethod.IsAbstract;
        }

        #endregion
    }
}
