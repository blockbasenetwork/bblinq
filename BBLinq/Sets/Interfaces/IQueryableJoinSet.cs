using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BlockBase.BBLinq.Queries.Interfaces;

namespace BlockBase.BBLinq.Sets.Interfaces
{
    public interface IQueryableJoinSet<TA, TB>
    {
        public IQueryableJoinSet<TA, TB> Where(Expression<Func<TA, TB, bool>> predicate);
        
        public ISelectQuery GetSelectQuery();
        public void BatchSelect();
        public Task<IEnumerable<dynamic>> SelectAsync();

        public ISelectQuery GetSelectQuery<TRecordResult>(Expression<Func<TA, TB, TRecordResult>> mapper);
        public void BatchSelect<TRecordResult>(Expression<Func<TA, TB, TRecordResult>> mapper);
        public Task<IEnumerable<TRecordResult>> SelectAsync<TRecordResult>(Expression<Func<TA, TB, TRecordResult>> mapper);
    }

    public interface IQueryableJoinSet<TA, TB, TC> : IBlockBaseBaseSet<IQueryableJoinSet<TA, TB, TC>>
    {
        public IQueryableJoinSet<TA, TB, TC> Where(Expression<Func<TA, TB, TC, bool>> predicate);

        public ISelectQuery GetSelectQuery();
        public void BatchSelect();
        public Task<IEnumerable<dynamic>> SelectAsync();

        public ISelectQuery GetSelectQuery<TRecordResult>(Expression<Func<TA, TB, TC, TRecordResult>> mapper);
        public void BatchSelect<TRecordResult>(Expression<Func<TA, TB, TC, TRecordResult>> mapper);
        public Task<IEnumerable<TRecordResult>> SelectAsync<TRecordResult>(Expression<Func<TA, TB, TC, TRecordResult>> mapper);
    }

    public interface IQueryableJoinSet<TA, TB, TC, TD> : IBlockBaseBaseSet<IQueryableJoinSet<TA, TB, TC, TD>>
    {
        public IQueryableJoinSet<TA, TB, TC, TD> Where(Expression<Func<TA, TB, TC, TD, bool>> predicate);
        public IQueryableJoinSet<TA, TB, TC, TD> Skip(int skipNumber);
        public IQueryableJoinSet<TA, TB, TC, TD> Take(int takeNumber);

        public ISelectQuery GetSelectQuery();
        public void BatchSelect();
        public Task<IEnumerable<dynamic>> SelectAsync();

        public ISelectQuery GetSelectQuery<TRecordResult>(Expression<Func<TA, TB, TC, TD, TRecordResult>> mapper);
        public void BatchSelect<TRecordResult>(Expression<Func<TA, TB, TC, TD, TRecordResult>> mapper);
        public Task<IEnumerable<TRecordResult>> SelectAsync<TRecordResult>(Expression<Func<TA, TB, TC, TD, TRecordResult>> mapper);
    }

    public interface IQueryableJoinSet<TA, TB, TC, TD, TE> : IBlockBaseBaseSet<IQueryableJoinSet<TA, TB, TC, TD, TE>>
    {
        public IQueryableJoinSet<TA, TB, TC, TD, TE> Where(Expression<Func<TA, TB, TC, TD, TE, bool>> predicate);
        public IQueryableJoinSet<TA, TB, TC, TD, TE> Skip(int skipNumber);
        public IQueryableJoinSet<TA, TB, TC, TD, TE> Take(int takeNumber);

        public ISelectQuery GetSelectQuery();
        public void BatchSelect();
        public Task<IEnumerable<dynamic>> SelectAsync();

        public ISelectQuery GetSelectQuery<TRecordResult>(Expression<Func<TA, TB, TC, TD, TE, TRecordResult>> mapper);
        public void BatchSelect<TRecordResult>(Expression<Func<TA, TB, TC, TD, TE, TRecordResult>> mapper);
        public Task<IEnumerable<TRecordResult>> SelectAsync<TRecordResult>(Expression<Func<TA, TB, TC, TD, TE, TRecordResult>> mapper);
    }

    public interface IQueryableJoinSet<TA, TB, TC, TD, TE, TF> : IBlockBaseBaseSet<IQueryableJoinSet<TA, TB, TC, TD, TE, TF>>
    {
        public IQueryableJoinSet<TA, TB, TC, TD, TE, TF> Where(Expression<Func<TA, TB, TC, TD, TE, TF, bool>> predicate);
        public IQueryableJoinSet<TA, TB, TC, TD, TE, TF> Skip(int skipNumber);
        public IQueryableJoinSet<TA, TB, TC, TD, TE, TF> Take(int takeNumber);

        public ISelectQuery GetSelectQuery();
        public void BatchSelect();
        public Task<IEnumerable<dynamic>> SelectAsync();

