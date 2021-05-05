using BlockBase.BBLinq.Enumerables;
using BlockBase.BBLinq.Model.Base;

namespace BlockBase.BBLinq.Model.Nodes
{
    internal class ArithmeticOperationExpressionNode : BinaryExpressionNode<BlockBaseArithmeticOperator, ValueNode, ValueNode>
    {
        public ArithmeticOperationExpressionNode(BlockBaseArithmeticOperator @operator, ValueNode left, ValueNode right):base(@operator, left, right) { }
        public ArithmeticOperationExpressionNode(BlockBaseArithmeticOperator @operator, object left, object right):base(@operator, new ValueNode(left), new ValueNode(right)) { }
    }
}
