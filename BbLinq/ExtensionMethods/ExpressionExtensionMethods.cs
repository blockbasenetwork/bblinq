using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace BlockBase.BBLinq.Exceptions
{
    public static class ExpressionExtensionMethods
    {

        public static bool IsNot(this Expression expression)
        {
            return expression.NodeType == ExpressionType.Not;
        }

        public static bool IsAcceptedAssignment(this Expression expression)
        {
            return false;
        }

        public static bool IsNegate(this Expression expression)
        {
            return expression.NodeType == ExpressionType.Negate;
        }

        public static bool IsUnaryPlus(this Expression expression)
        {
            return expression.NodeType == ExpressionType.UnaryPlus;
        }

        public static bool IsUnacceptedAssignment(this Expression expression)
        {
            return expression.NodeType == ExpressionType.Assign ||
                   expression.NodeType == ExpressionType.AddAssign ||
                   expression.NodeType == ExpressionType.AddAssignChecked ||
                   expression.NodeType == ExpressionType.AndAssign ||
                   expression.NodeType == ExpressionType.DivideAssign ||
                   expression.NodeType == ExpressionType.ExclusiveOrAssign ||
                   expression.NodeType == ExpressionType.LeftShiftAssign ||
                   expression.NodeType == ExpressionType.ModuloAssign ||
                   expression.NodeType == ExpressionType.MultiplyAssign ||
                   expression.NodeType == ExpressionType.MultiplyAssignChecked ||
                   expression.NodeType == ExpressionType.OrAssign ||
                   expression.NodeType == ExpressionType.PowerAssign ||
                   expression.NodeType == ExpressionType.RightShiftAssign ||
                   expression.NodeType == ExpressionType.SubtractAssign ||
                   expression.NodeType == ExpressionType.SubtractAssignChecked ||
                   expression.NodeType == ExpressionType.PreDecrementAssign ||
                   expression.NodeType == ExpressionType.PreDecrementAssign ||
                   expression.NodeType == ExpressionType.PostDecrementAssign ||
                   expression.NodeType == ExpressionType.PostIncrementAssign;
        }

        public static bool IsAcceptedOperation(this Expression expression)
        {
            return false;
        }

        public static bool IsUnacceptedOperation(this Expression expression)
        {
            return expression.NodeType == ExpressionType.ArrayIndex ||
                   expression.NodeType == ExpressionType.ArrayLength ||
                   expression.NodeType == ExpressionType.Call ||
                   expression.NodeType == ExpressionType.Convert ||
                   expression.NodeType == ExpressionType.ConvertChecked ||
                   expression.NodeType == ExpressionType.Increment ||
                   expression.NodeType == ExpressionType.Lambda ||
                   expression.NodeType == ExpressionType.LeftShift ||
                   expression.NodeType == ExpressionType.ListInit ||
                   expression.NodeType == ExpressionType.NewArrayBounds ||
                   expression.NodeType == ExpressionType.NewArrayInit ||
                   expression.NodeType == ExpressionType.Not ||
                   expression.NodeType == ExpressionType.Quote ||
                   expression.NodeType == ExpressionType.RightShift ||
                   expression.NodeType == ExpressionType.TypeAs ||
                   expression.NodeType == ExpressionType.TypeIs ||
                   expression.NodeType == ExpressionType.Block ||
                   expression.NodeType == ExpressionType.DebugInfo ||
                   expression.NodeType == ExpressionType.Default ||
                   expression.NodeType == ExpressionType.Extension ||
                   expression.NodeType == ExpressionType.Goto ||
                   expression.NodeType == ExpressionType.Increment ||
                   expression.NodeType == ExpressionType.Index ||
                   expression.NodeType == ExpressionType.Label ||
                   expression.NodeType == ExpressionType.RuntimeVariables ||
                   expression.NodeType == ExpressionType.Loop ||
                   expression.NodeType == ExpressionType.Switch ||
                   expression.NodeType == ExpressionType.Throw ||
                   expression.NodeType == ExpressionType.Try ||
                   expression.NodeType == ExpressionType.Unbox ||
                   expression.NodeType == ExpressionType.TypeEqual ||
                   expression.NodeType == ExpressionType.OnesComplement ||
                   expression.NodeType == ExpressionType.IsTrue ||
                   expression.NodeType == ExpressionType.IsFalse ||
                   expression.NodeType == ExpressionType.Decrement;
        }

        public static bool IsAcceptedLogic(this Expression expression)
        {

            return expression.NodeType == ExpressionType.And ||
                   expression.NodeType == ExpressionType.AndAlso ||
                   expression.NodeType == ExpressionType.Or ||
                   expression.NodeType == ExpressionType.OrElse;
        }

        public static bool IsUnacceptedLogic(this Expression expression)
        {
            return 
                   expression.NodeType == ExpressionType.Coalesce ||
                   expression.NodeType == ExpressionType.ExclusiveOr;
        }
        
        public static bool IsAcceptedComparison(this Expression expression)
        {
            return expression.NodeType == ExpressionType.Equal ||
                   expression.NodeType == ExpressionType.GreaterThan ||
                   expression.NodeType == ExpressionType.GreaterThanOrEqual ||
                   expression.NodeType == ExpressionType.LessThan ||
                   expression.NodeType == ExpressionType.LessThanOrEqual ||
                   expression.NodeType == ExpressionType.NotEqual;
        }

        public static bool IsUnacceptedComparison(this Expression expression)
        {
            return false;
        }


        public static bool IsAcceptedArithmetic(this Expression expression)
        {
            return expression.NodeType == ExpressionType.Add ||
                   expression.NodeType == ExpressionType.Divide ||
                   expression.NodeType == ExpressionType.Modulo ||
                   expression.NodeType == ExpressionType.Multiply ||
                   expression.NodeType == ExpressionType.Negate ||
                   expression.NodeType == ExpressionType.UnaryPlus ||
                   expression.NodeType == ExpressionType.Subtract;

        }

        public static bool IsUnacceptedArithmetic(this Expression expression)
        {
            return expression.NodeType == ExpressionType.AddChecked ||
                   expression.NodeType == ExpressionType.MultiplyChecked ||
                   expression.NodeType == ExpressionType.NegateChecked ||
                   expression.NodeType == ExpressionType.SubtractChecked;
        }

        public static bool IsPropertyAccess(this MemberExpression expression, IReadOnlyCollection<ParameterExpression> parameters)
        {
            var isOnParameter = false;
            foreach (var parameter in parameters)
            {
                var expressionParameter = expression.Expression;
                isOnParameter = expressionParameter == parameter;
                if (isOnParameter)
                {
                    break;
                }
            }
            return isOnParameter;
        }

        public static bool IsFieldAccess(this Expression expression)
        {
            return expression.NodeType == ExpressionType.MemberAccess;
        }

        public static object GetValue(this MemberExpression expression)
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

        private static object GetValue(object origin, object accessor)
        {
            return accessor switch
            {
                PropertyInfo property => property.GetValue(origin),
                FieldInfo field => field.GetValue(origin),
                _ => null
            };
        }
    }
}
