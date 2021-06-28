using BlockBase.BBLinq.Model.Base;
using BlockBase.BBLinq.Model.Nodes;
using System;

namespace BlockBase.BBLinq.Exceptions
{
    public class InvalidComparisonExpressionException : Exception
    {
        private static string GenerateMessage(string @operator, ExpressionNode left,
            ExpressionNode right)
        {
            var leftValue = left switch
            {
                ValueNode leftValueNode => leftValueNode.Value.ToString(),
                PropertyNode leftPropertyNode => leftPropertyNode.Property.Name,
                _ => left.GetType().ToString()
            };

            var rightValue = right switch
            {
                ValueNode rightValueNode => rightValueNode.Value.ToString(),
                PropertyNode rightPropertyNode => rightPropertyNode.Property.Name,
                _ => right.GetType().ToString()
            };

            return $"The comparison operation {@operator} between {leftValue} and {rightValue} is not valid!";
        }

        internal InvalidComparisonExpressionException(string @operator, ExpressionNode left, ExpressionNode right) :
            base(GenerateMessage(@operator, left, right))
        { }
    }
}
