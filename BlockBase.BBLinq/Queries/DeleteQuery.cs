using System.Linq.Expressions;
using BlockBase.BBLinq.Builders;
using BlockBase.BBLinq.ExtensionMethods;
using BlockBase.BBLinq.Parser;

namespace BlockBase.BBLinq.Queries
{
    /// <summary>
    /// A DELETE query used to perform fluent SQL deletions.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DeleteQuery<T>
    {
        /// <summary>
        /// An object used as reference for the DELETE operation
        /// </summary>
        public T Record { get; }

        /// <summary>
        /// A condition used as reference for the DELETE operation
        /// </summary>
        public LambdaExpression Where { get; }

        /// <summary>
        /// Constructor used with the condition
        /// </summary>
        /// <param name="where">condition for delete operation</param>
        public DeleteQuery(LambdaExpression where)
        {
            Where = where;
        }

        /// <summary>
        /// Constructor used with the record
        /// </summary>
        /// <param name="record">record to delete</param>
        public DeleteQuery(T record)
        {
            Record = record;
        }

        /// <summary>
        /// Returns the SQL query built from the request
        /// </summary>
        /// <returns>A delete sql query string</returns>
        public override string ToString()
        {
            var tableName = typeof(T).GetTableName();
            var queryBuilder = new BbSqlQueryBuilder();
            var condition = string.Empty;

            queryBuilder.Delete().WhiteSpace().From(tableName);

            if (Where != null)
            {
                condition = ExpressionParser.ParseQuery(Where.Body);
            }
            else if (Record != null)
            {
                condition = ExpressionParser.GenerateConditionFromObject(Record);
            }

            if (condition != string.Empty)
            {
                queryBuilder.WhiteSpace().Where(condition);
            }
            return queryBuilder.End().ToString();
        }
    }
}
