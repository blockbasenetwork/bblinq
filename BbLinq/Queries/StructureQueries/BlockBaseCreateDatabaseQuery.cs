using System;
using System.Collections.Generic;
using System.Reflection;
using BlockBase.BBLinq.Builders;
using BlockBase.BBLinq.Exceptions;
using BlockBase.BBLinq.ExtensionMethods;
using BlockBase.BBLinq.Pocos;
using BlockBase.BBLinq.Queries.Base;

namespace BlockBase.BBLinq.Queries.StructureQueries
{
    public class CreateDatabaseQuery : BlockBaseQuery, IQuery
    {
        private readonly string _databaseName;
        private Type[] _tables;


        public CreateDatabaseQuery(string databaseName, Type[] tables)
        {
            _databaseName = databaseName;
            _tables = tables;
        }

        public string GenerateQueryString()
        {
            _tables = _tables.OrderTablesByDependency();
            var queryBuilder = new BlockBaseQueryBuilder();
            var entityColumns = new Dictionary<string, List<ColumnDefinition>>();
            foreach (var entity in _tables)
            {
                var properties = entity.GetProperties();
                var columnList = new List<ColumnDefinition>();
                foreach (var property in properties)
                {
                    if (IsValidColumn(property))
                    {
                        columnList.Add(ColumnDefinition.From(property));
                    }
                }
                entityColumns.Add(entity.GetTableName(), columnList);
            }

            queryBuilder.CreateDatabase(_databaseName);
            foreach (var (name, columns) in entityColumns)
            {
                queryBuilder.CreateTable(name, columns.ToArray());
            }
            return queryBuilder.ToString();
        }
    }
}
