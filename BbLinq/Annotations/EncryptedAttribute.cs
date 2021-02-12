using System;

namespace BlockBase.BBLinq.Annotations
{
    /// <summary>
    /// Sets the field as encrypted
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple =false,Inherited =false)]
    public class EncryptedAttribute : Attribute
    {
        /// <summary>
        /// Number of buckets used to distribute data
        /// </summary>
        public int? Buckets { get; set; }
    }
}
