using System;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BlockBase.BBLinq.Parser;
using BlockBase.BBLinq.Context;
using BlockBase.BBLinq.ExtensionMethods;
using BlockBase.BBLinq.Queries;
using BlockBase.BBLinq.Interfaces;
using BlockBase.BBLinq.Pocos.Components;
using BlockBase.BBLinq.Pocos.Results;
using BlockBase.BBLinq.Properties;
using Newtonsoft.Json;

namespace BlockBase.BBLinq.Sets
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
