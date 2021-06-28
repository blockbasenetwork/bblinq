using BlockBase.BBLinq.Enumerables;
using BlockBase.BBLinq.Model.Base;

namespace BlockBase.BBLinq.Model.Nodes
{
    internal class ArithmeticOperationExpressionNode : BinaryExpressionNode<BlockBaseArithmeticOperator, ValueNode, ValueNode>
    {
        public ArithmeticOperationExpressionNode(BlockBaseArithmeticOperator @operator, ValueNode left, ValueNode right, bool isWrapped = false) : base(@operator, left, right, isWrapped) { }
        public ArithmeticOperationExpressionNode(BlockBaseArithmeticOperator @operator, object left, object right, bool isWrapped = false) : base(@operator, new ValueNode(left), new ValueNode(right), isWrapped) { }
    }
}
