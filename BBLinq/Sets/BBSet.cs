using System;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using agap2IT.Labs.BlockBase.BBLinq.Parser;
using agap2IT.Labs.BlockBase.BBLinq.Context;
using agap2IT.Labs.BlockBase.BBLinq.ExtensionMethods;
using agap2IT.Labs.BlockBase.BBLinq.Queries;
using agap2IT.Labs.BlockBase.BBLinq.Interfaces;
using agap2IT.Labs.BlockBase.BBLinq.Pocos.Components;
using agap2IT.Labs.BlockBase.BBLinq.Pocos.Results;
using agap2IT.Labs.BlockBase.BBLinq.Properties;
using Newtonsoft.Json;

namespace agap2IT.Labs.BlockBase.BBLinq.Sets
{
    public abstract class BbSet{}
    public class BbSet<T> : BbSet, IBbSet<T> where T:class
    {
        private Expression<Func<T, bool>> _filter;

        public IBbJoinSet<T, TB> Join<TB>(Expression<Func<T, TB, bool>> on)
        {
            return new BbJoinSet<T, TB>(on);
        }

        public async Task DeleteAsync(Expression<Func<T, bool>> where)
        {
            var query = new DeleteQuery<T>(where);
            await GlobalContext.Instance.Executor.ExecuteQueryAsync(query.ToString());
        }

        public async Task DeleteAsync(T record)
        {
            var query = new DeleteQuery<T>(record);
            await GlobalContext.Instance.Executor.ExecuteQueryAsync(query.ToString());
        }

        public async Task InsertAsync(T item)
        {
            var query = new InsertQuery<T>(item);
            await GlobalContext.Instance.Executor.ExecuteQueryAsync(query.ToString());
        }


        public async Task<IEnumerable<T>> SelectAsync()
        {
            var query = new SelectQuery<T>(typeof(T), null, _filter, null);
            var result = GlobalContext.Instance.Executor.ExecuteQueryAsync(query.ToString());
            return await ResultParser.ParseResult<T>(result);
        }

        public async Task<IEnumerable<TB>> SelectAsync<TB>(Expression<Func<T, TB>> mapper)
        {
            var query = new SelectQuery<TB>(typeof(T), null, _filter, mapper);
            var result = GlobalContext.Instance.Executor.ExecuteQueryAsync(query.ToString());
            if (!typeof(TB).IsDynamic() && typeof(TB) == query.Origin)
                return await ResultParser.ParseResult<TB>(result);
            var properties = ExpressionParser.ParseSelectBody(mapper.Body);
            return await ResultParser.ParseResult<TB>(result, properties, typeof(TB));
        }

        public async Task UpdateAsync(T item)
        {
            var query = new UpdateQuery<T>(item, _filter);
            await GlobalContext.Instance.Executor.ExecuteQueryAsync(query.ToString());
        }

        public IBbSet<T> Where(Expression<Func<T, bool>> filter)
        {
            _filter = filter;
            return this;
        }

    }
}
