using System.Collections.Generic;

namespace agap2IT.Labs.BlockBase.BBLinq.Poco
{
    public class RootObject
    {
        public bool Succeeded { get; set; }
        public object Exception { get; set; }
        public List<ResponseString> Response { get; set; }
        public object ResponseMessage { get; set; }
    }
}
