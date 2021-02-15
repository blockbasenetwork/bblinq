using System;
using System.Collections.Generic;
using BlockBase.BBLinq.Exceptions;
using BlockBase.BBLinq.ExtensionMethods;

namespace BlockBase.BBLinq.Queries.BaseQueries
{
    public abstract class CreateDatabaseQuery : Query
    {
        public string DatabaseName { get; protected set; }
        public Type[] Entities { get; protected set; }

        protected CreateDatabaseQuery(string databaseName, Type[] entities)
        {
            DatabaseName = databaseName;
            Entities = entities;
            OrderTablesByDependency();
        }

        /// <summary>
        /// Orders the table list by dependency
        /// </summary>
        protected void OrderTablesByDependency()
        {
            var unsortedList = new List<Type>();
            var sortedList = new List<Type>();
            
            foreach (var entity in Entities)
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

            var loopLimit = unsortedList.Count+1;
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
        }
    }
}
