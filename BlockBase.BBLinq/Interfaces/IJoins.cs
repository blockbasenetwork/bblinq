using System;
using System.Linq.Expressions;

namespace BlockBase.BBLinq.Interfaces
{
    public interface IJoins<TA>
    {
        public TResult Join<TB, TResult>() where TResult: IJoins<TA, TB>;
        public TResult Join<TB, TResult>(Expression<Func<TA,TB>> on) where TResult : IJoins<TA, TB>;
    }

    public interface IJoins<TA,TB>
    {
        public IJoins<TA,TB,TC> Join<TC>();
        public IJoins<TA, TB, TC> Join<TC>(LambdaExpression on);
    }

    public interface IJoins<TA, TB, TC>
    {
        public IJoins<TA, TB, TC, TD> Join<TD>();
        public IJoins<TA, TB, TC, TD> Join<TD>(LambdaExpression on);
    }

    public interface IJoins<TA, TB, TC, TD>
    {
        public IJoins<TA, TB, TC, TD, TE> Join<TE>();
        public IJoins<TA, TB, TC, TD, TE> Join<TE>(LambdaExpression on);
    }

    public interface IJoins<TA, TB, TC, TD, TE>
    {
        public IJoins<TA, TB, TC, TD, TE, TF> Join<TF>();
        public IJoins<TA, TB, TC, TD, TE, TF> Join<TF>(LambdaExpression on);
    }

    public interface IJoins<TA, TB, TC, TD, TE, TF>
    {
        public IJoin<TA, TB, TC, TD, TE, TF, TG> Join<TG>();
        public IJoin<TA, TB, TC, TD, TE, TF, TG> Join<TG>(LambdaExpression on);
    }

    public interface IJoin<TA, TB, TC, TD, TE, TF, TG>
    {
    }
}
