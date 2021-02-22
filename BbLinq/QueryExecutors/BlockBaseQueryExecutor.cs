using BlockBase.BBLinq.QueryExecutors.Base;
using BlockBase.BBLinq.Result;
using System;
using System.Threading.Tasks;

namespace BlockBase.BBLinq.QueryExecutors
{
    public class BlockBaseQueryExecutor : QueryExecutor
    {

        public override async Task<QueryResult> ExecuteQueryAsync(string query, bool setDatabase = true, bool useTransaction = false)
        {
            throw new NotImplementedException();
        }

        public override async Task<QueryResult<T>> ExecuteQueryAsync<T>(string query, bool setDatabase = true, bool useTransaction = false)
        {
            throw new NotImplementedException();
        }
    }
}
