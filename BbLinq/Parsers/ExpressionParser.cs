using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using BlockBase.BBLinq.Enumerables;
using BlockBase.BBLinq.Exceptions;
using BlockBase.BBLinq.ExtensionMethods;
using BlockBase.BBLinq.Pocos.Nodes;

namespace BlockBase.BBLinq.Parsers
{
    public class ExpressionParser
    {
        private IReadOnlyCollection<ParameterExpression> _parameters;
        #region Parse Where
        public ExpressionNode ParseExpression(Expression expression)
        {
            if (!IsAcceptableOperation(expression))
            {
                throw new UnavailableExpressionException(expression);
            }
            if (expression.NodeType == ExpressionType.ArrayIndex && expression is BinaryExpression binaryArrayExpression)
            {
                var arrayNode = ParseMemberExpression(binaryArrayExpression.Left as MemberExpression);
                var indexNode = ParseExpression(binaryArrayExpression.Right);
                if (arrayNode is ValueNode {Value: Array array} && 
                    indexNode is ValueNode {Value: int index})
                {
                    return new ValueNode(array.GetValue(index));
                }
            }

            switch (expression)
            {
                case MethodCallExpression methodCallExpression:
                    return ParseCallExpression(methodCallExpression);
                case LambdaExpression lambdaExpression:
                    _parameters = lambdaExpression.Parameters;
                    return ParseExpression(lambdaExpression.Body);
                case BinaryExpression binaryExpression:
                    return ParseBinaryExpression(binaryExpression);
                case UnaryExpression unaryExpression:
                    return ParseUnaryExpression(unaryExpression);
                case ConstantExpression constantExpression:
                    return new ValueNode(constantExpression.Value);
                case MemberExpression memberExpression:
                    return ParseMemberExpression(memberExpression);
            }
            throw new UnavailableExpressionException(expression);
        }

        private ExpressionNode ParseCallExpression(MethodCallExpression methodCallExpression)
        {
            var method = methodCallExpression.Method;
            var objectExpression = methodCallExpression.Object;
            switch (objectExpression)
            {
                case MemberExpression memberObjectExpression:
                    var valueNode = GetValue(memberObjectExpression);
                    if (valueNode == null)
                    {
                        throw new UnavailableExpressionException(methodCallExpression);
                    }
                    var res = method.Invoke(valueNode, new object[] { });
                    return new ValueNode(res);
                case ConstantExpression constantObjectExpression:
                    var result = method.Invoke(constantObjectExpression.Value, new object[] { });
                    return new ValueNode(result);
            }
            throw new UnavailableExpressionException(methodCallExpression);
        }

        private ExpressionNode ParseMemberExpression(MemberExpression memberExpression)
        {
            if (_parameters.Contains(memberExpression.Expression))
            {
                return new PropertyNode(memberExpression.Member as PropertyInfo);
            }
            var value = GetValue(memberExpression);
            return new ValueNode(value);
        }

        private object GetValue(MemberExpression expression)
        {
            switch (expression.Expression)
            {
                case ConstantExpression constantExpression:
                    return GetValue(constantExpression.Value, expression.Member);
                case MemberExpression innerMemberExpression:
                {
                    var value = GetValue(innerMemberExpression);
                    if (value != null)
                    {
                        return GetValue(value, expression.Member);
                    }
                    break;
                }
            }
            return null;
        }

        private object GetValue(object origin, object accessor)
        {
            return accessor switch
            {
                PropertyInfo property when property != null => property.GetValue(origin),
                FieldInfo field when field != null => field.GetValue(origin),
                _ => null
            };
        }

