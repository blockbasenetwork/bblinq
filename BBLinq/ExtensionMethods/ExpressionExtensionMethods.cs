using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using agap2IT.Labs.BlockBase.BBLinq.Enums;
using agap2IT.Labs.BlockBase.BBLinq.Properties;

namespace agap2IT.Labs.BlockBase.BBLinq.ExtensionMethods
{
    /// <summary>
    /// Extension methods for the Expression type and its derivatives
    /// </summary>
    internal static class ExpressionExtensionMethods
    {
        /// <summary>
        /// Fetches the parameters and respective types for a Lambda expression
        /// </summary>
        /// <param name="expression">a lambda expression</param>
        /// <returns>a list of name, type tuples </returns>
        internal static Dictionary<string, Type> GetParameterDictionary(this LambdaExpression expression)
        {
            var parameters = (expression.Parameters);
            return parameters.ToDictionary(param => @param.Name, param => param.Type);
        }

        /// <summary>
        /// Returns a Debug string from an expression
        /// </summary>
        /// <param name="expression">the expression</param>
        /// <returns>a debug string</returns>
        internal static string GetDebugString(this Expression expression)
        {
            var propertyInfo = typeof(Expression).GetProperty("DebugView", BindingFlags.Instance | BindingFlags.NonPublic);
            return propertyInfo?.GetValue(expression) as string;
        }

        /// <summary>
        /// Returns an internal expression
        /// </summary>
        /// <param name="expression">the expression</param>
        /// <returns>the internal expression</returns>
        internal static Expression GetExpression(this Expression expression)
        {
            var propertyInfo = expression.GetType().GetProperty("Expression");
            return propertyInfo?.GetValue(expression) as Expression;
        }

        /// <summary>
        /// Returns an internal expression
        /// </summary>
        /// <param name="expression">the expression</param>
        /// <returns>the internal expression</returns>
        internal static MemberInfo GetMember(this Expression expression)
        {
            var propertyInfo = expression.GetType().GetProperty("Member");
            return propertyInfo?.GetValue(expression) as MemberInfo;
        }

        /// <summary>
        /// Returns a constant expression value
        /// </summary>
        /// <param name="expression">the expression</param>
        /// <returns>the expression value</returns>
        internal static object GetValue(this MemberExpression expression)
        {
            var valueProp = (expression.GetExpression() as ConstantExpression)?.Value;
            var value = (expression.Member as FieldInfo)?.GetValue(valueProp);
            return value is string ? $"{Resources.QUERY_TEXT_WRAPPER_LEFT}{value}{Resources.QUERY_TEXT_WRAPPER_RIGHT}" : value;
        }

        /// <summary>
        /// Categorizes the binary expression type
        /// </summary>
        /// <param name="expression">the expression</param>
        /// <returns>a binary expression type</returns>
        internal static BinaryExpressionCategoryEnum GetExpressionType(this BinaryExpression expression)
        {
            switch(expression.NodeType)
            {
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                    return BinaryExpressionCategoryEnum.And;
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                    return BinaryExpressionCategoryEnum.Or;
                case ExpressionType.Equal:
                case ExpressionType.LessThan:
                case ExpressionType.GreaterThan:
                case ExpressionType.NotEqual:
                case ExpressionType.LessThanOrEqual:
                case ExpressionType.GreaterThanOrEqual:
                    return BinaryExpressionCategoryEnum.Comparison;
                default:
                    return BinaryExpressionCategoryEnum.None;
            }
        }
    }
}
