using System;

namespace BlockBase.BBLinq.DataAnnotations
{
    /// <summary>
    /// The Column attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnAttribute : BlockBaseDataAnnotationAttribute
    {
        public ColumnAttribute(string name)
        {
            Name = name;
        }

        /// <summary>
        /// The Column's name
        /// </summary>
        public string Name { get;}
    }
}
