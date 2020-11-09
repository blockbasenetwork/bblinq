using System.Threading.Tasks;
using agap2IT.Labs.BlockBase.BBLinq.Helper;
using agap2IT.Labs.BlockBase.BBLinq.Properties;

namespace agap2IT.Labs.BlockBase.BBLinq.Context
{
    internal class BbLinqExecutor
    {
        private readonly string _node;
        private readonly string _databaseName;
        internal BbLinqExecutor(string node, string databaseName)
        {
            _node = node;
            _databaseName = databaseName;
        }

        /// <summary>
        /// Performs a query request to the node and expects the database's structure as a root structure
        /// </summary>
        /// <returns>the database's structure as a root structure</returns>
        internal async Task<string> GetStructureAsync()
        {
            var request = HttpHelper.ComposeWebRequestGet($"{_node}{Resources.PATH_GET_STRUCTURE}");
            var json = await HttpHelper.CallWebRequestNoSslVerification(request);
            return json;
        }

        /// <summary>
        /// Performs a query request to the node and expects a result that is deserialized into a RootObject
        /// </summary>
        /// <param name="bodyQuery">the query to execute</param>
        /// <returns>a root result</returns>
        internal async Task<string> ExecuteQueryAsync(string bodyQuery)
        {
            bodyQuery = $"{SQLExpressions.USE} {_databaseName};\n{bodyQuery}";
            var request = HttpHelper.ComposeWebRequestPost($"{_node}{Resources.PATH_EXECUTE_QUERY}");
            var json = await HttpHelper.CallWebRequestNoSslVerification(request, bodyQuery);
            return json;
        }

        /// <summary>
        /// Performs a query request to the node and expects a JSON as result
        /// </summary>
        /// <param name="bodyQuery">the query to execute</param>
        /// <returns>a result</returns>
        internal async Task<string> ExecuteQueryToJsonAsync(string bodyQuery)
        {
            var request = HttpHelper.ComposeWebRequestPost($"{_node}{Resources.PATH_EXECUTE_QUERY_TO_CONTENT}");
            var json = await HttpHelper.CallWebRequestNoSslVerification(request, bodyQuery);
            return json;
        }
    }
}
