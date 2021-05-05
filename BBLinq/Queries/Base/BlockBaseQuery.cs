using System;
using System.Collections.Generic;
using System.Reflection;
using BlockBase.BBLinq.ExtensionMethods;
using BlockBase.BBLinq.Queries.Interfaces;

namespace BlockBase.BBLinq.Queries.Base
{
    public abstract class BlockBaseQuery : IQuery
    {
        public bool IsEncrypted { get; }

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

        protected BlockBaseQuery(bool isEncrypted)
        {
            IsEncrypted = isEncrypted;
        }

        protected bool IsValidColumn(PropertyInfo property)
        {
            var type = property.IsNullable() ? property.PropertyType.GetNullableType() : property.PropertyType;

            return !property.IsVirtualOrStaticOrAbstract() &&
                   type.MatchesDataType(_availableDataTypes) &&
                   !property.IsNotMapped();
        }

        protected PropertyInfo[] GetFilteredProperties(Type type, bool addPrimaryKey = true)
        {
            var properties = type.GetProperties();
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


        public virtual string GenerateQueryString()
        {
            return string.Empty;
        }
    }
}
