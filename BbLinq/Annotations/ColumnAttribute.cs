using System;

namespace BlockBase.BBLinq.Annotations
{
    /// <summary>
    /// The Column attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple =false,Inherited =false)]
    public class ColumnAttribute : Attribute
    {

        public ColumnAttribute(string name)
        {
            Name = name;
        }

        /// <summary>
        /// The Column's name
        /// </summary>
        public string Name { get; set; }
    }
}
