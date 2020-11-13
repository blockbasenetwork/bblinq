using BlockBase.BBLinq.Builders;
using BlockBase.BBLinq.ExtensionMethods;
using BlockBase.BBLinq.Parser;

namespace BlockBase.BBLinq.Queries
{
    public class InsertQuery<T>
    {
        /// <summary>
        /// An object used as reference for the DELETE operation
        /// </summary>
        public T Record { get; }

        /// <summary>
        /// Constructor used with the record
        /// </summary>
        /// <param name="record">the record to delete</param>
        public InsertQuery(T record)
        {
            Record = record;
        }

        /// <summary>
        /// Returns the SQL query built from the request
        /// </summary>
        /// <returns>A insert sql query string</returns>
        public override string ToString()
        {
            var type = typeof(T);
            var tableName = type.GetTableName();
            var fieldValuePairings = Record.GetFieldsAndValues();
            var fields = new string[fieldValuePairings.Length];
            var values = new string[fieldValuePairings.Length];
            
            for (var counter = 0; counter < fieldValuePairings.Length; counter++)
            {
                fields[counter] = fieldValuePairings[counter].FieldName;
                values[counter] = ExpressionParser.WrapValue(fieldValuePairings[counter].Value);
            }

            var queryBuilder = new BbSqlQueryBuilder();

            queryBuilder.Insert().WhiteSpace()
                .Into(tableName).WhiteSpace()
                .Fields(fields).WhiteSpace()
                .Values(values);

            return queryBuilder.End().ToString();
        }
    }
}
