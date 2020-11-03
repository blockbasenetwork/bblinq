namespace agap2IT.Labs.BlockBase.BBLinq.DataAnnotations.DataTypes
{
    /// <summary>
    /// An attribute on a property to make it an encrypted range value
    /// </summary>
    public class EncryptedRangeAttribute : DataTypeAttribute
    {
        /// <summary>
        /// The minimum value
        /// </summary>
        public long Min { get; set; }

        /// <summary>
        /// The amount of buckets used for encryption
        /// </summary>
        public long Buckets { get; set; }

        /// <summary>
        /// The maximum value
        /// </summary>
        public long Max { get; set; }
    }
}
