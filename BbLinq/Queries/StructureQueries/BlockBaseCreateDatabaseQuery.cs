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
            OrderTablesByDependency();
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


        /// <summary>
        /// Orders the table list by dependency
        /// </summary>
        protected void OrderTablesByDependency()
        {
            var unsortedList = new List<Type>();
            var sortedList = new List<Type>();

            foreach (var entity in _tables)
            {
                var foreignKeys = entity.GetForeignKeyProperties();
                if (foreignKeys == null || foreignKeys.Length == 0)
                {
                    sortedList.Add(entity);
                }
                else
                {
                    unsortedList.Add(entity);
                }
            }

            var loopLimit = unsortedList.Count + 1;
            while (unsortedList.Count != 0)
            {
                var unsortedListCount = unsortedList.Count;
                for (var unsortedCounter = 0; unsortedCounter < unsortedListCount; unsortedCounter++)
                {
                    var currentUnsorted = unsortedList[unsortedCounter];
                    var foreignKeyProperties = currentUnsorted.GetForeignKeyProperties();
                    var checkCount = 0;
                    foreach (var foreignKeyProperty in foreignKeyProperties)
                    {
                        var foreignKey = foreignKeyProperty.GetForeignKeys()[0];
                        foreach (var sortedType in sortedList)
                        {
                            if (foreignKey.Parent == sortedType)
                            {
                                checkCount++;
                                if (checkCount == foreignKeyProperties.Length)
                                {
                                    break;
                                }
                            }
                        }
                    }

                    if (checkCount == foreignKeyProperties.Length)
                    {
                        sortedList.Add(currentUnsorted);
                        unsortedList.Remove(currentUnsorted);
                        unsortedCounter--;
                        unsortedListCount--;
                    }
                }
                loopLimit--;
                if (loopLimit != 0)
                {
                    continue;
                }
                var unsortedNames = new List<string>();
                foreach (var unsorted in unsortedList)
                {
                    unsortedNames.Add(unsorted.Name);
                }
                throw new IncompleteModelException(unsortedNames);
            }
            _tables = sortedList.ToArray();
        }
    }
}
