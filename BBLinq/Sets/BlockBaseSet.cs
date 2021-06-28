using BlockBase.BBLinq.Builders;
using BlockBase.BBLinq.Enumerables;
using BlockBase.BBLinq.ExtensionMethods;
using BlockBase.BBLinq.Model.Database;
using BlockBase.BBLinq.Model.Nodes;
using BlockBase.BBLinq.Parsers;
using BlockBase.BBLinq.Queries.BlockBaseQueries;
using BlockBase.BBLinq.Queries.Interfaces;
using BlockBase.BBLinq.QueryExecutors;
using BlockBase.BBLinq.Sets.Base;
using BlockBase.BBLinq.Sets.Interfaces;
using BlockBase.BBLinq.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BlockBase.BBLinq.Sets
{
    public class BlockBaseSet<T> : BlockBaseBaseSet<BlockBaseSet<T>>, IBlockBaseSet<T>
    {
        private Expression<Func<T, bool>> _predicate;
        private int? _recordsToSkip;
        private int? _recordsToTake;
        private bool _encryptQuery;
        private List<IQuery> _batchQueries;
        #region Insert

        public IQuery GetInsertQuery(T record)
        {
            return new BlockBaseRecordInsertQuery(typeof(T), record, _encryptQuery);
        }

        public void BatchInsert(T record)
        {
            var query = GetInsertQuery(record);
            _batchQueries.Add(query);
        }

        public async Task InsertAsync(T record)
        {
            var query = GetInsertQuery(record);
            var executor = new BlockBaseQueryExecutor() { UseDatabase = true };
            await executor.ExecuteQueryAsync(query, Settings);
        }

        public IQuery GetInsertQuery(IEnumerable<T> records)
        {
            return new BlockBaseRecordInsertQuery(typeof(T), records, _encryptQuery);
        }

        public void BatchInsert(IEnumerable<T> records)
        {
            var query = GetInsertQuery(records);
            _batchQueries.Add(query);
        }

        public async Task InsertAsync(IEnumerable<T> records)
        {
            var query = GetInsertQuery(records);
            var executor = new BlockBaseQueryExecutor() { UseDatabase = true };
            await executor.ExecuteQueryAsync(query, Settings);
        }
        #endregion

        #region Addons
        public IQueryableSet<T> Where(Expression<Func<T, bool>> predicate)
        {
            _predicate = predicate;
            return this;
        }

        public IQueryableSet<T> Skip(int skipNumber)
        {
            _recordsToSkip = skipNumber;
            return this;
        }

        public IQueryableSet<T> Take(int takeNumber)
        {
            _recordsToTake = takeNumber;
            return this;
        }


        #endregion

        #region Delete
        public IQuery GetDeleteQuery()
        {
            var condition = (new BlockBaseExpressionParser()).Parse(_predicate);
            return new BlockBaseRecordDeleteQuery(typeof(T).GetTableName(), condition, _encryptQuery);
        }

        public void BatchDelete()
        {
            var query = GetDeleteQuery();
            _batchQueries.Add(query);
        }

        public async Task DeleteAsync()
        {
            var query = GetDeleteQuery();
            var executor = new BlockBaseQueryExecutor() { UseDatabase = true };
            await executor.ExecuteQueryAsync(query, Settings);
        }

        public IQuery GetDeleteQuery(T record)
        {
            return new BlockBaseRecordDeleteQuery(typeof(T).GetTableName(), NodeBuilder.GenerateComparisonNodeOnKey(record), _encryptQuery);
        }

        public void BatchDelete(T record)
        {
            var query = GetDeleteQuery(record);
            _batchQueries.Add(query);
        }

        public async Task DeleteAsync(T record)
        {
            var query = GetDeleteQuery(record);
            var executor = new BlockBaseQueryExecutor() { UseDatabase = true };
            await executor.ExecuteQueryAsync(query, Settings);
        }
        #endregion

        #region Update
        public IQuery GetUpdateQuery(T record)
        {
            var condition = _predicate != null ?
                (new BlockBaseExpressionParser()).Parse(_predicate) :
                NodeBuilder.GenerateComparisonNodeOnKey(record);
            return new BlockBaseRecordUpdateQuery(typeof(T), record, condition, _encryptQuery);
        }

        public void BatchUpdate(T record)
        {
            var query = GetUpdateQuery(record);
            _batchQueries.Add(query);
        }


        public async Task UpdateAsync(T record)
        {
            var query = GetUpdateQuery(record);
            var executor = new BlockBaseQueryExecutor() { UseDatabase = true };
            await executor.ExecuteQueryAsync(query, Settings);
        }

        public IQuery GetUpdateQuery(object record)
        {
            var condition = _predicate != null ? (new BlockBaseExpressionParser()).Parse(_predicate) : NodeBuilder.GenerateComparisonNodeOnObjectKey(typeof(T), record);
            return new BlockBaseRecordUpdateQuery(typeof(T), record, condition, _encryptQuery);
        }


        public async Task UpdateAsync(object record)
        {
            var query = GetUpdateQuery(record);
            var executor = new BlockBaseQueryExecutor() { UseDatabase = true };
            await executor.ExecuteQueryAsync(query, Settings);
        }


        #endregion

        #region Select
        public ISelectQuery GetSelectQuery()
        {
            var selectedProperties = new[] { new BlockBaseColumn() { Table = typeof(T).GetTableName() } };
            var condition = (new BlockBaseExpressionParser()).Parse(_predicate);
            var joins = new[] { new JoinNode(typeof(T).GetPrimaryKey(), null, BlockBaseJoinEnum.Inner) };
            return new BlockBaseRecordSelectQuery(typeof(T), null, selectedProperties, joins, condition, _recordsToTake, _recordsToSkip,
                _encryptQuery);
        }

        public void BatchSelect()
        {
            var query = GetSelectQuery();
            _batchQueries.Add(query);
        }

        public async Task<T> FirstOrDefault()
        {
            return (await SelectAsync()).FirstOrDefault();
        }

        public async Task<T> SingleOrDefault()
        {
            return (await SelectAsync()).SingleOrDefault();
        }

        public async Task<int> Count()
        {
            return (await SelectAsync()).Count();
        }

        public async Task<IEnumerable<T>> SelectAsync()
        {
            var query = GetSelectQuery();
            var executor = new BlockBaseQueryExecutor() { UseDatabase = true };
            var res = await executor.ExecuteQueryAsync<T>(query, Settings);
            return res;
        }

        #endregion

        public ISelectQuery GetSelectQuery<TRecordResult>(Expression<Func<T, TRecordResult>> mapper)
        {
            var condition = (new BlockBaseExpressionParser()).Parse(_predicate);
            var joins = new[] { new JoinNode(typeof(T).GetPrimaryKey(), null, BlockBaseJoinEnum.Inner) };
            var selectedProperties = (new BlockBaseExpressionParser()).ParseSelectionColumns(mapper);
            var query = new BlockBaseRecordSelectQuery(typeof(TRecordResult), mapper, selectedProperties, joins,
                condition, _recordsToTake, _recordsToSkip, _encryptQuery);
            return query;
        }

        public void BatchSelect<TRecordResult>(Expression<Func<T, TRecordResult>> mapper)
        {
            var query = GetSelectQuery(mapper);
            _batchQueries.Add(query);
        }

        public async Task<TRecordResult> FirstOrDefault<TRecordResult>(Expression<Func<T, TRecordResult>> mapper)
        {
            return (await SelectAsync(mapper)).FirstOrDefault();
        }

        public async Task<TRecordResult> SingleOrDefault<TRecordResult>(Expression<Func<T, TRecordResult>> mapper)
        {
            return (await SelectAsync(mapper)).SingleOrDefault();
        }

        public async Task<int> Count<TRecordResult>(Expression<Func<T, TRecordResult>> mapper)
        {
            return (await SelectAsync(mapper)).Count();
        }

        public async Task<IEnumerable<TRecordResult>> SelectAsync<TRecordResult>(Expression<Func<T, TRecordResult>> mapper)
        {
            var query = GetSelectQuery(mapper);
            var executor = new BlockBaseQueryExecutor() { UseDatabase = true };
            var res = await executor.ExecuteQueryAsync<TRecordResult>(query, Settings);
            return res;
        }

        #region Join

        public IJoin<T, TB> Join<TB>(BlockBaseJoinEnum type = BlockBaseJoinEnum.Inner)
        {
            return new BlockBaseJoin<T, TB>(Executor, Settings, _batchQueries, type);
        }

        #endregion

        #region Encrypt
        IFetchableSet<T> IBlockBaseBaseSet<IFetchableSet<T>>.Encrypt()
        {
            _encryptQuery = true;
            return this;
        }
        IInsertableSet<T> IBlockBaseBaseSet<IInsertableSet<T>>.Encrypt()
        {
            _encryptQuery = true;
            return this;
        }
        IQueryableSet<T> IBlockBaseBaseSet<IQueryableSet<T>>.Encrypt()
        {
            _encryptQuery = true;
            return this;
        }
        IBlockBaseSet<T> IBlockBaseBaseSet<IBlockBaseSet<T>>.Encrypt()
        {
            _encryptQuery = true;
            return this;
        }
        #endregion

        public ISelectQuery GetGetQuery(object id)
        {
            var condition = NodeBuilder.GenerateComparisonNodeOnKey(typeof(T), id);
            var selectedProperties = new[] { new BlockBaseColumn() { Table = typeof(T).GetTableName() } };
            var joins = new[] { new JoinNode(typeof(T).GetPrimaryKey(), null, BlockBaseJoinEnum.Inner) };
            var query = new BlockBaseRecordSelectQuery(typeof(T), null, selectedProperties, joins,
                condition, 1, 0, _encryptQuery);
            return query;
        }

        public void BatchGet(object id)
        {
            var query = GetGetQuery(id);
            _batchQueries.Add(query);
        }

        public async Task<T> GetAsync(object id)
        {
            var query = GetGetQuery(id);
            var executor = new BlockBaseQueryExecutor() { UseDatabase = true };
            var res = (await executor.ExecuteQueryAsync<T>(query, Settings)).ToArray();
            return res.Length == 0 ? default : res[0];
        }

        public BlockBaseSet(BlockBaseQueryExecutor executor, BlockBaseSettings settings, List<IQuery> batchQueries) : base(executor, settings)
        {
            _batchQueries = batchQueries;
        }
    }
}
