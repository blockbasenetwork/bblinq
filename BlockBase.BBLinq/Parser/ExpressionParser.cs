using System.Collections.Generic;
using System.Linq.Expressions;
using BlockBase.BBLinq.Builders;
using BlockBase.BBLinq.ExtensionMethods;
using BlockBase.BBLinq.Pocos.Components;

namespace BlockBase.BBLinq.Parser
{
    public static class ExpressionParser
    {

        /// <summary>
        /// Generates a condition based on the primary key of an object
        /// </summary>
        /// <param name="record">a record</param>
        /// <returns>a SQL condition string</returns>
        public static string GenerateConditionFromObject(object record)
        {
            var type = record.GetType();
            var primaryKey = type.GetPrimaryKey();
            if (primaryKey == null)
            {
                return string.Empty;
            }
            var tableName = type.GetTableName();
            var fieldName = primaryKey.GetFieldName();
            var value = primaryKey.GetValue(record);
            var queryBuilder = new BbSqlQueryBuilder();
            queryBuilder.FieldOnTable(tableName, fieldName).Equals();
            queryBuilder.Value(WrapValue(value));
            return queryBuilder.ToString();
        }


        /// <summary>
        /// Returns the value if it is a number or wraps it if not
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>a recognized value expression</returns>
        public static string WrapValue(object value)
        {
            var queryBuilder = new BbSqlQueryBuilder();
            if (value.IsNumber())
            {
                queryBuilder.Value(value.ToString());
            }
            else
            {
                queryBuilder.WrapText(value.ToString());
            }
            return queryBuilder.ToString();
        }

        #region Expression parser

        /// <summary>
        /// Parses a complex query
        /// </summary>
        /// <param name="expression">a complex expression</param>
        /// <returns>the query as a string</returns>
        public static string ParseQuery(Expression expression)
        {
            var builder = new BbSqlQueryBuilder();
            if (expression.IsOperator())
            {
                var value = ExecuteExpression(expression);
                return WrapValue(value);
            }
            else if (expression.NodeType == ExpressionType.Convert)
            {
                return ParseQuery((expression as UnaryExpression).Operand);
            }
            else if (expression is MemberExpression memberExpression)
            {
                if (memberExpression.IsConstantMemberAccess())
                {
                    var value = ExecuteExpression(memberExpression);
                    builder.Append(WrapValue(value));
                }
                else if (memberExpression.IsPropertyMemberAccess())
                {
                    var tableField = ParsePropertyAccess(memberExpression);
                    builder.FieldOnTable(tableField.TableName, tableField.FieldName);
                }
            }
            else if (expression is BinaryExpression binaryExpression)
            {
                builder.Append(ParseQuery(binaryExpression.Left));
                builder.WhiteSpace();
                builder.Append(ParseOperator(binaryExpression.NodeType));
                builder.WhiteSpace();
                builder.Append(ParseQuery(binaryExpression.Right));
            }
            else if (expression is ConstantExpression constantExpression)
            {
                return WrapValue(constantExpression.Value);
            }
            

            return builder.ToString();
        }

        /// <summary>
        /// Parses a Property Access
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        private static TableField ParsePropertyAccess(MemberExpression expression)
        {
            var member = expression.Member;
            var tableName = member.DeclaringType.GetTableName();
            var fieldName = member.GetFieldName();
            return new TableField(){TableName = tableName, FieldName = fieldName};
        }

        /// <summary>
        /// Parses a comparison or bitwise operator on an expression
        /// </summary>
        /// <param name="nodeType">the node type that describes a</param>
        /// <returns>the SQL expression related to the operator in question</returns>
        public static string ParseOperator(ExpressionType nodeType)
        {
            var builder = new BbSqlQueryBuilder();
            switch (nodeType)
            {
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                    builder.And();
                    break;
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                    builder.Or();
                    break;
                case ExpressionType.Equal:
                    builder.Equals();
                    break;
                case ExpressionType.NotEqual:
                    builder.DifferentFrom();
                    break;
                case ExpressionType.GreaterThan:
                    builder.GreaterThan();
                    break;
                case ExpressionType.GreaterThanOrEqual:
                    builder.EqualOrGreaterThan();
                    break;
                case ExpressionType.LessThan:
                    builder.LessThan();
                    break;
                case ExpressionType.LessThanOrEqual:
                    builder.EqualOrLessThan();
                    break;
            }
            return builder.ToString();
        }

        #endregion
        /// <summary>
        /// Executes a lambda expression
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        private static object ExecuteExpression(Expression expression)
        {
            var result = Expression.Lambda(expression).Compile().DynamicInvoke();
            return result;
        }

        /// <summary>
        /// Parses a select body
        /// </summary>
        /// <param name="selectBody"></param>
        /// <returns></returns>
        public static IEnumerable<PropertyFieldAssignment> ParseSelectBody(Expression selectBody)
        {
            return selectBody switch
            {
                MemberInitExpression initExpression => initExpression.GetTableFieldPropertyTuples(),
                NewExpression newExpression => newExpression.GetTableFieldPropertyTuples(),
                _ => null
            };
        }

    }
}
