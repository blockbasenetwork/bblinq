using BBLinq.Context;
using BBLinq.Interfaces;
using BBLinq.Parser;
using BBLinq.Queries;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BBLinq.Data
{
    public abstract class BBSet{}
    public class BBSet<T> : BBSet, IBBSet<T> where T:class
    {
        private Expression<Func<T, bool>> _filter;

        public IBBJoinSet<T, TB> Join<TB>(Expression<Func<T, TB, bool>> on)
        {
            return new BBJoinSet<T, TB>(on);
        }

        public async Task DeleteAsync(T item)
        {
            var query = new DeleteQuery<T>(item);
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
            var result = await GlobalContext.Instance.Executor.ExecuteQueryAsync(query.ToString());
            return ResultParser.ParseRoot<T>(result);
        }

        public async Task<IEnumerable<TB>> SelectAsync<TB>(Expression<Func<T, TB>> mapper)
        {
            var query = new SelectQuery<TB>(typeof(T), null, _filter, mapper);
            var result = await GlobalContext.Instance.Executor.ExecuteQueryAsync(query.ToString());
            return ResultParser.ParseRoot<TB>(result);
        }

        public async Task UpdateAsync(T item)
        {
            var query = new UpdateQuery<T>(item);
            await GlobalContext.Instance.Executor.ExecuteQueryAsync(query.ToString());
        }

        public IBBSet<T> Where(Expression<Func<T, bool>> filter)
        {
            _filter = filter;
            return this;
        }
    }
}
