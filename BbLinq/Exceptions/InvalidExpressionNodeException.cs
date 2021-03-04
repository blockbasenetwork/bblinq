using System;
using BlockBase.BBLinq.Pocos.Nodes;

namespace BlockBase.BBLinq.Exceptions
{
    public class InvalidExpressionNodeException : Exception
    {
        public ExpressionNode Node { get; set; }
        public InvalidExpressionNodeException(ExpressionNode node) : base($"The expression {node.ToString()} is incorrect")
        {
            Node = node;
        }
    }
}
