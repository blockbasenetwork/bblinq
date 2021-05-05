using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BlockBase.BBLinq.Exceptions
{
    public class UnsupportedExpressionOperatorException : Exception
    {
        public UnsupportedExpressionOperatorException(ExpressionType e):base($"The expression operator {e} is not supported") { }
    }
}
