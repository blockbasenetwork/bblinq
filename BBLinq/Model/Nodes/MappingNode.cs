using BlockBase.BBLinq.Enumerables;
using BlockBase.BBLinq.Model.Base;

namespace BlockBase.BBLinq.Model.Nodes
{
    internal class MappingNode : BinaryExpressionNode<BlockBaseComparisonOperator, PropertyNode, PropertyNode>
    {
        public MappingNode(PropertyNode left, PropertyNode right) : base(BlockBaseComparisonOperator.EqualTo, left, right, false)
        {
        }
    }
}
