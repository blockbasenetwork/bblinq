using System.Collections.Generic;

namespace BBLinq.POCO
{
    public class ResponseString
    {
        public List<string> columns { get; set; }
        public List<List<string>> data { get; set; }
    }
}
