using System.Linq.Expressions;
using BlockBase.BBLinq.Builders;
using BlockBase.BBLinq.ExtensionMethods;
using BlockBase.BBLinq.Parser;

namespace BlockBase.BBLinq.Queries
{
    public class UpdateQuery<T>
    {
        /// <summary>
        /// An object used as reference for the DELETE operation
        /// </summary>
        public T Record { get; }

        /// <summary>
        /// A condition used as reference for the DELETE operation
        /// </summary>
        public LambdaExpression Where { get; }
        public UpdateQuery(T record, LambdaExpression where)
        {
            Record = record;
            Where = where;
        }

        /// <summary>
        /// The constructor that uses an object as reference
        /// </summary>
        /// <param name="record">the record</param>
        public UpdateQuery(T record)
        {
            Record = record;
        }

        /// <summary>
        /// Returns the SQL query built from the request
        /// </summary>
        /// <returns>A update sql query string</returns>
        public override string ToString()
        {
            var queryBuilder = new BbSqlQueryBuilder();
            var type = typeof(T);
            var tableName = type.GetTableName();
            var fieldValuePairings = Record.GetFieldsAndValues();
            var fields = new string[fieldValuePairings.Length];
            var values = new string[fieldValuePairings.Length];
            string condition = string.Empty;

            for (var counter = 0; counter < fieldValuePairings.Length; counter++)
            {
                fields[counter] = fieldValuePairings[counter].FieldName;
                values[counter] = ExpressionParser.WrapValue(fieldValuePairings[counter].Value);
            }

            queryBuilder.Update(tableName).WhiteSpace().Set(fields, values);

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
