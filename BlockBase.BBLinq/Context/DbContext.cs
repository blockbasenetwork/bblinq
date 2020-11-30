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
    public abstract class DbContext<TSettings, TQueryExecutor, TQueryBuilder, TDictionary> where TSettings:DbSettings where TQueryExecutor : SqlQueryExecutor where TQueryBuilder : SqlQueryBuilder where TDictionary:SqlDictionary
    {
        private readonly ContextCache _cache = ContextCache.Instance;

        protected DbContext(TSettings settings, TQueryExecutor executor, TQueryBuilder queryBuilder, TDictionary dictionary)
        {
            _cache.Add("settings",settings);
            _cache.Add("executor", executor);
            _cache.Add("builder", queryBuilder);
            _cache.Add("dictionary", dictionary);
                
            var properties = GetType().GetProperties();
            var dbSets = new List<PropertyInfo>();

            foreach (var prop in properties)
            {
                if (prop.PropertyType.Is(typeof(DbSet)))
                {
                    dbSets.Add(prop);
                }
            }
            foreach (var prop in dbSets)
            {
                prop.SetValue(this, Activator.CreateInstance(prop.PropertyType));
            }
        }
    }
}
