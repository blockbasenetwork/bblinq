using BlockBase.BBLinq.Enumerables;
using BlockBase.BBLinq.Model.Base;

namespace BlockBase.BBLinq.Model.Nodes
{
    internal class ComparisonNode : BinaryExpressionNode<BlockBaseComparisonOperator, ExpressionNode, ExpressionNode>
    {
        public ComparisonNode(BlockBaseComparisonOperator @operator, PropertyNode leftNode, ValueNode rightNode,
            bool isWrapped = false)
            : base(@operator, leftNode, rightNode, isWrapped)
        {
        }

        public ComparisonNode(BlockBaseComparisonOperator @operator, PropertyNode leftNode, PropertyNode rightNode,
            bool isWrapped = false)
            : base(@operator, leftNode, rightNode, isWrapped)
        {
        }
    }
}
