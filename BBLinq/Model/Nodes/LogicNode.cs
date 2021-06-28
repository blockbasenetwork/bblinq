using BlockBase.BBLinq.Enumerables;
using BlockBase.BBLinq.Model.Base;

namespace BlockBase.BBLinq.Model.Nodes
{
    internal class LogicNode : BinaryExpressionNode<BlockBaseLogicOperator, ExpressionNode, ExpressionNode>
    {
        public LogicNode(BlockBaseLogicOperator @operator, LogicNode left, LogicNode right, bool isWrapped = false) : base(@operator, left, right, isWrapped) { }
        public LogicNode(BlockBaseLogicOperator @operator, ComparisonNode left, LogicNode right, bool isWrapped = false) : base(@operator, left, right, isWrapped) { }
        public LogicNode(BlockBaseLogicOperator @operator, LogicNode left, ComparisonNode right, bool isWrapped = false) : base(@operator, left, right, isWrapped) { }
        public LogicNode(BlockBaseLogicOperator @operator, ComparisonNode left, ComparisonNode right, bool isWrapped = false) : base(@operator, left, right, isWrapped) { }
    }
}
