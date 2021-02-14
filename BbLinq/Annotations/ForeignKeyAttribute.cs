using System;

namespace BlockBase.BBLinq.Annotations
{
    /// <summary>
    /// Foreign Key attribute. Either use the KeyType or the TableName
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple =false,Inherited =false)]
    public class ForeignKeyAttribute : Attribute
    {

        public ForeignKeyAttribute(string parentName)
        {
            ParentName = parentName;
        }


        public ForeignKeyAttribute(Type parent)
        {
            Parent = parent;
        }

        /// <summary>
        /// The parent table's name
        /// </summary>
        public string ParentName { get; }

        /// <summary>
        /// The type of the parent table
        /// </summary>
        public Type Parent { get; }
    }
}
