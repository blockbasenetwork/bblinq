using BlockBase.BBLinq.Exceptions;
using BlockBase.BBLinq.Queries.Interfaces;
using BlockBase.BBLinq.QueryExecutors.Interfaces;
using BlockBase.BBLinq.Sets.Interfaces;
using BlockBase.BBLinq.Settings.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlockBase.BBLinq.Contexts.Base
{
    public abstract class DatabaseContext<TSettings, TQueryExecutor> : IDisposable where TSettings : DatabaseSettings where TQueryExecutor : IQueryExecutor
    {
        protected TSettings Settings;
        protected TQueryExecutor QueryExecutor;
        protected List<IQuery> BatchQueries;

        protected DatabaseContext(TSettings settings, TQueryExecutor executor, List<IQuery> batchQueries)
        {
            Settings = settings;
            BatchQueries = batchQueries;
            QueryExecutor = executor;
            InstantiateSets();
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
                        var instance = Activator.CreateInstance(prop.PropertyType, QueryExecutor, Settings, BatchQueries);
                        prop.SetValue(this, instance);
                    }
                }
            }
        }

        protected ISet Set<T>() where T : class
        {
            var properties = GetType().GetProperties();

            foreach (var prop in properties)
            {
                var interfaces = prop.PropertyType.GetInterfaces();
                if ((from @interface in interfaces where @interface == typeof(ISet) select prop.PropertyType.GetGenericArguments()).Any(genericArguments => genericArguments.Length > 0 && genericArguments[0] == typeof(T)))
                {
                    return (ISet)prop.GetValue(this);
                }
            }
            throw new NoSetAvailableException(typeof(T).Name);
        }

        public void Dispose()
        {
        }
    }
}
