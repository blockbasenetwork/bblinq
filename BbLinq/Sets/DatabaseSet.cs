using BlockBase.BBLinq.Executors;

namespace BlockBase.BBLinq.Sets
{
    public abstract class DatabaseSet<TResult, TQueryExecutor> where TResult: DatabaseSet<TResult, TQueryExecutor> where TQueryExecutor : IQueryExecutor, new()
    {
        protected TQueryExecutor Executor;

        protected DatabaseSet()
        {
            Executor = new TQueryExecutor();
        }
    }
}
