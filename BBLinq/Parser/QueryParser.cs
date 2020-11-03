using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using agap2IT.Labs.BlockBase.BBLinq.DataAnnotations;
using agap2IT.Labs.BlockBase.BBLinq.Enums;
using agap2IT.Labs.BlockBase.BBLinq.ExtensionMethods;
using agap2IT.Labs.BlockBase.BBLinq.Properties;
using agap2IT.Labs.BlockBase.BBLinq.Queries;

namespace agap2IT.Labs.BlockBase.BBLinq.Parser
{
    /// <summary>
    /// Parser used to convert query objects to database queries
    /// </summary>
    internal static class QueryParser
    {
        #region Main Parsing Methods

        /// <summary>
        /// Parses an insert query
        /// </summary>
        /// <typeparam name="T">The entity's type</typeparam>
        /// <param name="query">an insert query</param>
        /// <returns>A record insert query string</returns>
        internal static string ParseInsertRecordQuery<T>(InsertQuery<T> query)
        {
            var queryBuilder = new QueryBuilder();
            queryBuilder = queryBuilder.Insert().WhiteSpace().Into().WhiteSpace();
            queryBuilder = queryBuilder.Table(GetTableName(typeof(T))).WhiteSpace();
            var fields = GetFields(typeof(T));
            var enumerable = fields as string[] ?? fields.ToArray();
            queryBuilder = queryBuilder.WrapFields(enumerable);
            var values = GetValues(enumerable, query);
            queryBuilder = queryBuilder.WhiteSpace().Values(values);
            return queryBuilder.ToString();
        }

        /// <summary>
        /// Parses a select query
        /// </summary>
        /// <typeparam name="T">The entity's type</typeparam>
        /// <param name="query">a select query</param>
        /// <returns>A record select query string</returns>
        internal static string ParseSelectRecordsQuery<T>(SelectQuery<T> query)
        {
            var queryBuilder = new QueryBuilder();
            queryBuilder = queryBuilder.Select().WhiteSpace();
            queryBuilder = ParseSelectableFields(queryBuilder, query.Select);
            queryBuilder = ParseFrom(queryBuilder, query.Origin);
            queryBuilder = ParseJoins(queryBuilder, query.Joins);
            queryBuilder = ParseFilter(queryBuilder, query.Where);
            return queryBuilder.ToString();
        }

        /// <summary>
        /// Parses an update query
        /// </summary>
        /// <typeparam name="T">The entity's type</typeparam>
        /// <param name="query">an update query</param>
        /// <returns>A record update query string</returns>
        internal static string ParseUpdateRecordQuery<T>(UpdateQuery<T> query)
        {
            var queryBuilder = new QueryBuilder();
            queryBuilder = queryBuilder
                .Update(GetTableName(typeof(T)))
                .WhiteSpace()
                .Set(GetFieldValueSet(query.Record));
            queryBuilder = ParseFilter(queryBuilder, query.Where);

            return queryBuilder.ToString();
        }

        /// <summary>
        /// Parses an update query
        /// </summary>
        /// <typeparam name="T">The entity's type</typeparam>
        /// <param name="query">an update query</param>
        /// <returns>A record delete query string</returns>
        internal static string ParseDeleteRecordQuery<T>(DeleteQuery<T> query)
        {
            var queryBuilder = new QueryBuilder();
            queryBuilder = queryBuilder.Delete().WhiteSpace();
            queryBuilder = ParseFrom(queryBuilder, typeof(T));
            queryBuilder = ParseFilter(queryBuilder, query.Where);
            return queryBuilder.ToString();
        }

        #endregion


        #region Secondary Parsing Methods

        /// <summary>
        /// Based on a lambda expression, the fields and their respective table names are gathered
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        internal static QueryBuilder ParseSelectableFields(QueryBuilder builder, LambdaExpression query)
        {
            var @params = query.GetParameterDictionary();
            builder = query.Body switch
            {
                MemberInitExpression initExpression => ParseObjectBindings(builder, initExpression.Bindings, @params),
                NewExpression newExpression => ParseObjectConstructor(builder, newExpression, @params),
                _ => builder
            };
            return builder;
        }

        internal static QueryBuilder ParseJoins(QueryBuilder builder, IEnumerable<LambdaExpression> joins)
        {
            foreach (var onQuery in joins)
            {
                var parameter = onQuery.Parameters;
                builder.WhiteSpace().Join(GetTableName(parameter[^1].Type));
                builder = builder.On().WhiteSpace();
                ParseQuery(builder, onQuery.Body);
            }
            return builder;
        }