        private ExpressionNode ParseBinaryExpression(BinaryExpression binaryExpression)
        {
            var @operator = ParseExpressionOperator(binaryExpression.NodeType);
            var left = ParseExpression(binaryExpression.Left);
            var right = ParseExpression(binaryExpression.Right);
            if (IsArithmeticOperator(binaryExpression))
            {
                throw new UnavailableArithmeticOperationException(binaryExpression);
            }
            if (IsComparisonOperator(binaryExpression))
            {
                return new ComparisonNode(@operator, left, right);
            }
            if (IsLogicOperator(binaryExpression))
            {
                return new LogicNode(@operator, left, right);
            }
            throw new UnavailableExpressionException(binaryExpression);
        }

        private ExpressionNode ParseUnaryExpression(UnaryExpression unaryExpression)
        {
            var operand = unaryExpression.Operand;
            var expressionValue = ParseMemberExpression(operand as MemberExpression);
            if (expressionValue is PropertyNode propertyNode)
            {
                switch (unaryExpression.NodeType)
                {
                    case ExpressionType.Not:
                        return new ComparisonNode(BlockBaseOperator.DifferentFrom, propertyNode, new ValueNode(true));
                }
            }
            if (expressionValue is ValueNode valueNode)
            {
                switch (unaryExpression.NodeType)
                {
                    case ExpressionType.ArrayLength when valueNode.Value is Array array:
                        return new ValueNode(array.Length);
                    case ExpressionType.Convert:
                    case ExpressionType.ConvertChecked:
                        return new ValueNode(Convert.ChangeType(valueNode.Value, unaryExpression.Type));
                    case ExpressionType.Negate:
                    case ExpressionType.NegateChecked:
                        var result = valueNode.Value.ToString();
                        if (result != null)
                        {
                            return new ValueNode(-1 * double.Parse(result));
                        }
                        break;
                    case ExpressionType.Not:
                        
                        break;
                    case ExpressionType.UnaryPlus:
                        return new ValueNode(valueNode.Value);
                }
            }
            throw new UnavailableExpressionException(unaryExpression);
        }

        private bool IsAcceptableOperation(Expression expression)
        {
            var @operator = expression.NodeType;
            return
                @operator == ExpressionType.Call ||
                @operator == ExpressionType.Convert ||
                @operator == ExpressionType.ConvertChecked ||
                @operator == ExpressionType.ArrayLength ||
                @operator == ExpressionType.ArrayIndex ||
                @operator == ExpressionType.Constant ||
                @operator == ExpressionType.Lambda ||
                @operator == ExpressionType.UnaryPlus ||
                @operator == ExpressionType.MemberAccess ||
                @operator == ExpressionType.Negate ||
                @operator == ExpressionType.NegateChecked ||
                @operator == ExpressionType.Not ||
                @operator == ExpressionType.IsTrue ||
                @operator == ExpressionType.IsFalse ||
                IsArithmeticOperator(expression) ||
                IsLogicOperator(expression) ||
                IsComparisonOperator(expression);
        }

        private bool IsArithmeticOperator(Expression expression)
        {
            var @operator = expression.NodeType;
            return
                @operator == ExpressionType.Add ||
                @operator == ExpressionType.AddChecked ||
                @operator == ExpressionType.Divide ||
                @operator == ExpressionType.Modulo ||
                @operator == ExpressionType.Multiply ||
                @operator == ExpressionType.MultiplyChecked ||
                @operator == ExpressionType.Power ||
                @operator == ExpressionType.Subtract ||
                @operator == ExpressionType.SubtractChecked;
        }

        private bool IsComparisonOperator(Expression expression)
        {
            var @operator = expression.NodeType;
            return
                @operator == ExpressionType.Equal ||
                @operator == ExpressionType.GreaterThan || 
                @operator == ExpressionType.LessThan ||
                @operator == ExpressionType.LessThanOrEqual ||
                @operator == ExpressionType.GreaterThanOrEqual ||
                @operator == ExpressionType.NotEqual;
        }

        private bool IsLogicOperator(Expression expression)
        {
            var @operator = expression.NodeType;
            return
                @operator == ExpressionType.And ||
                @operator == ExpressionType.AndAlso ||
                @operator == ExpressionType.Or ||
                @operator == ExpressionType.OrElse;
        }

