using System.Collections.Generic;
using System.Linq.Expressions;
using BlockBase.BBLinq.Contexts;
using BlockBase.BBLinq.Exceptions;
using BlockBase.BBLinq.Executors;
using BlockBase.BBLinq.Pocos.Nodes;
using BlockBase.BBLinq.Properties;
using BlockBase.BBLinq.Queries.Base;

namespace BlockBase.BBLinq.Sets
{
    public class BlockBaseBaseJoinSet<TResult> : DatabaseSet<TResult, BlockBaseQueryExecutor> where TResult : BlockBaseBaseJoinSet<TResult>
    {
        public JoinNode[] Joins { get; protected set; }
        public Expression Predicate { get; protected set; }
        public int RecordsToSkip { get; protected set; }
        public int RecordsToTake { get; protected set; }
        public bool EncryptQuery { get; protected set; }

        protected void StoreQueryInBatch(IQuery query)
        {
            var queries = ContextCache.Instance.Get<List<IQuery>>(Resources.CACHE_QUERIES);
            if (queries == null)
            {
                throw new UnavailableCacheException();
            }
            queries.Add(query);
        }

        public TResult Skip(int recordsToSkip)
        {
            RecordsToSkip = recordsToSkip;
            return (TResult) this;
        }

        public TResult Take(int recordsToTake)
        {
            RecordsToTake = recordsToTake;
            return (TResult)this;
        }


        public TResult Encrypt()
        {
            EncryptQuery = true;
            return (TResult)this;
        }
    }
}
