namespace BlockBase.BBLinq.Pocos.Components
{
    /// <summary>
    /// A table, field tuple
    /// </summary>
    public struct TableField
    {
        /// <summary>
        /// The table's name
        /// </summary>
        public string TableName { get; set; }
        
        /// <summary>
        /// The field's name
        /// </summary>
        public string FieldName { get; set; }
    }
}
