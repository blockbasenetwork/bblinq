namespace BlockBase.BBLinq.Model.Base
{
    internal class BinaryExpressionNode<TOperator, TLeft, TRight> :ExpressionNode
    {
        public TOperator Operator { get; set; }
        public TLeft Left { get; set; }
        public TRight Right { get; set; }

        public BinaryExpressionNode(TOperator @operator, TLeft left, TRight right)
        {
            Operator = @operator;
            Left = left;
            Right = right;
        }
    }
}
