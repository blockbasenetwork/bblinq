using System.Reflection;
using BlockBase.BBLinq.Enumerables;

namespace BlockBase.BBLinq.Pocos.Nodes
{
    public class JoinNode : ComparisonNode
    {
        public JoinNode(PropertyInfo left, PropertyInfo right) : this(new PropertyNode(left), new PropertyNode(right))
        {

        }


        public JoinNode( PropertyNode leftNode, PropertyNode rightNode) : base(BlockBaseOperator.EqualTo, leftNode, rightNode)
        {

        }
    }
}
