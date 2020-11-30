using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using BlockBase.BBLinq.Helpers;

namespace BlockBase.BBLinq.QueryExecutors
{
    /// <summary>
    /// Base query executor
    /// </summary>
    public abstract class SqlQueryExecutor
    {
        
        public abstract Task<string> ExecuteQueryAsync(string query);

        /// <summary>
        /// Executes an asynchronous query
        /// </summary>
        /// <param name="request">An Http request</param>
        /// <param name="queryData">Data related to the query</param>
        /// <returns>The query's result</returns>
        protected async Task<string> ExecuteQueryAsync(HttpWebRequest request, Dictionary<string, string> queryData)
        {
            var result = await HttpHelper.CallWebRequestNoSSLVerification(request, queryData);
            return result;
        }
    }
}
