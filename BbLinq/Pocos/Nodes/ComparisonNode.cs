using BlockBase.BBLinq.Enumerables;

namespace BlockBase.BBLinq.Pocos.Nodes
{
    public class ComparisonNode : BinaryNode
    {
        public ComparisonNode(BlockBaseOperator @operator, ExpressionNode leftNode, ExpressionNode rightNode)
            : base(@operator, leftNode, rightNode) { }
    }
}
