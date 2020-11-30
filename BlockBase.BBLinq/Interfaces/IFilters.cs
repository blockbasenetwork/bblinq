using System;
using System.Linq.Expressions;

namespace BlockBase.BBLinq.Interfaces
{
    public interface IFilters<T, out TResult>
    {
        public TResult Where(Expression<Func<T, bool>> predicate);
    }

    public interface IFilterable<TA, TB, out TResult>
    {
        public TResult Where(Expression<Func<TA, TB, bool>> predicate);
    }

    public interface IFilterable<TA, TB, TC, out TResult>
    {
        public TResult Where(Expression<Func<TA, TB, TC, bool>> predicate);
    }

    public interface IFilterable<TA, TB, TC, TD, out TResult>
    {
        public TResult Where(Expression<Func<TA, TB, TC, TD, bool>> predicate);
    }

    public interface IFilterable<TA, TB, TC, TD, TE, out TResult>
    {
        public TResult Where(Expression<Func<TA, TB, TC, TD, TE, bool>> predicate);
    }

    public interface IFilterable<TA, TB, TC, TD, TE, TF, out TResult>
    {
        public TResult Where(Expression<Func<TA, TB, TC, TD, TE, TF, bool>> predicate);
    }

    public interface IFilterable<TA, TB, TC, TD, TE, TF, TG, out TResult>
    {
        public TResult Where(Expression<Func<TA, TB, TC, TD, TE, TF, TG, bool>> predicate);
    }

    public interface IFilterable<TA, TB, TC, TD, TE, TF, TG, TH, out TResult>
    {
        public TResult Where(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, bool>> predicate);
    }

    public interface IFilterable<TA, TB, TC, TD, TE, TF, TG, TH, TI, out TResult>
    {
        public TResult Where(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, bool>> predicate);
    }

    public interface IFilterable<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, out TResult>
    {
        public TResult Where(Expression<Func<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, bool>> predicate);
    }
}
