using BlockBase.BBLinq.ExtensionMethods;
using BlockBase.BBLinq.Parsers;
using BlockBase.BBLinq.Pocos.ExpressionParser;
using BlockBase.BBLinq.Validators;
using System;
using System.Linq.Expressions;

namespace BlockBase.BBLinq.Queries.BlockBase
{
    public class DeleteQuery<T> : BlockBaseQuery
    {


        /// <summary>
        /// An object used as reference for the DELETE operation
        /// </summary>
        public T Record { get; }

        /// <summary>
        /// A condition used as reference for the DELETE operation
        /// </summary>
        public Expression<Func<T, bool>> Predicate { get; }

        /// <summary>
        /// Constructor for 
        /// </summary>
        public DeleteQuery()
        {
        }

        /// <summary>
        /// Constructor used with the condition
        /// </summary>
        public DeleteQuery(T record)
        {
            Record = record;
        }

        /// <summary>
        /// Constructor used with the condition
        /// </summary>
        public DeleteQuery(Expression<Func<T, bool>> predicate)
        {
            Predicate = predicate;
        }

        public override string GenerateQuery()
        {
            var type = typeof(T);
            var tableName = type.GetTableName();
            QueryBuilder.Clear();

            if (Record == null && Predicate == null)
            {
                QueryBuilder.DeleteAllRecords(tableName);
            }
            else if (Record != null)
            {
                var conditionNode = GenerateIdPredicate(Record);
                PredicateValidator.Validate(conditionNode);
                var conditionExpression = GenerateConditionString(conditionNode);
                QueryBuilder.DeleteRecordWithCondition(tableName, conditionExpression);
            }
            else if (Predicate != null)
            {
                var conditionNode = ExpressionParser.ParseExpression(Predicate);
                PredicateValidator.Validate(conditionNode);
                var conditionExpression = GenerateConditionString(conditionNode as BinaryExpressionNode);
                QueryBuilder.DeleteRecordWithCondition(tableName, conditionExpression);

            }
            return QueryBuilder.ToString();
        }



    }
}
