using System;

namespace BlockBase.BBLinq.Annotations
{
    /// <summary>
    /// Sets the field as encrypted
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class EncryptedAttribute : BlockBaseAnnotationAttribute
    {
        public EncryptedAttribute(int buckets = 0)
        {
            Buckets = buckets;
        }

        /// <summary>
        /// Number of buckets used to distribute data
        /// </summary>
        public int Buckets { get; set; }
    }
}
