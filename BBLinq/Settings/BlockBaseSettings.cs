using BlockBase.BBLinq.Settings.Base;

namespace BlockBase.BBLinq.Settings
{
    public class BlockBaseSettings : DatabaseSettings
    {
        /// <summary>
        /// Defines dates as format invariant
        /// </summary>
        public bool IsDateTimeInvariant { get; set; }
    }
}
