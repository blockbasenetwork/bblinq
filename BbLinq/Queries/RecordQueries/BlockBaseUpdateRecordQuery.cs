using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using BlockBase.BBLinq.Builders;
using BlockBase.BBLinq.Enumerables;
using BlockBase.BBLinq.ExtensionMethods;
using BlockBase.BBLinq.Parsers;
using BlockBase.BBLinq.Pocos.Nodes;
using BlockBase.BBLinq.Queries.Base;

namespace BlockBase.BBLinq.Queries.RecordQueries
{
    public class BlockBaseUpdateRecordQuery<T> : IQuery
    {
        public T Record { get; }

        public ExpressionNode Condition { get; private set; }

        public BlockBaseUpdateRecordQuery(T record)
        {
            Record = record;
            Condition = GenerateConditionFromObject(record);
        }

        public BlockBaseUpdateRecordQuery(T record, ComparisonNode condition)
        {
            Record = record;
            Condition = condition;
        }

        public BlockBaseUpdateRecordQuery(T record, Expression<Func<T,bool>> condition)
        {
            Record = record;
            var expressionParser = new ExpressionParser();
            Condition = expressionParser.Reduce(expressionParser.ParseExpression(condition)).Item1;
        }

        protected ComparisonNode GenerateConditionFromObject(object obj)
        {
            var primaryKey = obj.GetType().GetPrimaryKeyProperties()[0];
            var leftNode = new PropertyNode(primaryKey);
            var rightNode = new ValueNode(primaryKey.GetValue(Record));
            return new ComparisonNode(BlockBaseOperator.EqualTo, leftNode, rightNode);
        }

        public string GenerateQueryString()
        {
            var tableName = typeof(T).GetTableName();
            var queryBuilder = new BlockBaseQueryBuilder();
            var values = GetValuesToUpdate();
            queryBuilder.Update(tableName, values, Condition );
            return queryBuilder.ToString();
        }

        private (string, object)[] GetValuesToUpdate()
        {
            var type = typeof(T);

            return null;
        }

        protected PropertyInfo[] GetFilteredProperties<T>(bool addPrimaryKey = true)
        {
            var properties = typeof(T).GetProperties();
            var filteredProperties = new List<PropertyInfo>();
            foreach (var property in properties)
            {
                if (!addPrimaryKey)
                {
                    if (property.IsPrimaryKey())
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
