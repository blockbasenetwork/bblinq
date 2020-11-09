using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace agap2IT.Labs.BlockBase.BBLinq.Pocos.Results
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
