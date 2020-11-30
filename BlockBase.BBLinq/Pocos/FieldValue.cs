using System;
using System.Collections.Generic;
using System.Text;

namespace BlockBase.BBLinq.Pocos
{
    public struct FieldValue
    {
        public string Table{ get; set; }
        public string Field{ get; set; }
        public object Value{ get; set; }
    }
}
