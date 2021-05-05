using System;
using BlockBase.BBLinq.DataAnnotations;
using BlockBase.BBLinq.DataAnnotations.Base;

namespace BlockBase.BBLinq.DataAnnotations
{
    /// <summary>
    /// Sets the field as encrypted
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class EncryptedValueAttribute : BbLinqAttribute
    {
        public EncryptedValueAttribute(int buckets = 0)
        {
            Buckets = buckets;
        }

        /// <summary>
        /// Number of buckets used to distribute data
        /// </summary>
        public int Buckets { get; }
    }
}
