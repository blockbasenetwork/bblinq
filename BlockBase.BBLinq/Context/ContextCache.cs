using System.Collections.Generic;

namespace BlockBase.BBLinq.Context
{
    /// <summary>
    /// The context's cache
    /// </summary>
    public sealed class ContextCache
    {
        /// <summary>
        /// Instance of the context cache
        /// </summary>
        public static ContextCache Instance { get; } = new ContextCache();

        /// <summary>
        /// The cache's content
        /// </summary>
        private Dictionary<string, object> _cacheContent;

        /// <summary>
        /// The default's cache constructor
        /// </summary>
        private ContextCache()
        {
            _cacheContent = new Dictionary<string, object>();
        }

        public void Clear()
        {
            _cacheContent = new Dictionary<string, object>();
        }

        /// <summary>
        /// Adds a new record to the cache
        /// </summary>
        /// <typeparam name="T">the record's type</typeparam>
        /// <param name="key">the record's name</param>
        /// <param name="value">the record</param>
        public void Add<T>(string key, T value)
        {
            lock (_cacheContent)
            {
                _cacheContent[key] = value;
            }
        }

        /// <summary>
        /// Fetches a record from the cache
        /// </summary>
        /// <typeparam name="T">the record's type</typeparam>
        /// <param name="key">the record's key</param>
        /// <returns>Default value if the record doesn't exist or the type does not match the expected type. It returns the error otherwise</returns>
        public T Get<T>(string key)
        {
            lock (_cacheContent)
            {
                if (!_cacheContent.ContainsKey(key))
                {
                    return default;
                }

                var value = _cacheContent[key];
                if (!(value is T)) return default;
                return (T) value;
            }
        }
    }
}
