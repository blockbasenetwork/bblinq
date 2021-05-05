using BlockBase.BBLinq.Enumerables;
using BlockBase.BBLinq.Model.Base;

namespace BlockBase.BBLinq.Model.Nodes
{
    internal class ComparisonNode : BinaryExpressionNode<BlockBaseComparisonOperator, ExpressionNode, ExpressionNode>
    {
        public ComparisonNode(BlockBaseComparisonOperator @operator, PropertyNode leftNode, ValueNode rightNode)
            : base(@operator, leftNode, rightNode) { }

        public ComparisonNode(BlockBaseComparisonOperator @operator, PropertyNode leftNode, PropertyNode rightNode)
            : base(@operator, leftNode, rightNode) { }
    }
}
