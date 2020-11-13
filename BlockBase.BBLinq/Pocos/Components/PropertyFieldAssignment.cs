using System.Reflection;

namespace BlockBase.BBLinq.Pocos.Components
{
    /// <summary>
    /// A Table, Field, Property tuple
    /// </summary>
    public struct PropertyFieldAssignment
    {
        /// <summary>
        /// The table's name
        /// </summary>
        public string TableName { get; set; }
        
        /// <summary>
        /// The field's name
        /// </summary>
        public string FieldName { get; set; }
        
        /// <summary>
        /// The property
        /// </summary>
        public PropertyInfo Property { get; set; }

    }
}
