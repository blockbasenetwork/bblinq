using System;
using System.Collections.Generic;
using System.Reflection;
using BlockBase.BBLinq.Exceptions;
using BlockBase.BBLinq.ExtensionMethods;
using BlockBase.BBLinq.Pocos.ExpressionParser;
using BlockBase.BBLinq.Queries.Base;
using BlockBase.BBLinq.QueryBuilder;

namespace BlockBase.BBLinq.Queries.BlockBase
{
    public class BlockBaseQuery : Query
    {
        protected BlockBaseQueryBuilder QueryBuilder;
        public BlockBaseQuery()
        {
            QueryBuilder = new BlockBaseQueryBuilder();
        }

        public override string GenerateQuery()
        {
            throw new System.NotImplementedException();
        }

        public virtual BinaryExpressionNode GenerateIdPredicate(object record)
        {
            var type = record.GetType();
            var primaryKeys = record.GetType().GetPrimaryKeyProperties();
            if (primaryKeys == null || primaryKeys.Length == 0)
            {
                throw new NoPrimaryKeyFoundException(type.Name);
            }
            var primaryKey = primaryKeys[0];
            var value = primaryKey.GetValue(record);
            var conditionExpression = new BinaryExpressionNode()
            {
                Left = new PropertyExpression() { Column = primaryKey, Table = type },
                Operator = ExpressionOperator.Equals,
                Right = new ValueExpression() { Value = value }
            };
            return conditionExpression;
        }

        public virtual string GenerateConditionString(BinaryExpressionNode node)
        {
            return ParseBinaryExpressionNode(node);
        }

        protected virtual string ParseValueExpression(ValueExpression expression)
        {
            var queryBuilder = new BlockBaseQueryBuilder();
            queryBuilder.WrapValue(expression.Value);
            return queryBuilder.ToString();
        }

        protected virtual string ParsePropertyExpression(PropertyExpression expression)
        {
            var tableName = expression.Table.GetTableName();
            var columnName = expression.Column.GetColumnName();

            var queryBuilder = new BlockBaseQueryBuilder();
            queryBuilder.Append(tableName).TableColumnSeparator().Append(columnName);
            return queryBuilder.ToString();
        }

        protected virtual string ParseOperator(ExpressionOperator @operator)
        {
            switch (@operator)
            {
                case ExpressionOperator.And:
                    return "&&";
                case ExpressionOperator.DifferentFrom:
                    return "!=";
                case ExpressionOperator.Equals:
                    return "=";
                case ExpressionOperator.GreaterOrEqualTo:
                    return ">=";
                case ExpressionOperator.GreaterThan:
                    return ">";
                case ExpressionOperator.LessThan:
                    return "<";
                case ExpressionOperator.LessOrEqualTo:
                    return ">=";
                case ExpressionOperator.Or:
                    return "||";
            }
            return string.Empty;
        }

        protected virtual string ParseBinaryExpressionNode(BinaryExpressionNode node)
        {
            var leftResult = string.Empty;
            var rightResult = string.Empty;
            var @operator = ParseOperator(node.Operator);
            switch (node.Left)
            {
                case BinaryExpressionNode binary:
                    leftResult = ParseBinaryExpressionNode(binary);
                    break;
                case ValueExpression value:
                    leftResult = ParseValueExpression(value);
                    break;
                case PropertyExpression property:
                    leftResult = ParsePropertyExpression(property);
                    break;
            }
            switch (node.Right)
            {
                case BinaryExpressionNode binary:
                    rightResult = ParseBinaryExpressionNode(binary);
                    break;
                case ValueExpression value:
                    rightResult = ParseValueExpression(value);
                    break;
                case PropertyExpression property:
                    rightResult = ParsePropertyExpression(property);
                    break;
            }

            if (leftResult == string.Empty || rightResult == string.Empty)
            {
                throw new InvalidExpressionNodeException(node);
            }

            var queryBuilder = new BlockBaseQueryBuilder().Append(leftResult).WhiteSpace().Append(@operator)
                .WhiteSpace().Append(rightResult);
            return queryBuilder.ToString();
        }

        protected bool IsValidColumn(PropertyInfo property)
        {
            var type = property.IsNullable() ? property.PropertyType.GetNullableType() : property.PropertyType;

            return !property.IsVirtualOrStaticOrAbstract() &&
                   type.IsAcceptableDataType() &&
                   !property.IsNotMapped();
        }


        protected PropertyInfo[] GetFilteredProperties<T>(bool addPrimaryKey = true)
        {
            var properties = typeof(T).GetProperties();
            var filteredProperties = new List<PropertyInfo>();
            foreach (var property in properties)
            {
                if (!addPrimaryKey)
                {
                    var primaryKey = property.GetPrimaryKeys();
                    if (primaryKey != null && primaryKey.Length > 0)
                    {
                        continue;
                    }
                }

                if (IsValidColumn(property))
                {
                    filteredProperties.Add(property);
                }
            }
            return filteredProperties.ToArray();
        }
    }
}
