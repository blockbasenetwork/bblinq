using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BlockBase.BBLinq.Interfaces;
using BlockBase.BBLinq.Parsers;
using BlockBase.BBLinq.Queries;
using BlockBase.BBLinq.Results;

namespace BlockBase.BBLinq.Sets
{
    public class BbSet<T, TKey> : BbBaseSet<BbSet<T, TKey>>,
        IFilters<T, BbSet<T, TKey>>,
        ILists<T>,
        IGets<T>,
        IGetsByKey<T, TKey>,
        IDeletes<T>,
        IInserts<T>,
        IUpdates<T> where T : class
    {

        #region Lists and Gets

        public async Task<QueryResult<IEnumerable<T>>> List()
        {
            var query = new SelectQuery(typeof(T), null, Filter, Encrypted, null, RecordLimit, RecordsToSkip, null);
            var result = Executor.ExecuteQueryAsync(query.ToString());
            var properties = ParseSelectBody<T>(null);
            return await ResultParser.ParseListResult<T>(result, properties);
        }

        public async Task<QueryResult<IEnumerable<TB>>> List<TB>(Expression<Func<T, TB>> mapper)
        {
            var query = new SelectQuery(typeof(T), null, Filter, Encrypted, mapper, RecordLimit, RecordsToSkip, null);
            var result = Executor.ExecuteQueryAsync(query.ToString());
            var properties = ParseSelectBody<TB>(mapper);
            return await ResultParser.ParseListResult<TB>(result, properties);
        }

        public async Task<QueryResult<TB>> Get<TB>(Expression<Func<T, TB>> mapper)
        {
            var query = new SelectQuery(typeof(T), null, Filter, Encrypted, mapper, 1, RecordsToSkip, null);
            var result = Executor.ExecuteQueryAsync(query.ToString());
            var properties = ParseSelectBody<TB>(mapper);
            return await ResultParser.ParseFetchResult<TB>(result, properties);
        }

        public async Task<QueryResult<T>> Get(TKey key)
        {
            var query = new SelectQuery(typeof(T), null, null, Encrypted, null, 1, RecordsToSkip, key);
            var result = Executor.ExecuteQueryAsync(query.ToString());
            var properties = ParseSelectBody<T>(null);
            return await ResultParser.ParseFetchResult<T>(result, properties);
        }


        #endregion

        /// <summary>
        /// Executes a delete query
        /// </summary>
        /// <returns>The query's result</returns>
        public Task<QueryResult> Delete()
        {
            var query = new DeleteQuery<T>(Filter);
            var result = Executor.ExecuteQueryAsync(query.ToString());
            return ResultParser.ParseQueryResult(result);
        }

        /// <summary>
        /// Executes a delete query
        /// </summary>
        /// <param name="record">the existing record</param>
        /// <returns>The query's result</returns>
        public Task<QueryResult> Delete(T record)
        {
            var query = new DeleteQuery<T>(record);
            var result = Executor.ExecuteQueryAsync(query.ToString());
            return ResultParser.ParseQueryResult(result);
        }

        /// <summary>
        /// Executes an insert query
        /// </summary>
        /// <param name="record">the new record</param>
        /// <returns>The query's result</returns>
        public Task<QueryResult> Insert(T record)
        {
            var query = new InsertQuery<T>(record);
            var result = Executor.ExecuteQueryAsync(query.ToString());
            return ResultParser.ParseQueryResult(result);
        }

        /// <summary>
        /// Executes an update query
        /// </summary>
        /// <param name="record">the record with the updated data</param>
        /// <returns>The query's result</returns>
        public Task<QueryResult> Update(T record)
        {
            var query = new UpdateQuery<T>(record, Filter);
            var result = Executor.ExecuteQueryAsync(query.ToString());
            return ResultParser.ParseQueryResult(result);
        }

        /// <summary>
        /// Adds a filter on a select query
        /// </summary>
        /// <param name="predicate">the filter</param>
        /// <returns>the updated set</returns>
        public BbSet<T, TKey> Where(Expression<Func<T, bool>> predicate)
        {
            return this.Where(predicate as LambdaExpression);
        }

        /// <summary>
        /// Sets the limit on the request
        /// </summary>
        /// <param name="limit">number of elements</param>
        /// <returns></returns>
        public BbSet<T, TKey> Limit(int limit)
        {
            RecordLimit = limit;
            return this;
        }

        /// <summary>
        /// Sets the offset on the request
        /// </summary>
        /// <param name="limit">number of elements to offset</param>
        /// <returns></returns>
        public BbSet<T, TKey> Offset(int offset)
        {
            RecordsToSkip = offset;
            return this;
        }

        /// <summary>
        /// Generates a join based on an expression
        /// </summary>
        /// <typeparam name="TB">the second table</typeparam>
        /// <returns>a blockbase join</returns>
        public BbJoin<T, TB> Join<TB>() where TB : class
        {
            var on = GenerateJoinExpression(new List<Type>() { typeof(T) }, typeof(TB));
            return new BbJoin<T, TB>(on);
        }

        /// <summary>
        /// Generates a join based on an expression
        /// </summary>
        /// <typeparam name="TB">the second table</typeparam>
        /// <param name="on">the join expression</param>
        /// <returns>a blockbase join</returns>
        public BbJoin<T, TB> Join<TB>(Expression<Func<T, TB>> @on) where TB : class
        {
            return new BbJoin<T, TB>(@on);
        }

    }
}
