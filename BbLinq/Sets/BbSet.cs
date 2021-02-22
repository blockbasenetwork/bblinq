using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BlockBase.BBLinq.Queries.Base;
using BlockBase.BBLinq.Queries.BlockBase;
using BlockBase.BBLinq.Result;

namespace BlockBase.BBLinq.Sets
{
    public class BbSet<T> : BlockBaseBaseSet<T, BbSet<T>> where T : class
    {
        protected async Task<QueryResult<TResult>> ExecuteQuery<TResult>(Query query)
        {
            var queryString = query.GenerateQuery();
            return await QueryExecutor.ExecuteQueryAsync<TResult>(queryString);
        }

        #region SELECT ALL
        public SelectQuery Select()
        {
            return new SelectQuery(typeof(T), null, Filter, Encrypted, null, RecordLimit, RecordsToSkip);
        }

        public QueryResult BatchSelect()
        {
            var query = Select();
            return StoreQueryInBatch(query);
        }

        public async Task<QueryResult<IEnumerable<T>>> SelectAsync()
        {
            var query = Select();
            return await ExecuteQuery<IEnumerable<T>>(query);
        }

        #endregion

        #region SELECT FIELDS
        public SelectQuery Select<TResult>(Expression<Func<T, TResult>> mapper)
        {
            return new SelectQuery(typeof(T), null, Filter, Encrypted, mapper, RecordLimit, RecordsToSkip);
        }

        public QueryResult BatchSelect<TResult>(Expression<Func<T, TResult>> mapper)
        {
            var query = Select(mapper);
            return StoreQueryInBatch(query);
        }

        public async Task<QueryResult<IEnumerable<TResult>>> SelectAsync<TResult>(Expression<Func<T, TResult>> mapper)
        {
            var query = Select(mapper);
            return await ExecuteQuery<IEnumerable<TResult>>(query);
        }
        #endregion

        #region GET
        public SelectQuery Get(object id)
        {
            return new SelectQuery(typeof(T), id, null, Encrypted);
        }

        public QueryResult BatchGet(object id)
        {
            var query = Get(id);
            return StoreQueryInBatch(query);
        }

        public async Task<QueryResult<IEnumerable<T>>> GetAsync(object id)
        {
            var query = Get(id);
            return await ExecuteQuery<IEnumerable<T>>(query);
        }
        #endregion

        #region GET FIELDS
        public SelectQuery Get<TResult>(object id, Expression<Func<T, TResult>> mapper)
        {
            return new SelectQuery(typeof(T), id, mapper, Encrypted);
        }

        public QueryResult BatchGet<TResult>(object id, Expression<Func<T, TResult>> mapper)
        {
            var query = Get(id, mapper);
            return StoreQueryInBatch(query);
        }

        public async Task<QueryResult<IEnumerable<TResult>>> GetAsync<TResult>(object id, Expression<Func<T, TResult>> mapper)
        {
            var query = Get(id, mapper);
            return await ExecuteQuery<IEnumerable<TResult>>(query);
        }
        #endregion

        #region INSERT
        public InsertQuery<T> Insert(T record)
        {
            return new InsertQuery<T>(record);
        }

        public QueryResult BatchInsert(T record)
        {
            var query = Insert(record);
            return StoreQueryInBatch(query);
        }

        public async Task<QueryResult<string>> InsertAsync<TResult>(T record)
        {
            var query = Insert(record);
            return await ExecuteQuery<string>(query);
        }
        #endregion

        #region UPDATE
        public UpdateQuery<T> Update(T record)
        {
            return new UpdateQuery<T>(record);
        }

        public QueryResult BatchUpdate<TResult>(T record)
        {
            var query = Update(record);
            return StoreQueryInBatch(query);
        }

        public async Task<QueryResult<IEnumerable<TResult>>> UpdateAsync<TResult>(T record)
        {
            var query = Insert(record);
            return await ExecuteQuery<IEnumerable<TResult>>(query);
        }
        #endregion

        #region DELETE

        #region DELETE ALL
        public DeleteQuery<T> Delete()
        {
            return new DeleteQuery<T>();
        }

        public QueryResult BatchDelete<TResult>()
        {
            var query = Delete();
            return StoreQueryInBatch(query);
        }

        public async Task<QueryResult> DeleteAsync()
        {
            var query = Delete();
            return await ExecuteQuery<string>(query);
        }

        #endregion

        #region DELETE RECORD
        public DeleteQuery<T> Delete(T record)
        {
            return new DeleteQuery<T>(record);
        }

        public QueryResult BatchDelete<TResult>(T record)
        {
            var query = Delete(record);
            return StoreQueryInBatch(query);
        }

        public async Task<QueryResult> DeleteAsync(T record)
        {
            var query = Delete(record);
            return await ExecuteQuery<string>(query);
        }

        #endregion

        #region DELETE ON CONDITION
        public DeleteQuery<T> Delete(Predicate<T> predicate)
        {
            return new DeleteQuery<T>();
        }

        public QueryResult BatchDelete<TResult>(Predicate<T> predicate)
        {
            var query = Delete(predicate);
            return StoreQueryInBatch(query);
        }

        public async Task<QueryResult> DeleteAsync(Predicate<T> predicate)
        {
            var query = Delete(predicate);
            return await ExecuteQuery<string>(query);
        }

        #endregion
        #endregion
    }
}
