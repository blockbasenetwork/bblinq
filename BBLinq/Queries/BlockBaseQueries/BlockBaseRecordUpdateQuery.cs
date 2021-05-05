using BlockBase.BBLinq.Queries.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BlockBase.BBLinq.Builders;
using BlockBase.BBLinq.ExtensionMethods;
using BlockBase.BBLinq.Model.Base;
using BlockBase.BBLinq.Model.Database;
using BlockBase.BBLinq.Queries.Interfaces;

namespace BlockBase.BBLinq.Queries.BlockBaseQueries
{
    public class BlockBaseRecordUpdateQuery : BlockBaseQuery, IUpdateQuery
    {
        internal BlockBaseRecordUpdateQuery(Type entityType, object record, ExpressionNode condition, bool isEncrypted) :base(isEncrypted)
        {
            EntityType = entityType;
            Record = record;
            if (condition == null)
            {
                Condition = NodeBuilder.GenerateComparisonNodeOnKey(record);
            }
            Condition = condition;
        }

        public Type EntityType { get; }

        public object Record { get; }

        internal ExpressionNode Condition { get; }

        public override string GenerateQueryString()
        {
            var queryBuilder = new BlockBaseQueryBuilder();
            var tableName = EntityType.GetTableName();
            var columns = GetColumnsAndValues();
            queryBuilder.UpdateRecord(tableName, columns, Condition, IsEncrypted);
            return queryBuilder.ToString();
        }

        internal (BlockBaseColumn, object)[] GetColumnsAndValues()
        {
            var properties = new List<PropertyInfo>();

            if(EntityType == Record.GetType())
            {
                properties.AddRange(GetFilteredProperties(EntityType, false));
            }
            else
            {
                var unfilteredProperties = Record.GetType().GetProperties();
                var primaryKey = EntityType.GetPrimaryKeyProperty();
                foreach (var property in unfilteredProperties)
                {
                    if (property.Name != primaryKey.Name)
                    {
                        properties.Add(property);
                    }
                }
            }

            return (from property in properties let value = property.GetValue(Record) where value != null select (BlockBaseColumn.From(property), value)).ToArray();
        }
    }
}
