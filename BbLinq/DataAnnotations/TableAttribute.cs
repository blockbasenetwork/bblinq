using System;

namespace BlockBase.BBLinq.DataAnnotations
{
    /// <summary>
    /// The table attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class TableAttribute : BlockBaseDataAnnotationAttribute
    {
        public TableAttribute(string name)
        {
            Name = name;
        }

        /// <summary>
        /// The table's name
        /// </summary>
        public string Name { get;}
    }
}
