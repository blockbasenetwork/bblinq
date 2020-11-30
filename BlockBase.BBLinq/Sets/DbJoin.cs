using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using BlockBase.BBLinq.ExtensionMethods;

namespace BlockBase.BBLinq.Sets
{
    public class DbJoin
    {
        /// <summary>
        /// Generates a join based on a list of existing types and a new type
        /// </summary>
        /// <param name="existingTypes">the list of existing types</param>
        /// <param name="newType">the new type</param>
        /// <returns>a join predicate</returns>
        protected LambdaExpression GenerateJoinExpression(IEnumerable<Type> existingTypes, Type newType)
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
