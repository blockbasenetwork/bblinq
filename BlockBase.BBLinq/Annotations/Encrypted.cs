using System;

namespace BlockBase.BBLinq.Annotations
{
    /// <summary>
    /// An attribute on a property to make it encrypted
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class EncryptedAttribute : Attribute
    {
        /// <summary>
        /// The number of buckets used for encryption
        /// </summary>
        public int Buckets { get; set; }
    }
}
