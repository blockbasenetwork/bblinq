using System;

namespace BlockBase.BBLinq.Annotations
{
    
    [AttributeUsage(AttributeTargets.Property, AllowMultiple =false,Inherited =false)]
    public class RangeAttribute : Attribute
    {
        public int MinimumValue { get; set; }
        public int MaximumValue { get; set; }
        public int Buckets { get; set; }
    }
}
