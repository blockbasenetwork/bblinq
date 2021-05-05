using BlockBase.BBLinq.QueryExecutors.Interfaces;
using BlockBase.BBLinq.Settings.Base;

namespace BlockBase.BBLinq.Sets.Base
{
    public abstract class DatabaseSet<TResult, TQueryExecutor, TSettings> where TResult: DatabaseSet<TResult, TQueryExecutor, TSettings> where TQueryExecutor : IQueryExecutor where TSettings:DatabaseSettings
    {
        protected TQueryExecutor Executor;
        protected TSettings Settings;

        protected DatabaseSet(TQueryExecutor executor, TSettings settings)
        {
            Executor = executor;
            Settings = settings;
        }
    }
}
