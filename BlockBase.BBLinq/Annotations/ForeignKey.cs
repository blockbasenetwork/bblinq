using System;

namespace BlockBase.BBLinq.Annotations
{
    /// <summary>
    /// An attribute on a property to make it foreign key to another table
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ForeignKeyAttribute : Attribute
    {
        /// <summary>
        /// The other table's name
        /// </summary>
        public string Name { get; set; }
    }
}
