using BlockBase.BBLinq.Queries.Base;
using System;
using System.Collections.Generic;
using BlockBase.BBLinq.ExtensionMethods;
using BlockBase.BBLinq.Pocos;
using BlockBase.BBLinq.Exceptions;

namespace BlockBase.BBLinq.Queries.BlockBase
{
    public class CreateDatabaseQuery : BlockBaseQuery
    {
        private readonly string _databaseName;
        private Type[] _tables;

        public CreateDatabaseQuery(string databaseName, Type[] tables)
        {
            _databaseName = databaseName;
            _tables = tables;
        }

        public override string GenerateQuery()
        {
            OrderTablesByDependency();
            var entityColumns = new Dictionary<Type, List<BlockBaseColumn>>();
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
                entityColumns.Add(entity, columnList);
            }
            

            return QueryBuilder.CreateDatabase(_databaseName, entityColumns).ToString();
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
