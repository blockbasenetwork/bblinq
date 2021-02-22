using BlockBase.BBLinq.Settings.Base;

namespace BlockBase.BBLinq.Settings
{
    /// <summary>
    /// Settings for a BlockBase node
    /// </summary>
    public sealed class BlockBaseSettings : DbSettings
    {
        /// <summary>
        /// The node's user account
        /// </summary>
        public string UserAccount { get; set; }

        /// <summary>
        /// The node's private key
        /// </summary>
        public string PrivateKey { get; set; }
    }
}
