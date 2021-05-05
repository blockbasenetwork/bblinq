using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BlockBase.BBLinq.Exceptions
{
    public class UnsupportedExpressionException : Exception
    {
        public UnsupportedExpressionException(Expression e):base($"The expression {e} is not supported") { }
    }
}
