using System;
using System.Linq.Expressions;

namespace BlockBase.BBLinq.Exceptions
{
    public class UnavailableExpressionException : Exception
    {
        public Expression Expression { get; }

        public UnavailableExpressionException(Expression expression) : base($"The expression {expression} is not accepted by BBSQL")
        {
            Expression = expression;
        }
    }
}
