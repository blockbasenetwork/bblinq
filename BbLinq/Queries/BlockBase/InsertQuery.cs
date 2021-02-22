using BlockBase.BBLinq.Queries.Base;
using System;
using System.Collections.Generic;
using System.Reflection;
using BlockBase.BBLinq.ExtensionMethods;

namespace BlockBase.BBLinq.Queries.BlockBase
{
    public class InsertQuery<T> : BlockBaseQuery
    {
        /// <summary>
        /// An object used as reference for the DELETE operation
        /// </summary>
        public List<T> Records { get; }

        public InsertQuery(T record)
        {
            Records = new List<T>() { record };
        }

        public InsertQuery(List<T> records)
        {
            Records = records;
        }

        public override string GenerateQuery()
        {
            var tableName = typeof(T).GetTableName();
            var filteredProperties = GetFilteredProperties<T>();
            var columnNames = new List<string>();
            var columnValues = new object[Records.Count, filteredProperties.Length];
            for (var propertyCounter = 0; propertyCounter < filteredProperties.Length; propertyCounter++)
            {
                columnNames.Add(filteredProperties[propertyCounter].GetColumnName());
                for (var recordCounter = 0; recordCounter < Records.Count; recordCounter++)
                {
                    columnValues[recordCounter, propertyCounter] =
                        filteredProperties[propertyCounter].GetValue(Records[recordCounter]);
                }
            }
            QueryBuilder.Clear().InsertValuesIntoTable(tableName, columnNames.ToArray(), columnValues);
            return QueryBuilder.ToString();
        }

    }
}
