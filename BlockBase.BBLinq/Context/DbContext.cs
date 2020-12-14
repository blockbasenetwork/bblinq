using System;
using System.Collections.Generic;
using System.Reflection;
using BlockBase.BBLinq.Builders;
using BlockBase.BBLinq.Dictionaries;
using BlockBase.BBLinq.ExtensionMethods;
using BlockBase.BBLinq.QueryExecutors;
using BlockBase.BBLinq.Sets;
using BlockBase.BBLinq.Settings;

namespace BlockBase.BBLinq.Context
{
    public abstract class DbContext<TSettings, TQueryExecutor, TQueryBuilder, TDictionary> : IDisposable where TSettings:DbSettings where TQueryExecutor : SqlQueryExecutor where TDictionary : SqlDictionary where TQueryBuilder:SqlQueryBuilder<TDictionary, TQueryBuilder>
    {
        private readonly ContextCache _cache = ContextCache.Instance;

        /// <summary>
        /// Builds a new context that adds settings, executors, builders and dictionaries to the cache. It also starts each data set.
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="executor"></param>
        /// <param name="queryBuilder"></param>
        /// <param name="dictionary"></param>
        protected DbContext(TSettings settings, TQueryExecutor executor, TQueryBuilder queryBuilder, TDictionary dictionary)
        {
            _cache.Add("settings",settings);
            _cache.Add("executor", executor);
            _cache.Add("queryBuilder", queryBuilder);
            _cache.Add("dictionary", dictionary);
                
            var properties = GetType().GetProperties();
            var dbSets = new List<PropertyInfo>();

            foreach (var prop in properties)
            {
                if (prop.PropertyType.Is(typeof(DbSet<TQueryExecutor>)))
                {
                    dbSets.Add(prop);
                }
            }
            foreach (var prop in dbSets)
            {
                prop.SetValue(this, Activator.CreateInstance(prop.PropertyType));
            }
        }


        /// <summary>
        /// Clears the context so that the executor is only available when needed
        /// </summary>
        public void Dispose()
        {
            _cache.Clear();
        }
    }
}
