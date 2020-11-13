using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BlockBase.BBLinq.Context;
using BlockBase.BBLinq.ExtensionMethods;
using BlockBase.BBLinq.Interfaces;
using BlockBase.BBLinq.Parser;
using BlockBase.BBLinq.Queries;

namespace BlockBase.BBLinq.Sets
{
    /// <summary>
    /// A Two-table join
    /// </summary>
    /// <typeparam name="TA">The first type</typeparam>
    /// <typeparam name="TB">The second type</typeparam>
    public class BbJoinSet<TA, TB> : IBbJoinSet<TA, TB>
    {
        private readonly Expression<Func<TA, TB, bool>> _on;
        private Expression<Func<TA, TB, bool>> _filter;

        /// <summary>
        /// The default constructor
        /// </summary>
        /// <param name="on">the on expression</param>
        public BbJoinSet(Expression<Func<TA, TB, bool>> on)
        {
            _on = on;
        }

        /// <summary>
        /// Performs a join on another table
        /// </summary>
        /// <typeparam name="TC">the other table's type</typeparam>
        /// <param name="on">expression that specifies the join</param>
        /// <returns>a new Join set</returns>
        public IBbJoinSet<TA, TB, TC> Join<TC>(Expression<Func<TA, TB, TC, bool>> on)
        {
            return new BbJoinSet<TA, TB, TC>(_on, on);
        }

        /// <summary>
        /// Performs a select query on a dataset
        /// </summary>
        /// <typeparam name="TC">the resulting type</typeparam>
        /// <param name="mapper">the selection expression</param>
        /// <returns>a list of results</returns>
        public async Task<IEnumerable<TC>> SelectAsync<TC>(Expression<Func<TA, TB, TC>> mapper)
        {

            var query = new SelectQuery<TC>(typeof(TA), new[] { _on }, _filter, mapper);
            var result = GlobalContext.Instance.Executor.ExecuteQueryAsync(query.ToString());
            if (!typeof(TC).IsDynamic() && typeof(TC) == query.Origin)
                return await ResultParser.ParseResult<TC>(result);
            var properties = ExpressionParser.ParseSelectBody(mapper.Body);
            return await ResultParser.ParseResult<TC>(result, properties, typeof(TC));
        }

        /// <summary>
        /// Adds a filter to the join set
        /// </summary>
        /// <param name="filter">a filter expression</param>
        /// <returns></returns>
        public IBbJoinSet<TA, TB> Where(Expression<Func<TA, TB, bool>> filter)
        {
            _filter = filter;
            return this;
        }
    }

    /// <summary>
    /// A 3-table join
    /// </summary>
    /// <typeparam name="TA">The first type</typeparam>
    /// <typeparam name="TB">The second type</typeparam>
    /// <typeparam name="TC">The third type</typeparam>
    public class BbJoinSet<TA, TB, TC> : IBbJoinSet<TA, TB, TC>
    {
        private readonly IEnumerable<LambdaExpression> _joins;
        private Expression<Func<TA, TB, TC, bool>> _filter;

        /// <summary>
        /// The default constructor
        /// </summary>
        /// <param name="join">the join expression</param>
        /// <param name="on">the on expression</param>
        public BbJoinSet(LambdaExpression join, Expression<Func<TA, TB, TC, bool>> on)
        {
            _joins = new List<LambdaExpression>() { join, on };
        }

        /// <summary>
        /// Performs a join on another table
        /// </summary>
        /// <typeparam name="TD">the other table's type</typeparam>
        /// <param name="on">expression that specifies the join</param>
        /// <returns>a new Join set</returns>
        public IBbJoinSet<TA, TB, TC, TD> Join<TD>(Expression<Func<TA, TB, TC, TD, bool>> on)
        {
            return new BbJoinSet<TA, TB, TC, TD>(_joins, on);
        }

