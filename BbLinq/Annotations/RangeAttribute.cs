using System;

namespace BlockBase.BBLinq.Annotations
{
    
    [AttributeUsage(AttributeTargets.Property, AllowMultiple =false,Inherited =false)]
    public class RangeAttribute : Attribute
    {
        public RangeAttribute(int minimum, int maximum, int buckets = 1)
        {
            Minimum = minimum;
            Maximum = maximum;
            Buckets = buckets;
        }

        /// <summary>
        /// The minimum range value
        /// </summary>
        public int Minimum{ get; set; }

        /// <summary>
        /// The maximum range value
        /// </summary>
        public int Maximum{ get; set; }

        /// <summary>
        /// The number of buckets
        /// </summary>
        public int Buckets { get; set; }
    }
}
