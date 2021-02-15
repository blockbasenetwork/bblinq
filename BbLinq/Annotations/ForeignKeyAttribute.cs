using System;

namespace BlockBase.BBLinq.Annotations
{
    /// <summary>
    /// Foreign Key attribute. Either use the KeyType or the TableName
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ForeignKeyAttribute : BlockBaseAnnotationAttribute
    {
        public ForeignKeyAttribute(Type parent)
        {
            Parent = parent;
        }

        /// <summary>
        /// The type of the parent table
        /// </summary>
        public Type Parent { get; }
    }
}
