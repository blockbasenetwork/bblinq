using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using BlockBase.BBLinq.Context;
using BlockBase.BBLinq.ExtensionMethods;
using BlockBase.BBLinq.Pocos;
using BlockBase.BBLinq.QueryExecutors;

namespace BlockBase.BBLinq.Sets
{
    public abstract class DbSet
    {

    }

    public class DbSet<TQueryExecutor> : DbSet where TQueryExecutor : SqlQueryExecutor
    {
        protected TQueryExecutor Executor => ContextCache.Instance.Get<TQueryExecutor>("executor");

        /// <summary>
        /// Parses a select body
        /// </summary>
        /// <param name="selectBody"></param>
        /// <returns></returns>
        protected Dictionary<FieldValue, PropertyInfo> ParseSelectBody<T>(Expression selectBody)
        {
            if (selectBody != null)
            {
                return (selectBody as LambdaExpression)?.Body switch
                {
                    MemberInitExpression initExpression => initExpression.GetFieldPropertyPairs(),
                    NewExpression newExpression => newExpression.GetFieldPropertyPairs(),
                    _ => null
                };
            }
            var properties = typeof(T).GetProperties();
            var tableName = typeof(T).GetTableName();
            var result = new Dictionary<FieldValue, PropertyInfo>();
            foreach (var property in properties)
            {
                var field = new FieldValue() { Table = tableName, Field = property.GetFieldName() };
                result.Add(field, property);
            }
            return result;
        }
    }
}