        public ISelectQuery GetSelectQuery<TRecordResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TRecordResult>> mapper);
        public void BatchSelect<TRecordResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TRecordResult>> mapper);
        public Task<IEnumerable<TRecordResult>> SelectAsync<TRecordResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TRecordResult>> mapper);
    }

    public interface IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG> : IBlockBaseBaseSet<IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG>>
    {
        public IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG> Where(Expression<Func<TA, TB, TC, TD, TE, TF, TG, bool>> predicate);
        public IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG> Skip(int skipNumber);
        public IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG> Take(int takeNumber);

        public ISelectQuery GetSelectQuery();
        public void BatchSelect();
        public Task<IEnumerable<dynamic>> SelectAsync();

        public ISelectQuery GetSelectQuery<TRecordResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TRecordResult>> mapper);
        public void BatchSelect<TRecordResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TRecordResult>> mapper);
        public Task<IEnumerable<TRecordResult>> SelectAsync<TRecordResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TRecordResult>> mapper);
    }
    public interface IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH> : IBlockBaseBaseSet<IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH>>
    {
        public IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH> Where(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, bool>> predicate);
        public IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH> Skip(int skipNumber);
        public IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH> Take(int takeNumber);

        public ISelectQuery GetSelectQuery();
        public void BatchSelect();
        public Task<IEnumerable<dynamic>> SelectAsync();

        public ISelectQuery GetSelectQuery<TRecordResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TRecordResult>> mapper);
        public void BatchSelect<TRecordResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TRecordResult>> mapper);
        public Task<IEnumerable<TRecordResult>> SelectAsync<TRecordResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TRecordResult>> mapper);
    }

    public interface IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI> : IBlockBaseBaseSet<IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI>>
    {
        public IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI> Where(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, bool>> predicate);
        public IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI> Skip(int skipNumber);
        public IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI> Take(int takeNumber);

        public ISelectQuery GetSelectQuery();
        public void BatchSelect();
        public Task<IEnumerable<dynamic>> SelectAsync();

        public ISelectQuery GetSelectQuery<TRecordResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, TRecordResult>> mapper);
        public void BatchSelect<TRecordResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, TRecordResult>> mapper);
        public Task<IEnumerable<TRecordResult>> SelectAsync<TRecordResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, TRecordResult>> mapper);
    }
    public interface IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ> : IBlockBaseBaseSet<IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ>>
    {
        public IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ> Where(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, bool>> predicate);
        public IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ> Skip(int skipNumber);
        public IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ> Take(int takeNumber);

        public ISelectQuery GetSelectQuery();
        public void BatchSelect();
        public Task<IEnumerable<dynamic>> SelectAsync();

        public ISelectQuery GetSelectQuery<TRecordResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TRecordResult>> mapper);
        public void BatchSelect<TRecordResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TRecordResult>> mapper);
        public Task<IEnumerable<TRecordResult>> SelectAsync<TRecordResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TRecordResult>> mapper);
    }
    public interface IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK> : IBlockBaseBaseSet<IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK>>
    {
        public IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK> Where(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, bool>> predicate);
        public IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK> Skip(int skipNumber);
        public IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK> Take(int takeNumber);

        public ISelectQuery GetSelectQuery();
        public void BatchSelect();
        public Task<IEnumerable<dynamic>> SelectAsync();

        public ISelectQuery GetSelectQuery<TRecordResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TRecordResult>> mapper);
        public void BatchSelect<TRecordResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TRecordResult>> mapper);
        public Task<IEnumerable<TRecordResult>> SelectAsync<TRecordResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TRecordResult>> mapper);
    }
    public interface IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL> : IBlockBaseBaseSet<IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL>>
    {
        public IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL> Where(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, bool>> predicate);
        public IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL> Skip(int skipNumber);
        public IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL> Take(int takeNumber);

        public ISelectQuery GetSelectQuery();
        public void BatchSelect();
        public Task<IEnumerable<dynamic>> SelectAsync();

        public ISelectQuery GetSelectQuery<TRecordResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TRecordResult>> mapper);
        public void BatchSelect<TRecordResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TRecordResult>> mapper);
        public Task<IEnumerable<TRecordResult>> SelectAsync<TRecordResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TRecordResult>> mapper);
    }
    public interface IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM> : IBlockBaseBaseSet<IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM>>
    {
        public IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM> Where(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, bool>> predicate);
        public IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM> Skip(int skipNumber);
        public IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM> Take(int takeNumber);

        public ISelectQuery GetSelectQuery();
        public void BatchSelect();
        public Task<IEnumerable<dynamic>> SelectAsync();

        public ISelectQuery GetSelectQuery<TRecordResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TRecordResult>> mapper);
        public void BatchSelect<TRecordResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TRecordResult>> mapper);
        public Task<IEnumerable<TRecordResult>> SelectAsync<TRecordResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TRecordResult>> mapper);
    }
    public interface IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TN> : IBlockBaseBaseSet<IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TN>>
    {
        public IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TN> Where(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TN, bool>> predicate);
        public IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TN> Skip(int skipNumber);
        public IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TN> Take(int takeNumber);

        public ISelectQuery GetSelectQuery();
        public void BatchSelect();
        public Task<IEnumerable<dynamic>> SelectAsync();

        public ISelectQuery GetSelectQuery<TRecordResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TN, TRecordResult>> mapper);
        public void BatchSelect<TRecordResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TN, TRecordResult>> mapper);
        public Task<IEnumerable<TRecordResult>> SelectAsync<TRecordResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TN, TRecordResult>> mapper);
    }
    public interface IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TN, TO> : IBlockBaseBaseSet<IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TN, TO>>
    {
        public IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TN, TO> Where(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TN, TO, bool>> predicate);
        public IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TN, TO> Skip(int skipNumber);
        public IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TN, TO> Take(int takeNumber);

        public ISelectQuery GetSelectQuery();
        public void BatchSelect();
        public Task<IEnumerable<dynamic>> SelectAsync();

        public ISelectQuery GetSelectQuery<TRecordResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TN, TO, TRecordResult>> mapper);
        public void BatchSelect<TRecordResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TN, TO, TRecordResult>> mapper);
        public Task<IEnumerable<TRecordResult>> SelectAsync<TRecordResult>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TN, TO, TRecordResult>> mapper);
    }

}
