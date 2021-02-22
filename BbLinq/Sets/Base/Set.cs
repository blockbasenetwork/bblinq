using BlockBase.BBLinq.Context.Base;
using BlockBase.BBLinq.Exceptions;
using BlockBase.BBLinq.Properties;
using BlockBase.BBLinq.Queries.Base;
using BlockBase.BBLinq.Result;
using System.Collections.Generic;

namespace BlockBase.BBLinq.Sets.Base
{
    public abstract class Set<T> : ISet where T : class
    {
        protected QueryResult StoreQueryInBatch(Query query)
        {
            var queries = ContextCache.Instance.Get<List<Query>>(Resources.CACHE_QUERIES);
            if (queries == null)
            {
                throw new MisusedCacheAccessException();
            }
            queries.Add(query);
            return new QueryResult(true, string.Format(Resources.CACHE_ADDED_TO_BATCH, GetType().Name));
        }
    }
}
