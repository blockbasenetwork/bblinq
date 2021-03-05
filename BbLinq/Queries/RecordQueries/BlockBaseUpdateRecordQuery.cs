using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using BlockBase.BBLinq.Builders;
using BlockBase.BBLinq.Enumerables;
using BlockBase.BBLinq.ExtensionMethods;
using BlockBase.BBLinq.Parsers;
using BlockBase.BBLinq.Pocos.Nodes;
using BlockBase.BBLinq.Queries.Base;

namespace BlockBase.BBLinq.Queries.RecordQueries
{
    public class BlockBaseUpdateRecordQuery<T> : BlockBaseQuery, IQuery
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
            Condition = expressionParser.Reduce(expressionParser.ParseExpression(condition));
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
            var filteredValues = GetFilteredProperties<T>(false);
            var values = new List<(string, object)>();
            foreach (var value in filteredValues)
            {
                values.Add((value.Name, value.GetValue(Record)));
            }
            return values.ToArray();
        }

        
    }
}
