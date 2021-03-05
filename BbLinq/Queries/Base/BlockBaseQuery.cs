using System;
using System.Collections.Generic;
using System.Reflection;
using BlockBase.BBLinq.ExtensionMethods;

namespace BlockBase.BBLinq.Queries.Base
{
    public class BlockBaseQuery
    {

        private readonly List<Type> _availableDataTypes = new List<Type>()
        {
            typeof(bool),
            typeof(int),
            typeof(decimal),
            typeof(double),
            typeof(TimeSpan),
            typeof(string),
            typeof(DateTime),
            typeof(Guid)
        };
        protected bool IsValidColumn(PropertyInfo property)
        {
            var type = property.IsNullable() ? property.PropertyType.GetNullableType() : property.PropertyType;

            return !property.IsVirtualOrStaticOrAbstract() &&
                   type.IsValidDataType(_availableDataTypes) &&
                   !property.IsNotMapped();
        }

        protected PropertyInfo[] GetFilteredProperties<T>(bool addPrimaryKey = true)
        {
            var properties = typeof(T).GetProperties();
            var filteredProperties = new List<PropertyInfo>();
            foreach (var property in properties)
            {
                if (!addPrimaryKey)
                {
                    if (property.IsPrimaryKey())
                    {
                        continue;
                    }
                }

                if (IsValidColumn(property))
                {
                    filteredProperties.Add(property);
                }
            }
            return filteredProperties.ToArray();
        }
    }
}
