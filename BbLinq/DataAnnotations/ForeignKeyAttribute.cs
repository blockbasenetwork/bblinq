using System;

namespace BlockBase.BBLinq.DataAnnotations
{
    /// <summary>
    /// Foreign Key attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ForeignKeyAttribute : BlockBaseDataAnnotationAttribute
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
