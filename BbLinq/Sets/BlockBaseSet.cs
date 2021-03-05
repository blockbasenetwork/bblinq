using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BlockBase.BBLinq.Parsers;
using BlockBase.BBLinq.Queries.RecordQueries;

namespace BlockBase.BBLinq.Sets
{
    public class BlockBaseSet<T> : BlockBaseBaseSet<BlockBaseSet<T>>
    {
        public Expression<Func<T, bool>> Predicate { get; private set; }

        public int RecordsToSkip { get; private set; }
        public int RecordsToTake { get; private set; }
        public bool EncryptQuery { get; private set; }

        public BlockBaseSet<T> Where(Expression<Func<T, bool>> predicate)
        {
            Predicate = predicate;
            return this;
        }

        public BlockBaseSet<T> Skip(int skipNumber)
        {
            RecordsToSkip = skipNumber;
            return this;
        }

        public BlockBaseSet<T> Take(int takeNumber)
        {
            RecordsToTake = takeNumber;
            return this;
        }

        public BlockBaseSet<T> Encrypt()
        {
            EncryptQuery = true;
            return this;
        }

        #region INSERT

        #region Insert Single

        public BlockBaseInsertRecordQuery<T> Insert(T record)
        {
            return new BlockBaseInsertRecordQuery<T>(record);
        }

        public void BatchInsert(T record)
        {
            var query = Insert(record);
            StoreQueryInBatch(query);
        }

        public async Task InsertAsync<TResult>(T record)
        {
            var query = Insert(record);
            await Executor.ExecuteQueryAsync(query.GenerateQueryString());
        }

        #endregion

        #region Insert Range

        public BlockBaseInsertRecordQuery<T> Insert(List<T> records)
        {
            return new BlockBaseInsertRecordQuery<T>(records);
        }

        public void BatchInsert(List<T> records)
        {
            var query = Insert(records);
            StoreQueryInBatch(query);
        }

        public async Task InsertAsync<TResult>(List<T> records)
        {
            var query = Insert(records);
            await Executor.ExecuteQueryAsync(query.GenerateQueryString());
        }

        #endregion

        #endregion

        #region Update
        public BlockBaseUpdateRecordQuery<T> Update(T record)
        {
            return new BlockBaseUpdateRecordQuery<T>(record, Predicate);
        }

        public void BatchUpdate<TResult>(T record)
        {
            var query = Update(record);
            StoreQueryInBatch(query);
        }

        public async Task UpdateAsync<TResult>(T record)
        {
            var query = Update(record);
            await Executor.ExecuteQueryAsync(query.GenerateQueryString());
        }
        #endregion

        #region Delete

        #region DELETE ALL
        public BlockBaseDeleteRecordQuery<T> Delete()
        {
            return new BlockBaseDeleteRecordQuery<T>(Predicate);
        }

        public void BatchDelete<TResult>()
        {
            var query = Delete();
            StoreQueryInBatch(query);
        }

        public async Task DeleteAsync()
        {
            var query = Delete();
            await Executor.ExecuteQueryAsync(query.GenerateQueryString());
        }
        #endregion

        #region DELETE ALL
        public BlockBaseDeleteRecordQuery<T> Delete(T record)
        {
            return new BlockBaseDeleteRecordQuery<T>(record);
        }

        public void BatchDelete<TResult>(T record)
        {
            var query = Delete(record);
            StoreQueryInBatch(query);
        }

        public async Task DeleteAsync(T record)
        {
            var query = Delete(record);
            await Executor.ExecuteQueryAsync(query.GenerateQueryString());
        }
        #endregion

        #endregion

        #region Select

        #region SELECT ALL
        public BlockBaseSelectRecordQuery<T> Select()
        {
            return new BlockBaseSelectRecordQuery<T>(typeof(T), null, null, null, Predicate, RecordsToTake, RecordsToTake,
                EncryptQuery);
        }

        public void BatchSelect()
        {
            var query = Select();
            StoreQueryInBatch(query);
        }

        public async Task<IEnumerable<T>> SelectAsync()
        {
            var query = Select();
            return await Executor.ExecuteQueryAsync<IEnumerable<T>>(query.GenerateQueryString());
        }
        #endregion


        #region SELECT w/ MAPPER
        public BlockBaseSelectRecordQuery<T> Select<TResult>(Func<T, TResult> mapper)
        {
            var origin = typeof(T);
            var expressionParser = new ExpressionParser();
            var predicate = expressionParser.Reduce(expressionParser.ParseExpression(Predicate));
            return new BlockBaseSelectRecordQuery<T>(origin, null, null, null, Predicate, RecordsToTake, RecordsToTake,
               EncryptQuery);
        }

        public void BatchSelect<TResult>(Func<T, TResult> mapper)
        {
            var query = Select(mapper);
            StoreQueryInBatch(query);
        }

        public async Task<IEnumerable<TResult>> SelectAsync<TResult>(Func<T, TResult> mapper)
        {
            var query = Select(mapper);
            return await Executor.ExecuteQueryAsync<IEnumerable<TResult>>(query.GenerateQueryString());
        }
        #endregion

        #endregion

        #region Join

        public BlockBaseJoinSet<T, TB> Join<TB>()
        {
            var joins = new ExpressionParser().ParseJoins(new[] {typeof(T), typeof(TB)});
            return new BlockBaseJoinSet<T, TB>(joins);
        }

        #endregion
    }
}
