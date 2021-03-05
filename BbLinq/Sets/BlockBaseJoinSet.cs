using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BlockBase.BBLinq.Parsers;
using BlockBase.BBLinq.Pocos.Nodes;
using BlockBase.BBLinq.Queries.RecordQueries;

namespace BlockBase.BBLinq.Sets
{
    #region 2-Join

    public class BlockBaseJoinSet<TA, TB> : BlockBaseBaseJoinSet<BlockBaseJoinSet<TA, TB>>
    {

        public BlockBaseJoinSet(JoinNode[] joins)
        {
            Joins = joins;
        }

        public BlockBaseJoinSet<TA, TB> Where(Expression<Func<TA, TB, bool>> predicate)
        {
            Predicate = predicate;
            return this;
        }

        #region Dict select
        public BlockBaseSelectRecordQuery<Dictionary<string, object>> Select()
        {
            return new BlockBaseSelectRecordQuery<Dictionary<string, object>>(typeof(TA), null, Joins, null, Predicate, RecordsToTake, RecordsToTake, EncryptQuery);
        }

        public void BatchSelect()
        {
            StoreQueryInBatch(Select());
        }

        public async Task<Dictionary<string, object>> SelectAsync()
        {
            var query = Select();
            return await Executor.ExecuteQueryAsync<Dictionary<string, object>>(query.GenerateQueryString());
        }
        #endregion

        #region Select All to Object
        public BlockBaseSelectRecordQuery<TResult> Select<TResult>()
        {
            return new BlockBaseSelectRecordQuery<TResult>(typeof(TA), null, Joins, null, Predicate, RecordsToTake, RecordsToTake, EncryptQuery);
        }

        public void BatchSelect<TResult>()
        {
            StoreQueryInBatch(Select<TResult>());
        }

        public async Task<IEnumerable<TResult>> SelectAsync<TResult>()
        {
            var query = Select<TResult>();
            return await Executor.ExecuteQueryAsync<IEnumerable<TResult>>(query.GenerateQueryString());
        }
        #endregion

        #region Select with mapper
        public BlockBaseSelectRecordQuery<TResult> Select<TResult>(Expression<Func<TA, TB, TResult>> mapper)
        {
            return new BlockBaseSelectRecordQuery<TResult>(typeof(TA), null, Joins, mapper, Predicate, RecordsToTake, RecordsToTake, EncryptQuery);
        }

        public void BatchSelect<TResult>(Expression<Func<TA, TB, TResult>> mapper)
        {
            StoreQueryInBatch(Select(mapper));
        }

        public async Task<IEnumerable<TResult>> SelectAsync<TResult>(Expression<Func<TA, TB, TResult>> mapper)
        {
            var query = Select(mapper);
            return await Executor.ExecuteQueryAsync<IEnumerable<TResult>>(query.GenerateQueryString());
        }
        #endregion


        public BlockBaseJoinSet<TA, TB, TC> Join<TC>()
        {
            var joins = new ExpressionParser().ParseJoins(Joins, typeof(TC));
            return new BlockBaseJoinSet<TA, TB, TC>(joins);
        }
    }


    #endregion

    #region 3-Join
    public class BlockBaseJoinSet<TA, TB, TC> : BlockBaseBaseJoinSet<BlockBaseJoinSet<TA, TB, TC>>
    {

        public BlockBaseJoinSet(JoinNode[] joins)
        {
            Joins = joins;
        }

        public BlockBaseJoinSet<TA, TB, TC> Where(Expression<Func<TA, TB, TC, bool>> predicate)
        {
            Predicate = predicate;
            return this;
        }

        #region Dict select
        public BlockBaseSelectRecordQuery<Dictionary<string, object>> Select()
        {
            return new BlockBaseSelectRecordQuery<Dictionary<string, object>>(typeof(TA), null, Joins, null, Predicate, RecordsToTake, RecordsToTake, EncryptQuery);
        }

        public void BatchSelect()
        {
            StoreQueryInBatch(Select());
        }

        public async Task<Dictionary<string, object>> SelectAsync()
        {
            var query = Select();
            return await Executor.ExecuteQueryAsync<Dictionary<string, object>>(query.GenerateQueryString());
        }
        #endregion

        #region Select All to Object
        public BlockBaseSelectRecordQuery<TResult> Select<TResult>()
        {
            return new BlockBaseSelectRecordQuery<TResult>(typeof(TA), null, Joins, null, Predicate, RecordsToTake, RecordsToTake, EncryptQuery);
        }

        public void BatchSelect<TResult>()
        {
            StoreQueryInBatch(Select<TResult>());
        }

        public async Task<IEnumerable<TResult>> SelectAsync<TResult>()
        {
            var query = Select<TResult>();
            return await Executor.ExecuteQueryAsync<IEnumerable<TResult>>(query.GenerateQueryString());
        }
        #endregion

