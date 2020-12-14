using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BlockBase.BBLinq.Interfaces;
using BlockBase.BBLinq.Parsers;
using BlockBase.BBLinq.Queries;
using BlockBase.BBLinq.Results;

namespace BlockBase.BBLinq.Sets
{
    public class BbJoin<TResult> : BbBaseSet<TResult> where TResult:BbJoin<TResult>
    {
        protected List<LambdaExpression> Joins = new List<LambdaExpression>();
    }

    public class BbJoin<TA,TB> : BbJoin<BbJoin<TA, TB>>, ILists<TA, TB>, IGets<TA, TB> where TB : class where TA : class
    {
        public BbJoin(LambdaExpression on)
        {
            Joins.Add(on);
        }

        public BbJoin<TA, TB, TC> Join<TC>() where TC : class
        {
            var on = GenerateJoinExpression(GetType().GetGenericArguments(), typeof(TC));
            return new BbJoin<TA, TB, TC>(Joins, on);
        }

        public async Task<QueryResult<IEnumerable<TC>>> List<TC>(Expression<Func<TA, TB, TC>> mapper)
        {
            var query = new SelectQuery(typeof(TA), Joins, Filter,  Encrypted, mapper, RecordLimit, RecordsToSkip, null);
            var result = Executor.ExecuteQueryAsync(query.ToString());
            var properties = ParseSelectBody<TC>(mapper);
            return await ResultParser.ParseListResult<TC>(result, properties);
        }

        public async Task<QueryResult<TC>> Get<TC>(Expression<Func<TA, TB, TC>> mapper)
        {
            var query = new SelectQuery(typeof(TA), Joins, Filter, Encrypted, mapper, 1, RecordsToSkip, null);
            var result = Executor.ExecuteQueryAsync(query.ToString());
            var properties = ParseSelectBody<TC>(mapper);
            return await ResultParser.ParseFetchResult<TC>(result, properties);
        }

        public BbJoin<TA, TB> Where(Expression<Func<TA, TB, bool>> filter)
        {
           return base.Where(filter);
        }


        /// <summary>
        /// Sets the limit on the request
        /// </summary>
        /// <param name="limit">number of elements</param>
        /// <returns></returns>
        public BbJoin<TA, TB> Limit(int limit)
        {
            RecordLimit = limit;
            return this;
        }

        /// <summary>
        /// Sets the offset on the request
        /// </summary>
        /// <param name="limit">number of elements to offset</param>
        /// <returns></returns>
        public BbJoin<TA, TB> Offset(int offset)
        {
            RecordsToSkip = offset;
            return this;
        }
    }

    public class BbJoin<TA, TB, TC> : BbJoin<BbJoin<TA, TB, TC>>, ILists<TA, TB, TC>, IGets<TA, TB, TC> where TC:class where TB : class where TA : class
    {
        public BbJoin(List<LambdaExpression> expressions, LambdaExpression on)
        {
            Joins = expressions;
            Joins.Add(on);
        }

        public BbJoin<TA, TB, TC, TD> Join<TD>() where TD : class
        {
            var on = GenerateJoinExpression(GetType().GetGenericArguments(), typeof(TD));
            return new BbJoin<TA, TB, TC, TD>(Joins, on);
        }

        public BbJoin<TA, TB, TC, TD> Join<TD>(Expression<Func<TA, TB, TC>> @on) where TD : class
        {
            return new BbJoin<TA, TB, TC, TD>(Joins, on);
        }


        public async Task<QueryResult<IEnumerable<TD>>> List<TD>(Expression<Func<TA, TB, TC, TD>> mapper)
        {
            var query = new SelectQuery(typeof(TA), Joins, Filter, Encrypted, mapper, RecordLimit, RecordsToSkip, null);
            var result = Executor.ExecuteQueryAsync(query.ToString());
            var properties = ParseSelectBody<TD>(mapper);
            return await ResultParser.ParseListResult<TD>(result, properties);
        }

        public async Task<QueryResult<TD>> Get<TD>(Expression<Func<TA, TB, TC, TD>> mapper)
        {
            var query = new SelectQuery(typeof(TA), Joins, Filter, Encrypted, mapper, 1, RecordsToSkip, null);
            var result = Executor.ExecuteQueryAsync(query.ToString());
            var properties = ParseSelectBody<TD>(mapper);
            return await ResultParser.ParseFetchResult<TD>(result, properties);
        }

        public BbJoin<TA, TB, TC> Where(Expression<Func<TA, TB, TC, bool>> filter)
        {
            return base.Where(filter);
        }


