using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlockBase.BBLinq.Builders;
using BlockBase.BBLinq.Dictionaries;
using BlockBase.BBLinq.Exceptions;
using BlockBase.BBLinq.ExtensionMethods;
using BlockBase.BBLinq.Parsers;
using BlockBase.BBLinq.Queries;
using BlockBase.BBLinq.QueryExecutors;
using BlockBase.BBLinq.Results;
using BlockBase.BBLinq.Sets;
using BlockBase.BBLinq.Settings;

namespace BlockBase.BBLinq.Context
{
    public class BbContext : DbContext<BbSettings, BbQueryExecutor, BbSqlQueryBuilder, BbSqlDictionary>
    {
        private BbQueryExecutor Executor => ContextCache.Instance.Get<BbQueryExecutor>("executor");
        private BbSettings Settings => ContextCache.Instance.Get<BbSettings>("settings");

        public BbContext(BbSettings settings) : base(settings, new BbQueryExecutor(), new BbSqlQueryBuilder(), new BbSqlDictionary())
        {
        }


        /// <summary>
        /// Creates the database
        /// </summary>
        public async Task<QueryResult> CreateDatabase()
        {

            if (Settings == default || Executor == default)
            {
                throw new NoContextAvailableException();
            }

            var bbSets = GetType().GetProperties();
            var types = new List<Type>();
            foreach (var set in bbSets)
            {
                if (!set.PropertyType.Is(typeof(DbSet))) continue;
                var type = set.PropertyType.GetGenericArguments()[0];
                types.Add(type);
            }

            var query = new CreateDatabaseQuery(Settings.DatabaseName, types.ToArray());
            (Executor).UseDatabase = false;
            var res = Executor.ExecuteQueryAsync(query.ToString());
            (Executor).UseDatabase = true;
            return await ResultParser.ParseQueryResult(res);
        }

        /// <summary>
        /// Deletes the database
        /// </summary>
        public async Task<QueryResult> DropDatabase()
        {
            if (Settings == null || Executor == null)
            {
                throw new NoContextAvailableException();
            }

            var query = new DropDatabaseQuery(Settings.DatabaseName);
            Executor.UseDatabase = false;
            var res = Executor.ExecuteQueryAsync(query.ToString());
            Executor.UseDatabase = true;
            return await ResultParser.ParseQueryResult(res);
        }
    }
}
