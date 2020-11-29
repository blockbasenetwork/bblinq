using System;

namespace BlockBase.BBLinq.Annotations
{
    /// <summary>
    /// A ranged field attribute
    /// </summary>
    public class RangeAttribute : Attribute
    {
        /// <summary>
        /// The number of buckets used for encryption
        /// </summary>
        public int Buckets { get; set; }

        /// <summary>
        /// The minimum number of rows used
        /// </summary>
        public int Minimum { get; set; }

        /// <summary>
        /// The maximum number of rows used
        /// </summary>
        public int Maximum { get; set; }
    }
}
