using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BlockBase.BBLinq.Results;

namespace BlockBase.BBLinq.Interfaces
{
    public interface ILists<T>
    {
        public Task<QueryResult<IEnumerable<T>>> List();
        public Task<QueryResult<IEnumerable<TB>>> List<TB>(Expression<Func<T, TB>> mapper);
    }
    public interface ILists<TA, TB>
    {
        public Task<QueryResult<IEnumerable<TC>>> List<TC>(Expression<Func<TA, TB, TC>> mapper);
    }

    public interface ILists<TA, TB, TC>
    {
        public Task<QueryResult<IEnumerable<TD>>> List<TD>(Expression<Func<TA, TB, TC, TD>> mapper);
    }

    public interface ILists<TA, TB, TC, TD>
    {
        public Task<QueryResult<IEnumerable<TE>>> List<TE>(Expression<Func<TA, TB, TC, TD, TE>> mapper);
    }

    public interface ILists<TA, TB, TC, TD, TE>
    {
        public Task<QueryResult<IEnumerable<TF>>> List<TF>(Expression<Func<TA, TB, TC, TD, TE, TF>> mapper);
    }

    public interface ILists<TA, TB, TC, TD, TE, TF>
    {
        public Task<QueryResult<IEnumerable<TG>>> List<TG>(Expression<Func<TA, TB, TC, TD, TE, TF, TG>> mapper);
    }

    public interface ILists<TA, TB, TC, TD, TE, TF, TG>
    {
        public Task<QueryResult<IEnumerable<TH>>> List<TH>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH>> mapper);
    }

    public interface ILists<TA, TB, TC, TD, TE, TF, TG, TH>
    {
        public Task<QueryResult<IEnumerable<TI>>> List<TI>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI>> mapper);
    }

    public interface ILists<TA, TB, TC, TD, TE, TF, TG, TH, TI>
    {
        public Task<QueryResult<IEnumerable<TJ>>> List<TJ>(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ>> mapper);
    }
}
