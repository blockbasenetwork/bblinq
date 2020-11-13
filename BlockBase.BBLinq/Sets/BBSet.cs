using System;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;
using BlockBase.BBLinq.Parser;
using BlockBase.BBLinq.Context;
using BlockBase.BBLinq.ExtensionMethods;
using BlockBase.BBLinq.Queries;
using BlockBase.BBLinq.Interfaces;

namespace BlockBase.BBLinq.Sets
{
    /// <summary>
    /// An abstract BbSet
    /// </summary>
    public abstract class BbSet{}
    
    /// <summary>
    /// A set of data represented by a type
    /// </summary>
    /// <typeparam name="T">the dataset's type</typeparam>
    public class BbSet<T> : BbSet, IBbSet<T> where T:class
    {
        private Expression<Func<T, bool>> _filter;

        /// <summary>
        /// Performs a join over the dataset and another type
        /// </summary>
        /// <typeparam name="TB">the second type</typeparam>
        /// <param name="on">expression used for the join</param>
        /// <returns></returns>
        public IBbJoinSet<T, TB> Join<TB>(Expression<Func<T, TB, bool>> on)
        {
            return new BbJoinSet<T, TB>(on);
        }

        /// <summary>
        /// Performs a delete over the dataset
        /// </summary>
        /// <param name="where">the delete condition</param>
        /// <returns></returns>
        public async Task DeleteAsync(Expression<Func<T, bool>> where)
        {
            var query = new DeleteQuery<T>(where);
            await GlobalContext.Instance.Executor.ExecuteQueryAsync(query.ToString());
        }

        /// <summary>
        /// Performs a delete over the dataset
        /// </summary>
        /// <param name="record">deletes a record</param>
        /// <returns></returns>
        public async Task DeleteAsync(T record)
        {
            var query = new DeleteQuery<T>(record);
            await GlobalContext.Instance.Executor.ExecuteQueryAsync(query.ToString());
        }

        /// <summary>
        /// Performs an insert on the dataset
        /// </summary>
        /// <param name="item">the item to be inserted</param>
        /// <returns></returns>
        public async Task InsertAsync(T item)
        {
            var query = new InsertQuery<T>(item);
            await GlobalContext.Instance.Executor.ExecuteQueryAsync(query.ToString());
        }

        /// <summary>
        /// Performs a select query on the dataset
        /// </summary>
        /// <returns>The query's result</returns>
        public async Task<IEnumerable<T>> SelectAsync()
        {
            var query = new SelectQuery<T>(typeof(T), null, _filter, null);
            var result = GlobalContext.Instance.Executor.ExecuteQueryAsync(query.ToString());
            return await ResultParser.ParseResult<T>(result);
        }

        /// <summary>
        /// Fetches data from the database
        /// </summary>
        /// <typeparam name="TB">The resulting type</typeparam>
        /// <param name="mapper">a mapping expression</param>
        /// <returns>A result</returns>
        public async Task<IEnumerable<TB>> SelectAsync<TB>(Expression<Func<T, TB>> mapper)
        {
            var query = new SelectQuery<TB>(typeof(T), null, _filter, mapper);
            var result = GlobalContext.Instance.Executor.ExecuteQueryAsync(query.ToString());
            if (!typeof(TB).IsDynamic() && typeof(TB) == query.Origin)
                return await ResultParser.ParseResult<TB>(result);
            var properties = ExpressionParser.ParseSelectBody(mapper.Body);
            return await ResultParser.ParseResult<TB>(result, properties, typeof(TB));
        }

        /// <summary>
        /// Updates the item
        /// </summary>
        /// <param name="item">the item to be updated</param>
        /// <returns>The result</returns>
        public async Task UpdateAsync(T item)
        {
            var query = new UpdateQuery<T>(item, _filter);
            await GlobalContext.Instance.Executor.ExecuteQueryAsync(query.ToString());
        }

        /// <summary>
        /// Adds the filter to the set
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public IBbSet<T> Where(Expression<Func<T, bool>> filter)
        {
            _filter = filter;
            return this;
        }

    }
}
