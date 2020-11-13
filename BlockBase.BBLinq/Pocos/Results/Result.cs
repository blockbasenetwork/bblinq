using System.Collections.Generic;
using Newtonsoft.Json;

namespace BlockBase.BBLinq.Pocos.Results
{
    /// <summary>
    /// A JSON response
    /// </summary>
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
}
