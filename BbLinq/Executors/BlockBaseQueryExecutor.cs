using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BlockBase.BBLinq.Helpers;
using BlockBase.BBLinq.Settings;

namespace BlockBase.BBLinq.Executors
{
    public class BlockBaseQueryExecutor : IQueryExecutor
    {
        public Task ExecuteQueryAsync(string query, bool useDatabase = true)
        {
            return ExecuteQueryAsync(query, useDatabase, false);

        }

        public Task<T> ExecuteQueryAsync<T>(string query, bool useDatabase = true)
        {
            return ExecuteQueryAsync<T>(query, useDatabase, false);
        }

        public Task ExecuteQueryAsync(string query, bool useDatabase, bool useTransaction )
        {
            //TODO GetSettings
            //TODO Add UseDatabase
            //TODO Add transaction
            //TODO Call Query
            //TODO throw needed exceptions
            throw new NotImplementedException();
        }

        public Task<T> ExecuteQueryAsync<T>(string query, bool useDatabase, bool useTransaction)
        {
            //TODO GetSettings
            //TODO Add UseDatabase
            //TODO Add transaction
            //TODO Call Query
            //TODO throw needed exceptions
            //TODO convert result
            throw new NotImplementedException();
        }

        private BlockBaseSettings GetSettings()
        {
            return null;
        }

        private async Task<string> ExecuteQueryAsync(string query)
        {
            var settings = GetSettings();
            var request = HttpHelper.ComposePostWebRequest($"{settings.NodeAddress}");
            var signature = SignatureHelper.SignHash(settings.PrivateKey, Encoding.UTF8.GetBytes(query));
            var queryRequest = new Dictionary<string, string>
            {
                {"Query", query},
                {"Account", settings.UserAccount},
                {"Signature", signature}
            };
            return await ExecuteQueryAsync(request, queryRequest);
        }

        private static async Task<string> ExecuteQueryAsync(HttpWebRequest request, Dictionary<string, string> queryData)
        {
            var result = await HttpHelper.CallWebRequestNoSSLVerification(request, queryData);
            return result;
        }
    }
}
