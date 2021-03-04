using System;

namespace BlockBase.BBLinq.DataAnnotations
{
    /// <summary>
    /// A Range column
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class RangeAttribute : BlockBaseDataAnnotationAttribute
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
        public int Minimum { get; }

        /// <summary>
        /// The maximum range value
        /// </summary>
        public int Maximum { get; }

        /// <summary>
        /// The number of buckets
        /// </summary>
        public int Buckets { get; }
    }
}
