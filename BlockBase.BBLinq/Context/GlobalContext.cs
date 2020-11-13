namespace BlockBase.BBLinq.Context
{
    /// <summary>
    /// Context used to store items needed to the context execution such as query executors
    /// </summary>
    public sealed class GlobalContext
    {
        private static GlobalContext _instance;
        private static readonly object Padlock = new object();
        private static BbLinqExecutor _executor;

        private GlobalContext()  { }

        public static GlobalContext Instance
        {
            get
            {
                lock (Padlock)
                {
                    return _instance ??= new GlobalContext();
                }
            }
        }

        /// <summary>
        /// Deletes the content on the context
        /// </summary>
        public void Clear()
        {
            _executor = default;
        }

        /// <summary>
        /// The query executor available
        /// </summary>
        public BbLinqExecutor Executor
        {
            get => _executor;
            set => _executor = value;
        }
    }
}
