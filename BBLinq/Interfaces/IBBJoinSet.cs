using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace agap2IT.Labs.BlockBase.BBLinq.Interfaces
{
    public interface IBbJoinSet<TA, TB>
    {
        public Task<IEnumerable<TC>> SelectAsync<TC>(Expression<Func<TA, TB, TC>> mapper);
        public IBbJoinSet<TA, TB> Where(Expression<Func<TA, TB, bool>> filter);
        public IBbJoinSet<TA, TB, TC> Join<TC>(Expression<Func<TA, TB, TC, bool>> on);
    }

    public interface IBbJoinSet<TA, TB, TC>
    {
        public Task<IEnumerable<TD>> SelectAsync<TD>(Expression<Func<TA, TB, TC, TD>> mapper);
        public IBbJoinSet<TA, TB, TC> Where(Expression<Func<TA, TB, TC, bool>> filter);
        public IBbJoinSet<TA, TB, TC, TD> Join<TD>(Expression<Func<TA, TB, TC, TD, bool>> on);
    }

    public interface IBbJoinSet<TA, TB, TC, TD>
    {
        public Task<IEnumerable<TE>> SelectAsync<TE>(Expression<Func<TA, TB, TC, TD, TE>> mapper);
        public IBbJoinSet<TA, TB, TC, TD> Where(Expression<Func<TA, TB, TC, TD, bool>> filter);
        public IBbJoinSet<TA, TB, TC, TD, TE> Join<TE>(Expression<Func<TA, TB, TC, TD, TE, bool>> on);
    }

    public interface IBbJoinSet<TA, TB, TC, TD, TE>
    {
        public Task<IEnumerable<TF>> SelectAsync<TF>(Expression<Func<TA, TB, TC, TD, TE, TF>> mapper);
        public IBbJoinSet<TA, TB, TC, TD, TE> Where(Expression<Func<TA, TB, TC, TD, TE, bool>> filter);
        public IBbJoinSet<TA, TB, TC, TD, TE, TF> Join<TF>(Expression<Func<TA, TB, TC, TD, TE, TF, bool>> on);
    }

    public interface IBbJoinSet<TA, TB, TC, TD, TE, TF>
    {
        public Task<IEnumerable<TG>> SelectAsync<TG>(Expression<Func<TA, TB, TC, TD, TE, TF, TG>> mapper);
        public IBbJoinSet<TA, TB, TC, TD, TE, TF> Where(Expression<Func<TA, TB, TC, TD, TE, TF, bool>> filter);
        public IBbJoinSet<TA, TB, TC, TD, TE, TF, TG> Join<TG>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, bool>> on);
    }

    public interface IBbJoinSet<TA, TB, TC, TD, TE, TF, TG>
    {
        public Task<IEnumerable<TH>> SelectAsync<TH>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH>> mapper);
        public IBbJoinSet<TA, TB, TC, TD, TE, TF, TG> Where(Expression<Func<TA, TB, TC, TD, TE, TF, TG, bool>> filter);
    }
}
