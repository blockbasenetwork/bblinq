namespace BlockBase.BBLinq.Pocos.Components
{
    /// <summary>
    /// A pairing of field name and value
    /// </summary>
    public readonly struct FieldValuePairing
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="name">the field's name</param>
        /// <param name="value">the field's value</param>
        public FieldValuePairing(string name, object value)
        {
            FieldName = name;
            Value = value;
        }

        /// <summary>
        /// The field's name
        /// </summary>
        public string FieldName { get; }
        
        /// <summary>
        /// THe field's value
        /// </summary>
        public object Value { get; }
    }
}
