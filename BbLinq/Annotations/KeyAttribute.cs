using System;

namespace BlockBase.BBLinq.Annotations
{
    /// <summary>
    /// Primary key attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple =false,Inherited =false)]
    public class KeyAttribute : Attribute
    {
    }
}
