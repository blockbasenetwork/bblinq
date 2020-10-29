using BBLinq.Helper;
using BBLinq.POCO;
using Newtonsoft.Json;
using System.Collections;
using System.Threading.Tasks;

namespace BBLinq.Context
{
    internal class BBLinqExecutor
    {
        private string _node;
        private string _databaseName;
        internal BBLinqExecutor(string node, string databaseName)
        {
            _node = node;
            _databaseName = databaseName;
        }



        internal async Task<RootStructureObject> GetStructureAsync()
        {
            var request = HttpHelper.ComposeWebRequestGet($"{_node}/api/Requester/GetStructure");
            var json = await HttpHelper.CallWebRequestNoSSLVerification(request);
            var items = JsonConvert.DeserializeObject<RootStructureObject>(json);
            return items;
        }

        internal async Task<RootObject> ExecuteQueryAsync(string bodyQuery)
        {
            bodyQuery = $"USE {_databaseName};\n{bodyQuery}";
            var request = HttpHelper.ComposeWebRequestPost($"{_node}/api/Requester/ExecuteQuery");
            var json = await HttpHelper.CallWebRequestNoSSLVerification(request, bodyQuery);
            var responseObject = JsonConvert.DeserializeObject<RootObject>(json);
            return responseObject;
        }

        internal async Task<string> ExecuteQueryToJsonAsync(string bodyQuery)
        {
            var request = HttpHelper.ComposeWebRequestPost($"{_node}/api/Requester/ExecuteQuery");
            var json = await HttpHelper.CallWebRequestNoSSLVerification(request, bodyQuery);
            return json;
        }
    }
}
