using System;

namespace BlockBase.BBLinq.Annotations
{
    /// <summary>
    /// The Column attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple =false,Inherited =false)]
    public class ColumnAttribute : Attribute
    {
        /// <summary>
        /// The Column's name
        /// </summary>
        public string Name { get; set; }
    }
}
