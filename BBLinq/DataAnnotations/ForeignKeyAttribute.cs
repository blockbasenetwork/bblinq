using BlockBase.BBLinq.DataAnnotations.Base;
using BlockBase.BBLinq.ExtensionMethods;
using System;
using System.Reflection;

namespace BlockBase.BBLinq.DataAnnotations
{
    /// <summary>
    /// Foreign Key attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ForeignKeyAttribute : BbLinqAttribute
    {
        private Type _parent;

        /// <summary>
        /// The type of the parent table
        /// </summary>
        public Type Parent
        {
            get => _parent;
            set
            {
                _parent = value;
                if (value != null)
                {
                    PrimaryKey = value.GetPrimaryKeyProperty();
                }
            }
        }

        internal PropertyInfo PrimaryKey { get; private set; }
    }
}