        /// <summary>
        /// Sets the limit on the request
        /// </summary>
        /// <param name="limit">number of elements</param>
        /// <returns></returns>
        public BbJoin<TA, TB, TC> Limit(int limit)
        {
            RecordLimit = limit;
            return this;
        }

        /// <summary>
        /// Sets the offset on the request
        /// </summary>
        /// <param name="limit">number of elements to offset</param>
        /// <returns></returns>
        public BbJoin<TA, TB, TC> Offset(int offset)
        {
            RecordsToSkip = offset;
            return this;
        }
    }


    public class BbJoin<TA, TB, TC, TD> : BbJoin<BbJoin<TA, TB, TC, TD>>, ILists<TA, TB, TC, TD>, IGets<TA, TB, TC, TD> where TB : class where TA : class where TC : class where TD : class
    {
        public BbJoin(List<LambdaExpression> expressions, LambdaExpression on)
        {
            Joins = expressions;
            Joins.Add(on);
        }

        public BbJoin<TA, TB, TC, TD, TE> Join<TE>() where TE : class
        {
            var on = GenerateJoinExpression(GetType().GetGenericArguments(), typeof(TE));
            return new BbJoin<TA, TB, TC, TD, TE>(Joins, on);
        }

        public BbJoin<TA, TB, TC, TD, TE> Join<TE>(Expression<Func<TA, TB, TC, TD>> @on) where TE : class
        {
            return new BbJoin<TA, TB, TC, TD, TE>(Joins, on);
        }

        public async Task<QueryResult<IEnumerable<TE>>> List<TE>(Expression<Func<TA, TB, TC, TD, TE>> mapper)
        {
            var query = new SelectQuery(typeof(TA), Joins, Filter,  Encrypted, mapper, RecordLimit, RecordsToSkip, null);
            var result = Executor.ExecuteQueryAsync(query.ToString());
            var properties = ParseSelectBody<TE>(mapper);
            return await ResultParser.ParseListResult<TE>(result, properties);
        }

        public async Task<QueryResult<TE>> Get<TE>(Expression<Func<TA, TB, TC, TD, TE>> mapper)
        {
            var query = new SelectQuery(typeof(TA), Joins, Filter,  Encrypted, mapper, 1, RecordsToSkip, null);
            var result = Executor.ExecuteQueryAsync(query.ToString());
            var properties = ParseSelectBody<TE>(mapper);
            return await ResultParser.ParseFetchResult<TE>(result, properties);
        }

        public BbJoin<TA, TB, TC, TD> Where(Expression<Func<TA, TB, TC, TD, bool>> filter)
        {
            return base.Where(filter);
        }

        /// <summary>
        /// Sets the limit on the request
        /// </summary>
        /// <param name="limit">number of elements</param>
        /// <returns></returns>
        public BbJoin<TA, TB, TC, TD> Limit(int limit)
        {
            RecordLimit = limit;
            return this;
        }

        /// <summary>
        /// Sets the offset on the request
        /// </summary>
        /// <param name="limit">number of elements to offset</param>
        /// <returns></returns>
        public BbJoin<TA, TB, TC, TD> Offset(int offset)
        {
            RecordsToSkip = offset;
            return this;
        }
    }

    public class BbJoin<TA, TB, TC, TD, TE> : BbJoin<BbJoin<TA, TB, TC, TD, TE>>, ILists<TA, TB, TC, TD, TE>, IGets<TA, TB, TC, TD, TE> where TB : class where TA : class where TC : class where TD : class where TE : class
    {
        public BbJoin(List<LambdaExpression> expressions, LambdaExpression on)
        {
            Joins = expressions;
            Joins.Add(on);
        }

        public BbJoin<TA, TB, TC, TD, TE, TF> Join<TF>() where TF : class
        {
            var on = GenerateJoinExpression(GetType().GetGenericArguments(), typeof(TF));
            return new BbJoin<TA, TB, TC, TD, TE, TF>(Joins, on);
        }

        public BbJoin<TA, TB, TC, TD, TE, TF> Join<TF>(Expression<Func<TA, TB, TC, TD, TE>> @on) where TF : class
        {
            return new BbJoin<TA, TB, TC, TD, TE, TF>(Joins, on);
        }

        public async Task<QueryResult<IEnumerable<TF>>> List<TF>(Expression<Func<TA, TB, TC, TD, TE, TF>> mapper)
        {
            var query = new SelectQuery(typeof(TA), null, Filter,  Encrypted, mapper, RecordLimit, RecordsToSkip, null);
            var result = Executor.ExecuteQueryAsync(query.ToString());
            var properties = ParseSelectBody<TF>(mapper);
            return await ResultParser.ParseListResult<TF>(result, properties);
        }

