using BBLinq.Enumerables;
using BBLinq.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows.Markup;

namespace BBLinq.Parser
{
    internal class QueryParser
    {
        internal static string ParseInsert(object obj)
        {
            var type = obj.GetType();
            var properties = ParseInsertValues(obj);
            
            var content = $"INSERT INTO {type.Name}";
            content += " (";
            content += properties.Item1;
            content += string.Join(", ", properties);
            content += ") ";
            content += "VALUES";
            content += " (";
            content += properties.Item2;
            content += ") ";
            return content;
        }

        private static (string, string) ParseInsertValues(object obj)
        {
            var properties = string.Empty;
            var values = string.Empty;
            var propertyItems = obj.GetType().GetProperties();
            foreach(var prop in propertyItems)
            {
                var value = prop.GetValue(obj);
                if (value == null || (value is string @string && @string == string.Empty))
                {
                    continue;
                }
                values += (value is string s) ? $"'{s}'" : value;
            }
            return (properties, values);
        }

        private static string ParseInsertValues(object obj, List<string> properties)
        {
            var content = string.Empty;
            foreach(var propName in properties)
            {
                var prop = obj.GetType().GetProperty(propName);
                var val = prop.GetValue(obj);
                content += (val is string) ? $"'{val}'" : $"{val}";
                if (propName != properties.Last()) content += ", ";
            }
            return content;
        }

        private static List<string> ListProperties(Type type)
        {
            var content = new List<string>();
            var properties = type.GetProperties();
            foreach (PropertyInfo prop in type.GetProperties())
            {
                content.Add(prop.Name);
            }
            return content;
        }

        internal static string ParseFilter(LambdaExpression filterExpression)
        {
            var content = "WHERE ";
            var @params = filterExpression.GetParameterDictionary();
            var sqlCharacters = new Dictionary<string, string>
            {
                { "\"", "\'" },
                { "==", "=" }
            };
            return content+ParseQuery(filterExpression.Body, @params, sqlCharacters);
        }

        internal static string ParseJoins(IEnumerable<LambdaExpression> joinExpressions)
        {
            string content = string.Empty;
            if (joinExpressions == default || joinExpressions.Count() == 0) return content;
            content += " ";
            foreach (var join in joinExpressions)
            {
                content += ParseJoin(join);
                if (join != joinExpressions.Last()) content += " ";
            }
            return content;

        }

        internal static string ParseJoin(LambdaExpression join)
        {
            var content = string.Empty;
            var @params = join.GetParameterDictionary();
            var expressionBody = join.Body;
            var sqlCharacters = new Dictionary<string, string>();
            sqlCharacters.Add("\"", "\'");
            sqlCharacters.Add("==", "=");
            var tableName = join.Parameters[join.Parameters.Count - 1].Type.Name;
            content += "JOIN " + tableName + " ON " + ParseQuery(expressionBody, @params, sqlCharacters);
            return content;
        }

        internal static string End()
        {
            return ";";
        }

        #region Query Parser
        private static string ParseQuery(Expression expression, Dictionary<string, Type> @params, Dictionary<string, string> sqlCharacters)
        {
            var expressionType = expression.GetExpressionType();
            if (expressionType == BinaryExpressionTypeEnum.And || expressionType == BinaryExpressionTypeEnum.Or)
            {
                var castExpression = expression as BinaryExpression;
                return ParseQuery(castExpression.Left, @params, sqlCharacters) + 
                       (expressionType == BinaryExpressionTypeEnum.And ? " AND ":" OR ") + 
                       ParseQuery(castExpression.Right, @params, sqlCharacters);
            }
            else if (expressionType == BinaryExpressionTypeEnum.Comparison)
            {
                var debugString = ParseComparisonQuery(expression);
                foreach (KeyValuePair<string, Type> entity in @params)
                {
                    debugString = debugString.Replace("$" + entity.Key + ".", entity.Value.Name + ".");
                }
                foreach (KeyValuePair<string, string> @chars in sqlCharacters)
                {
                    debugString = debugString.Replace(@chars.Key, @chars.Value);
                }
                return debugString;
            }
            return string.Empty;
        }

        public static string ParseComparisonQuery(Expression expression)
        {
            var content = string.Empty;
            var left = ParseField((expression as BinaryExpression).Left);
            var right = ParseField((expression as BinaryExpression).Right);
            content += left;
            switch (expression.NodeType)
            {
                case ExpressionType.Equal: content += " == "; break;
                case ExpressionType.LessThan: content += " < "; break;
                case ExpressionType.LessThanOrEqual: content += " >= "; break;
                case ExpressionType.GreaterThan: content += " > "; break;
                case ExpressionType.GreaterThanOrEqual: content += " >= "; break;
            }
            content += right;
            return content;
        }

        private static string ParseField(Expression field)
        {
            switch(field.NodeType)
            {
                case ExpressionType.MemberAccess: 
                    return (field.GetExpression() is ConstantExpression)? 
                                (field as MemberExpression).GetValue().ToString():
                                field.GetDebugString();
                case ExpressionType.Convert: return "$"+(field as UnaryExpression).Operand.ToString();
                case ExpressionType.Constant: return field.ToString();
                   
            }
            return string.Empty;
        }
        #endregion

        #region Object element parsing
        private static string ParseObjectBindings(IReadOnlyCollection<MemberBinding> bindings, Dictionary<string, Type> @params)
        {
            string content = string.Empty;
            foreach (MemberAssignment exp in bindings)
            {
                var bdField = exp.Expression.GetDebugString();
                var anonymousTableName = (exp.Expression as MemberExpression).Expression.ToString();
                var realTableName = @params[anonymousTableName].Name;
                bdField = bdField.Replace("$" + anonymousTableName, realTableName);
                content += bdField;
                if (exp != bindings.Last())
                {
                    content += ", ";
                }
            }
            return content;
        }

        private static string ParseObjectConstructor(NewExpression expression, Dictionary<string, Type> @params)
        {
            string content = string.Empty;
            var memberArguments = new Dictionary<Expression, MemberInfo>();
            for (var memberCounter = 0; memberCounter < expression.Arguments.Count(); memberCounter++)
            {
                memberArguments.Add(expression.Arguments[memberCounter], expression.Members[memberCounter]);
            }
            foreach (var argument in memberArguments)
            {
                var bdField = argument.Key.GetDebugString();
                var anonymousTableName = (argument.Key as MemberExpression).Expression.ToString();
                var realTableName = @params[anonymousTableName].Name;
                bdField = bdField.Replace("$" + anonymousTableName, realTableName);
                content += bdField;
                if (argument.Key != memberArguments.Last().Key)
                {
                    content += ", ";
                }
            }
            return content;
        }

        #endregion

        #region From 
        internal static string From(Type type)
        {
            return $"FROM {type.Name}";
        }

        internal static string From<T>()
        {
            return $"FROM {typeof(T).Name}";
        }

        internal static string From(string tableName)
        {
            return $"FROM {tableName}";
        }
        #endregion

        #region Select 
        internal static string ParseSelect(LambdaExpression selectExpression)
        {
            var content = "SELECT ";
            var @params = selectExpression.GetParameterDictionary();
            content += (selectExpression.Body is MemberInitExpression initBody) ?
                            ParseObjectBindings(initBody.Bindings, @params) :
                            (selectExpression.Body is NewExpression newBody) ?
                                ParseObjectConstructor((newBody), @params) : "";
            return content;
        }
        internal static string SelectWhole<T>()
        {
            return $"SELECT {typeof(T).Name}.*";
        }
        #endregion
    }
}
