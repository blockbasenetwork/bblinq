using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using BlockBase.BBLinq.Annotations;
using BlockBase.BBLinq.Pocos;

namespace BlockBase.BBLinq.ExtensionMethods
{
    /// <summary>
    /// Extension methods for members such as methods and properties
    /// </summary>
    public static class MemberExtensionMethods
    {
        public static string GetFieldName(this MemberInfo property)
        {
            var fieldAttributes = property.GetCustomAttributes(typeof(FieldAttribute), false);
            if (fieldAttributes.Length != 0 && fieldAttributes[0] is FieldAttribute field)
            {
                return field.Name;
            }
            return property.Name;
        }

        /// <summary>
        /// Validates if the property is a primary key
        /// </summary>
        /// <param name="property">A property</param>
        /// <returns>true if it is a primary key. False otherwise</returns>
        public static bool IsPrimaryKey(this MemberInfo property)
        {
            var attributes = property.GetCustomAttributes<PrimaryKeyAttribute>();
            return !attributes.IsNullOrEmpty();
        }

        /// <summary>
        /// Validates if the property is a foreign key
        /// </summary>
        /// <param name="property">A property</param>
        /// <returns>true if it is a foreign key. False otherwise</returns>
        public static bool IsForeignKey(this MemberInfo property)
        {
            var attributes = property.GetCustomAttributes<ForeignKeyAttribute>();
            return !attributes.IsNullOrEmpty();
        }

        /// <summary>
        /// Validates if the property is a range
        /// </summary>
        /// <param name="property">A property</param>
        /// <returns>true if it is a range value. False otherwise</returns>
        public static bool IsRanged(this MemberInfo property)
        {
            var attributes = property.GetCustomAttributes<RangeAttribute>();
            return !attributes.IsNullOrEmpty();
        }

        /// <summary>
        /// Validates if the property is not null
        /// </summary>
        /// <param name="property">A property</param>
        /// <returns>true if it is not null. False otherwise</returns>
        public static bool IsNotNull(this MemberInfo property)
        {
            var attributes = property.GetCustomAttributes<NotNullAttribute>();
            return !attributes.IsNullOrEmpty();
        }


        /// <summary>
        /// Validates if the property is not null
        /// </summary>
        /// <param name="property">A property</param>
        /// <returns>true if it is not null. False otherwise</returns>
        public static bool IsEncrypted(this MemberInfo property)
        {
            var attributes = property.GetCustomAttributes<EncryptedAttribute>();
            return !attributes.IsNullOrEmpty();
        }

        /// <summary>
        /// Fetches the number of buckets 
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public static int GetBuckets(this MemberInfo property)
        {
            var attributes = property.GetCustomAttributes<EncryptedAttribute>();
            var encryptedAttributes = attributes as EncryptedAttribute[] ?? attributes.ToArray();
            return encryptedAttributes.IsNullOrEmpty() ? 0 : (encryptedAttributes[0]).Buckets;
        }


        /// <summary>
        /// Fetches the range
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public static RangeAttribute GetRange(this MemberInfo property)
        {
            var attributes = property.GetCustomAttributes<RangeAttribute>();
            var rangeAttributes = attributes as RangeAttribute[] ?? attributes.ToArray();
            return rangeAttributes.Length == 0 ? null : rangeAttributes[0];
        }

        /// <summary>
        /// Fetches the range
        /// </summary>
        /// <param name="property"></param>
        /// <param name="otherTypes">List of potential types for a foreign key</param>
        /// <returns></returns>
        public static FieldValue GetForeignField(this MemberInfo property, Type[] otherTypes)
        {
            var attributes = property.GetCustomAttributes<ForeignKeyAttribute>();
            var foreignAttributes = attributes as ForeignKeyAttribute[] ?? attributes.ToArray();
            var tableField = new FieldValue();
            if (foreignAttributes.Length == 0)
                return tableField;
            var foreignKey = foreignAttributes[0];
            tableField.Table = foreignKey.Name;
            foreach (var type in otherTypes)
            {
                if (type.GetTableName() != tableField.Table) continue;
                var primaryKey = type.GetPrimaryKey();
                tableField.Field = primaryKey.GetFieldName();
                return tableField;
            }

            return default;
        }
    }
}
