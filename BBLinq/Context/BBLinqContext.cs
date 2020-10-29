using BBLinq.Data;
using System;
using System.Linq;

namespace BBLinq.Context
{
    public abstract class BBContext : IDisposable
    {
        public BBContext(string node, string databaseName)
        {
            var executor = new BBLinqExecutor(node, databaseName);
            GlobalContext.Instance.Executor = executor;
            var bbSets = GetType().GetProperties().Where(x => x.PropertyType.BaseType == typeof(BBSet));
            foreach (var prop in bbSets)
            {
                prop.SetValue(this, Activator.CreateInstance(prop.PropertyType));
            }
        }

        public void Dispose()
        {
            GlobalContext.Instance.Clear();
        }
    }
}
