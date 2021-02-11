using System;

namespace BlockBase.BBLinq.Annotations
{
    /// <summary>
    /// Sets the field as encrypted
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple =false,Inherited =false)]
    public class EncryptedAttribute : Attribute
    {
        public int Buckets { get; set; }
    }
}
