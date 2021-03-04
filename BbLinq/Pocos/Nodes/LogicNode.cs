using BlockBase.BBLinq.Enumerables;

namespace BlockBase.BBLinq.Pocos.Nodes
{
    public class LogicNode : BinaryNode
    {
        public LogicNode(BlockBaseOperator @operator, ExpressionNode leftNode, ExpressionNode rightNode) : base(@operator, leftNode, rightNode) { }
    }
}
