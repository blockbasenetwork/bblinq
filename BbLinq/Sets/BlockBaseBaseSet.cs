using BlockBase.BBLinq.Context.Base;
using BlockBase.BBLinq.Properties;
using BlockBase.BBLinq.QueryExecutors;
using BlockBase.BBLinq.Sets.Base;
using System.Linq.Expressions;

namespace BlockBase.BBLinq.Sets
{
    public abstract class BlockBaseBaseSet<TEntity, TResult> : Set<TEntity> where TEntity : class where TResult : BlockBaseBaseSet<TEntity, TResult>

    {
        protected int RecordsToSkip;
        protected int RecordLimit;
        protected bool Encrypted;
        protected LambdaExpression Filter;

        protected BlockBaseQueryExecutor QueryExecutor =>
            ContextCache.Instance.Get<BlockBaseQueryExecutor>(Resources.CACHE_EXECUTOR);

        /// <summary>
        /// Returns the set to its original form
        /// </summary>
        public virtual TResult Clear()
        {
            Filter = null;
            Encrypted = false;
            RecordLimit = -1;
            RecordsToSkip = 0;
            return (TResult)this;
        }

        /// <summary>
        /// Adds a encrypt flag so that the query, when executed is encrypted
        /// </summary>
        public TResult Encrypt()
        {
            Encrypted = true;
            return (TResult)this;
        }

        /// <summary>
        /// Sets the amount of records the user intends to skip before fetching a result
        /// </summary>
        public TResult Skip(int amount)
        {
            RecordsToSkip = amount;
            return (TResult)this;
        }

        /// <summary>
        /// Sets the amount of records the user intends to fetch
        /// </summary>
        public TResult Take(int amount)
        {
            RecordLimit = amount;
            return (TResult)this;
        }

        /// <summary>
        /// Adds a filter on a select query
        /// </summary>
        protected virtual TResult Where(LambdaExpression predicate)
        {
            Filter = predicate;
            return (TResult)this;
        }

    }
}
