namespace BlockBase.BBLinq.Settings
{
    /// <summary>
    /// Settings for a basic database connection
    /// </summary>
    public class DbSettings
    {
        /// <summary>
        /// The node's address
        /// </summary>
        public string NodeAddress { get; set; }
        
        /// <summary>
        /// The database's address
        /// </summary>
        public string DatabaseName { get; set; }
    }
}
