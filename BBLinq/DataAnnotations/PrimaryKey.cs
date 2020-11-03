using System;

namespace agap2IT.Labs.BlockBase.BBLinq.DataAnnotations
{
    /// <summary>
    /// An attribute on a property to make it the table's primary key
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class PrimaryKeyAttribute : Attribute 
    {
    }
}
