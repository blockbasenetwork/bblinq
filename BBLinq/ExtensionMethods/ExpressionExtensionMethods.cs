using BBLinq.Enumerables;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace BBLinq.ExtensionMethods
{
    
    internal static class ExpressionExtensionMethods
    {

        internal static Dictionary<string, Type> GetParameterDictionary(this LambdaExpression expression)
        {
            var res = new Dictionary<string, Type>();
            var parameters = (expression.Parameters);
            foreach (var @param in parameters)
            {
                res.Add(@param.Name, param.Type);
            }
            return res;
        }

        internal static string GetDebugString(this Expression expression)
        {
            var propertyInfo = typeof(Expression).GetProperty("DebugView", BindingFlags.Instance | BindingFlags.NonPublic);
            return propertyInfo.GetValue(expression) as string;
        }

        internal static Expression GetExpression(this Expression expression)
        {
            var propertyInfo = expression.GetType().GetProperty("Expression");
            return propertyInfo.GetValue(expression) as Expression;
        }

        internal static object GetValue(this MemberExpression expression)
        {
            var valueProp = (expression.GetExpression() as ConstantExpression).Value;
            var value = (expression.Member as FieldInfo).GetValue(valueProp);
            return value is string ? "'" + value + "'" : value;
        }

        internal static BinaryExpressionTypeEnum GetExpressionType(this Expression expression)
        {
            switch(expression.NodeType)
            {
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                    return BinaryExpressionTypeEnum.And;
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                    return BinaryExpressionTypeEnum.Or;
                case ExpressionType.Equal:
                case ExpressionType.LessThan:
                case ExpressionType.GreaterThan:
                case ExpressionType.LessThanOrEqual:
                case ExpressionType.GreaterThanOrEqual:
                    return BinaryExpressionTypeEnum.Comparison;
                default:
                    return BinaryExpressionTypeEnum.None;
            }
        }
    }
}
