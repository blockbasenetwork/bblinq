using BlockBase.BBLinq.Builders;
using BlockBase.BBLinq.ExtensionMethods;
using BlockBase.BBLinq.Model.Database;
using BlockBase.BBLinq.Queries.Base;
using BlockBase.BBLinq.Queries.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlockBase.BBLinq.Queries.BlockBaseQueries
{
    public class BlockBaseRecordInsertQuery : BlockBaseQuery, IInsertQuery
    {
        public Type EntityType { get; }
        public object[] Records { get; }

        public BlockBaseRecordInsertQuery(Type entityType, object record, bool isEncrypted) : base(isEncrypted)
        {
            EntityType = entityType;
            if (record is IEnumerable<object> recordEnum)
            {
                Records = recordEnum.ToArray();
            }
            else
            {
                Records = new[] { record };
            }
        }


        public override string GenerateQueryString()
        {
            var tableName = EntityType.GetTableName();
            var queryBuilder = new BlockBaseQueryBuilder();
            var filteredProperties = GetFilteredProperties(EntityType);
            var columns = new List<BlockBaseColumn>();
            foreach (var column in filteredProperties)
            {
                columns.Add(BlockBaseColumn.From(column));
            }
            var values = filteredProperties.GetValues(Records);
            queryBuilder.InsertRecord(tableName, columns.ToArray(), values, IsEncrypted);
            return queryBuilder.ToString();
        }
    }
}
