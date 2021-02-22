using BlockBase.BBLinq.Pocos.ExpressionParser;
using System;

namespace BlockBase.BBLinq.Exceptions
{
    public class InvalidExpressionNodeException : Exception
    {
        public ExpressionNode Node { get; set; }
        public InvalidExpressionNodeException(ExpressionNode node) : base("The expression is incorrect")
        {
            Node = node;
        }
    }
}
