using System.Linq.Expressions;
using BlockBase.BBLinq.Builders;
using BlockBase.BBLinq.ExtensionMethods;
using BlockBase.BBLinq.Parser;

namespace BlockBase.BBLinq.Queries
{
    public class UpdateQuery<T>
    {
        public T Record { get; }
        public LambdaExpression Where { get; }
        public UpdateQuery(T record, LambdaExpression where)
        {
            Record = record;
            Where = where;
        }

        public UpdateQuery(T record)
        {
            Record = record;
        }

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
