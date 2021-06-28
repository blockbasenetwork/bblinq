using Newtonsoft.Json;
using System.Collections.Generic;

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
        public IEnumerable<T> Result { get; set; }
    }
}
