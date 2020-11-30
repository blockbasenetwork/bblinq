using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using BlockBase.BBLinq.Pocos;

namespace BlockBase.BBLinq.ExtensionMethods
{
    public static class ExpressionExtensionMethods
    {
        /// <summary>
        /// Checks if the expression is an operator
        /// </summary>
        /// <param name="expression">The expression</param>
        /// <returns>true if it is an operator. false otherwise</returns>
        public static bool IsOperator(this Expression expression)
        {
            var operators = new []
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

        /// <summary>
        /// Checks if the expression is a constant member access
        /// </summary>
        /// <param name="expression">The expression</param>
        /// <returns>true if it is a constant member access. false otherwise</returns>
        public static bool IsConstantMemberAccess(this MemberExpression expression)
        {
            var innerExpression = expression.Expression;
            return innerExpression != null && innerExpression.NodeType == ExpressionType.Constant;
        }

        /// <summary>
        /// Checks if the expression is a property member access
        /// </summary>
        /// <param name="expression">The expression</param>
        /// <returns>true if it is a property member access. false otherwise</returns>
        public static bool IsPropertyMemberAccess(this MemberExpression expression)
        {
            var innerExpression = expression.Expression;
            return innerExpression != null && innerExpression.NodeType == ExpressionType.Parameter;
        }

        public static FieldValue[] GetFields(this NewExpression expression)
        {
            var arguments = expression.Arguments;
            var result = new List<FieldValue>();
            foreach (Expression argument in arguments)
            {
                if (!(argument is MemberExpression member)) continue;
                result.Add(member.ToFieldValue());

            }
            return result.ToArray();
        }

        public static FieldValue[] GetFields(this MemberInitExpression expression)
        {
            var bindings = expression.Bindings;
            var result = new List<FieldValue>();
            foreach (var binding in bindings)
            {
                if (!(binding is MemberAssignment assignment)) continue;
                var member = assignment.Expression as MemberExpression;
                result.Add(member.ToFieldValue());
            }
            return result.ToArray();
        }

        public static FieldValue ToFieldValue(this MemberExpression expression)
        {
            var table = expression.Member.DeclaringType.GetTableName();
            var field = expression.Member.GetFieldName();
            return new FieldValue(){Table = table, Field = field};
        }

        public static Dictionary<FieldValue, PropertyInfo> GetFieldPropertyPairs(this MemberInitExpression expression)
        {
            var bindings = expression.Bindings;
            var result = new Dictionary<FieldValue, PropertyInfo>();
            for (var counter = 0; counter < bindings.Count; counter++)
            {
                if (!(bindings[counter] is MemberAssignment assignment)) continue;
                var member = assignment.Expression as MemberExpression;
                var field = member.ToFieldValue();
                var property = (PropertyInfo)bindings[counter].Member;
                result.Add(field, property);
            }
            return result;
        }

        public static Dictionary<FieldValue, PropertyInfo> GetFieldPropertyPairs(this NewExpression expression)
        {
            var arguments = expression.Arguments;
            var members = expression.Members;
            var result = new Dictionary<FieldValue, PropertyInfo>();
            for (var counter = 0; counter < arguments.Count; counter++)
            {
                if (!(arguments[counter] is MemberExpression member)) continue;
                var field = member.ToFieldValue();
                var property = (PropertyInfo)members[counter];
                result.Add(field, property);
            }

            return result;
        }
    }
}
