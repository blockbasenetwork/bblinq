using BlockBase.BBLinq.Enumerables;
using BlockBase.BBLinq.Model.Base;
using System.Reflection;

namespace BlockBase.BBLinq.Model.Nodes
{
    internal class JoinNode : BinaryExpressionNode<BlockBaseComparisonOperator, PropertyNode, PropertyNode>
    {
        public BlockBaseJoinEnum Type { get; set; }

        public JoinNode(PropertyNode left, PropertyNode right, BlockBaseJoinEnum type) : base(
            BlockBaseComparisonOperator.EqualTo, left, right, false)
        {
            Type = type;
        }
        public JoinNode(PropertyInfo left, PropertyInfo right, BlockBaseJoinEnum type) : this(new PropertyNode(left), new PropertyNode(right), type) { }
    }
}
