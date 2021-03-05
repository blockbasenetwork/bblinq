using System.Collections.Generic;
using BlockBase.BBLinq.Contexts;
using BlockBase.BBLinq.Exceptions;
using BlockBase.BBLinq.Executors;
using BlockBase.BBLinq.Properties;
using BlockBase.BBLinq.Queries.Base;

namespace BlockBase.BBLinq.Sets
{
    public class BlockBaseBaseSet<TResult> : DatabaseSet<TResult, BlockBaseQueryExecutor> where TResult : BlockBaseBaseSet<TResult>
    {
        protected void StoreQueryInBatch(IQuery query)
        {
            var queries = ContextCache.Instance.Get<List<IQuery>>(Resources.CACHE_QUERIES);
            if (queries == null)
            {
                throw new UnavailableCacheException();
            }
            queries.Add(query);
        }
    }
}
