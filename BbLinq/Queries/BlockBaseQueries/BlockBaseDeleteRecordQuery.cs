using System;
using System.Linq.Expressions;
using BlockBase.BBLinq.ExtensionMethods;
using BlockBase.BBLinq.Queries.BaseQueries;
using BlockBase.BBLinq.QueryBuilders;

namespace BlockBase.BBLinq.Queries.BlockBaseQueries
{
    public class BlockBaseDeleteRecordQuery<T> : DeleteRecordQuery<T>
    {
        public BlockBaseDeleteRecordQuery(T record) : base(record)
        {
        }

        public BlockBaseDeleteRecordQuery(LambdaExpression condition) : base(condition)
        {
        }

        public override string ToString()
        {
            var queryBuilder = new BbSqlQueryBuilder();
            var tableName = typeof(T).GetTableName();
            queryBuilder.Clear().Delete().WhiteSpace().From().WhiteSpace().Append(tableName);

            if (Condition != null)
            {
                queryBuilder.WhiteSpace().Where().WhiteSpace().ParseQuery(Condition.Body);
            }
            else if (Record != null)
            {
                queryBuilder.WhiteSpace().Where().WhiteSpace().GenerateConditionFromObject(Record);
            }
            return queryBuilder.End().ToString();
        }
    }
}
