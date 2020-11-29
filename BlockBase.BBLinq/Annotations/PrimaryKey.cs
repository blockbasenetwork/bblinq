using System;

namespace BlockBase.BBLinq.Annotations
{
    /// <summary>
    /// An attribute on a property to make it the table's primary key
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class PrimaryKeyAttribute : Attribute 
    {
    }
}
