using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BlockBase.BBLinq.Builders;
using BlockBase.BBLinq.Exceptions;
using BlockBase.BBLinq.Helpers;
using BlockBase.BBLinq.Parsers;
using BlockBase.BBLinq.Queries.Interfaces;
using BlockBase.BBLinq.QueryExecutors.Interfaces;
using BlockBase.BBLinq.Settings.Base;

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
            var parsedResult = (new BlockBaseResultParser()).Parse<string>(result, null);
            if (!parsedResult.Succeeded)
            {
                throw new QueryExecutionException(parsedResult.Message);
            }

            foreach (var response in parsedResult.Responses)
            {
                var exception = "";
                if (!response.Executed)
                {
                    exception += response.Message;
                }
                if (exception != "")
                    throw new QueryExecutionException(exception);
            }
        }

        public async Task ExecuteBatchQueryAsync(List<IQuery> queries, DatabaseSettings settings)
        {
            var queryString = "";
            var useDatabase = true;
            for (var queryIndex = 0; queryIndex < queries.Count; queryIndex++)
            {
                queryString+=BuildQueryString(queries[queryIndex].GenerateQueryString(), settings, useDatabase);
                if (queryIndex == 0)
                {
                    useDatabase = false;
                }
            }

            //queryString = WrapQueryInTransaction(queryString);
            var requestBody = GenerateRequestBody(queryString, settings);
            var result = await CallRequest(settings, requestBody);
            var parsedResult = (new BlockBaseResultParser()).Parse<string>(result, null);
            if (!parsedResult.Succeeded)
            {
                throw new QueryExecutionException(parsedResult.Message);
            }

            foreach (var response in parsedResult.Responses)
            {
                if (!response.Executed)
                {
                    throw new QueryExecutionException(response.Message);
                }
            }
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

            if (parsedResult.Responses == null)
            {

            }
            var result = new List<TResult>();
            foreach (var response in parsedResult.Responses)
            {
                if (response == null) continue;
                if (response.Content == null)
                {
                    if (!response.Executed)
                    {
                        throw new QueryExecutionException(parsedResult.Message);
                    }
                }
                else
                {
                    result.AddRange(response?.Content);
                }
            }
            return result;
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