        #region Select with mapper
        public BlockBaseSelectRecordQuery<TResult> Select<TResult>(Expression<Func<TA, TB, TC, TResult>> mapper)
        {
            return new BlockBaseSelectRecordQuery<TResult>(typeof(TA), null, Joins, mapper, Predicate, RecordsToTake, RecordsToTake, EncryptQuery);
        }

        public void BatchSelect<TResult>(Expression<Func<TA, TB, TC, TResult>> mapper)
        {
            StoreQueryInBatch(Select(mapper));
        }

        public async Task<IEnumerable<TResult>> SelectAsync<TResult>(Expression<Func<TA, TB, TC, TResult>> mapper)
        {
            var query = Select(mapper);
            return await Executor.ExecuteQueryAsync<IEnumerable<TResult>>(query.GenerateQueryString());
        }
        #endregion


        public BlockBaseJoinSet<TA, TB, TC, TD> Join<TD>()
        {
            var joins = new ExpressionParser().ParseJoins(Joins, typeof(TD));
            return new BlockBaseJoinSet<TA, TB, TC, TD>(joins);
        }
    }


    #endregion

    #region 4-Join
    public class BlockBaseJoinSet<TA, TB, TC, TD> : BlockBaseBaseJoinSet<BlockBaseJoinSet<TA, TB, TC, TD>>
    {

        public BlockBaseJoinSet(JoinNode[] joins)
        {
            Joins = joins;
        }

        public BlockBaseJoinSet<TA, TB, TC, TD> Where(Expression<Func<TA, TB, TC, TD, bool>> predicate)
        {
            Predicate = predicate;
            return this;
        }

        #region Dict select
        public BlockBaseSelectRecordQuery<Dictionary<string, object>> Select()
        {
            return new BlockBaseSelectRecordQuery<Dictionary<string, object>>(typeof(TA), null, Joins, null, Predicate, RecordsToTake, RecordsToTake, EncryptQuery);
        }

        public void BatchSelect()
        {
            StoreQueryInBatch(Select());
        }

        public async Task<Dictionary<string, object>> SelectAsync()
        {
            var query = Select();
            return await Executor.ExecuteQueryAsync<Dictionary<string, object>>(query.GenerateQueryString());
        }
        #endregion

        #region Select All to Object
        public BlockBaseSelectRecordQuery<TResult> Select<TResult>()
        {
            return new BlockBaseSelectRecordQuery<TResult>(typeof(TA), null, Joins, null, Predicate, RecordsToTake, RecordsToTake, EncryptQuery);
        }

        public void BatchSelect<TResult>()
        {
            StoreQueryInBatch(Select<TResult>());
        }

        public async Task<IEnumerable<TResult>> SelectAsync<TResult>()
        {
            var query = Select<TResult>();
            return await Executor.ExecuteQueryAsync<IEnumerable<TResult>>(query.GenerateQueryString());
        }
        #endregion

        #region Select with mapper
        public BlockBaseSelectRecordQuery<TResult> Select<TResult>(Expression<Func<TA, TB, TC, TD, TResult>> mapper)
        {
            return new BlockBaseSelectRecordQuery<TResult>(typeof(TA), null, Joins, mapper, Predicate, RecordsToTake, RecordsToTake, EncryptQuery);
        }

        public void BatchSelect<TResult>(Expression<Func<TA, TB, TC, TD, TResult>> mapper)
        {
            StoreQueryInBatch(Select(mapper));
        }

        public async Task<IEnumerable<TResult>> SelectAsync<TResult>(Expression<Func<TA, TB, TC, TD, TResult>> mapper)
        {
            var query = Select(mapper);
            return await Executor.ExecuteQueryAsync<IEnumerable<TResult>>(query.GenerateQueryString());
        }
        #endregion


        public BlockBaseJoinSet<TA, TB, TC, TD, TE> Join<TE>()
        {
            var joins = new ExpressionParser().ParseJoins(Joins, typeof(TE));
            return new BlockBaseJoinSet<TA, TB, TC, TD, TE>(joins);
        }
    }

    #endregion

    #region 5-Join
    public class BlockBaseJoinSet<TA, TB, TC, TD, TE> : BlockBaseBaseJoinSet<BlockBaseJoinSet<TA, TB, TC, TD, TE>>
    {

        public BlockBaseJoinSet(JoinNode[] joins)
        {
            Joins = joins;
        }

        public BlockBaseJoinSet<TA, TB, TC, TD, TE> Where(Expression<Func<TA, TB, TC, TD, TE, bool>> predicate)
        {
            Predicate = predicate;
            return this;
        }

