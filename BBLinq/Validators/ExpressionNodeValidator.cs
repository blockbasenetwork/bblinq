using BlockBase.BBLinq.Exceptions;
using BlockBase.BBLinq.Model.Nodes;

namespace BlockBase.BBLinq.Validators
{
    internal static class ExpressionNodeValidator
    {
        public static void ValidateComparisonNode(ComparisonNode node)
        {
            if (node.Left is PropertyNode leftProperty)
            {
                if (node.Right is ValueNode rightValue)
                {
                    if ((leftProperty.Property.PropertyType == rightValue.Value.GetType()) || (leftProperty.Property.PropertyType.IsEnum && rightValue.Value.GetType() == typeof(int)))
                    {
                        return;
                    }
                }
                else if (node.Right is PropertyNode rightProperty)
                {
                    if (leftProperty.Property.PropertyType == rightProperty.Property.PropertyType)
                    {
                        return;
                    }
                }
            }
            throw new InvalidComparisonExpressionException(node.Operator.ToString(), node.Left, node.Right);
        }

        public static void ValidateLogicNode(LogicNode node)
        {
            switch (node.Left)
            {
                case ComparisonNode comparisonLeft:
                    ValidateComparisonNode(comparisonLeft);
                    return;
                case LogicNode logicLeft:
                    ValidateLogicNode(logicLeft);
                    return;
            }
            switch (node.Right)
            {
                case ComparisonNode comparisonLeft:
                    ValidateComparisonNode(comparisonLeft);
                    return;
                case LogicNode logicLeft:
                    ValidateLogicNode(logicLeft);
                    return;
            }

            throw new InvalidLogicExpressionException(node);
        }
    }
}
