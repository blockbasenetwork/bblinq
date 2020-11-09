using System;

namespace BlockBase.BBLinq.DataAnnotations
{
    /// <summary>
    /// An attribute on a class to replace its name with a suitable one for the table
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class TableAttribute : Attribute
    {
        /// <summary>
        /// The table's name
        /// </summary>
        public string Name { get; set; }

        public TableAttribute(string name)
        {
            Name = name;
        }
    }
}
