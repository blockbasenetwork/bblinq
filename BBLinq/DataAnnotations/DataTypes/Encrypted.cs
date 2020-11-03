using System;

namespace agap2IT.Labs.BlockBase.BBLinq.DataAnnotations.DataTypes
{
    /// <summary>
    /// An attribute on a property to make it an encrypted value
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class EncryptedAttribute : Attribute
    {
        /// <summary>
        /// The amount of buckets used for encryption
        /// </summary>
        public int Buckets { get; set; }
    }
}
