using System;

namespace BlockBase.BBLinq.Annotations
{
    
    [AttributeUsage(AttributeTargets.Property, AllowMultiple =false,Inherited =false)]
    public class RangeAttribute : Attribute
    {
        /// <summary>
        /// The minimum range value
        /// </summary>
        public int MinimumValue { get; set; }

        /// <summary>
        /// The maximum range value
        /// </summary>
        public int MaximumValue { get; set; }

        /// <summary>
        /// The number of buckets
        /// </summary>
        public int Buckets { get; set; }
    }
}
