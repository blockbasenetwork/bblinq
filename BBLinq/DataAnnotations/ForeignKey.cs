using System;

namespace agap2IT.Labs.BlockBase.BBLinq.DataAnnotations
{
    /// <summary>
    /// An attribute on a property to make it foreign key to another table
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class ForeignKeyAttribute : Attribute
    {
        /// <summary>
        /// The other table's name
        /// </summary>
        public string Name { get; set; }
    }
}
