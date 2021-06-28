using System;
using System.Linq.Expressions;

namespace BlockBase.BBLinq.Exceptions
{
    public class UnsupportedExpressionException : Exception
    {
        public UnsupportedExpressionException(Expression e) : base($"The expression {e} is not supported") { }
    }
}
