using System.Linq.Expressions;

namespace BlockBase.BBLinq.ExtensionMethods
{
    public static class ExpressionExtensionMethods
    {
        /// <summary>
        /// Checks if the expression is an operator
        /// </summary>
        /// <param name="expression">The expression</param>
        /// <returns>true if it is an operator. false otherwise</returns>
        public static bool IsOperator(this Expression expression)
        {
            var operators = new[]
            {
                ExpressionType.Add,
                ExpressionType.Subtract,
                ExpressionType.Multiply,
                ExpressionType.Divide
            };
            foreach (var @operator in operators)
            {
                if (expression.NodeType == @operator)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Checks if the expression is a constant member access
        /// </summary>
        /// <param name="expression">The expression</param>
        /// <returns>true if it is a constant member access. false otherwise</returns>
        public static bool IsConstantMemberAccess(this MemberExpression expression)
        {
            var innerExpression = expression.Expression;
            return innerExpression != null && innerExpression.NodeType == ExpressionType.Constant;
        }

        /// <summary>
        /// Checks if the expression is a property member access
        /// </summary>
        /// <param name="expression">The expression</param>
        /// <returns>true if it is a property member access. false otherwise</returns>
        public static bool IsPropertyMemberAccess(this MemberExpression expression)
        {
            var innerExpression = expression.Expression;
            return innerExpression != null && innerExpression.NodeType == ExpressionType.Parameter;
        }
    }
}