        private static QueryBuilder ParseQuery(QueryBuilder builder, Expression query)
        {
            if (!(query is BinaryExpression binaryQuery)) return builder;
            
            var expressionType = binaryQuery.GetExpressionType();
            switch (expressionType)
            {
                case BinaryExpressionCategoryEnum.And:
                case BinaryExpressionCategoryEnum.Or:
                    builder = ParseQuery(builder, binaryQuery.Left);
                    builder = builder.WhiteSpace();
                    builder = expressionType == BinaryExpressionCategoryEnum.And ? builder.And() : builder.Or();
                    builder = builder.WhiteSpace();
                    ParseQuery(builder, binaryQuery.Right);
                    break;
                case BinaryExpressionCategoryEnum.Comparison:
                    var left = binaryQuery.Left;
                    var right = binaryQuery.Right;
                    builder = ParseMemberAccess(builder, left);
                    builder = ParseComparison(builder, binaryQuery.NodeType);
                    builder = ParseMemberAccess(builder, right);
                    break;
                case BinaryExpressionCategoryEnum.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return builder;
        }

        private static QueryBuilder ParseMemberAccess(QueryBuilder builder, Expression expression)
        {
            if (expression.NodeType == ExpressionType.Constant)
            {
                return builder.Append(expression.GetDebugString().Replace("\"", Resources.QUERY_TEXT_WRAPPER_LEFT));
            }
            var exp = expression.GetExpression();
            var member = expression.GetMember();
            return builder.Field(GetTableName(exp.Type), member.Name);
        }

        private static QueryBuilder ParseComparison(QueryBuilder builder, ExpressionType type)
        {
            return type switch
            {
                ExpressionType.Equal => builder.EqualTo(),
                ExpressionType.LessThan => builder.LessThan(),
                ExpressionType.LessThanOrEqual => builder.EqualOrLessThan(),
                ExpressionType.GreaterThan => builder.GreaterThan(),
                ExpressionType.GreaterThanOrEqual => builder.EqualOrGreaterThan(),
                ExpressionType.NotEqual => builder.DifferentFrom(),
                _ => builder
            };
        }

        /// <summary>
        /// Generates a FROM expression based on the table's name attribute or the type's name
        /// </summary>
        /// <param name="builder">the query builder used to generate the query</param>
        /// <param name="tableType">the type associated to the table</param>
        /// <returns>An updated QueryBuilder</returns>
        internal static QueryBuilder ParseFrom(QueryBuilder builder, Type tableType)
        {
            builder.From(GetTableName(tableType));
            return builder;
        }

        internal static QueryBuilder ParseFilter(QueryBuilder builder, LambdaExpression query)
        {
            if(query != null) builder = ParseQuery(builder.WhiteSpace().Where().WhiteSpace(), query.Body);
            return builder;
        }

        #endregion

        #region General Parser

        /// <summary>
        /// Converts the expression from an object construct
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="expression"></param>
        /// <param name="params"></param>
        /// <returns></returns>
        private static QueryBuilder ParseObjectConstructor(QueryBuilder builder, NewExpression expression, Dictionary<string, Type> @params)
        {
            var assignments = expression.Arguments;
            var last = assignments.Last();
            foreach (var expression1 in assignments)
            {
                var memberAssignment = (MemberExpression) expression1;
                {
                    var tableName = GetTableName(@params[(memberAssignment.Expression as ParameterExpression)?.Name ?? string.Empty]);
                    var propertyName = memberAssignment.Member.Name;
                    builder = builder.Field(tableName, propertyName);
                }

                if (memberAssignment != last)
                {
                    builder = builder.FieldSeparator();
                }
            }
            return builder;
        }

        /// <summary>
        /// Converts the object bindings to an expression on a query builder
        /// </summary>
        /// <param name="builder">the query builder</param>
        /// <param name="bindings">a list of bindings associated to each properties</param>
        /// <param name="params">a list of parameters</param>
        /// <returns>the updated query builder</returns>
        private static QueryBuilder ParseObjectBindings(QueryBuilder builder, IReadOnlyCollection<MemberBinding> bindings, Dictionary<string, Type> @params)
        {
            var last = bindings.Last();
            foreach (var memberBinding in bindings)
            {
                if (((MemberAssignment) memberBinding).Expression is MemberExpression bindingExpression)
                {
                    var tableName = GetTableName(@params[(bindingExpression.Expression as ParameterExpression)?.Name ?? string.Empty]);
                    var propertyName = bindingExpression.Member.Name;
                    builder = builder.Field(tableName, propertyName);
                }

                if (memberBinding != last)
                {
                    builder = builder.FieldSeparator();
                }
            }
            return builder;
        }

        /// <summary>
        /// Fetches the table's name
        /// </summary>
        /// <param name="tableType">the table's type</param>
        /// <returns>the table's name</returns>
        private static string GetTableName(Type tableType)
        {
            var tableName = tableType.GetTableName();
            if (tableName == string.Empty)
            {
                tableName = tableType.Name;
            }
            return tableName;
        }

        private static IEnumerable<string> GetFields(Type type)
        {
            return type.GetProperties().Select(prop => prop.Name).ToList();
        }

        private static IEnumerable<string> GetValues<T>(IEnumerable<string> fields, InsertQuery<T> expression)
        {
            var lst = new List<string>();
            var properties = typeof(T).GetProperties();
            foreach (var field in fields)
            {
                var property = properties.FirstOrDefault(x => x.Name == field);
                if (property == null) continue;
                var value = property.GetValue(expression.Record)?.ToString();
                if (property.PropertyType == typeof(string)) 
                    value = $"{Resources.QUERY_TEXT_WRAPPER_LEFT}{value}{Resources.QUERY_TEXT_WRAPPER_RIGHT}";
                lst.Add(value);
            }
            return lst;
        }

        private static List<(string, string)> GetFieldValueSet<T>(T record)
        {
            var valueSets = new List<(string, string)>();
            var properties = typeof(T).GetProperties().Where(x => !x.IsPrimaryKey());
            foreach (var property in properties)
            {
                var value = property.GetValue(record)?.ToString();
                if (property.PropertyType == typeof(string))
                    value = $"{Resources.QUERY_TEXT_WRAPPER_LEFT}{value}{Resources.QUERY_TEXT_WRAPPER_RIGHT}";

                valueSets.Add((property.Name, value));
            }
            return valueSets;
        }
        #endregion
    }
}
