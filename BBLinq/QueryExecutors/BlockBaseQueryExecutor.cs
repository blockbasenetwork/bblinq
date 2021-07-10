using System;
using BlockBase.BBLinq.Builders;
using BlockBase.BBLinq.Exceptions;
using BlockBase.BBLinq.Helpers;
using BlockBase.BBLinq.Parsers;
using BlockBase.BBLinq.Queries.Interfaces;
using BlockBase.BBLinq.QueryExecutors.Interfaces;
using BlockBase.BBLinq.Settings.Base;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockBase.BBLinq.QueryExecutors
{
    public class BlockBaseQueryExecutor : IQueryExecutor
    {
        public bool UseDatabase { get; set; }

        #region Callers
        public async Task ExecuteQueryAsync(IQuery query, DatabaseSettings settings)
        {
            var queryString = BuildQueryString(query.GenerateQueryString(), settings, true);
            var requestBody = GenerateRequestBody(queryString, settings);
            var result = await CallRequest(settings, requestBody);
        }

        public async Task ExecuteBatchQueryAsync(List<IQuery> queries, DatabaseSettings settings, bool useTransaction)
        {
            var queryBuilder = new BlockBaseQueryBuilder();
            var queryString = "";
            AddUseDatabase(queryBuilder, settings);
            if (!queries.Any())
            {
                throw new Exception("No queries in batch");
            }
            for (var queryIndex = 0; queryIndex < queries.Count; queryIndex++)
            {
                queryString += BuildQueryString(queries[queryIndex].GenerateQueryString(), settings, false);
            }

            if (useTransaction)
            {
                queryString = WrapQueryInTransaction(queryString);
            }
            queryBuilder.Append(queryString);
            queryString = queryBuilder.ToString();
            var requestBody = GenerateRequestBody(queryString, settings);
            var result = await CallRequest(settings, requestBody);
            var parsedResult = (new BlockBaseResultParser()).Parse<string>(result, null, true);
            if (!parsedResult.Succeeded)
            {
                throw new QueryExecutionException(parsedResult.Message);
            }
        }

        private string WrapQueryInTransaction(string queryString)
        {
            return "Begin;" + queryString + "Commit;";
        }

        public async Task<IEnumerable<TResult>> ExecuteQueryAsync<TResult>(ISelectQuery query, DatabaseSettings settings)
        {
            var queryString = BuildQueryString(query.GenerateQueryString(), settings, true);
            var requestBody = GenerateRequestBody(queryString, settings);
            var callResult = await CallRequest(settings, requestBody);
            var parsedResult = (new BlockBaseResultParser()).Parse<TResult>(callResult, query);
            if (!parsedResult.Succeeded)
            {
                throw new QueryExecutionException(parsedResult.Message);
            }
            return parsedResult.Result;
        }

        #endregion

        #region Completers
        private string BuildQueryString(string queryString, DatabaseSettings settings, bool useDatabase)
        {
            var queryBuilder = new BlockBaseQueryBuilder();
            if (useDatabase)
            {
                AddUseDatabase(queryBuilder, settings);
            }
            queryBuilder.Append(queryString);
            return queryBuilder.ToString();
        }

        private void AddUseDatabase(BlockBaseQueryBuilder queryBuilder, DatabaseSettings settings)
        {
            if (UseDatabase)
            {
                queryBuilder.UseDatabase(settings.DatabaseName);
            }
        }

        #endregion

        #region Request auxiliary

        private static Dictionary<string, string> GenerateRequestBody(string query, DatabaseSettings settings)
        {
            var signature = SignatureHelper.SignHash(settings.Password, Encoding.UTF8.GetBytes(query));
            var queryRequest = new Dictionary<string, string>
            {
                {"Query", query},
                {"Account", settings.UserAccount},
                {"Signature", signature}
            };
            return queryRequest;
        }

        #endregion

        private static async Task<string> CallRequest(DatabaseSettings settings, Dictionary<string, string> body)
        {
            var request = HttpHelper.ComposePostWebRequest($"{settings.Host}/api/Requester/ExecuteQuery");
            return await HttpHelper.CallWebRequestNoSslVerification(request, body);
        }
    }
}
