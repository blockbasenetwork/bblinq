using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using BlockBase.BBLinq.ExtensionMethods;
using BlockBase.BBLinq.Interfaces;
using BlockBase.BBLinq.QueryExecutors;

namespace BlockBase.BBLinq.Sets
{
    public class BbBaseSet<TResult>: DbSet<BbQueryExecutor>,
        ISkips<TResult>,
        ITakes<TResult>,
        IEncrypts<TResult> where TResult : BbBaseSet<TResult>
    {

        protected int RecordsToSkip;
        protected int RecordLimit;
        protected bool Encrypted;
        protected LambdaExpression Filter;

        /// <summary>
        /// Returns the set to its original form
        /// </summary>
        /// <returns></returns>
        public virtual TResult Clear()
        {
            Filter = null;
            Encrypted = false;
            RecordLimit = -1;
            RecordsToSkip = 0;
            return (TResult)this;
        }

        /// <summary>
        /// Adds a encrypt flag so that the query, when executed is encrypted
        /// </summary>
        /// <returns>the updated BbSet</returns>
        public TResult Encrypt()
        {
            Encrypted = true;
            return (TResult)this;
        }

        /// <summary>
        /// Sets the amount of records the user intends to skip before fetching a result
        /// </summary>
        /// <param name="amount">the amount of records to skip</param>
        /// <returns>the updated set</returns>
        public TResult Skip(int amount)
        {
            RecordsToSkip = amount;
            return (TResult)this;
        }

        /// <summary>
        /// Sets the amount of records the user intends to fetch
        /// </summary>
        /// <param name="amount">the amount of records to fetch</param>
        /// <returns>the updated set</returns>
        public TResult Take(int amount)
        {
            RecordLimit = amount;
            return (TResult)this;
        }

        /// <summary>
        /// Adds a filter on a select query
        /// </summary>
        /// <param name="predicate">the filter</param>
        /// <returns>the updated set</returns>
        protected virtual TResult Where(LambdaExpression predicate)
        {
            Filter = predicate;
            return (TResult)this;
        }

        /// <summary>
        /// Generates a join based on a list of existing types and a new type
        /// </summary>
        /// <param name="existingTypes">the list of existing types</param>
        /// <param name="newType">the new type</param>
        /// <returns>a join predicate</returns>
        protected static LambdaExpression GenerateJoinExpression(IEnumerable<Type> existingTypes, Type newType)
        {

            var leftParameter = Expression.Parameter(newType, newType.Name.ToLower());
            var expressionList = new List<Expression>();
            var @params = new List<ParameterExpression>() { leftParameter };

            foreach (var existingType in existingTypes)
            {
                var oldToNewForeignKey = existingType.GetForeignKey(newType);
                var newToOldForeignKey = newType.GetForeignKey(existingType);
                if (oldToNewForeignKey == null && newToOldForeignKey == null)
                {
                    continue;
                }
                var rightParameter = Expression.Parameter(existingType, existingType.Name.ToLower());
                @params.Add(rightParameter);
                if (oldToNewForeignKey != null)
                {
                    var pk = Expression.Property(leftParameter, newType.GetPrimaryKey());
                    var fk = Expression.Property(rightParameter, oldToNewForeignKey);
                    var expression = Expression.Equal(pk, fk);
                    expressionList.Add(expression);

                }
                else
                {
                    var pk = Expression.Property(rightParameter, existingType.GetPrimaryKey());
                    var fk = Expression.Property(leftParameter, newToOldForeignKey);
                    var expression = Expression.Equal(pk, fk);
                    expressionList.Add(expression);
                }
            }

            var join = expressionList[0];
            for (var i = 1; i < expressionList.Count; i++)
            {
                join = Expression.And(join, expressionList[i]);
            }
            var exp = Expression.Lambda(join, @params);
            return exp;
        }
    }
}
