using Newtonsoft.Json;

namespace BlockBase.BBLinq.Pocos.Results
{
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
