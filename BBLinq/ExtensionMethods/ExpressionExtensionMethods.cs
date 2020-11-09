using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using BlockBase.BBLinq.Enums;
using BlockBase.BBLinq.Pocos.Components;

namespace BlockBase.BBLinq.ExtensionMethods
{
    /// <summary>
    /// Extension methods for the Expression type and its derivatives
    /// </summary>
    internal static class ExpressionExtensionMethods
    {

        #region Binary Expressions
        /// <summary>
        /// Categorizes the binary expression type
        /// </summary>
        /// <param name="expression">the expression</param>
        /// <returns>a binary expression type</returns>
        internal static BinaryExpressionCategory GetExpressionType(this BinaryExpression expression)
        {
            switch (expression.NodeType)
            {
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                    return BinaryExpressionCategory.And;
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                    return BinaryExpressionCategory.Or;
                case ExpressionType.Equal:
                case ExpressionType.LessThan:
                case ExpressionType.GreaterThan:
                case ExpressionType.NotEqual:
                case ExpressionType.LessThanOrEqual:
                case ExpressionType.GreaterThanOrEqual:
                    return BinaryExpressionCategory.Comparison;
                default:
                    return BinaryExpressionCategory.None;
            }
        }


        #endregion

        #region Expressions
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
        /// Retrieves a list of (table, field) names
        /// </summary>
        /// <param name="expression">an expression for a new object</param>
        /// <returns>a list of table,field name pairings</returns>
        internal static IEnumerable<TableField> GetTableAndFieldsPairings(this NewExpression expression)
        {
            var arguments = expression.Arguments;
            var result = new List<TableField>();
            foreach (var argument in arguments)
            {
                var exp = argument.GetExpression();
                var member = argument.GetMember();
                if (exp == null || member == null)
                {
                    continue;
                }
                var table = exp.Type.GetTableName();
                var field = member.GetFieldName();
                result.Add(new TableField() { TableName = table, FieldName = field });
            }
            return result;
        }

        /// <summary>
        /// Retrieves a list of (table, field) names
        /// </summary>
        /// <param name="expression">an expression for a object initializer</param>
        /// <returns>a list of table,field name pairings</returns>
        internal static IEnumerable<TableField> GetTablesAndFieldsPairings(this MemberInitExpression expression)
        {
            var bindings = expression.Bindings;
            var result = new List<TableField>();
            foreach (var binding in bindings)
            {
                if (!(binding is MemberAssignment assignment)) continue;
                var member = assignment.Expression;
                var table = member.GetExpression().Type.GetTableName();
                var field = member.GetMember().GetFieldName();
                result.Add(new TableField() {TableName = table, FieldName = field});
            }
            return result;
        }

        /// <summary>
        /// Retrieves a list of (table, field) names
        /// </summary>
        /// <param name="expression">an expression for a new object</param>
        /// <returns>a list of table,field name pairings</returns>
        internal static IEnumerable<PropertyFieldAssignment> GetTableFieldPropertyTuples(this NewExpression expression)
        {
            var arguments = expression.Arguments;
            var members = expression.Members;
            var result = new List<PropertyFieldAssignment>();
            for (var counter = 0; counter < arguments.Count; counter++)
            {
                var exp = arguments[counter].GetExpression();
                var member = arguments[counter].GetMember();
                if (exp == null || member == null)
                {
                    continue;
                }
                var table = exp.Type.GetTableName();
                var field = member.GetFieldName();
                var property = (PropertyInfo)members[counter];
                result.Add(new PropertyFieldAssignment() { TableName = table, FieldName = field, Property = property });
            }
            return result;
        }

        /// <summary>
        /// Retrieves a list of (table, field) names
        /// </summary>
        /// <param name="expression">an expression for a object initializer</param>
        /// <returns>a list of table,field name pairings</returns>
        internal static IEnumerable<PropertyFieldAssignment> GetTableFieldPropertyTuples(this MemberInitExpression expression)
        {
            var bindings = expression.Bindings;
            var result = new List<PropertyFieldAssignment>();
            for (var counter = 0; counter < bindings.Count; counter++)
            {
                if (!(bindings[counter] is MemberAssignment assignment)) continue;
                var member = assignment.Expression;
                var table = member.GetExpression().Type.GetTableName();
                var field = member.GetMember().GetFieldName();
                var property = (PropertyInfo)bindings[counter].Member;
                //var property = members[counter].Name;
                result.Add(new PropertyFieldAssignment() { TableName = table, FieldName = field, Property = property });
            }
            return result;
        }

        internal static bool IsConstantMemberAccess(this MemberExpression expression)
        {
            var innerExpression = expression.Expression;
            return innerExpression != null && innerExpression.NodeType == ExpressionType.Constant;
        }

        internal static bool IsPropertyMemberAccess(this MemberExpression expression)
        {
            var innerExpression = expression.Expression;
            return innerExpression != null && innerExpression.NodeType == ExpressionType.Parameter;
        }

        internal static bool IsOperator(this Expression expression)
        {
            var operators = new ExpressionType[]
            {
                ExpressionType.Add,
                ExpressionType.Subtract,
                ExpressionType.Multiply,
                ExpressionType.Divide
            };
           foreach (var @operator in operators)
           {
               if (expression.NodeType == @operator)
               {
                   return true;
               }
           }

           return false;
        }
        #endregion
    }
}