        /// <summary>
        /// Performs a select query on a dataset
        /// </summary>
        /// <typeparam name="TD">the resulting type</typeparam>
        /// <param name="mapper">the selection expression</param>
        /// <returns>a list of results</returns>
        public async Task<IEnumerable<TD>> SelectAsync<TD>(Expression<Func<TA, TB, TC, TD>> mapper)
        {
            var query = new SelectQuery<TD>(typeof(TA), _joins, _filter, mapper);
            var result = GlobalContext.Instance.Executor.ExecuteQueryAsync(query.ToString());
            if (!typeof(TD).IsDynamic() && typeof(TD) == query.Origin)
                return await ResultParser.ParseResult<TD>(result);
            var properties = ExpressionParser.ParseSelectBody(mapper.Body);
            return await ResultParser.ParseResult<TD>(result, properties, typeof(TD));
        }

        /// <summary>
        /// Adds a filter to the join set
        /// </summary>
        /// <param name="filter">a filter expression</param>
        /// <returns></returns>
        public IBbJoinSet<TA, TB, TC> Where(Expression<Func<TA, TB, TC, bool>> filter)
        {
            _filter = filter;
            return this;
        }
    }

    /// <summary>
    /// A 4-table join
    /// </summary>
    /// <typeparam name="TA">The first type</typeparam>
    /// <typeparam name="TB">The second type</typeparam>
    /// <typeparam name="TC">The third type</typeparam>
    /// <typeparam name="TD">The fourth type</typeparam>
    public class BbJoinSet<TA, TB, TC, TD> : IBbJoinSet<TA, TB, TC, TD>
    {
        private readonly IEnumerable<LambdaExpression> _joins;
        private Expression<Func<TA, TB, TC, TD, bool>> _filter;

        /// <summary>
        /// The default constructor
        /// </summary>
        /// <param name="joins">the list of join expression</param>
        /// <param name="on">the on expression</param>
        public BbJoinSet(IEnumerable<LambdaExpression> joins, Expression<Func<TA, TB, TC, TD, bool>> on)
        {
            _joins = new List<LambdaExpression>();
            var j = ((List<LambdaExpression>) _joins);
            j.AddRange(joins);
            j.Add(on);
        }

        /// <summary>
        /// Performs a join on another table
        /// </summary>
        /// <typeparam name="TE">the other table's type</typeparam>
        /// <param name="on">expression that specifies the join</param>
        /// <returns>a new Join set</returns>
        public IBbJoinSet<TA, TB, TC, TD, TE> Join<TE>(Expression<Func<TA, TB, TC, TD, TE, bool>> on)
        {
            return new BbJoinSet<TA, TB, TC, TD, TE>(_joins, on);
        }

        /// <summary>
        /// Performs a select query on a dataset
        /// </summary>
        /// <typeparam name="TE">the resulting type</typeparam>
        /// <param name="mapper">the selection expression</param>
        /// <returns>a list of results</returns>
        public async Task<IEnumerable<TE>> SelectAsync<TE>(Expression<Func<TA, TB, TC, TD, TE>> mapper)
        {
            var query = new SelectQuery<TD>(typeof(TA), _joins, _filter, mapper);
            var result = GlobalContext.Instance.Executor.ExecuteQueryAsync(query.ToString());
            if (!typeof(TE).IsDynamic() && typeof(TE) == query.Origin)
                return await ResultParser.ParseResult<TE>(result);
            var properties = ExpressionParser.ParseSelectBody(mapper.Body);
            return await ResultParser.ParseResult<TE>(result, properties, typeof(TE));
        }

        /// <summary>
        /// Adds a filter to the join set
        /// </summary>
        /// <param name="filter">a filter expression</param>
        /// <returns></returns>
        public IBbJoinSet<TA, TB, TC, TD> Where(Expression<Func<TA, TB, TC, TD, bool>> filter)
        {
            _filter = filter;
            return this;
        }
    }