        #region Dict select
        public BlockBaseSelectRecordQuery<Dictionary<string, object>> Select()
        {
            return new BlockBaseSelectRecordQuery<Dictionary<string, object>>(typeof(TA), null, Joins, null, Predicate, RecordsToTake, RecordsToTake, EncryptQuery);
        }

        public void BatchSelect()
        {
            StoreQueryInBatch(Select());
        }

        public async Task<Dictionary<string, object>> SelectAsync()
        {
            var query = Select();
            return await Executor.ExecuteQueryAsync<Dictionary<string, object>>(query.GenerateQueryString());
        }
        #endregion

        #region Select All to Object
        public BlockBaseSelectRecordQuery<TResult> Select<TResult>()
        {
            return new BlockBaseSelectRecordQuery<TResult>(typeof(TA), null, Joins, null, Predicate, RecordsToTake, RecordsToTake, EncryptQuery);
        }

        public void BatchSelect<TResult>()
        {
            StoreQueryInBatch(Select<TResult>());
        }

        public async Task<IEnumerable<TResult>> SelectAsync<TResult>()
        {
            var query = Select<TResult>();
            return await Executor.ExecuteQueryAsync<IEnumerable<TResult>>(query.GenerateQueryString());
        }
        #endregion

        #region Select with mapper
        public BlockBaseSelectRecordQuery<TResult> Select<TResult>(Expression<Func<TA, TB, TC, TD, TE, TResult>> mapper)
        {
            return new BlockBaseSelectRecordQuery<TResult>(typeof(TA), null, Joins, mapper, Predicate, RecordsToTake, RecordsToTake, EncryptQuery);
        }

        public void BatchSelect<TResult>(Expression<Func<TA, TB, TC, TD, TE, TResult>> mapper)
        {
            StoreQueryInBatch(Select(mapper));
        }

        public async Task<IEnumerable<TResult>> SelectAsync<TResult>(Expression<Func<TA, TB, TC, TD, TE, TResult>> mapper)
        {
            var query = Select(mapper);
            return await Executor.ExecuteQueryAsync<IEnumerable<TResult>>(query.GenerateQueryString());
        }
        #endregion


        public BlockBaseJoinSet<TA, TB, TC, TD, TE, TF> Join<TF>()
        {
            var joins = new ExpressionParser().ParseJoins(Joins, typeof(TF));
            return new BlockBaseJoinSet<TA, TB, TC, TD, TE, TF>(joins);
        }
    }

    #endregion

    #region 6-Join
    public class BlockBaseJoinSet<TA, TB, TC, TD, TE, TF> : BlockBaseBaseJoinSet<BlockBaseJoinSet<TA, TB, TC, TD, TE, TF>>
    {

        public BlockBaseJoinSet(JoinNode[] joins)
        {
            Joins = joins;
        }

        public BlockBaseJoinSet<TA, TB, TC, TD, TE, TF> Where(Expression<Func<TA, TB, TC, TD, TE, TF, bool>> predicate)
        {
            Predicate = predicate;
            return this;
        }

        #region Dict select
        public BlockBaseSelectRecordQuery<Dictionary<string, object>> Select()
        {
            return new BlockBaseSelectRecordQuery<Dictionary<string, object>>(typeof(TA), null, Joins, null, Predicate, RecordsToTake, RecordsToTake, EncryptQuery);
        }

        public void BatchSelect()
        {
            StoreQueryInBatch(Select());
        }

        public async Task<Dictionary<string, object>> SelectAsync()
        {
            var query = Select();
            return await Executor.ExecuteQueryAsync<Dictionary<string, object>>(query.GenerateQueryString());
        }
        #endregion

        #region Select All to Object
        public BlockBaseSelectRecordQuery<TResult> Select<TResult>()
        {
            return new BlockBaseSelectRecordQuery<TResult>(typeof(TA), null, Joins, null, Predicate, RecordsToTake, RecordsToTake, EncryptQuery);
        }

        public void BatchSelect<TResult>()
        {
            StoreQueryInBatch(Select<TResult>());
        }

        public async Task<IEnumerable<TResult>> SelectAsync<TResult>()
        {
            var query = Select<TResult>();
            return await Executor.ExecuteQueryAsync<IEnumerable<TResult>>(query.GenerateQueryString());
        }
        #endregion

        #region Select with mapper
        public BlockBaseSelectRecordQuery<TResult> Select<TResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TResult>> mapper)
        {
            return new BlockBaseSelectRecordQuery<TResult>(typeof(TA), null, Joins, mapper, Predicate, RecordsToTake, RecordsToTake, EncryptQuery);
        }

