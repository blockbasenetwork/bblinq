using System.Collections.Generic;
using Newtonsoft.Json;

namespace BlockBase.BBLinq.Model.Responses
{
    internal abstract class RequestResultBase
    {
        [JsonProperty("succeeded")]
        public bool Succeeded { get; set; }

        [JsonProperty("exception")]
        public string Exception { get; set; }

        [JsonProperty("responseMessage")]
        public string Message { get; set; }
    }

    internal class RequestResult<T> : RequestResultBase
    {
        [JsonIgnore]
        public List<ExecutionResult<T>> Responses { get; set; } = new List<ExecutionResult<T>>();
    }
}