    /// <summary>
    /// A 5-table join
    /// </summary>
    /// <typeparam name="TA">The first type</typeparam>
    /// <typeparam name="TB">The second type</typeparam>
    /// <typeparam name="TC">The third type</typeparam>
    /// <typeparam name="TD">The fourth type</typeparam>
    /// <typeparam name="TE">The fifth type</typeparam>
    public class BbJoinSet<TA, TB, TC, TD, TE> : IBbJoinSet<TA, TB, TC, TD, TE>
    {
        private readonly IEnumerable<LambdaExpression> _joins;
        private Expression<Func<TA, TB, TC, TD, TE, bool>> _filter;

        /// <summary>
        /// The default constructor
        /// </summary>
        /// <param name="joins">the list of join expression</param>
        /// <param name="on">the on expression</param>
        public BbJoinSet(IEnumerable<LambdaExpression> joins, Expression<Func<TA, TB, TC, TD, TE, bool>> on)
        {
            _joins = new List<LambdaExpression>();
            var j = ((List<LambdaExpression>)_joins);
            j.AddRange(joins);
            j.Add(on);
        }

        /// <summary>
        /// Performs a join on another table
        /// </summary>
        /// <typeparam name="TF">the other table's type</typeparam>
        /// <param name="on">expression that specifies the join</param>
        /// <returns>a new Join set</returns>
        public IBbJoinSet<TA, TB, TC, TD, TE, TF> Join<TF>(Expression<Func<TA, TB, TC, TD, TE, TF, bool>> on)
        {
            return new BbJoinSet<TA, TB, TC, TD, TE, TF>(_joins, on);
        }

        /// <summary>
        /// Performs a select query on a dataset
        /// </summary>
        /// <typeparam name="TF">the resulting type</typeparam>
        /// <param name="mapper">the selection expression</param>
        /// <returns>a list of results</returns>
        public async Task<IEnumerable<TF>> SelectAsync<TF>(Expression<Func<TA, TB, TC, TD, TE, TF>> mapper)
        {
            var query = new SelectQuery<TD>(typeof(TA), _joins, _filter, mapper);
            var result = GlobalContext.Instance.Executor.ExecuteQueryAsync(query.ToString());
            if (!typeof(TF).IsDynamic() && typeof(TF) == query.Origin)
                return await ResultParser.ParseResult<TF>(result);
            var properties = ExpressionParser.ParseSelectBody(mapper.Body);
            return await ResultParser.ParseResult<TF>(result, properties, typeof(TF));
        }

        /// <summary>
        /// Adds a filter to the join set
        /// </summary>
        /// <param name="filter">a filter expression</param>
        /// <returns></returns>
        public IBbJoinSet<TA, TB, TC, TD, TE> Where(Expression<Func<TA, TB, TC, TD, TE, bool>> filter)
        {
            _filter = filter;
            return this;
        }
    }

    /// <summary>
    /// A 6-table join
    /// </summary>
    /// <typeparam name="TA">The first type</typeparam>
    /// <typeparam name="TB">The second type</typeparam>
    /// <typeparam name="TC">The third type</typeparam>
    /// <typeparam name="TD">The fourth type</typeparam>
    /// <typeparam name="TE">The fifth type</typeparam>
    /// <typeparam name="TF">The sixth type</typeparam>
    public class BbJoinSet<TA, TB, TC, TD, TE, TF> : IBbJoinSet<TA, TB, TC, TD, TE, TF>
    {
        private readonly IEnumerable<LambdaExpression> _joins;
        private Expression<Func<TA, TB, TC, TD, TE, TF, bool>> _filter;

        /// <summary>
        /// The default constructor
        /// </summary>
        /// <param name="joins">the list of join expression</param>
        /// <param name="on">the on expression</param>
        public BbJoinSet(IEnumerable<LambdaExpression> joins, Expression<Func<TA, TB, TC, TD,TE,TF, bool>> on)
        {
            _joins = new List<LambdaExpression>();
            var j = ((List<LambdaExpression>)_joins);
            j.AddRange(joins);
            j.Add(on);
        }

