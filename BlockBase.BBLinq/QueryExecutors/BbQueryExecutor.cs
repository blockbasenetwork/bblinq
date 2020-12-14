using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BlockBase.BBLinq.Builders;
using BlockBase.BBLinq.Context;
using BlockBase.BBLinq.Helpers;
using BlockBase.BBLinq.Properties;
using BlockBase.BBLinq.Settings;

namespace BlockBase.BBLinq.QueryExecutors
{
    public class BbQueryExecutor : SqlQueryExecutor
    {
        private BbSettings Settings => ContextCache.Instance.Get<BbSettings>("settings");

        /// <summary>
        /// If it's true, it'll add a "USE database". Turn it to false to act over the structure and to add If statements
        /// </summary>
        public bool UseDatabase { get; set; } = true;

        /// <summary>
        /// Executes an asynchronous query
        /// </summary>
        /// <param name="query">a query string</param>
        /// <returns>The query's result</returns>
        public override async Task<string> ExecuteQueryAsync(string query)
        {
            if (UseDatabase)
            {
                var queryBuilder = new BbSqlQueryBuilder();
                queryBuilder.Use().WhiteSpace().Append(Settings.DatabaseName).End().Append(query);
                query = queryBuilder.ToString();
            }
            var request = HttpHelper.ComposePostWebRequest($"{Settings.NodeAddress}{Resources.PATH_EXECUTE_QUERY}");
            var signature = SignatureHelper.SignHash(Settings.PrivateKey, Encoding.UTF8.GetBytes(query));
            var queryRequest = new Dictionary<string, string>
            {
                {"Query", query}, 
                {"Account", Settings.UserAccount}, 
                {"Signature", signature}
            };
            return await ExecuteQueryAsync(request, queryRequest);
        }
    }
}
