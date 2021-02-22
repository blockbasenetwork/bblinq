using BlockBase.BBLinq.Exceptions;
using BlockBase.BBLinq.Properties;
using BlockBase.BBLinq.Queries.Base;
using BlockBase.BBLinq.QueryExecutors.Base;
using BlockBase.BBLinq.Result;
using BlockBase.BBLinq.Sets.Base;
using BlockBase.BBLinq.Settings.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlockBase.BBLinq.Context.Base
{
    public abstract class DbContext<TSettings, TExecutor> : IDisposable where TSettings : DbSettings where TExecutor : QueryExecutor

    {
        private static ContextCache Cache => ContextCache.Instance;

        protected DbContext(TSettings settings, TExecutor executor)
        {
            Cache.Set(Resources.CACHE_SETTINGS, settings);
            Cache.Set(Resources.CACHE_QUERIES, new List<Query>());
            Cache.Set(Resources.CACHE_EXECUTOR, executor);
            InstantiateSets();
        }

        public async Task<QueryResult> ExecuteBatch()
        {
            var batch = Cache.Get<List<Query>>(Resources.CACHE_QUERIES);
            var executor = Cache.Get<TExecutor>(Resources.CACHE_EXECUTOR);
            return await executor.ExecuteBatchAsync(batch);
        }

        protected Set<T> Set<T>() where T : class
        {
            var properties = GetType().GetProperties();

            foreach (var prop in properties)
            {
                var interfaces = prop.PropertyType.GetInterfaces();
                foreach (var @interface in interfaces)
                {
                    if (@interface != typeof(ISet))
                    {
                        continue;
                    }
                    var genericArguments = prop.PropertyType.GetGenericArguments();
                    if (genericArguments.Length > 0 && genericArguments[0] == typeof(T))
                    {
                        return (Set<T>)prop.GetValue(this);
                    }
                }
            }

            throw new NoSetAvailableException(typeof(T).Name);
        }

        /// <summary>
        /// Sets a default value for each set on the context
        /// </summary>
        private void InstantiateSets()
        {
            var properties = GetType().GetProperties();

            foreach (var prop in properties)
            {
                var interfaces = prop.PropertyType.GetInterfaces();
                foreach (var @interface in interfaces)
                {
                    if (@interface == typeof(ISet))
                    {
                        prop.SetValue(this, Activator.CreateInstance(prop.PropertyType));
                    }
                }
            }
        }

        /// <summary>
        /// Clears the context so that the executor is only available when needed
        /// </summary>
        public void Dispose()
        {
            Cache.Clear();
        }
    }
}
