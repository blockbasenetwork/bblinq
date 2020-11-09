using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using agap2IT.Labs.BlockBase.BBLinq.Context;
using agap2IT.Labs.BlockBase.BBLinq.ExtensionMethods;
using agap2IT.Labs.BlockBase.BBLinq.Interfaces;
using agap2IT.Labs.BlockBase.BBLinq.Parser;
using agap2IT.Labs.BlockBase.BBLinq.Pocos.Components;
using agap2IT.Labs.BlockBase.BBLinq.Queries;

namespace agap2IT.Labs.BlockBase.BBLinq.Sets
{
    public class BbJoinSet<TA, TB> : IBbJoinSet<TA, TB>
    {
        private readonly Expression<Func<TA, TB, bool>> _on;
        private Expression<Func<TA, TB, bool>> _filter;

        public BbJoinSet(Expression<Func<TA, TB, bool>> on)
        {
            _on = on;
        }

        public IBbJoinSet<TA, TB, TC> Join<TC>(Expression<Func<TA, TB, TC, bool>> on)
        {
            return new BbJoinSet<TA, TB, TC>(_on, on);
        }

        public async Task<IEnumerable<TC>> SelectAsync<TC>(Expression<Func<TA, TB, TC>> mapper)
        {

            var query = new SelectQuery<TC>(typeof(TA), new[] { _on }, _filter, mapper);
            var result = GlobalContext.Instance.Executor.ExecuteQueryAsync(query.ToString());
            if (!typeof(TC).IsDynamic() && typeof(TC) == query.Origin)
                return await ResultParser.ParseResult<TC>(result);
            var properties = ExpressionParser.ParseSelectBody(mapper.Body);
            return await ResultParser.ParseResult<TC>(result, properties, typeof(TC));
        }

        public IBbJoinSet<TA, TB> Where(Expression<Func<TA, TB, bool>> filter)
        {
            _filter = filter;
            return this;
        }
    }

    public class BbJoinSet<TA, TB, TC> : IBbJoinSet<TA, TB, TC>
    {
        private readonly IEnumerable<LambdaExpression> _joins;
        private Expression<Func<TA, TB, TC, bool>> _filter;

        public BbJoinSet(LambdaExpression join, Expression<Func<TA, TB, TC, bool>> on)
        {
            _joins = new List<LambdaExpression>() { join, on };
        }

        public IBbJoinSet<TA, TB, TC, TD> Join<TD>(Expression<Func<TA, TB, TC, TD, bool>> on)
        {
            return new BbJoinSet<TA, TB, TC, TD>(_joins, on);
        }

        public async Task<IEnumerable<TD>> SelectAsync<TD>(Expression<Func<TA, TB, TC, TD>> mapper)
        {
            var query = new SelectQuery<TD>(typeof(TA), _joins, _filter, mapper);
            var result = GlobalContext.Instance.Executor.ExecuteQueryAsync(query.ToString());
            if (!typeof(TD).IsDynamic() && typeof(TD) == query.Origin)
                return await ResultParser.ParseResult<TD>(result);
            var properties = ExpressionParser.ParseSelectBody(mapper.Body);
            return await ResultParser.ParseResult<TD>(result, properties, typeof(TD));
        }

        public IBbJoinSet<TA, TB, TC> Where(Expression<Func<TA, TB, TC, bool>> filter)
        {
            _filter = filter;
            return this;
        }
    }

    public class BbJoinSet<TA, TB, TC, TD> : IBbJoinSet<TA, TB, TC, TD>
    {
        private readonly IEnumerable<LambdaExpression> _joins;
        private Expression<Func<TA, TB, TC, TD, bool>> _filter;

        public BbJoinSet(IEnumerable<LambdaExpression> joins, Expression<Func<TA, TB, TC, TD, bool>> on)
        {
            _joins = new List<LambdaExpression>();
            var j = ((List<LambdaExpression>) _joins);
            j.AddRange(joins);
            j.Add(on);
        }

        public IBbJoinSet<TA, TB, TC, TD, TE> Join<TE>(Expression<Func<TA, TB, TC, TD, TE, bool>> on)
        {
            return new BbJoinSet<TA, TB, TC, TD, TE>(_joins, on);
        }

        public async Task<IEnumerable<TE>> SelectAsync<TE>(Expression<Func<TA, TB, TC, TD, TE>> mapper)
        {
            var query = new SelectQuery<TD>(typeof(TA), _joins, _filter, mapper);
            var result = GlobalContext.Instance.Executor.ExecuteQueryAsync(query.ToString());
            if (!typeof(TE).IsDynamic() && typeof(TE) == query.Origin)
                return await ResultParser.ParseResult<TE>(result);
            var properties = ExpressionParser.ParseSelectBody(mapper.Body);
            return await ResultParser.ParseResult<TE>(result, properties, typeof(TE));
        }

        public IBbJoinSet<TA, TB, TC, TD> Where(Expression<Func<TA, TB, TC, TD, bool>> filter)
        {
            _filter = filter;
            return this;
        }
    }

