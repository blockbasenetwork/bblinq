using System.Collections.Generic;
using System.Reflection;
using BlockBase.BBLinq.Builders;
using BlockBase.BBLinq.ExtensionMethods;
using BlockBase.BBLinq.Queries.Base;

namespace BlockBase.BBLinq.Queries.RecordQueries
{
    public class BlockBaseInsertRecordQuery<T> : BlockBaseQuery, IQuery
    {
        public List<T> Records { get; }

        public BlockBaseInsertRecordQuery(T record)
        {
            Records = new List<T>() {record};
        }

        public BlockBaseInsertRecordQuery(List<T> records)
        {
            Records = records;
        }

        public string GenerateQueryString()
        {
            var tableName = typeof(T).GetTableName();
            var queryBuilder = new BlockBaseQueryBuilder();
            var filteredProperties = GetFilteredProperties<T>();
            var columns = GetColumnNames(filteredProperties);
            var values = GetValues(filteredProperties);
            queryBuilder.Insert(tableName, columns, values);
            return queryBuilder.ToString();
        }

        private string[] GetColumnNames(PropertyInfo[] columns)
        {
            var columnNames = new List<string>();
            foreach (var column in columns)
            {
                columnNames.Add(column.Name);
            }
            return columnNames.ToArray();
        }

        private object[][] GetValues(PropertyInfo[] columns)
        {
            List<object[]> values = new List<object[]>();
            foreach (var record in Records)
            {
                var recordValues = new List<object>();
                foreach (var column in columns)
                {
                    recordValues.Add(column.GetValue(record));
                }
                values.Add(recordValues.ToArray());
            }

            return values.ToArray();
        }
    }
}
