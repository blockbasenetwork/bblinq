using BlockBase.BBLinq.Enumerables;
using BlockBase.BBLinq.Exceptions;
using BlockBase.BBLinq.ExtensionMethods;
using BlockBase.BBLinq.Model.Base;
using BlockBase.BBLinq.Model.Database;
using BlockBase.BBLinq.Model.Nodes;
using BlockBase.BBLinq.Validators;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace BlockBase.BBLinq.Parsers
{
    internal class BlockBaseExpressionParser
    {
        private int _depth;
        private List<ParameterExpression> _parameters;

        #region ParseCondition

        public ExpressionNode Parse(LambdaExpression expression)
        {
            if (expression == null)
            {
                return null;
            }
            _depth = 0;
            _parameters = expression.Parameters.ToList();
            var result = Parse(expression.Body);
            return result;
        }

        private ExpressionNode Parse(Expression expression)
        {
            return expression switch
            {
                BinaryExpression binaryExpression => ParseBinaryExpression(binaryExpression),
                MemberExpression memberExpression => ParseMemberExpression(memberExpression),
                ConstantExpression constantExpression => ParseConstantExpression(constantExpression),
                UnaryExpression unaryExpression => ParseUnaryExpression(unaryExpression),
                MethodCallExpression methodCallExpression => ParseMethodCallExpression(methodCallExpression),
                _ => throw new UnsupportedExpressionException(expression)
            };
        }

        #region Method Call

        private ExpressionNode ParseMethodCallExpression(MethodCallExpression expression)
        {
            var method = expression.Method;
            var valueExpression = expression.Object;
            object value = null;
            if (valueExpression is ConstantExpression constantValue)
            {
                value = constantValue.Value;
            }
            else if (method.Name == "Contains")
            {
                var valExpression = expression.Arguments.FirstOrDefault(x => x.NodeType == ExpressionType.MemberAccess || x.NodeType == ExpressionType.Constant && typeof(IEnumerable).IsAssignableFrom(x.Type) && x.Type != typeof(string));
                var possibleValueExpression = expression.Arguments.FirstOrDefault(x => x.NodeType == ExpressionType.MemberAccess && (x.Type == typeof(string) || !typeof(IEnumerable).IsAssignableFrom(x.Type)));

                if (valExpression == null || possibleValueExpression == null)
                {
                    throw new UnsupportedExpressionException(expression);
                }

                var property = ParseMemberExpression(possibleValueExpression as MemberExpression) as PropertyNode;
                var possibleValues = ((IEnumerable)(valExpression as MemberExpression).GetValue());
                if (possibleValues == null)
                {
                    var memberExpression = (MemberExpression)expression.Object;
                    var getCallerExpression = Expression.Lambda<Func<object>>(memberExpression);
                    var getCaller = getCallerExpression.Compile();
                    var caller = getCaller();
                    if (caller is IEnumerable enumerable)
                    {
                        possibleValues = enumerable;
                    }
                }
                List<ComparisonNode> comparisonNodes = new List<ComparisonNode>();
                foreach (var val in possibleValues)
                {
                    comparisonNodes.Add(new ComparisonNode(BlockBaseComparisonOperator.EqualTo, property, new ValueNode(val)));
                }

                if (comparisonNodes.Count == 0)
                {
                    return null;
                }
                if (comparisonNodes.Count == 1)
                {
                    return comparisonNodes[0];
                }
                else
                {
                    var first = comparisonNodes[0];
                    var second = comparisonNodes[1];
                    var logicNode = new LogicNode(BlockBaseLogicOperator.Or, first, second);
                    comparisonNodes.Remove(first);
                    comparisonNodes.Remove(second);
                    foreach (var comparisonNode in comparisonNodes)
                    {
                        logicNode = new LogicNode(BlockBaseLogicOperator.Or, logicNode, comparisonNode);
                    }
                    return logicNode;
                }


            }
            else if (valueExpression is MemberExpression valueMember)
            {
                value = valueMember.GetValue();
            }
            if (value != null)
            {
                var result = method.Invoke(value, new object[] { });
                return new ValueNode(result);
            }
            throw new UnsupportedExpressionException(expression);
        }


        #endregion

        #region Unary
        private ExpressionNode ParseUnaryExpression(UnaryExpression expression)
        {
            if (expression.IsNot())
            {
                return ParseNot(expression);
            }
            if (expression.IsNegate())
            {
                return null;
            }
            if (expression.IsUnaryPlus())
            {
                return null;
            }

            if (expression.NodeType == ExpressionType.Convert && expression.Type == typeof(int) &&
                expression.Operand.Type.IsEnum && expression.Operand is MemberExpression operand)
            {
                return ParseMemberExpression(operand);
            }
            throw new UnsupportedExpressionException(expression);
        }

        #endregion

        #region Sub-Unary
        private ExpressionNode ParseNot(UnaryExpression expression)
        {
            var operandNode = Parse(expression.Operand);
            if (operandNode is PropertyNode operandProperty)
            {
                return GetIsNotNode(operandProperty);
            }

            if (operandNode is ComparisonNode { Left: PropertyNode left })
            {
                return GetIsNotNode(left);
            }

            if (operandNode is ValueNode { Value: bool value })
            {
                return new ValueNode(!value);
            }
            throw new UnsupportedExpressionException(expression);
        }
        #endregion

        #region Binary

        private ExpressionNode ParseBinaryExpression(BinaryExpression expression)
        {
            if (expression.IsAcceptedComparison())
            {
                return ParseComparisonExpression(expression);
            }

            if (expression.IsAcceptedArithmetic())
            {
                return ParseArithmeticExpression(expression);
            }

            if (expression.IsAcceptedLogic())
            {
                return ParseLogicExpression(expression);
            }
            throw new UnsupportedExpressionException(expression);
        }

        #endregion

        #region Sub-Binary
        private ExpressionNode ParseComparisonExpression(BinaryExpression expression)
        {
            if (expression.IsAcceptedComparison())
            {
                if (expression.NodeType == ExpressionType.Convert)
                {

                }
                var @operator = ParseComparisonOperator(expression.NodeType);
                var (left, right) = ParseSides(expression);
                ComparisonNode result = null;
                switch (left)
                {
                    case ValueNode value when right is PropertyNode property:
                        @operator = InvertOperator(@operator);
                        result = new ComparisonNode(@operator, property, value);
                        break;
                    case PropertyNode leftProperty:
                        switch (right)
                        {
                            case PropertyNode rightProperty:
                                result = new ComparisonNode(@operator, leftProperty, rightProperty);
                                break;
                            case ValueNode rightValue:
                                result = new ComparisonNode(@operator, leftProperty, rightValue);
                                break;
                        }
                        break;
                }
                ExpressionNodeValidator.ValidateComparisonNode(result);
                return result;
            }
            throw new UnsupportedExpressionException(expression);
        }

        private ExpressionNode ParseLogicExpression(BinaryExpression expression)
        {
            if (expression.IsAcceptedLogic())
            {
                var @operator = ParseLogicOperator(expression.NodeType);
                var (left, right) = ParseSides(expression);
                if (left is PropertyNode propLeft)
                {
                    left = GetIsNode(propLeft);
                }
                if (right is PropertyNode propRight)
                {
                    right = GetIsNode(propRight);
                }
                switch (left)
                {
                    case ComparisonNode comparisonLeft when right is ComparisonNode comparisonRight:
                        return new LogicNode(@operator, comparisonLeft, comparisonRight);
                    case ComparisonNode comparisonLeft when right is LogicNode logicRight:
                        return new LogicNode(@operator, comparisonLeft, logicRight);
                    case LogicNode logicLeft when right is ComparisonNode comparisonRight:
                        return new LogicNode(@operator, logicLeft, comparisonRight);
                    case LogicNode logicLeft when right is LogicNode logicRight:
                        return new LogicNode(@operator, logicLeft, logicRight);
                }
            }
            throw new UnsupportedExpressionException(expression);
        }

        private ExpressionNode ParseArithmeticExpression(BinaryExpression expression)
        {
            var @operator = ParseArithmeticOperator(expression.NodeType);
            var (left, right) = ParseSides(expression);
            switch (left)
            {
                case ValueNode leftValue when right is ValueNode rightValue:
                    if (leftValue.Value.IsNumber() && leftValue.Value.IsNumber())
                    {
                        var result = ExecuteOperatorOnNumbers(@operator, leftValue.Value, rightValue.Value);
                        if (result == null)
                        {
                            throw new UnsupportedExpressionException(expression);
                        }
                        return new ValueNode(result);
                    }
                    if (@operator == BlockBaseArithmeticOperator.Add)
                    {
                        return new ValueNode(leftValue.Value.ToString() + rightValue.Value);
                    }
                    break;
            }
            throw new UnsupportedExpressionException(expression);
        }



        #endregion

        #region Sub-Binary

        public dynamic ExecuteOperatorOnNumbers(BlockBaseArithmeticOperator @operator, dynamic left, dynamic right)
        {
            return @operator switch
            {
                BlockBaseArithmeticOperator.Add => left + right,
                BlockBaseArithmeticOperator.Subtract => left - right,
                BlockBaseArithmeticOperator.Divide => left / right,
                BlockBaseArithmeticOperator.Multiply => left * right,
                BlockBaseArithmeticOperator.Modulo => left % right,
                _ => null
            };
        }

        public (ExpressionNode, ExpressionNode) ParseSides(BinaryExpression expression)
        {
            _depth++;
            var left = Parse(expression.Left);
            var right = Parse(expression.Right);
            _depth--;
            return (left, right);
        }

        #endregion

        #region Member Access 
        private ExpressionNode ParseMemberExpression(MemberExpression expression)
        {
            if (expression.IsPropertyAccess(_parameters))
            {
                var type = expression.Expression.Type;
                var property = type.GetProperty(expression.Member.Name);

                var node = new PropertyNode(property);
                if (_depth == 0)
                {
                    var isNode = GetIsNode(node);
                    if (isNode != null)
                    {
                        return isNode;
                    }
                }
                return node;
            }
            var value = expression.GetValue();
            if (value != null && _depth > 0)
            {
                return new ValueNode(value);
            }
            throw new UnsupportedExpressionException(expression);
        }
        #endregion

        #region Constant
        private ExpressionNode ParseConstantExpression(ConstantExpression expression)
        {
            if (_depth == 0)
            {
                throw new UnsupportedExpressionException(expression);
            }
            return new ValueNode(expression.Value);
        }

        #endregion

        #region Auxiliary
        private ComparisonNode GetIsNode(PropertyNode node, bool value = true)
        {
            return node.Property.PropertyType != typeof(bool) ? null :
                new ComparisonNode(BlockBaseComparisonOperator.EqualTo, node, new ValueNode(value));
        }

        private LogicNode GetIsNotNode(PropertyNode node)
        {
            return node.Property.PropertyType != typeof(bool) ? null :
                new LogicNode(BlockBaseLogicOperator.Or, new ComparisonNode(BlockBaseComparisonOperator.DifferentFrom, node, new ValueNode(true)),
            new ComparisonNode(BlockBaseComparisonOperator.DifferentFrom, node, new ValueNode(null)), true);
        }

        #endregion

        #region Operators
        private BlockBaseComparisonOperator ParseComparisonOperator(ExpressionType expressionOperator)
        {
            return expressionOperator switch
            {
                ExpressionType.Equal => BlockBaseComparisonOperator.EqualTo,
                ExpressionType.NotEqual => BlockBaseComparisonOperator.DifferentFrom,
                ExpressionType.GreaterThan => BlockBaseComparisonOperator.GreaterThan,
                ExpressionType.GreaterThanOrEqual => BlockBaseComparisonOperator.GreaterOrEqualTo,
                ExpressionType.LessThan => BlockBaseComparisonOperator.LessThan,
                ExpressionType.LessThanOrEqual => BlockBaseComparisonOperator.LessOrEqualTo,
                _ => throw new UnsupportedExpressionOperatorException(expressionOperator)
            };
        }

        private BlockBaseArithmeticOperator ParseArithmeticOperator(ExpressionType expressionOperator)
        {
            return expressionOperator switch
            {
                ExpressionType.Add => BlockBaseArithmeticOperator.Add,
                ExpressionType.Subtract => BlockBaseArithmeticOperator.Subtract,
                ExpressionType.Divide => BlockBaseArithmeticOperator.Divide,
                ExpressionType.Multiply => BlockBaseArithmeticOperator.Multiply,
                ExpressionType.Modulo => BlockBaseArithmeticOperator.Modulo,
                _ => throw new UnsupportedExpressionOperatorException(expressionOperator)
            };
        }

        private BlockBaseLogicOperator ParseLogicOperator(ExpressionType expressionOperator)
        {
            switch (expressionOperator)
            {
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                    return BlockBaseLogicOperator.And;
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                    return BlockBaseLogicOperator.Or;
                default:
                    throw new UnsupportedExpressionOperatorException(expressionOperator);
            }
        }
        #endregion

        #endregion

        #region Parse Selection

        public BlockBaseColumn[] ParseSelectionColumns(LambdaExpression expression)
        {
            _parameters = expression.Parameters.ToList();
            BlockBaseColumn[] result = expression.Body switch
            {
                MemberExpression memberExpression => ParseMemberSelection(memberExpression),
                MemberInitExpression memberInitExpression => ParseMemberInitSelection(memberInitExpression),
                NewExpression newExpression => ParseNewSelection(newExpression),
                _ => null
            };

            if (result != null)
            {
                var selectionProperties = new List<BlockBaseColumn>();
                foreach (var property in result)
                {
                    var exists = false;
                    foreach (var existingProperty in selectionProperties)
                    {
                        if (existingProperty.Name != property.Name || existingProperty.Table != property.Table)
                        {
                            continue;
                        }
                        exists = true;
                        break;
                    }

                    if (!exists)
                    {
                        selectionProperties.Add(property);
                    }
                }
                return selectionProperties.ToArray();
            }
            return null;
        }

        public BlockBaseColumn[] ParseMemberSelection(MemberExpression memberExpression)
        {
            return new[] { BlockBaseColumn.From(memberExpression.GetProperty()) };
        }

        public BlockBaseColumn[] ParseMemberInitSelection(MemberInitExpression memberInitExpression)
        {
            var columns = new List<BlockBaseColumn>();
            var bindings = memberInitExpression.Bindings;
            foreach (var binding in bindings)
            {
                if (binding is MemberAssignment assignment)
                {
                    if (assignment.Expression is MemberExpression memberExpression)
                    {
                        columns.Add(BlockBaseColumn.From(memberExpression.GetProperty()));
                    }
                    else if (assignment.Expression is ParameterExpression parameterExpression &&
                             _parameters.Select(x => x.Name).Contains(parameterExpression.Name))
                    {
                        var parameter = _parameters.FirstOrDefault(x => x.Name == parameterExpression.Name);
                        if (parameter != null)
                        {
                            var properties = parameter.Type.GetProperties()
                                .Where(x => !x.IsVirtualOrStaticOrAbstract());
                            
                            columns.AddRange( properties.Select(BlockBaseColumn.From));
                        }
                    }
                    else
                    {
                        var properties = RetrievePropertyFromExpression(assignment.Expression);
                        foreach (var property in properties)
                        {
                            columns.Add(BlockBaseColumn.From(property));
                        }
                    }
                }
                else
                {
                    throw new Exception();
                }
            }
            return columns.ToArray();
        }

        public BlockBaseColumn[] ParseNewSelection(NewExpression newExpression)
        {
            var columns = new List<BlockBaseColumn>();
            var arguments = newExpression.Arguments;
            foreach (var argument in arguments)
            {
                if (argument is MemberExpression memberExpression)
                {
                    columns.Add(BlockBaseColumn.From(memberExpression.GetProperty()));
                }
                else
                {
                    var properties = RetrievePropertyFromExpression(argument);
                    foreach (var property in properties)
                    {
                        columns.Add(BlockBaseColumn.From(property));
                    }
                }
            }
            return columns.ToArray();
        }

        private PropertyInfo[] RetrievePropertyFromExpression(Expression expression)
        {
            var properties = new List<PropertyInfo>();
            switch (expression)
            {
                case MemberExpression memberExpression:
                    var member = memberExpression.Member;
                    if (member is PropertyInfo)
                    {
                        return new[] { memberExpression.GetProperty() };
                    }
                    return null;
                case BinaryExpression binaryExpression:
                    var left = RetrievePropertyFromExpression(binaryExpression.Left);
                    var right = RetrievePropertyFromExpression(binaryExpression.Right);
                    if (left != null)
                    {
                        properties.AddRange(left);
                    }

                    if (right != null && right.Length != 0)
                    {
                        properties.AddRange(right);
                    }

                    return properties.ToArray();
                case MethodCallExpression callExpression:
                    var obj = RetrievePropertyFromExpression(callExpression.Object);
                    if (obj != null)
                    {
                        properties.AddRange(obj);
                    }
                    return properties.ToArray();
            }
            return properties.ToArray();
        }
        #endregion

        public BlockBaseComparisonOperator InvertOperator(BlockBaseComparisonOperator @operator)
        {
            return @operator switch
            {
                BlockBaseComparisonOperator.GreaterOrEqualTo => BlockBaseComparisonOperator.LessThan,
                BlockBaseComparisonOperator.LessThan => BlockBaseComparisonOperator.GreaterOrEqualTo,
                BlockBaseComparisonOperator.GreaterThan => BlockBaseComparisonOperator.LessOrEqualTo,
                BlockBaseComparisonOperator.LessOrEqualTo => BlockBaseComparisonOperator.GreaterThan,
                _ => @operator
            };
        }
    }
}
