namespace BlockBase.BBLinq.Model.Base
{
    internal class BinaryExpressionNode<TOperator, TLeft, TRight> : ExpressionNode
    {
        public bool IsWrapped { get; set; }
        public TOperator Operator { get; set; }
        public TLeft Left { get; set; }
        public TRight Right { get; set; }

        public BinaryExpressionNode(TOperator @operator, TLeft left, TRight right, bool isWrapped)
        {
            Operator = @operator;
            Left = left;
            Right = right;
            IsWrapped = isWrapped;
        }
    }
}