        /// <summary>
        /// Performs a join on another table
        /// </summary>
        /// <typeparam name="TG">the other table's type</typeparam>
        /// <param name="on">expression that specifies the join</param>
        /// <returns>a new Join set</returns>
        public IBbJoinSet<TA, TB, TC, TD, TE, TF, TG> Join<TG>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, bool>> on)
        {
            return new BbJoinSet<TA, TB, TC, TD, TE, TF, TG>(_joins, on);
        }

        /// <summary>
        /// Performs a select query on a dataset
        /// </summary>
        /// <typeparam name="TG">the resulting type</typeparam>
        /// <param name="mapper">the selection expression</param>
        /// <returns>a list of results</returns>
        public async Task<IEnumerable<TG>> SelectAsync<TG>(Expression<Func<TA, TB, TC, TD, TE, TF, TG>> mapper)
        {
            var query = new SelectQuery<TD>(typeof(TA), _joins, _filter, mapper);
            var result = GlobalContext.Instance.Executor.ExecuteQueryAsync(query.ToString());
            if (!typeof(TG).IsDynamic() && typeof(TG) == query.Origin)
                return await ResultParser.ParseResult<TG>(result);
            var properties = ExpressionParser.ParseSelectBody(mapper.Body);
            return await ResultParser.ParseResult<TG>(result, properties, typeof(TG));
        }

        /// <summary>
        /// Adds a filter to the join set
        /// </summary>
        /// <param name="filter">a filter expression</param>
        /// <returns></returns>
        public IBbJoinSet<TA, TB, TC, TD, TE, TF> Where(Expression<Func<TA, TB, TC, TD, TE, TF, bool>> filter)
        {
            _filter = filter;
            return this;
        }
    }

    /// <summary>
    /// A 7-table join
    /// </summary>
    /// <typeparam name="TA">The first type</typeparam>
    /// <typeparam name="TB">The second type</typeparam>
    /// <typeparam name="TC">The third type</typeparam>
    /// <typeparam name="TD">The fourth type</typeparam>
    /// <typeparam name="TE">The fifth type</typeparam>
    /// <typeparam name="TF">The sixth type</typeparam>
    /// <typeparam name="TG">The seventh type</typeparam>
    public class BbJoinSet<TA, TB, TC, TD, TE, TF, TG> : IBbJoinSet<TA, TB, TC, TD, TE, TF, TG>
    {

        private readonly IEnumerable<LambdaExpression> _joins;
        private Expression<Func<TA, TB, TC, TD, TE, TF, TG, bool>> _filter;

        /// <summary>
        /// The default constructor
        /// </summary>
        /// <param name="joins">the list of join expression</param>
        /// <param name="on">the on expression</param>
        public BbJoinSet(IEnumerable<LambdaExpression> joins, Expression<Func<TA, TB, TC, TD, TE, TF,TG, bool>> on)
        {
            _joins = new List<LambdaExpression>();
            var j = ((List<LambdaExpression>)_joins);
            j.AddRange(joins);
            j.Add(on);
        }

        /// <summary>
        /// Performs a select query on a dataset
        /// </summary>
        /// <typeparam name="TH">the resulting type</typeparam>
        /// <param name="mapper">the selection expression</param>
        /// <returns>a list of results</returns>
        public async Task<IEnumerable<TH>> SelectAsync<TH>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH>> mapper)
        {
            var query = new SelectQuery<TD>(typeof(TA), _joins, _filter, mapper);
            var result = GlobalContext.Instance.Executor.ExecuteQueryAsync(query.ToString());
            if (!typeof(TH).IsDynamic() && typeof(TH) == query.Origin)
                return await ResultParser.ParseResult<TH>(result);
            var properties = ExpressionParser.ParseSelectBody(mapper.Body);
            return await ResultParser.ParseResult<TH>(result, properties, typeof(TH));
        }

        /// <summary>
        /// Adds a filter to the join set
        /// </summary>
        /// <param name="filter">a filter expression</param>
        /// <returns></returns>
        public IBbJoinSet<TA, TB, TC, TD, TE, TF, TG> Where(Expression<Func<TA, TB, TC, TD, TE, TF, TG, bool>> filter)
        {
            _filter = filter;
            return this;
        }
    }
}
