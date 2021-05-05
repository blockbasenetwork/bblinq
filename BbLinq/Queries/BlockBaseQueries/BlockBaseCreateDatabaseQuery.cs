using System;
using System.Collections.Generic;
using System.Text;
using BlockBase.BBLinq.Builders;
using BlockBase.BBLinq.ExtensionMethods;
using BlockBase.BBLinq.Model.Database;
using BlockBase.BBLinq.Queries.Base;

namespace BlockBase.BBLinq.Queries.BlockBaseQueries
{
    public class BlockBaseCreateDatabaseQuery : BlockBaseQuery
    {
        private readonly string _databaseName;
        private Type[] _tables;


        public BlockBaseCreateDatabaseQuery(string databaseName, Type[] tables) : base(false)
        {
            _databaseName = databaseName;
            _tables = tables;
        }

        public override string GenerateQueryString()
        {
            _tables = _tables.SortByDependency();
            var queryBuilder = new BlockBaseQueryBuilder();
            var entityColumns = new Dictionary<string, List<BlockBaseColumn>>();
            foreach (var entity in _tables)
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
                entityColumns.Add(entity.GetTableName(), columnList);
            }

            queryBuilder.CreateDatabase(_databaseName, IsEncrypted);
            foreach (var (name, columns) in entityColumns)
            {
                queryBuilder.CreateTable(name, columns.ToArray());
            }
            return queryBuilder.ToString();
        }
    }
}
