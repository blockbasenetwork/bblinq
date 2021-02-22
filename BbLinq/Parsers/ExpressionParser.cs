using BlockBase.BBLinq.Exceptions;
using BlockBase.BBLinq.Pocos.ExpressionParser;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BlockBase.BBLinq.Parsers
{
    public static class ExpressionParser
    {
        private static readonly Dictionary<ExpressionType, ExpressionOperator> AvailableOperators =
            new Dictionary<ExpressionType, ExpressionOperator>()
            {
                {ExpressionType.And, ExpressionOperator.And},
                {ExpressionType.AndAlso, ExpressionOperator.And},

                {ExpressionType.Or, ExpressionOperator.Or},
                {ExpressionType.OrElse, ExpressionOperator.Or},

                {ExpressionType.Equal, ExpressionOperator.Equals},

                {ExpressionType.GreaterThan, ExpressionOperator.GreaterThan},
                {ExpressionType.GreaterThanOrEqual, ExpressionOperator.GreaterOrEqualTo},

                {ExpressionType.LessThan, ExpressionOperator.LessThan},
                {ExpressionType.LessThanOrEqual, ExpressionOperator.LessThan},
            };

        public static ExpressionNode ParseExpression(Expression expression)
        {
            if (expression is LambdaExpression exp)
            {
                expression = exp.Body;
            }
            var parseResult = ParseExpressionToken(expression);
            return parseResult;
        }

        private static ExpressionNode ParseExpressionToken(Expression expression)
        {
            switch (expression.NodeType)
            {
                case ExpressionType.Constant:
                    return ParseConstantExpression(expression as ConstantExpression);
                case ExpressionType.MemberAccess:
                    return ParseMemberAccessExpression((expression as MemberExpression));
            }
            if (expression is BinaryExpression binExpression)
            {
                var operatorExists = AvailableOperators.ContainsKey(binExpression.NodeType);
                if (!operatorExists)
                {
                    return ExecuteExpression(binExpression);
                }
                return ParseBinaryExpression(binExpression);
            }
            if (expression is UnaryExpression unExpression)
            {
                return ParseUnaryExpression(unExpression);
            }
            throw new InvalidExpressionException(expression);
        }

        private static ExpressionNode ParseUnaryExpression(Expression expression)
        {
            return ExecuteExpression(expression);
        }

        private static ValueExpression ParseConstantExpression(ConstantExpression expression)
        {
            return ExecuteExpression(expression) as ValueExpression;
        }

        private static ExpressionNode ExecuteExpression(Expression expression)
        {
            try
            {
                var executionResult= Expression.Lambda(expression).Compile().DynamicInvoke();
                if (executionResult is Expression exp)
                {
                    if (exp.NodeType != expression.NodeType)
                    {
                        return ParseExpressionToken(exp);
                    }
                    var availableOperations = new List<string>();
                    foreach (var item in AvailableOperators)
                    {
                        availableOperations.Add($"{item.Key} - {item.Value}");
                    }
                    throw new UnavailableOperatorException(exp.NodeType.ToString(), availableOperations.ToArray());
                }
                if (executionResult != null)
                {
                    return new ValueExpression() { Value = executionResult };
                }
                throw new InvalidExpressionException(expression);
            }
            catch (Exception)
            {
                throw new InvalidExpressionException(expression);
            }
        }

        private static ExpressionNode ParseMemberAccessExpression(MemberExpression expression)
        {
            var innerExpression = expression.Expression;
            switch (innerExpression.NodeType)
            {
                case ExpressionType.Constant:
                    var result = ExecuteExpression(expression);
                    return new ValueExpression() { Value = result };
                case ExpressionType.Parameter:
                    var table = innerExpression.Type;
                    var columnName = expression.Member.Name;
                    var column = table.GetProperty(columnName);
                    if (column == null)
                    {
                        throw new Exception();
                    }
                    return new PropertyExpression() { Table = table, Column = column };
            }
            return null;
        }


        private static BinaryExpressionNode ParseBinaryExpression(BinaryExpression expression)
        {
            var @operator = AvailableOperators[expression.NodeType];

            var left = ParseExpressionToken(expression.Left);
            var right = ParseExpressionToken(expression.Right);
            return new BinaryExpressionNode()
            {
                Left = left,
                Right = right,
                Operator = @operator
            };
        }

    }
}