        public async Task<QueryResult<TF>> Get<TF>(Expression<Func<TA, TB, TC, TD, TE, TF>> mapper)
        {
            var query = new SelectQuery(typeof(TA), Joins, Filter,  Encrypted, mapper, 1, RecordsToSkip, null);
            var result = Executor.ExecuteQueryAsync(query.ToString());
            var properties = ParseSelectBody<TF>(mapper);
            return await ResultParser.ParseFetchResult<TF>(result, properties);
        }

        public BbJoin<TA, TB, TC, TD, TE> Where(Expression<Func<TA, TB, TC, TD, TE, bool>> filter)
        {
            return base.Where(filter);
        }

        /// <summary>
        /// Sets the limit on the request
        /// </summary>
        /// <param name="limit">number of elements</param>
        /// <returns></returns>
        public BbJoin<TA, TB, TC, TD, TE> Limit(int limit)
        {
            RecordLimit = limit;
            return this;
        }

        /// <summary>
        /// Sets the offset on the request
        /// </summary>
        /// <param name="limit">number of elements to offset</param>
        /// <returns></returns>
        public BbJoin<TA, TB, TC, TD, TE> Offset(int offset)
        {
            RecordsToSkip = offset;
            return this;
        }
    }

    public class BbJoin<TA, TB, TC, TD, TE, TF> : BbJoin<BbJoin<TA, TB, TC, TD, TE, TF>>, ILists<TA, TB, TC, TD, TE, TF>, IGets<TA, TB, TC, TD, TE, TF> where TB : class where TA : class where TC : class where TD : class where TE : class where TF : class
    {
        public BbJoin(List<LambdaExpression> expressions, LambdaExpression on)
        {
            Joins = expressions;
            Joins.Add(on);
        }

        public BbJoin<TA, TB, TC, TD, TE, TF, TG> Join<TG>() where TG : class
        {
            var on = GenerateJoinExpression(GetType().GetGenericArguments(), typeof(TG));
            return new BbJoin<TA, TB, TC, TD, TE, TF, TG>(Joins, on);
        }

        public BbJoin<TA, TB, TC, TD, TE, TF, TG> Join<TG>(Expression<Func<TA, TB, TC, TD, TE, TF>> @on) where TG : class
        {
            return new BbJoin<TA, TB, TC, TD, TE, TF, TG>(Joins, on);
        }

        public async Task<QueryResult<IEnumerable<TG>>> List<TG>(Expression<Func<TA, TB, TC, TD, TE, TF, TG>> mapper)
        {
            var query = new SelectQuery(typeof(TA), null, Filter,  Encrypted, mapper, RecordLimit, RecordsToSkip, null);
            var result = Executor.ExecuteQueryAsync(query.ToString());
            var properties = ParseSelectBody<TG>(mapper);
            return await ResultParser.ParseListResult<TG>(result, properties);
        }

        public async Task<QueryResult<TG>> Get<TG>(Expression<Func<TA, TB, TC, TD, TE, TF, TG>> mapper)
        {
            var query = new SelectQuery(typeof(TA), Joins, Filter,  Encrypted, mapper, 1, RecordsToSkip, null);
            var result = Executor.ExecuteQueryAsync(query.ToString());
            var properties = ParseSelectBody<TG>(mapper);
            return await ResultParser.ParseFetchResult<TG>(result, properties);
        }

        public BbJoin<TA, TB, TC, TD, TE, TF> Where(Expression<Func<TA, TB, TC, TD, TE, TF, bool>> filter)
        {
            return base.Where(filter);
        }

        /// <summary>
        /// Sets the limit on the request
        /// </summary>
        /// <param name="limit">number of elements</param>
        /// <returns></returns>
        public BbJoin<TA, TB, TC, TD, TE, TF> Limit(int limit)
        {
            RecordLimit = limit;
            return this;
        }

        /// <summary>
        /// Sets the offset on the request
        /// </summary>
        /// <param name="limit">number of elements to offset</param>
        /// <returns></returns>
        public BbJoin<TA, TB, TC, TD, TE, TF> Offset(int offset)
        {
            RecordsToSkip = offset;
            return this;
        }

    }

    public class BbJoin<TA, TB, TC, TD, TE, TF, TG> : BbJoin<BbJoin<TA, TB, TC, TD, TE, TF, TG>>, ILists<TA, TB, TC, TD, TE, TF, TG>, IGets<TA, TB, TC, TD, TE, TF, TG> where TB : class where TA : class where TC : class where TD : class where TE : class where TF : class where TG : class
    {
        public BbJoin(List<LambdaExpression> expressions, LambdaExpression on)
        {
            Joins = expressions;
            Joins.Add(on);
        }

