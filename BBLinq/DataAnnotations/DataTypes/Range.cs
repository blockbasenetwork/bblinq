namespace agap2IT.Labs.BlockBase.BBLinq.DataAnnotations.DataTypes
{
    /// <summary>
    /// An attribute on a property to make it a range
    /// </summary>
    public class RangeAttribute : DataTypeAttribute
    {
        /// <summary>
        /// The minimum value
        /// </summary>
        public double Min { get; set; }

        /// <summary>
        /// The maximum value
        /// </summary>
        public double Max { get; set; }
    }
}
