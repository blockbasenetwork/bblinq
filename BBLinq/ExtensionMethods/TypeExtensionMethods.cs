using BlockBase.BBLinq.DataAnnotations;
using BlockBase.BBLinq.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BlockBase.BBLinq.ExtensionMethods
{
    /// <summary>
    /// Extension methods for the Type type
    /// </summary>
    internal static class TypeExtensionMethods
    {
        #region Data Annotations

        #region Table
        public static TableAttribute GetTableAttribute(this PropertyInfo property)
        {
            var attribute = property.GetAttribute<TableAttribute>();
            return attribute;
        }

        public static string GetTableName(this Type entity)
        {
            var columnAttribute = entity.GetAttribute<TableAttribute>();
            return columnAttribute == null ? entity.Name : columnAttribute.Name;
        }

        #endregion

        #endregion

        public static PropertyInfo GetPrimaryKeyProperty(this Type entity)
        {
            var properties = entity.GetPropertiesWithAttribute<PrimaryKeyAttribute>();
            return properties != null && properties.Length > 0 ? properties[0] : null;
        }

        public static Type GetNullableType(this Type type)
        {
            return Nullable.GetUnderlyingType(type);
        }

        public static bool MatchesDataType(this Type type, List<Type> types)
        {
            if (type.IsEnum)
            {
                type = typeof(int);
            }
            return types.Contains(type);
        }

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


        public static ForeignKeyAttribute GetForeignKey(this PropertyInfo property)
        {
            var attribute = property.GetAttribute<ForeignKeyAttribute>();
            return attribute;
        }

        public static PropertyInfo[] GetForeignKeys(this Type entity)
        {
            return entity.GetPropertiesWithAttribute<ForeignKeyAttribute>();
        }

        public static PropertyInfo GetForeignKey(this Type entity, Type parentType)
        {
            var foreignKeys = entity.GetPropertiesWithAttribute<ForeignKeyAttribute>();
            return foreignKeys?.FirstOrDefault(fk => fk.GetForeignKey().Parent == parentType);
        }

        public static bool ContainsAllDependencies(this List<Type> entities, Type targetEntity)
        {
            var foreignKeys = targetEntity.GetForeignKeys();
            var hasDependencies = true;
            foreach (var foreignKey in foreignKeys)
            {
                if (!entities.Contains(foreignKey.GetForeignKey().Parent))
                {
                    hasDependencies = false;
                }
            }
            return hasDependencies;
        }

        public static Type[] SortByDependency(this Type[] entities)
        {
            var list = new List<Type>(entities);
            var resultList = new List<Type>();
            var hasLooped = false;
            var counter = 0;
            while (list.Count > 0)
            {
                var currentEntity = list[counter];
                if (!currentEntity.HasDependencies() || resultList.ContainsAllDependencies(currentEntity))
                {
                    list.Remove(currentEntity);
                    resultList.Add(currentEntity);
                    hasLooped = false;
                }
                else
                {
                    counter++;
                }

                if (counter != list.Count) continue;
                if (hasLooped) throw new NoDependencyFoundInEntitiesException(currentEntity.GetForeignKeys(), resultList.ToArray());
                if (list.Count <= 0) continue;
                hasLooped = true;
                counter = 0;
            }
            return resultList.ToArray();
        }

        public static PropertyInfo GetPrimaryKey(this Type entity)
        {
            var properties = entity.GetPropertiesWithAttribute<PrimaryKeyAttribute>();
            return properties != null && properties.Length > 0 ? properties[0] : null;
        }

        public static bool HasDependencies(this Type entity)
        {
            var foreignKeys = entity.GetPropertiesWithAttribute<ForeignKeyAttribute>();

            return foreignKeys != null && foreignKeys.Length > 0;
        }

    }
}
