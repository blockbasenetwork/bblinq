namespace BlockBase.BBLinq.Settings.Base
{
    /// <summary>
    /// Settings for a basic database connection
    /// </summary>
    public abstract class DatabaseSettings
    {
        /// <summary>
        /// The node's address
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// The database's address
        /// </summary>
        public string DatabaseName { get; set; }

        /// <summary>
        /// The nuser's account
        /// </summary>
        public string UserAccount { get; set; }

        /// <summary>
        /// The user's password
        /// </summary>
        public string Password { get; set; }
    }
}
