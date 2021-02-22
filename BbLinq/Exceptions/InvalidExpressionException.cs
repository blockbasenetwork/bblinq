using System;
using System.Linq.Expressions;

namespace BlockBase.BBLinq.Exceptions
{
    public class InvalidExpressionException : Exception
    {
        public Expression Expression { get; set; }
        public InvalidExpressionException(Expression e) : base($"The selected expression is not correct or it cannot by parsed to BBSQL.")
        {
            Expression = e;
        }
    }
}
