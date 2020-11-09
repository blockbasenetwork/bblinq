using System.Collections.Generic;
using Newtonsoft.Json;

namespace BlockBase.BBLinq.Pocos.Results
{
    public class Result
    {
        [JsonProperty("succeeded")]
        public bool Succeeded { get; set; }

        [JsonProperty("exception")]
        public string Exception { get; set; }

        [JsonProperty("responseMessage")]
        public string ResponseMessage { get; set; }

        [JsonProperty("response")]
        public IEnumerable<Response> Response { get; set; }
    }
}
