using System.Linq.Expressions;

namespace BlockBase.BBLinq.Queries.BaseQueries
{
    public abstract class DeleteRecordQuery<T> : Query
    {
        /// <summary>
        /// An object used as reference for the DELETE operation
        /// </summary>
        public T Record { get; protected set; }

        /// <summary>
        /// A condition used as reference for the DELETE operation
        /// </summary>
        public LambdaExpression Condition { get; protected set; }

        protected DeleteRecordQuery(T record)
        {
            Record = record;
        }

        protected DeleteRecordQuery(LambdaExpression condition)
        {
            Condition = condition;
        }
    }
}
