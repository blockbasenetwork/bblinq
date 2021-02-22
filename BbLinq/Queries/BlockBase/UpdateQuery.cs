using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using BlockBase.BBLinq.ExtensionMethods;
using BlockBase.BBLinq.Parsers;
using BlockBase.BBLinq.Pocos.ExpressionParser;
using BlockBase.BBLinq.Validators;

namespace BlockBase.BBLinq.Queries.BlockBase
{
    public class UpdateQuery<T> : BlockBaseQuery
    {
        /// <summary>
        /// An object used as reference for the UPDATE operation
        /// </summary>
        public T Record { get; }

        /// <summary>
        /// A condition used as reference for the UPDATE operation
        /// </summary>
        public Expression<Func<T, bool>> Predicate { get; }

        public UpdateQuery(T record, Expression<Func<T, bool>> predicate)
        {
            Record = record;
            Predicate = predicate;
        }

        /// <summary>
        /// The constructor that uses an object as reference
        /// </summary>
        /// <param name="record">the record</param>
        public UpdateQuery(T record)
        {
            Record = record;
        }

        public override string GenerateQuery()
        {
            var tableName = typeof(T).GetTableName();
            var filteredProperties = GetFilteredProperties<T>(false);
            var values = new Dictionary<string, object>();
            string conditionExpression;
            foreach (var property in filteredProperties)
            {
                values.Add(property.GetColumnName(), property.GetValue(Record));
            }

            if (Predicate == null)
            {
                var conditionNode = GenerateIdPredicate(Record);
                conditionExpression = GenerateConditionString(conditionNode);
            }
            else
            {
                var conditionNode = ExpressionParser.ParseExpression(Predicate); 
                PredicateValidator.Validate(conditionNode);
                conditionExpression = GenerateConditionString(conditionNode as BinaryExpressionNode);
            }

            return QueryBuilder.Clear().UpdateValuesInTable(tableName, values, conditionExpression).ToString();
        }
    }
}
