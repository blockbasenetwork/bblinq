using BlockBase.BBLinq.Pocos.ExpressionParser;
using System;

namespace BlockBase.BBLinq.Exceptions
{
    public class NonBinaryExpressionException : Exception
    {
        public ExpressionNode Node { get; set; }
        public NonBinaryExpressionException(ExpressionNode node) : base("The expression should be binary")
        {
            Node = node;
        }
    }
}
