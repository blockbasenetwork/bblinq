using System;
using System.Linq;
using BlockBase.BBLinq.Sets;

namespace BlockBase.BBLinq.Context
{
    /// <summary>
    /// The original BlockBase context
    /// </summary>
    public abstract class BbContext : IDisposable
    {
        /// <summary>
        /// Default constructor that sets up the executor and sets a default value for each set
        /// </summary>
        /// <param name="node">the node to be used</param>
        /// <param name="databaseName">the database to be used</param>
        protected BbContext(string node, string databaseName)
        {
            var executor = new BbLinqExecutor(node, databaseName);
            GlobalContext.Instance.Executor = executor;
            var bbSets = GetType().GetProperties().Where(x => x.PropertyType.BaseType == typeof(BbSet));
            foreach (var prop in bbSets)
            {
                prop.SetValue(this, Activator.CreateInstance(prop.PropertyType));
            }
        }

        /// <summary>
        /// Clears the context so that the executor is only available when needed
        /// </summary>
        public void Dispose()
        {
            GlobalContext.Instance.Clear();
        }

    }
}
