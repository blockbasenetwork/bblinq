using Newtonsoft.Json;

namespace agap2IT.Labs.BlockBase.BBLinq.Pocos.Results
{
    public class Response
    {
        [JsonProperty("columns")]
        public string[] Columns { get; set; }

        [JsonProperty("data")]
        public string[][] Data { get; set; }
    }
}
