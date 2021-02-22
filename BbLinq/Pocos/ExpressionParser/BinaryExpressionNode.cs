
namespace BlockBase.BBLinq.Pocos.ExpressionParser
{
    public class BinaryExpressionNode : ExpressionNode
    {
        public ExpressionNode Left { get; set; }
        public ExpressionOperator Operator { get; set; }
        public ExpressionNode Right { get; set; }
    }
}
