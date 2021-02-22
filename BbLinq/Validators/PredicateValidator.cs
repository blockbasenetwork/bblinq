using BlockBase.BBLinq.Exceptions;
using BlockBase.BBLinq.Pocos.ExpressionParser;

namespace BlockBase.BBLinq.Validators
{
    public static class PredicateValidator
    {
        public static void Validate(ExpressionNode expression)
        {
            if (expression is ValueExpression)
            {
                throw new NonBinaryExpressionException(expression);
            }

            if (expression is BinaryExpressionNode binaryExpressionNode)
            {
                ValidateBinaryNode(binaryExpressionNode);
            }
            else
            {
                throw new InvalidExpressionNodeException(expression);
            }

        }

        public static void ValidateBinaryNode(BinaryExpressionNode node)
        {
            var left = node.Left;
            var right = node.Right;
            if (left == null || right == null)
            {
                throw new InvalidExpressionNodeException(node);
            }

            if (left is ValueExpression && right is ValueExpression)
            {
                throw new NoPropertyToCompareException(node);
            }

            if (left is BinaryExpressionNode leftBinaryNode)
            {
                ValidateBinaryNode(leftBinaryNode);
            }

            if (right is BinaryExpressionNode rightBinaryNode)
            {
                ValidateBinaryNode(rightBinaryNode);
            }
        }

    }
}
