using BlockBase.BBLinq.Queries.Base;
using BlockBase.BBLinq.Result;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlockBase.BBLinq.QueryExecutors.Base
{
    public abstract class QueryExecutor
    {
        public abstract Task<QueryResult> ExecuteQueryAsync(string query, bool setDatabase = true, bool useTransaction = false);
        public abstract Task<QueryResult<T>> ExecuteQueryAsync<T>(string query, bool setDatabase = true, bool useTransaction = false);

        public virtual async Task<QueryResult> ExecuteBatchAsync(List<Query> batch)
        {
            var queryString = string.Empty;
            foreach (var queryItem in batch)
            {
                queryString += queryItem;
            }
            return await ExecuteQueryAsync(queryString);
        }
    }
}