    public class BbJoinSet<TA, TB, TC, TD, TE> : IBbJoinSet<TA, TB, TC, TD, TE>
    {
        private readonly IEnumerable<LambdaExpression> _joins;
        private Expression<Func<TA, TB, TC, TD, TE, bool>> _filter;

        public BbJoinSet(IEnumerable<LambdaExpression> joins, Expression<Func<TA, TB, TC, TD, TE, bool>> on)
        {
            _joins = new List<LambdaExpression>();
            var j = ((List<LambdaExpression>)_joins);
            j.AddRange(joins);
            j.Add(on);
        }

        public IBbJoinSet<TA, TB, TC, TD, TE, TF> Join<TF>(Expression<Func<TA, TB, TC, TD, TE, TF, bool>> on)
        {
            return new BbJoinSet<TA, TB, TC, TD, TE, TF>(_joins, on);
        }

        public async Task<IEnumerable<TF>> SelectAsync<TF>(Expression<Func<TA, TB, TC, TD, TE, TF>> mapper)
        {
            var query = new SelectQuery<TD>(typeof(TA), _joins, _filter, mapper);
            var result = GlobalContext.Instance.Executor.ExecuteQueryAsync(query.ToString());
            if (!typeof(TF).IsDynamic() && typeof(TF) == query.Origin)
                return await ResultParser.ParseResult<TF>(result);
            var properties = ExpressionParser.ParseSelectBody(mapper.Body);
            return await ResultParser.ParseResult<TF>(result, properties, typeof(TF));
        }

        public IBbJoinSet<TA, TB, TC, TD, TE> Where(Expression<Func<TA, TB, TC, TD, TE, bool>> filter)
        {
            _filter = filter;
            return this;
        }
    }

    public class BbJoinSet<TA, TB, TC, TD, TE, TF> : IBbJoinSet<TA, TB, TC, TD, TE, TF>
    {
        private readonly IEnumerable<LambdaExpression> _joins;
        private Expression<Func<TA, TB, TC, TD, TE, TF, bool>> _filter;

        public BbJoinSet(IEnumerable<LambdaExpression> joins, Expression<Func<TA, TB, TC, TD,TE,TF, bool>> on)
        {
            _joins = new List<LambdaExpression>();
            var j = ((List<LambdaExpression>)_joins);
            j.AddRange(joins);
            j.Add(on);
        }

        public IBbJoinSet<TA, TB, TC, TD, TE, TF, TG> Join<TG>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, bool>> on)
        {
            return new BbJoinSet<TA, TB, TC, TD, TE, TF, TG>(_joins, on);
        }

        public async Task<IEnumerable<TG>> SelectAsync<TG>(Expression<Func<TA, TB, TC, TD, TE, TF, TG>> mapper)
        {
            var query = new SelectQuery<TD>(typeof(TA), _joins, _filter, mapper);
            var result = GlobalContext.Instance.Executor.ExecuteQueryAsync(query.ToString());
            if (!typeof(TG).IsDynamic() && typeof(TG) == query.Origin)
                return await ResultParser.ParseResult<TG>(result);
            var properties = ExpressionParser.ParseSelectBody(mapper.Body);
            return await ResultParser.ParseResult<TG>(result, properties, typeof(TG));
        }

        public IBbJoinSet<TA, TB, TC, TD, TE, TF> Where(Expression<Func<TA, TB, TC, TD, TE, TF, bool>> filter)
        {
            _filter = filter;
            return this;
        }
    }
    public class BbJoinSet<TA, TB, TC, TD, TE, TF, TG> : IBbJoinSet<TA, TB, TC, TD, TE, TF, TG>
    {

        private readonly IEnumerable<LambdaExpression> _joins;
        private Expression<Func<TA, TB, TC, TD, TE, TF, TG, bool>> _filter;

        public BbJoinSet(IEnumerable<LambdaExpression> joins, Expression<Func<TA, TB, TC, TD, TE, TF,TG, bool>> on)
        {
            _joins = new List<LambdaExpression>();
            var j = ((List<LambdaExpression>)_joins);
            j.AddRange(joins);
            j.Add(on);
        }

        public async Task<IEnumerable<TH>> SelectAsync<TH>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH>> mapper)
        {
            var query = new SelectQuery<TD>(typeof(TA), _joins, _filter, mapper);
            var result = GlobalContext.Instance.Executor.ExecuteQueryAsync(query.ToString());
            if (!typeof(TH).IsDynamic() && typeof(TH) == query.Origin)
                return await ResultParser.ParseResult<TH>(result);
            var properties = ExpressionParser.ParseSelectBody(mapper.Body);
            return await ResultParser.ParseResult<TH>(result, properties, typeof(TH));
        }

        public IBbJoinSet<TA, TB, TC, TD, TE, TF, TG> Where(Expression<Func<TA, TB, TC, TD, TE, TF, TG, bool>> filter)
        {
            _filter = filter;
            return this;
        }
    }
}
