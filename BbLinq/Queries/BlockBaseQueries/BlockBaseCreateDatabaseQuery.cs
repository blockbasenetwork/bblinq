using System;
using System.Collections.Generic;
using System.Reflection;
using BlockBase.BBLinq.ExtensionMethods;
using BlockBase.BBLinq.Pocos;
using BlockBase.BBLinq.Queries.BaseQueries;
using BlockBase.BBLinq.QueryBuilders;

namespace BlockBase.BBLinq.Queries.BlockBaseQueries
{
    public class BlockBaseCreateDatabaseQuery : CreateDatabaseQuery
    {
        public BlockBaseCreateDatabaseQuery(string databaseName, Type[] entities) : base(databaseName, entities)
        {
        }

        public override string ToString()
        {
            var queryBuilder = new BbSqlQueryBuilder();
            queryBuilder.CreateDatabase(DatabaseName);
            foreach (var entity in Entities)
            {
                var properties = entity.GetProperties();
                var columnList = new List<BlockBaseColumn>();
                foreach (var property in properties)
                {
                    if (IsValidColumn(property))
                    {
                        columnList.Add(BlockBaseColumn.From(property));
                    }
                }
                queryBuilder.CreateTable(entity.GetTableName(), columnList.ToArray());
            }
            return queryBuilder.ToString();
        }

        private bool IsValidColumn(PropertyInfo property)
        {
            return !property.IsVirtualOrStaticOrAbstract() &&
                   property.IsAcceptableType() &&
                   !property.IsNotMapped();
        }

    }
}
