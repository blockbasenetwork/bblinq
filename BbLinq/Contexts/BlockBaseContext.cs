using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlockBase.BBLinq.Contexts.Base;
using BlockBase.BBLinq.Queries.BlockBaseQueries;
using BlockBase.BBLinq.Queries.Interfaces;
using BlockBase.BBLinq.QueryExecutors;
using BlockBase.BBLinq.Sets;
using BlockBase.BBLinq.Settings;

namespace BlockBase.BBLinq.Contexts
{
    public abstract class BlockBaseContext : DatabaseContext<BlockBaseSettings, BlockBaseQueryExecutor>
    {
        protected BlockBaseContext(BlockBaseSettings settings):base(settings, new BlockBaseQueryExecutor(), new List<IQuery>())
        {
        }

        public new BlockBaseSet<T> Set<T>() where T : class
        {
            return base.Set<T>() as BlockBaseSet<T>;
        }

        public async Task CreateDatabase()
        {
            var sets = GetType().GetProperties();
            var databaseName = Settings.DatabaseName;
            var tables =
                (from set in sets
                    where (set.PropertyType.GetInterface("ISet") != null)
                    select set.PropertyType.GetGenericArguments()[0]).ToArray();

            var query = new BlockBaseCreateDatabaseQuery(databaseName, tables);
            QueryExecutor.UseDatabase = false;
            await QueryExecutor.ExecuteQueryAsync(query, Settings);
        }

        public void InsertQueryInBatch(IQuery query)
        {
            BatchQueries.Add(query);
        }

        public async Task ExecuteQueryBatchAsync()
        {
            QueryExecutor.UseDatabase = true;
            await QueryExecutor.ExecuteBatchQueryAsync(BatchQueries, Settings);
        }

        /// <summary>
        /// Delete the database
        /// </summary>
        public async Task DropDatabase()
        {
            var databaseName = Settings.DatabaseName;
            var query = new BlockBaseDropDatabaseQuery(databaseName);
            QueryExecutor.UseDatabase = false;
            await QueryExecutor.ExecuteQueryAsync(query, Settings);
        }
    }
}
