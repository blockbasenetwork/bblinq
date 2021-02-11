using System;

namespace BlockBase.BBLinq.Annotations
{
    /// <summary>
    /// Foreign Key attribute. Either use the KeyType or the TableName
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple =false,Inherited =false)]
    public class ForeignKeyAttribute : Attribute
    {
        /// <summary>
        /// The type on the primary key side
        /// </summary>
        public Type KeyType { get; set; }
    }
}
