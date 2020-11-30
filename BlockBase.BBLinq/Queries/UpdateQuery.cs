using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BlockBase.BBLinq.Builders;
using BlockBase.BBLinq.ExtensionMethods;
using BlockBase.BBLinq.Pocos;

namespace BlockBase.BBLinq.Queries
{
    public class UpdateQuery<T> : BbQuery
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
            var tableName = typeof(T).GetTableName();
            var fieldValuePairings = Record.GetFields();
            var primaryKey = typeof(T).GetPrimaryKey();
            var ignore = new List<FieldValue>();
            if (primaryKey != null)
            {
                ignore.Add(new FieldValue(){Table = tableName, Field = primaryKey.GetFieldName()});
            }
            var fields = new List<string>();
            var values = new List<string>();

            for (var counter = 0; counter < fieldValuePairings.Length; counter++)
            {
                var field = fieldValuePairings[counter];
                if (ignore.Any(x => x.Table == field.Table && x.Field == field.Field)) continue;
                fields.Add(field.Field);
                values.Add(new BbSqlQueryBuilder().WrapValue(field.Value));
            }

            QueryBuilder.Clear().Update(tableName).WhiteSpace().Set(fields.ToArray(), values.ToArray());

            if (Where != null)
            {
                var query = new BbSqlQueryBuilder().ParseQuery(Where.Body);
                QueryBuilder.WhiteSpace().Where().WhiteSpace().Append(query.ToString());
            }
            else if (Record != null)
            {
                QueryBuilder.WhiteSpace().Where().WhiteSpace().GenerateConditionFromObject(Record);
            }
            return QueryBuilder.End().ToString();
        }
    }
}
