using System;
using System.Collections.Generic;
using System.Text;
using BlockBase.BBLinq.Enumerables;

namespace BlockBase.BBLinq.Pocos.Nodes
{
    public abstract class BinaryNode : ExpressionNode
    {
        public BlockBaseOperator Operator { get; }
        public ExpressionNode RightNode { get; }
        public ExpressionNode LeftNode { get; }

        protected BinaryNode(BlockBaseOperator @operator, ExpressionNode leftNode, ExpressionNode rightNode)
        {
            Operator = @operator;
            LeftNode = leftNode;
            RightNode = rightNode;
        }
    }
}
