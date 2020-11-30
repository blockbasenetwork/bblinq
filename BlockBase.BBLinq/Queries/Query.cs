using BlockBase.BBLinq.Builders;
using BlockBase.BBLinq.Context;
using BlockBase.BBLinq.Dictionaries;

namespace BlockBase.BBLinq.Queries
{

    /// <summary>
    /// Base for any query
    /// </summary>
    public abstract class Query<TQueryBuilder, TDictionary> where TQueryBuilder: SqlQueryBuilder<TDictionary, TQueryBuilder> where TDictionary : SqlDictionary
    {
        protected TQueryBuilder QueryBuilder => ContextCache.Instance.Get<TQueryBuilder>("queryBuilder");

        /// <summary>
        /// Fetches the content as a string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return QueryBuilder.ToString();
        }
    }
}
