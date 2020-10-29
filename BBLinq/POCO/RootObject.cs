using System.Collections.Generic;

namespace BBLinq.POCO
{
    public class RootObject
    {
        public bool succeeded { get; set; }
        public object exception { get; set; }
        public List<ResponseString> response { get; set; }
        public object responseMessage { get; set; }
    }
}
