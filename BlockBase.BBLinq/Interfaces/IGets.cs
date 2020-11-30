using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BlockBase.BBLinq.Results;

namespace BlockBase.BBLinq.Interfaces
{
    public interface IGetsByKey<T, in TKey> where T : class
    {
        public Task<QueryResult<T>> Get(TKey key);
    }

    public interface IGets<T> where T : class
    {
        public Task<QueryResult<TB>> Get<TB>(Expression<Func<T,TB>> mapper);
    }

    public interface IGets<TA, TB> where TA : class where TB:class
    {
        public Task<QueryResult<TC>> Get<TC>(Expression<Func<TA, TB, TC>> mapper);
    }

    public interface IGets<TA, TB, TC> where TA : class where TB : class where TC:class
    {
        public Task<QueryResult<TD>> Get<TD>(Expression<Func<TA, TB, TC, TD>> mapper);
    }

    public interface IGets<TA, TB, TC, TD> where TA : class where TB : class where TC : class where TD : class
    {
        public Task<QueryResult<TE>> Get<TE>(Expression<Func<TA, TB, TC, TD, TE>> mapper);
    }

    public interface IGets<TA, TB, TC, TD, TE> where TA : class where TB : class where TC : class where TD : class where TE : class
    {
        public Task<QueryResult<TF>> Get<TF>(Expression<Func<TA, TB, TC, TD, TE, TF>> mapper);
    }

    public interface IGets<TA, TB, TC, TD, TE, TF> where TA : class where TB : class where TC : class where TD : class where TE : class where TF : class
    {
        public Task<QueryResult<TG>> Get<TG>(Expression<Func<TA, TB, TC, TD, TE, TF, TG>> mapper);
    }

    public interface IGets<TA, TB, TC, TD, TE, TF, TG> where TA : class where TB : class where TC : class where TD : class where TE : class where TF : class where TG : class
    {
        public Task<QueryResult<TH>> Get<TH>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH>> mapper);
    }

    public interface IGets<TA, TB, TC, TD, TE, TF, TG, TH> where TA : class where TB : class where TC : class where TD : class where TE : class where TF : class where TG : class where TH : class
    {
        public Task<QueryResult<TI>> Get<TI>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI>> mapper);
    }

    public interface IGets<TA, TB, TC, TD, TE, TF, TG, TH, TI> where TA : class where TB : class where TC : class where TD : class where TE : class where TF : class where TG : class where TH : class where TI : class
    {
        public Task<QueryResult<TJ>> Get<TJ>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ>> mapper);
    }
}
