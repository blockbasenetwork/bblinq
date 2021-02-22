using BlockBase.BBLinq.Pocos.ExpressionParser;
using System;

namespace BlockBase.BBLinq.Exceptions
{
    public class NoPropertyToCompareException : Exception
    {
        public ExpressionNode Node { get; set; }
        public NoPropertyToCompareException(ExpressionNode node) : base("The expression has both sides as value")
        {
            Node = node;
        }
    }
}
