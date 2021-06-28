using System;
using System.Linq.Expressions;

namespace BlockBase.BBLinq.Exceptions
{
    public class UnsupportedExpressionOperatorException : Exception
    {
        public UnsupportedExpressionOperatorException(ExpressionType e) : base($"The expression operator {e} is not supported") { }
    }
}
