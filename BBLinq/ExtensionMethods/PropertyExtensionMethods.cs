using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using BlockBase.BBLinq.DataAnnotations;
using BlockBase.BBLinq.Exceptions;
using RangeAttribute = BlockBase.BBLinq.DataAnnotations.RangeAttribute;

namespace BlockBase.BBLinq.ExtensionMethods
{
    internal static class PropertyExtensionMethods
    {
        #region Data Annotations

        #region Column

        public static ColumnAttribute GetColumnAttribute(this PropertyInfo property)
        {
            var attribute = property.GetAttribute<ColumnAttribute>();
            return attribute;
        }

        public static bool IsColumn(this PropertyInfo property)
        {
            return property.GetColumnAttribute() != null;
        }

        public static string GetColumnName(this PropertyInfo property)
        {
            var columnAttribute = property.GetAttribute<ColumnAttribute>();
            return columnAttribute == null ? property.Name : columnAttribute.Name;
        }
        #endregion

        #region Comparable Date
        public static ComparableDateAttribute GetComparableDateAttribute(this PropertyInfo property)
        {
            var attribute = property.GetAttribute<ComparableDateAttribute>();
            return attribute;
        }

        public static bool IsComparableDate(this PropertyInfo property)
        {
            return property.GetComparableDateAttribute() != null;
        }
        #endregion

        #region Range
        public static RangeAttribute GetRangeAttribute(this PropertyInfo property)
        {
            var attribute = property.GetAttribute<RangeAttribute>();
            return attribute;
        }

        public static bool IsRange(this PropertyInfo property)
        {
            var attribute = property.GetAttribute<RangeAttribute>();
            return attribute != null;
        }
        #endregion

        #region Foreign Key
        public static ForeignKeyAttribute GetForeignKeyAttribute(this PropertyInfo property)
        {
            var attribute = property.GetAttribute<ForeignKeyAttribute>();
            return attribute;
        }

        public static bool IsForeignKey(this PropertyInfo property)
        {
            var attribute = property.GetForeignKeyAttribute();
            return attribute != null;
        }
        #endregion

        #region Decrypted Column
        public static DecryptedColumnAttribute GetDecryptedColumnAttribute(this PropertyInfo property)
        {
            var attribute = property.GetAttribute<DecryptedColumnAttribute>();
            return attribute;
        }

        public static bool IsDecryptedColumn(this PropertyInfo property)
        {
            return GetDecryptedColumnAttribute(property) != null;
        }
        #endregion

        #region Mapped
        public static MappedAttribute GetMappedAttribute(this PropertyInfo property)
        {
            var attribute = property.GetAttribute<MappedAttribute>();
            return attribute;
        }

        public static bool IsMapped(this PropertyInfo property)
        {
            return GetMappedAttribute(property) != null;
        }

        #endregion

        #region Not Mapped
        public static NotMappedAttribute GetNotMappedAttribute(this PropertyInfo property)
        {
            var attribute = property.GetAttribute<NotMappedAttribute>();
            return attribute;
        }

        public static bool IsNotMapped(this PropertyInfo property)
        {
            return GetNotMappedAttribute(property) != null;
        }
        #endregion

        #region Regex
        public static RegularExpressionAttribute GetRegexAttribute(this PropertyInfo property)
        {
            var attribute = property.GetAttribute<RegularExpressionAttribute>();
            return attribute;
        }
        #endregion

        #region Encrypted Value
        public static EncryptedValueAttribute GetEncryptedValueAttribute(this PropertyInfo property)
        {
            var attribute = property.GetAttribute<EncryptedValueAttribute>();
            return attribute;
        }

        public static bool IsEncryptedValue(this PropertyInfo property)
        {
            return GetEncryptedValueAttribute(property) != null;
        }

        public static int GetEncryptedValueBuckets(this PropertyInfo property)
        {
            var attribute = property.GetAttribute<EncryptedValueAttribute>();
            return attribute.Buckets;
        }
        #endregion

        #region Primary Key

        public static PrimaryKeyAttribute GetPrimaryKeyAttribute(this PropertyInfo property)
        {
            return property.GetAttribute<PrimaryKeyAttribute>();
        }

        public static bool IsPrimaryKey(this PropertyInfo property)
        {
            return property.GetPrimaryKeyAttribute() != null;
        }
        #endregion

        #endregion

        public static Type GetParentType(this PropertyInfo property)
        {
            var attribute = property.GetAttribute<ForeignKeyAttribute>();
            return attribute?.Parent;
        }

        public static PropertyInfo GetParentKey(this PropertyInfo property)
        {
            var parent = property.GetParentType();
            return parent?.GetPrimaryKey();
        }

        public static bool IsVirtualOrStaticOrAbstract(this PropertyInfo property)
        {
            var getGetMethod = property.GetGetMethod();
            if (getGetMethod == null)
            {
                return false;
            }
            var isVirtual = getGetMethod.IsVirtual && !getGetMethod.IsFinal;
            return getGetMethod.IsStatic || isVirtual || getGetMethod.IsAbstract;
        }

        public static bool IsNullable(this PropertyInfo propertyInfo)
        {
            var type = propertyInfo.PropertyType;
            return type.GetNullableType() != null;
        }
        public static object[][] GetValues(this PropertyInfo[] properties, object[] objects)
        {
            var values = new List<object[]>();
            foreach (var @object in objects)
            {
                var type = @object.GetType();
                var objectValues = new List<object>();
                foreach (var property in properties)
                {
                    if (type.GetProperty(property.Name) == null)
                    {
                        throw new NoPropertyFoundException(type.Name, property.Name);
                    }

                    var value = property.GetValue(@object);
                    if (value is Guid guidValue && guidValue == Guid.Empty && property.IsNullable())
                    {
                        value = null;
                    }

                    if (value is DateTime date && property.IsComparableDate())
                    {
                        value = date.ToUnixTimestamp();
                    }

                    if (value is int intValue && intValue == 0 && property.IsNullable())
                    {
                        value = null;
                    }
                    objectValues.Add(value);
                }
                values.Add(objectValues.ToArray());
            }

            return values.ToArray();
        }

        public static bool IsRequired(this PropertyInfo property)
        {
            var attribute = property.GetAttribute<RequiredAttribute>();
            return attribute != null;
        }
    }
}
