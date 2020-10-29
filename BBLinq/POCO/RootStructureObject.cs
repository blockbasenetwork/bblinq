using System.Collections.Generic;

namespace BBLinq.POCO
{
    public class RootStructureObject
    {
        public bool succeeded { get; set; }
        public object exception { get; set; }
        public List<StructureResponse> response { get; set; }
        public object responseMessage { get; set; }
    }
}
