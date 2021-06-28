using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace BlockBase.BBLinq.Helpers
{
    public static class HttpHelper
    {
        public static HttpWebRequest ComposePostWebRequest(string url) => ComposeWebRequest(url, "POST");

        public static HttpWebRequest ComposeWebRequest(string url, string method)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = method;
            request.ContentType = "application/json";

            return request;
        }

        public static async Task<string> CallWebRequestNoSslVerification(HttpWebRequest httpWebRequest, object jsonBody)
        {
            httpWebRequest.ServerCertificateValidationCallback = delegate { return true; };

            await using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                var json = JsonConvert.SerializeObject(jsonBody);

                await streamWriter.WriteAsync(json);
            }

            var response = (HttpWebResponse)httpWebRequest.GetResponse();

            return await new StreamReader(response.GetResponseStream()).ReadToEndAsync();
        }
    }
}