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
        /// Constructor used for string parent
        /// </summary>
        /// <param name="parentName">the parent's class name</param>
        /// <param name="keyPropertyName">the id property</param>
        public ForeignKeyAttribute(string parentName)
        {
            ParentName = parentName;
        }

        /// <summary>
        /// Constructor used for type parent
        /// </summary>
        /// <param name="parentName">the parent's class</param>
        /// <param name="keyPropertyName">the id property</param>
        public ForeignKeyAttribute(Type parent)
        {
            Parent = parent;
        }

        /// <summary>
        /// The parent table's name
        /// </summary>
        public string ParentName { get; private set; }

        /// <summary>
        /// The type of the parent table
        /// </summary>
        public Type Parent { get; private set; }
    }
}
