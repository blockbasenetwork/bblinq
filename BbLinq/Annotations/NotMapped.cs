using System;

namespace BlockBase.BBLinq.Annotations
{
    /// <summary>
    /// For properties that should not be mapped
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited =false)]
    public class NotMappedAttribute : Attribute
    {

    }
}
