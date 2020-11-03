using System.Collections.Generic;

namespace agap2IT.Labs.BlockBase.BBLinq.Poco
{
    public class RootStructureObject
    {
        public bool Succeeded { get; set; }
        public object Exception { get; set; }
        public List<StructureResponse> Response { get; set; }
        public object ResponseMessage { get; set; }
    }
}