        public void BatchSelect<TResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TResult>> mapper)
        {
            StoreQueryInBatch(Select(mapper));
        }

        public async Task<IEnumerable<TResult>> SelectAsync<TResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TResult>> mapper)
        {
            var query = Select(mapper);
            return await Executor.ExecuteQueryAsync<IEnumerable<TResult>>(query.GenerateQueryString());
        }
        #endregion


        public BlockBaseJoinSet<TA, TB, TC, TD, TE, TF, TG> Join<TG>()
        {
            var joins = new ExpressionParser().ParseJoins(Joins, typeof(TG));
            return new BlockBaseJoinSet<TA, TB, TC, TD, TE, TF, TG>(joins);
        }
    }

    #endregion

    #region 7-Join
    public class BlockBaseJoinSet<TA, TB, TC, TD, TE, TF, TG> : BlockBaseBaseJoinSet<BlockBaseJoinSet<TA, TB, TC, TD, TE, TF, TG>>
    {

        public BlockBaseJoinSet(JoinNode[] joins)
        {
            Joins = joins;
        }

        public BlockBaseJoinSet<TA, TB, TC, TD, TE, TF, TG> Where(Expression<Func<TA, TB, TC, TD, TE, TF, TG, bool>> predicate)
        {
            Predicate = predicate;
            return this;
        }

        #region Dict select
        public BlockBaseSelectRecordQuery<Dictionary<string, object>> Select()
        {
            return new BlockBaseSelectRecordQuery<Dictionary<string, object>>(typeof(TA), null, Joins, null, Predicate, RecordsToTake, RecordsToTake, EncryptQuery);
        }

        public void BatchSelect()
        {
            StoreQueryInBatch(Select());
        }

        public async Task<Dictionary<string, object>> SelectAsync()
        {
            var query = Select();
            return await Executor.ExecuteQueryAsync<Dictionary<string, object>>(query.GenerateQueryString());
        }
        #endregion

        #region Select All to Object
        public BlockBaseSelectRecordQuery<TResult> Select<TResult>()
        {
            return new BlockBaseSelectRecordQuery<TResult>(typeof(TA), null, Joins, null, Predicate, RecordsToTake, RecordsToTake, EncryptQuery);
        }

        public void BatchSelect<TResult>()
        {
            StoreQueryInBatch(Select<TResult>());
        }

        public async Task<IEnumerable<TResult>> SelectAsync<TResult>()
        {
            var query = Select<TResult>();
            return await Executor.ExecuteQueryAsync<IEnumerable<TResult>>(query.GenerateQueryString());
        }
        #endregion

        #region Select with mapper
        public BlockBaseSelectRecordQuery<TResult> Select<TResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TResult>> mapper)
        {
            return new BlockBaseSelectRecordQuery<TResult>(typeof(TA), null, Joins, mapper, Predicate, RecordsToTake, RecordsToTake, EncryptQuery);
        }

        public void BatchSelect<TResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TResult>> mapper)
        {
            StoreQueryInBatch(Select(mapper));
        }

        public async Task<IEnumerable<TResult>> SelectAsync<TResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TResult>> mapper)
        {
            var query = Select(mapper);
            return await Executor.ExecuteQueryAsync<IEnumerable<TResult>>(query.GenerateQueryString());
        }
        #endregion


        public BlockBaseJoinSet<TA, TB, TC, TD, TE, TF, TG, TH> Join<TH>()
        {
            var joins = new ExpressionParser().ParseJoins(Joins, typeof(TH));
            return new BlockBaseJoinSet<TA, TB, TC, TD, TE, TF, TG, TH>(joins);
        }
    }
    #endregion

    #region 8-Join
    public class BlockBaseJoinSet<TA, TB, TC, TD, TE, TF, TG, TH> : BlockBaseBaseJoinSet<BlockBaseJoinSet<TA, TB, TC, TD, TE, TF, TG, TH>>
    {

        public BlockBaseJoinSet(JoinNode[] joins)
        {
            Joins = joins;
        }

        public BlockBaseJoinSet<TA, TB, TC, TD, TE, TF, TG, TH> Where(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, bool>> predicate)
        {
            Predicate = predicate;
            return this;
        }

        #region Dict select
        public BlockBaseSelectRecordQuery<Dictionary<string, object>> Select()
        {
            return new BlockBaseSelectRecordQuery<Dictionary<string, object>>(typeof(TA), null, Joins, null, Predicate, RecordsToTake, RecordsToTake, EncryptQuery);
        }

        public void BatchSelect()
        {
            StoreQueryInBatch(Select());
        }

        public async Task<Dictionary<string, object>> SelectAsync()
        {
            var query = Select();
            return await Executor.ExecuteQueryAsync<Dictionary<string, object>>(query.GenerateQueryString());
        }
        #endregion

        #region Select All to Object
        public BlockBaseSelectRecordQuery<TResult> Select<TResult>()
        {
            return new BlockBaseSelectRecordQuery<TResult>(typeof(TA), null, Joins, null, Predicate, RecordsToTake, RecordsToTake, EncryptQuery);
        }

        public void BatchSelect<TResult>()
        {
            StoreQueryInBatch(Select<TResult>());
        }

        public async Task<IEnumerable<TResult>> SelectAsync<TResult>()
        {
            var query = Select<TResult>();
            return await Executor.ExecuteQueryAsync<IEnumerable<TResult>>(query.GenerateQueryString());
        }
        #endregion

        #region Select with mapper
        public BlockBaseSelectRecordQuery<TResult> Select<TResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TResult>> mapper)
        {
            return new BlockBaseSelectRecordQuery<TResult>(typeof(TA), null, Joins, mapper, Predicate, RecordsToTake, RecordsToTake, EncryptQuery);
        }

        public void BatchSelect<TResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TResult>> mapper)
        {
            StoreQueryInBatch(Select(mapper));
        }

        public async Task<IEnumerable<TResult>> SelectAsync<TResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TResult>> mapper)
        {
            var query = Select(mapper);
            return await Executor.ExecuteQueryAsync<IEnumerable<TResult>>(query.GenerateQueryString());
        }
        #endregion


        public BlockBaseJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI> Join<TI>()
        {
            var joins = new ExpressionParser().ParseJoins(Joins, typeof(TI));
            return new BlockBaseJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI>(joins);
        }
    }

    #endregion

    #region 9-Join

    public class BlockBaseJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI> : BlockBaseBaseJoinSet<BlockBaseJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI>>
    {

        public BlockBaseJoinSet(JoinNode[] joins)
        {
            Joins = joins;
        }

        public BlockBaseJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI> Where(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, bool>> predicate)
        {
            Predicate = predicate;
            return this;
        }

        #region Dict select
        public BlockBaseSelectRecordQuery<Dictionary<string, object>> Select()
        {
            return new BlockBaseSelectRecordQuery<Dictionary<string, object>>(typeof(TA), null, Joins, null, Predicate, RecordsToTake, RecordsToTake, EncryptQuery);
        }

        public void BatchSelect()
        {
            StoreQueryInBatch(Select());
        }

        public async Task<Dictionary<string, object>> SelectAsync()
        {
            var query = Select();
            return await Executor.ExecuteQueryAsync<Dictionary<string, object>>(query.GenerateQueryString());
        }
        #endregion

        #region Select All to Object
        public BlockBaseSelectRecordQuery<TResult> Select<TResult>()
        {
            return new BlockBaseSelectRecordQuery<TResult>(typeof(TA), null, Joins, null, Predicate, RecordsToTake, RecordsToTake, EncryptQuery);
        }

        public void BatchSelect<TResult>()
        {
            StoreQueryInBatch(Select<TResult>());
        }

        public async Task<IEnumerable<TResult>> SelectAsync<TResult>()
        {
            var query = Select<TResult>();
            return await Executor.ExecuteQueryAsync<IEnumerable<TResult>>(query.GenerateQueryString());
        }
        #endregion

        #region Select with mapper
        public BlockBaseSelectRecordQuery<TResult> Select<TResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, TResult>> mapper)
        {
            return new BlockBaseSelectRecordQuery<TResult>(typeof(TA), null, Joins, mapper, Predicate, RecordsToTake, RecordsToTake, EncryptQuery);
        }

        public void BatchSelect<TResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, TResult>> mapper)
        {
            StoreQueryInBatch(Select(mapper));
        }

        public async Task<IEnumerable<TResult>> SelectAsync<TResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, TResult>> mapper)
        {
            var query = Select(mapper);
            return await Executor.ExecuteQueryAsync<IEnumerable<TResult>>(query.GenerateQueryString());
        }
        #endregion


        public BlockBaseJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ> Join<TJ>()
        {
            var joins = new ExpressionParser().ParseJoins(Joins, typeof(TJ));
            return new BlockBaseJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ>(joins);
        }
    }


    #endregion

    #region 10-Join
    public class BlockBaseJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ> : BlockBaseBaseJoinSet<BlockBaseJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ>>
    {

        public BlockBaseJoinSet(JoinNode[] joins)
        {
            Joins = joins;
        }

        public BlockBaseJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ> Where(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, bool>> predicate)
        {
            Predicate = predicate;
            return this;
        }

        #region Dict select
        public BlockBaseSelectRecordQuery<Dictionary<string, object>> Select()
        {
            return new BlockBaseSelectRecordQuery<Dictionary<string, object>>(typeof(TA), null, Joins, null, Predicate, RecordsToTake, RecordsToTake, EncryptQuery);
        }

        public void BatchSelect()
        {
            StoreQueryInBatch(Select());
        }

        public async Task<Dictionary<string, object>> SelectAsync()
        {
            var query = Select();
            return await Executor.ExecuteQueryAsync<Dictionary<string, object>>(query.GenerateQueryString());
        }
        #endregion

        #region Select All to Object
        public BlockBaseSelectRecordQuery<TResult> Select<TResult>()
        {
            return new BlockBaseSelectRecordQuery<TResult>(typeof(TA), null, Joins, null, Predicate, RecordsToTake, RecordsToTake, EncryptQuery);
        }

        public void BatchSelect<TResult>()
        {
            StoreQueryInBatch(Select<TResult>());
        }

        public async Task<IEnumerable<TResult>> SelectAsync<TResult>()
        {
            var query = Select<TResult>();
            return await Executor.ExecuteQueryAsync<IEnumerable<TResult>>(query.GenerateQueryString());
        }
        #endregion

        #region Select with mapper
        public BlockBaseSelectRecordQuery<TResult> Select<TResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TResult>> mapper)
        {
            return new BlockBaseSelectRecordQuery<TResult>(typeof(TA), null, Joins, mapper, Predicate, RecordsToTake, RecordsToTake, EncryptQuery);
        }

        public void BatchSelect<TResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TResult>> mapper)
        {
            StoreQueryInBatch(Select(mapper));
        }

        public async Task<IEnumerable<TResult>> SelectAsync<TResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TResult>> mapper)
        {
            var query = Select(mapper);
            return await Executor.ExecuteQueryAsync<IEnumerable<TResult>>(query.GenerateQueryString());
        }
        #endregion


        public BlockBaseJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK> Join<TK>()
        {
            var joins = new ExpressionParser().ParseJoins(Joins, typeof(TK));
            return new BlockBaseJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK>(joins);
        }
    }



    #endregion

    #region 11-Join
    public class BlockBaseJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK> : BlockBaseBaseJoinSet<BlockBaseJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK>>
    {

        public BlockBaseJoinSet(JoinNode[] joins)
        {
            Joins = joins;
        }

        public BlockBaseJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK> Where(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, bool>> predicate)
        {
            Predicate = predicate;
            return this;
        }

        #region Dict select
        public BlockBaseSelectRecordQuery<Dictionary<string, object>> Select()
        {
            return new BlockBaseSelectRecordQuery<Dictionary<string, object>>(typeof(TA), null, Joins, null, Predicate, RecordsToTake, RecordsToTake, EncryptQuery);
        }

        public void BatchSelect()
        {
            StoreQueryInBatch(Select());
        }

        public async Task<Dictionary<string, object>> SelectAsync()
        {
            var query = Select();
            return await Executor.ExecuteQueryAsync<Dictionary<string, object>>(query.GenerateQueryString());
        }
        #endregion

        #region Select All to Object
        public BlockBaseSelectRecordQuery<TResult> Select<TResult>()
        {
            return new BlockBaseSelectRecordQuery<TResult>(typeof(TA), null, Joins, null, Predicate, RecordsToTake, RecordsToTake, EncryptQuery);
        }

        public void BatchSelect<TResult>()
        {
            StoreQueryInBatch(Select<TResult>());
        }

        public async Task<IEnumerable<TResult>> SelectAsync<TResult>()
        {
            var query = Select<TResult>();
            return await Executor.ExecuteQueryAsync<IEnumerable<TResult>>(query.GenerateQueryString());
        }
        #endregion

        #region Select with mapper
        public BlockBaseSelectRecordQuery<TResult> Select<TResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TResult>> mapper)
        {
            return new BlockBaseSelectRecordQuery<TResult>(typeof(TA), null, Joins, mapper, Predicate, RecordsToTake, RecordsToTake, EncryptQuery);
        }

        public void BatchSelect<TResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TResult>> mapper)
        {
            StoreQueryInBatch(Select(mapper));
        }

        public async Task<IEnumerable<TResult>> SelectAsync<TResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TResult>> mapper)
        {
            var query = Select(mapper);
            return await Executor.ExecuteQueryAsync<IEnumerable<TResult>>(query.GenerateQueryString());
        }
        #endregion


        public BlockBaseJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL> Join<TL>()
        {
            var joins = new ExpressionParser().ParseJoins(Joins, typeof(TL));
            return new BlockBaseJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL>(joins);
        }
    }



    #endregion

    #region 12-Join
    public class BlockBaseJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL> : BlockBaseBaseJoinSet<BlockBaseJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL>>
    {

        public BlockBaseJoinSet(JoinNode[] joins)
        {
            Joins = joins;
        }

        public BlockBaseJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL> Where(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, bool>> predicate)
        {
            Predicate = predicate;
            return this;
        }

        #region Dict select
        public BlockBaseSelectRecordQuery<Dictionary<string, object>> Select()
        {
            return new BlockBaseSelectRecordQuery<Dictionary<string, object>>(typeof(TA), null, Joins, null, Predicate, RecordsToTake, RecordsToTake, EncryptQuery);
        }

        public void BatchSelect()
        {
            StoreQueryInBatch(Select());
        }

        public async Task<Dictionary<string, object>> SelectAsync()
        {
            var query = Select();
            return await Executor.ExecuteQueryAsync<Dictionary<string, object>>(query.GenerateQueryString());
        }
        #endregion

        #region Select All to Object
        public BlockBaseSelectRecordQuery<TResult> Select<TResult>()
        {
            return new BlockBaseSelectRecordQuery<TResult>(typeof(TA), null, Joins, null, Predicate, RecordsToTake, RecordsToTake, EncryptQuery);
        }

        public void BatchSelect<TResult>()
        {
            StoreQueryInBatch(Select<TResult>());
        }

        public async Task<IEnumerable<TResult>> SelectAsync<TResult>()
        {
            var query = Select<TResult>();
            return await Executor.ExecuteQueryAsync<IEnumerable<TResult>>(query.GenerateQueryString());
        }
        #endregion

        #region Select with mapper
        public BlockBaseSelectRecordQuery<TResult> Select<TResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TResult>> mapper)
        {
            return new BlockBaseSelectRecordQuery<TResult>(typeof(TA), null, Joins, mapper, Predicate, RecordsToTake, RecordsToTake, EncryptQuery);
        }

        public void BatchSelect<TResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TResult>> mapper)
        {
            StoreQueryInBatch(Select(mapper));
        }

        public async Task<IEnumerable<TResult>> SelectAsync<TResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TResult>> mapper)
        {
            var query = Select(mapper);
            return await Executor.ExecuteQueryAsync<IEnumerable<TResult>>(query.GenerateQueryString());
        }
        #endregion


        public BlockBaseJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM> Join<TM>()
        {
            var joins = new ExpressionParser().ParseJoins(Joins, typeof(TM));
            return new BlockBaseJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM>(joins);
        }
    }



    #endregion

    #region 13-Join
    public class BlockBaseJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM> : BlockBaseBaseJoinSet<BlockBaseJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM>>
    {

        public BlockBaseJoinSet(JoinNode[] joins)
        {
            Joins = joins;
        }

        public BlockBaseJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM> Where(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, bool>> predicate)
        {
            Predicate = predicate;
            return this;
        }

        #region Dict select
        public BlockBaseSelectRecordQuery<Dictionary<string, object>> Select()
        {
            return new BlockBaseSelectRecordQuery<Dictionary<string, object>>(typeof(TA), null, Joins, null, Predicate, RecordsToTake, RecordsToTake, EncryptQuery);
        }

        public void BatchSelect()
        {
            StoreQueryInBatch(Select());
        }

        public async Task<Dictionary<string, object>> SelectAsync()
        {
            var query = Select();
            return await Executor.ExecuteQueryAsync<Dictionary<string, object>>(query.GenerateQueryString());
        }
        #endregion

        #region Select All to Object
        public BlockBaseSelectRecordQuery<TResult> Select<TResult>()
        {
            return new BlockBaseSelectRecordQuery<TResult>(typeof(TA), null, Joins, null, Predicate, RecordsToTake, RecordsToTake, EncryptQuery);
        }

        public void BatchSelect<TResult>()
        {
            StoreQueryInBatch(Select<TResult>());
        }

        public async Task<IEnumerable<TResult>> SelectAsync<TResult>()
        {
            var query = Select<TResult>();
            return await Executor.ExecuteQueryAsync<IEnumerable<TResult>>(query.GenerateQueryString());
        }
        #endregion

        #region Select with mapper
        public BlockBaseSelectRecordQuery<TResult> Select<TResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TResult>> mapper)
        {
            return new BlockBaseSelectRecordQuery<TResult>(typeof(TA), null, Joins, mapper, Predicate, RecordsToTake, RecordsToTake, EncryptQuery);
        }

        public void BatchSelect<TResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TResult>> mapper)
        {
            StoreQueryInBatch(Select(mapper));
        }

        public async Task<IEnumerable<TResult>> SelectAsync<TResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TResult>> mapper)
        {
            var query = Select(mapper);
            return await Executor.ExecuteQueryAsync<IEnumerable<TResult>>(query.GenerateQueryString());
        }
        #endregion


        public BlockBaseJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TN> Join<TN>()
        {
            var joins = new ExpressionParser().ParseJoins(Joins, typeof(TN));
            return new BlockBaseJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TN>(joins);
        }
    }



    #endregion

    #region 14-Join

    public class BlockBaseJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TN> : BlockBaseBaseJoinSet<BlockBaseJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TN>>
    {

        public BlockBaseJoinSet(JoinNode[] joins)
        {
            Joins = joins;
        }

        public BlockBaseJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TN> Where(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TN, bool>> predicate)
        {
            Predicate = predicate;
            return this;
        }

        #region Dict select
        public BlockBaseSelectRecordQuery<Dictionary<string, object>> Select()
        {
            return new BlockBaseSelectRecordQuery<Dictionary<string, object>>(typeof(TA), null, Joins, null, Predicate, RecordsToTake, RecordsToTake, EncryptQuery);
        }

        public void BatchSelect()
        {
            StoreQueryInBatch(Select());
        }

        public async Task<Dictionary<string, object>> SelectAsync()
        {
            var query = Select();
            return await Executor.ExecuteQueryAsync<Dictionary<string, object>>(query.GenerateQueryString());
        }
        #endregion

        #region Select All to Object
        public BlockBaseSelectRecordQuery<TResult> Select<TResult>()
        {
            return new BlockBaseSelectRecordQuery<TResult>(typeof(TA), null, Joins, null, Predicate, RecordsToTake, RecordsToTake, EncryptQuery);
        }

        public void BatchSelect<TResult>()
        {
            StoreQueryInBatch(Select<TResult>());
        }

        public async Task<IEnumerable<TResult>> SelectAsync<TResult>()
        {
            var query = Select<TResult>();
            return await Executor.ExecuteQueryAsync<IEnumerable<TResult>>(query.GenerateQueryString());
        }
        #endregion

        #region Select with mapper
        public BlockBaseSelectRecordQuery<TResult> Select<TResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TN, TResult>> mapper)
        {
            return new BlockBaseSelectRecordQuery<TResult>(typeof(TA), null, Joins, mapper, Predicate, RecordsToTake, RecordsToTake, EncryptQuery);
        }

        public void BatchSelect<TResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TN, TResult>> mapper)
        {
            StoreQueryInBatch(Select(mapper));
        }

        public async Task<IEnumerable<TResult>> SelectAsync<TResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TN, TResult>> mapper)
        {
            var query = Select(mapper);
            return await Executor.ExecuteQueryAsync<IEnumerable<TResult>>(query.GenerateQueryString());
        }
        #endregion


        public BlockBaseJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TN, TO> Join<TO>()
        {
            var joins = new ExpressionParser().ParseJoins(Joins, typeof(TO));
            return new BlockBaseJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TN, TO>(joins);
        }
    }

    #endregion

    #region 15-Join

    public class BlockBaseJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TN, TO> :
        BlockBaseBaseJoinSet<BlockBaseJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TN, TO>>
    {
        public BlockBaseJoinSet(JoinNode[] joins)
        {
            Joins = joins;
        }

        public BlockBaseJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TN, TO> Where(
            Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TN, TO, bool>> predicate)
        {
            Predicate = predicate;
            return this;
        }

        #region Dict select
        public BlockBaseSelectRecordQuery<Dictionary<string, object>> Select()
        {
            return new BlockBaseSelectRecordQuery<Dictionary<string, object>>(typeof(TA), null, Joins, null, Predicate, RecordsToTake, RecordsToTake, EncryptQuery);
        }

        public void BatchSelect()
        {
            StoreQueryInBatch(Select());
        }

        public async Task<Dictionary<string, object>> SelectAsync()
        {
            var query = Select();
            return await Executor.ExecuteQueryAsync<Dictionary<string, object>>(query.GenerateQueryString());
        }
        #endregion

        #region Select All to Object
        public BlockBaseSelectRecordQuery<TResult> Select<TResult>()
        {
            return new BlockBaseSelectRecordQuery<TResult>(typeof(TA), null, Joins, null, Predicate, RecordsToTake, RecordsToTake, EncryptQuery);
        }

        public void BatchSelect<TResult>()
        {
            StoreQueryInBatch(Select<TResult>());
        }

        public async Task<IEnumerable<TResult>> SelectAsync<TResult>()
        {
            var query = Select<TResult>();
            return await Executor.ExecuteQueryAsync<IEnumerable<TResult>>(query.GenerateQueryString());
        }
        #endregion

        #region Select with mapper
        public BlockBaseSelectRecordQuery<TResult> Select<TResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TN, TO, TResult>> mapper)
        {
            return new BlockBaseSelectRecordQuery<TResult>(typeof(TA), null, Joins, mapper, Predicate, RecordsToTake, RecordsToTake, EncryptQuery);
        }

        public void BatchSelect<TResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TN, TO, TResult>> mapper)
        {
            StoreQueryInBatch(Select<TResult>(mapper));
        }

        public async Task<IEnumerable<TResult>> SelectAsync<TResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TN, TO, TResult>> mapper)
        {
            var query = Select<TResult>(mapper);
            return await Executor.ExecuteQueryAsync<IEnumerable<TResult>>(query.GenerateQueryString());
        }
        #endregion
    }

    #endregion
}
