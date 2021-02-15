using System;

namespace BlockBase.BBLinq.Annotations
{
    /// <summary>
    /// The table attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class TableAttribute : BlockBaseAnnotationAttribute
    {

        public TableAttribute(string name)
        {
            Name = name;
        }

        /// <summary>
        /// The table's name
        /// </summary>
        public string Name { get; set; }
    }
}
