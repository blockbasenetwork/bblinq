using BlockBase.BBLinq.Enumerables;
using BlockBase.BBLinq.Model.Base;

namespace BlockBase.BBLinq.Model.Nodes
{
    internal class LogicNode : BinaryExpressionNode<BlockBaseLogicOperator, ExpressionNode, ExpressionNode>
    {
        public LogicNode(BlockBaseLogicOperator @operator, LogicNode left, LogicNode right) : base(@operator, left, right) { }
        public LogicNode(BlockBaseLogicOperator @operator, ComparisonNode left, LogicNode right) : base(@operator, left, right) { }
        public LogicNode(BlockBaseLogicOperator @operator, LogicNode left, ComparisonNode right) : base(@operator, left, right) { }
        public LogicNode(BlockBaseLogicOperator @operator, ComparisonNode left, ComparisonNode right) : base(@operator, left, right) { }
    }
}