        public BbJoin<TA, TB, TC, TD, TE, TF, TG, TH> Join<TH>() where TH : class
        {
            var on = GenerateJoinExpression(GetType().GetGenericArguments(), typeof(TH));
            return new BbJoin<TA, TB, TC, TD, TE, TF, TG, TH>(Joins, on);
        }

        public BbJoin<TA, TB, TC, TD, TE, TF, TG, TH> Join<TH>(Expression<Func<TA, TB, TC, TD, TE, TF, TG>> @on) where TH : class
        {
            return new BbJoin<TA, TB, TC, TD, TE, TF, TG, TH>(Joins, on);
        }

        public async Task<QueryResult<IEnumerable<TH>>> List<TH>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH>> mapper)
        {
            var query = new SelectQuery(typeof(TA), null, Filter,  Encrypted, mapper, RecordLimit, RecordsToSkip, null);
            var result = Executor.ExecuteQueryAsync(query.ToString());
            var properties = ParseSelectBody<TH>(mapper);
            return await ResultParser.ParseListResult<TH>(result, properties);
        }

        public async Task<QueryResult<TH>> Get<TH>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH>> mapper)
        {
            var query = new SelectQuery(typeof(TA), Joins, Filter,  Encrypted, mapper, 1, RecordsToSkip, null);
            var result = Executor.ExecuteQueryAsync(query.ToString());
            var properties = ParseSelectBody<TH>(mapper);
            return await ResultParser.ParseFetchResult<TH>(result, properties);
        }

        public BbJoin<TA, TB, TC, TD, TE, TF, TG> Where(Expression<Func<TA, TB, TC, TD, TE, TF, TG, bool>> filter)
        {
            return base.Where(filter);
        }

        /// <summary>
        /// Sets the limit on the request
        /// </summary>
        /// <param name="limit">number of elements</param>
        /// <returns></returns>
        public BbJoin<TA, TB, TC, TD, TE, TF, TG> Limit(int limit)
        {
            RecordLimit = limit;
            return this;
        }

        /// <summary>
        /// Sets the offset on the request
        /// </summary>
        /// <param name="limit">number of elements to offset</param>
        /// <returns></returns>
        public BbJoin<TA, TB, TC, TD, TE, TF, TG> Offset(int offset)
        {
            RecordsToSkip = offset;
            return this;
        }
    }

    public class BbJoin<TA, TB, TC, TD, TE, TF, TG, TH> : BbJoin<BbJoin<TA, TB, TC, TD, TE, TF, TG, TH>>, ILists<TA, TB, TC, TD, TE, TF, TG, TH>, IGets<TA, TB, TC, TD, TE, TF, TG, TH> where TB : class where TA : class where TC : class where TD : class where TE : class where TF : class where TG : class where TH : class
    {
        public BbJoin(List<LambdaExpression> expressions, LambdaExpression on)
        {
            Joins = expressions;
            Joins.Add(on);
        }

        public async Task<QueryResult<IEnumerable<TI>>> List<TI>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI>> mapper)
        {
            var query = new SelectQuery(typeof(TA), null, Filter,  Encrypted, mapper, RecordLimit, RecordsToSkip, null);
            var result = Executor.ExecuteQueryAsync(query.ToString());
            var properties = ParseSelectBody<TI>(mapper);
            return await ResultParser.ParseListResult<TI>(result, properties);
        }

        public async Task<QueryResult<TI>> Get<TI>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI>> mapper)
        {
            var query = new SelectQuery(typeof(TA), Joins, Filter,  Encrypted, mapper, 1, RecordsToSkip, null);
            var result = Executor.ExecuteQueryAsync(query.ToString());
            var properties = ParseSelectBody<TI>(mapper);
            return await ResultParser.ParseFetchResult<TI>(result, properties);
        }

        public BbJoin<TA, TB, TC, TD, TE, TF, TG, TH> Where(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, bool>> filter)
        {
            return base.Where(filter);
        }

        /// <summary>
        /// Sets the limit on the request
        /// </summary>
        /// <param name="limit">number of elements</param>
        /// <returns></returns>
        public BbJoin<TA, TB, TC, TD, TE, TF, TG, TH> Limit(int limit)
        {
            RecordLimit = limit;
            return this;
        }

        /// <summary>
        /// Sets the offset on the request
        /// </summary>
        /// <param name="limit">number of elements to offset</param>
        /// <returns></returns>
        public BbJoin<TA, TB, TC, TD, TE, TF, TG, TH> Offset(int offset)
        {
            RecordsToSkip = offset;
            return this;
        }
    }
}