        private BlockBaseOperator ParseExpressionOperator(ExpressionType @operator)
        {
            switch (@operator)
            {
                case ExpressionType.Add:
                case ExpressionType.AddChecked:
                    return BlockBaseOperator.Add;
                case ExpressionType.Divide:
                    return BlockBaseOperator.Divide;
                case ExpressionType.Modulo:
                    return BlockBaseOperator.Modulo;
                case ExpressionType.Multiply:
                case ExpressionType.MultiplyChecked:
                    return BlockBaseOperator.Multiply;
                case ExpressionType.Subtract:
                case ExpressionType.SubtractChecked:
                    return BlockBaseOperator.Subtract;
                case ExpressionType.Power:
                    return BlockBaseOperator.Power;
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                    return BlockBaseOperator.And;
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                    return BlockBaseOperator.Or;
                case ExpressionType.LessThan:
                    return BlockBaseOperator.LessThan;
                case ExpressionType.LessThanOrEqual:
                    return BlockBaseOperator.LessOrEqualTo;
                case ExpressionType.GreaterThan:
                    return BlockBaseOperator.GreaterThan;
                case ExpressionType.GreaterThanOrEqual:
                    return BlockBaseOperator.GreaterOrEqualTo;
                case ExpressionType.Equal:
                    return BlockBaseOperator.EqualTo;
                case ExpressionType.NotEqual:
                    return BlockBaseOperator.DifferentFrom;
                default:
                    return BlockBaseOperator.Unknown;
            }
        }

        public ExpressionNode Reduce(ExpressionNode node)
        {
            return Reduce(node, 0).Item1;
        }

        private (ExpressionNode, bool) Reduce(ExpressionNode node, int depth)
        {
            switch (node)
            {
                case ValueNode valueNode:
                    var valueResult = ReduceValueNode(valueNode, depth);
                    return valueResult;
                case PropertyNode propertyNode:
                    var propertyResult = ReducePropertyNode(propertyNode, depth);
                    return propertyResult;
                case ComparisonNode comparisonNode:
                    var comparisonResult = ReduceComparisonNode(comparisonNode, depth);
                    return comparisonResult;
                case LogicNode logicNode:
                    var logicResult = ReduceLogicNode(logicNode, depth);
                    return logicResult;
            }
            throw new InvalidExpressionNodeException(node);
        }

        private (ExpressionNode, bool) ReduceValueNode(ValueNode node, int depth)
        {
            if (depth == 0)
            {
                throw new InvalidExpressionNodeException(node);
            }
            return (node, false);
        }

        private (ExpressionNode, bool) ReducePropertyNode(PropertyNode node, int depth)
        {
            if (depth != 0) return (node, false);
            if (node.Property.PropertyType == typeof(bool))
            {
                return (new ComparisonNode(BlockBaseOperator.EqualTo, node, new ValueNode(true)), true);
            }
            throw new InvalidExpressionNodeException(node);
        }

        private (ExpressionNode, bool) ReduceComparisonNode(ComparisonNode node, int depth)
        {
            if (node.LeftNode is ValueNode && node.RightNode is ValueNode)
            {
                throw new InvalidExpressionNodeException(node);
            }
            var reducedLeft = Reduce(node.LeftNode, depth + 1);
            var reducedRight = Reduce(node.RightNode, depth + 1);
            var hasChanges = reducedRight.Item2 || reducedLeft.Item2;
            return hasChanges ? ReduceComparisonNode(new ComparisonNode(node.Operator, reducedLeft.Item1, reducedRight.Item1), depth) : (node, false);
        }


