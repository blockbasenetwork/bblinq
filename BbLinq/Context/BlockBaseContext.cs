using BlockBase.BBLinq.Context.Base;
using BlockBase.BBLinq.Properties;
using BlockBase.BBLinq.Queries.Base;
using BlockBase.BBLinq.Queries.BlockBase;
using BlockBase.BBLinq.QueryExecutors;
using BlockBase.BBLinq.Result;
using BlockBase.BBLinq.Sets;
using BlockBase.BBLinq.Settings;
using System;
using System.Threading.Tasks;

namespace BlockBase.BBLinq.Context
{
    /// <summary>
    /// Inherit this context 
    /// </summary>
    public abstract class BlockBaseContext : DbContext<BlockBaseSettings, BlockBaseQueryExecutor>
    {

        /// <summary>
        /// Constructor by settings wrapper
        /// </summary>
        protected BlockBaseContext(BlockBaseSettings settings) : base(settings, new BlockBaseQueryExecutor())
        {

        }

        /// <summary>
        /// Constructor by simple types
        /// </summary>
        protected BlockBaseContext(string address, string dbName, string account, string privateKey) : 
            base(new BlockBaseSettings(){PrivateKey = privateKey, UserAccount = account, DatabaseName = dbName, NodeAddress = address}, new BlockBaseQueryExecutor())
        {

        }

        /// <summary>
        /// Returns a set based on the type specified
        /// </summary>
        public new BbSet<T> Set<T>() where T:class
        {
            return (BbSet<T>) base.Set<T>();
        }

        /// <summary>
        /// Begins an If Statement by adding its condition
        /// </summary>
        public IfStatement If(Func<Query> condition)
        {
            return new IfStatement(condition.Invoke());
        }

        /// <summary>
        /// Executes a query specified on a string by the user. 
        /// </summary>
        public async Task<QueryResult<T>> ExecuteQuery<T>(string queryString, bool setDatabase = true, bool useInTransaction = false)
        {
            var executor = ContextCache.Instance.Get<BlockBaseQueryExecutor>(Resources.CACHE_EXECUTOR);
            return await executor.ExecuteQueryAsync<T>(queryString, setDatabase, useInTransaction);
        }
    }
}
