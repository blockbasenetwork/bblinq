namespace BBLinq.Context
{
    internal sealed class GlobalContext
    {
        private static GlobalContext instance = null;
        private static readonly object padlock = new object();
        private static BBLinqExecutor _executor;

        GlobalContext()
        {
        }

        internal static GlobalContext Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new GlobalContext();
                    }
                    return instance;
                }
            }
        }

        internal void Clear() => _executor = default;

        internal BBLinqExecutor Executor
        {
            get => _executor;
            set => _executor = value;
        }

    }
}
