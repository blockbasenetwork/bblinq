using System.IO;
using System.Net;
using System.Threading.Tasks;
using BlockBase.BBLinq.Properties;
using Newtonsoft.Json;

namespace BlockBase.BBLinq.Helper
{
    public static class HttpHelper
    {
        public static HttpWebRequest ComposeWebRequestPost(string url) => ComposeWebRequest(url, "POST");
        public static HttpWebRequest ComposeWebRequestGet(string url) => ComposeWebRequest(url, "GET");

        public static HttpWebRequest ComposeWebRequest(string url, string method)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = method;
            request.ContentType = Resources.REQUEST_CONTENT_TYPE;
            return request;
        }

        public static async Task<string> CallWebRequest(HttpWebRequest httpWebRequest, object jsonBody)
        {
            await using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                var json = JsonConvert.SerializeObject(jsonBody);

                await streamWriter.WriteAsync(json);
            }
            var response = (HttpWebResponse)httpWebRequest.GetResponse();
            return await new StreamReader(response.GetResponseStream()).ReadToEndAsync();
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

        public static async Task<string> CallWebRequestNoSslVerification(HttpWebRequest httpWebRequest)
        {
            httpWebRequest.ServerCertificateValidationCallback = delegate { return true; };
            var response = (HttpWebResponse)httpWebRequest.GetResponse();
            return await new StreamReader(response.GetResponseStream()).ReadToEndAsync();
        }


        public static async Task<string> CallWebRequest(HttpWebRequest httpWebRequest)
        {
            httpWebRequest.ServerCertificateValidationCallback = delegate { return true; };
            var response = (HttpWebResponse)httpWebRequest.GetResponse();
            return await new StreamReader(response.GetResponseStream()).ReadToEndAsync();
        }

    }
}
