using System;

namespace BlockBase.BBLinq.Annotations
{
    /// <summary>
    /// The table attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple =false,Inherited =false)]
    public class TableAttribute : Attribute
    {
        /// <summary>
        /// The table's name
        /// </summary>
        public string Name { get; set; }
    }
}