        private (ExpressionNode, bool) ReduceLogicNode(LogicNode node, int depth)
        {
            (ExpressionNode, bool) left = (null, false);
            (ExpressionNode, bool) right = (null, false);

            if (node.LeftNode is ValueNode || node.RightNode is ValueNode)
            {
                throw new InvalidExpressionNodeException(node);
            }


            if (node.LeftNode is PropertyNode propertyLeft && propertyLeft.Property.PropertyType == typeof(bool))
            {
                left = (new ComparisonNode(BlockBaseOperator.EqualTo, node.LeftNode, new ValueNode(true)), true);
            }
            else if (node.LeftNode is LogicNode logicNode)
            {
                left = ReduceLogicNode(logicNode, depth + 1);
            }
            else if (node.LeftNode is ComparisonNode comparisonNode)
            {
                left = ReduceComparisonNode(comparisonNode, depth + 1);
            }


            if (node.RightNode is PropertyNode propertyRight && propertyRight.Property.PropertyType == typeof(bool))
            {
                right = (new ComparisonNode(BlockBaseOperator.EqualTo, node.LeftNode, new ValueNode(true)), true);
            }
            else if (node.RightNode is LogicNode logicNode)
            {
                right = ReduceLogicNode(logicNode, depth + 1);
            }
            else if (node.RightNode is ComparisonNode comparisonNode)
            {
                right = ReduceComparisonNode(comparisonNode, depth + 1);
            }

            if (left.Item1 != null && right.Item1 != null)
            {
                if (left.Item2 || right.Item2)
                {
                    return ReduceLogicNode(new LogicNode(node.Operator, left.Item1, right.Item1), depth);
                }
                return (node, false);
            }
            throw new InvalidExpressionNodeException(node);
        }
        #endregion

        #region Parse Joins

        public JoinNode[] ParseJoins(JoinNode[] currentJoins, Type newType)
        {
            var types = new List<Type>();
            foreach (var currentJoin in currentJoins)
            {
                var left = (currentJoin.LeftNode as PropertyNode).Property.DeclaringType;
                var right = (currentJoin.RightNode as PropertyNode).Property.DeclaringType;
                var leftExists = false;
                bool rightExists = left == right;
                foreach (var type in types)
                {
                    if (left == type)
                    {
                        leftExists = true;
                    }
                    if (right == type)
                    {
                        rightExists = true;
                    }
                }

                if (!leftExists)
                {
                    types.Add(left);
                }
                if (!rightExists)
                {
                    types.Add(right);
                }
            }

            var join = CreateJoinNode(types, newType);
            if (join == null)
            {
                throw new Exception();
            }

            var joins = new List<JoinNode>(currentJoins) {@join};
            return joins.ToArray();
        }

        private JoinNode CreateJoinNode(List<Type> types, Type newType)
        {
            foreach (var type in types)
            {
                var key = type.GetForeignKey(newType);
                if (key == null)
                {
                    key = newType.GetForeignKey(type);
                }

                if (key != null)
                {
                    return new JoinNode(newType.GetPrimaryKeyProperty(), key);
                }
            }

            return null;
        }

        public JoinNode[] ParseJoins(Type[] tables)
        {

            var joins = new List<JoinNode>();
            var types = new List<Type>(tables);

            while (types.Count > 0)
            {
                var added = false;
                var currentTable = types[0];
                types.Remove(currentTable);

                var joinNode = CreateJoinNode(types, currentTable);
                if (joinNode != null)
                {
                    added = true;
                    joins.Add(joinNode);
                }

                if (!added && !IsInJoins(joins, currentTable))
                {
                    throw new Exception();
                }
            }

            return joins.ToArray();
        }

        public static bool IsInJoins(List<JoinNode> joins, Type type)
        {
            foreach (var join in joins)
            {
                var left = join.LeftNode as PropertyNode;
                var right = join.RightNode as PropertyNode;
                if (left.Property.DeclaringType == type || right.Property.DeclaringType == type)
                {
                    return true;
                }
            }

            return false;
        }

        #endregion
    }
}
