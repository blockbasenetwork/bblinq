using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BlockBase.BBLinq.Queries.Interfaces;

namespace BlockBase.BBLinq.Sets.Interfaces
{
    public interface IQueryableSet<T> : IBlockBaseBaseSet<IQueryableSet<T>>
    {
        public IQueryableSet<T> Where(Expression<Func<T, bool>> predicate);
        public IQueryableSet<T> Skip(int skipNumber);
        public IQueryableSet<T> Take(int takeNumber);
        
        public IQuery GetDeleteQuery();
        public void BatchDelete();
        public Task DeleteAsync();

        public IQuery GetDeleteQuery(T record);
        public void BatchDelete(T record);
        public Task DeleteAsync(T record);

        public IQuery GetUpdateQuery(T record);
        public void BatchUpdate(T record);
        public Task UpdateAsync(T record);

        public ISelectQuery GetSelectQuery();
        public void BatchSelect();
        public Task<IEnumerable<T>> SelectAsync();

        public ISelectQuery GetSelectQuery<TRecordResult>(Expression<Func<T, TRecordResult>> mapper);
        public void BatchSelect<TRecordResult>(Expression<Func<T, TRecordResult>> mapper);
        public Task<IEnumerable<TRecordResult>> SelectAsync<TRecordResult>(Expression<Func<T, TRecordResult>> mapper);

    }
}
