namespace BlockBase.BBLinq.Pocos.Components
{

    /// <summary>
    /// A table, field, value tuple
    /// </summary>
    public struct TableFieldValue
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
        /// The field's value
        /// </summary>
        public object FieldValue { get; set; }
    }
}
