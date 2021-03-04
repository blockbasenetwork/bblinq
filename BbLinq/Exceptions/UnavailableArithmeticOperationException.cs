using System;
using System.Linq.Expressions;
using BlockBase.BBLinq.Pocos.Nodes;

namespace BlockBase.BBLinq.Exceptions
{
    public class UnavailableArithmeticOperationException : Exception
    {
        public Expression Node { get; set; }
        public UnavailableArithmeticOperationException(Expression node) : base($"The following arithmetic expression {node.ToString()} is incorrect")
        {
            Node = node;
        }
    }
}
