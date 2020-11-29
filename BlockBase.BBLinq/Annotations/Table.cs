using System;

namespace BlockBase.BBLinq.Annotations
{
    /// <summary>
    /// An attribute on a class to replace its name with a suitable one for the table
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class TableAttribute : Attribute
    {
        /// <summary>
        /// The table's name
        /// </summary>
        public string Name { get; set; }
    }
}
