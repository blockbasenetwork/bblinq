using System.Collections.Generic;
using Newtonsoft.Json;

namespace BlockBase.BBLinq.Results
{
    public class Result
    {
        /// <summary>
        /// Checks if the query executed with success
        /// </summary>
        [JsonProperty("succeeded")]
        public bool Succeeded { get; set; }

        /// <summary>
        /// The resulting exception
        /// </summary>
        [JsonProperty("exception")]
        public string Exception { get; set; }


        /// <summary>
        /// The response message
        /// </summary>
        [JsonProperty("responseMessage")]
        public string ResponseMessage { get; set; }


        /// <summary>
        /// The list of responses
        /// </summary>
        [JsonProperty("response")]
        public IEnumerable<Response> Response { get; set; }
    }

    /// <summary>
    /// A JSON response
    /// </summary>
    public class Response
    {
        /// <summary>
        /// A list of columns
        /// </summary>
        [JsonProperty("columns")]
        public string[] Columns { get; set; }

        /// <summary>
        /// A set of data
        /// </summary>
        [JsonProperty("data")]
        public string[][] Data { get; set; }
    }
}
