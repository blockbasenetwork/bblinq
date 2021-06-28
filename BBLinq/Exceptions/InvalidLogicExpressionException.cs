using BlockBase.BBLinq.Model.Nodes;
using System;

namespace BlockBase.BBLinq.Exceptions
{
    public class InvalidLogicExpressionException : Exception
    {
        private static string GenerateMessage(LogicNode node)
        {
            string leftValue = string.Empty;
            string rightValue = string.Empty;
            if (node.Left == null)
                leftValue = "null";
            else if (!(node.Left is LogicNode || node.Left is PropertyNode))
                leftValue = node.Left.GetType().ToString();

            if (node.Right == null)
                rightValue = "null";
            else if (!(node.Left is LogicNode || node.Right is PropertyNode))
                rightValue = node.Right.GetType().ToString();

            return $"The logic operation {node.Operator} between {leftValue} and {rightValue} is not valid!";
        }

        internal InvalidLogicExpressionException(LogicNode node) :
            base(GenerateMessage(node))
        { }
    }
}
