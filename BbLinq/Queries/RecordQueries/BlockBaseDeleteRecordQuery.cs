using System;
using System.Linq.Expressions;
using BlockBase.BBLinq.Builders;
using BlockBase.BBLinq.Enumerables;
using BlockBase.BBLinq.ExtensionMethods;
using BlockBase.BBLinq.Parsers;
using BlockBase.BBLinq.Pocos.Nodes;
using BlockBase.BBLinq.Queries.Base;

namespace BlockBase.BBLinq.Queries.RecordQueries
{
    public class BlockBaseDeleteRecordQuery<T> : BlockBaseQuery, IQuery
    {
        public T Record { get; }

        public ExpressionNode Condition { get; private set; }

        public BlockBaseDeleteRecordQuery(T record)
        {
            Record = record;
            Condition = GenerateConditionFromObject(record);
        }

        public BlockBaseDeleteRecordQuery(ComparisonNode condition)
        {
            Condition = condition;
        }

        public BlockBaseDeleteRecordQuery()
        {

        }

        public BlockBaseDeleteRecordQuery(Expression<Func<T,bool>> condition)
        {
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
            queryBuilder.Delete(tableName, Condition);
            return queryBuilder.ToString();
        }
    }
}
