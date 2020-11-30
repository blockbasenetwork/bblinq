using System.Linq.Expressions;
using BlockBase.BBLinq.Dictionaries;
using BlockBase.BBLinq.ExtensionMethods;
using BlockBase.BBLinq.Pocos;

namespace BlockBase.BBLinq.Builders
{
    /// <summary>
    /// A sql query builder for BlockBase
    /// </summary>
    public class BbSqlQueryBuilder : SqlQueryBuilder<BbSqlDictionary, BbSqlQueryBuilder>
    {
        #region Simple expressions

        /// <summary>
        /// Appends the expression attributed to Different from
        /// </summary>
        /// <returns>An updated QueryBuilder</returns>
        public BbSqlQueryBuilder DifferentFrom()
        {
            return Append(Dictionary.DifferentFrom);
        }

        /// <summary>
        /// Appends the expression attributed to LESS
        /// </summary>
        /// <returns>An updated QueryBuilder</returns>
        public BbSqlQueryBuilder LessThan()
        {
            return Append(Dictionary.LessThan);
        }

        /// <summary>
        /// Appends the expression attributed to equal or LESS 
        /// </summary>
        /// <returns>An updated QueryBuilder</returns>
        public BbSqlQueryBuilder EqualOrLessThan()
        {
            return Append(Dictionary.EqualOrLessThan);
        }

        /// <summary>
        /// Appends the expression attributed to Greater
        /// </summary>
        /// <returns>An updated QueryBuilder</returns>
        public BbSqlQueryBuilder GreaterThan()
        {
            return Append(Dictionary.GreaterThan);
        }

        /// <summary>
        /// Appends the expression attributed to Equal or Greater
        /// </summary>
        /// <returns>An updated QueryBuilder</returns>
        public BbSqlQueryBuilder EqualOrGreaterThan()
        {
            return Append(Dictionary.EqualOrGreaterThan);
        }

        /// <summary>
        /// Appends the expression attributed to ADD
        /// </summary>
        /// <returns>An updated QueryBuilder</returns>
        public BbSqlQueryBuilder And()
        {
            return Append(Dictionary.And);
        }

        /// <summary>
        /// Adds a DELETE to a Query Builder
        /// </summary>
        /// <returns>The updated query builder</returns>
        public BbSqlQueryBuilder Delete()
        {
            return Append(Dictionary.Delete);
        }


        /// <summary>
        /// Adds a END to a Query Builder
        /// </summary>
        /// <returns>The updated query builder</returns>
        public BbSqlQueryBuilder End()
        {
            return Append(Dictionary.QueryEnd);
        }

        /// <summary>
        /// Adds a EQUALS to a Query Builder
        /// </summary>
        /// <returns>The updated query builder</returns>
        public BbSqlQueryBuilder Equals()
        {
            return Append(Dictionary.ValueEquals);
        }

        /// <summary>
        /// Adds a FROM to a Query Builder
        /// </summary>
        /// <returns>The updated query builder</returns>
        public BbSqlQueryBuilder From()
        {
            return Append(Dictionary.From);
        }

        /// <summary>
        /// Adds a INSERT to a Query Builder
        /// </summary>
        /// <returns>The updated query builder</returns>
        public BbSqlQueryBuilder Insert()
        {
            return Append(Dictionary.Insert);
        }

        /// <summary>
        /// Adds a INTO to a Query Builder
        /// </summary>
        /// <returns>The updated query builder</returns>
        public BbSqlQueryBuilder Into()
        {
            return Append(Dictionary.Into);
        }

        /// <summary>
        /// Adds a JOIN to a Query Builder with the table's name
        /// </summary>
        /// <returns>The updated query builder</returns>
        public BbSqlQueryBuilder Join()
        {
            return Append(Dictionary.Join);
        }

        /// <summary>
        /// Appends the expression attributed to OR
        /// </summary>
        /// <returns>An updated QueryBuilder</returns>
        public BbSqlQueryBuilder On()
        {
            return Append(Dictionary.On);
        }

        /// <summary>
        /// Appends the expression attributed to OR
        /// </summary>
        /// <returns>An updated QueryBuilder</returns>
        public BbSqlQueryBuilder Or()
        {
            return Append(Dictionary.Or);
        }

        /// <summary>
        /// Adds a SELECT to a Query Builder
        /// </summary>
        /// <returns>The updated query builder</returns>
        public BbSqlQueryBuilder Select()
        {
            return Append(Dictionary.Select);
        }


        /// <summary>
        /// Adds a SET to a Query Builder
        /// </summary>
        /// <returns>The updated query builder</returns>
        public BbSqlQueryBuilder Set()
        {
            return Append(Dictionary.Set);
        }

        /// <summary>
        /// Adds a separator between a table and a field to a Query Builder
        /// </summary>
        /// <returns>The updated query builder</returns>
        public BbSqlQueryBuilder FieldSeparator()
        {
            return Append(Dictionary.FieldSeparator);
        }

        /// <summary>
        /// Adds a separator between a table and a field to a Query Builder
        /// </summary>
        /// <returns>The updated query builder</returns>
        public BbSqlQueryBuilder TableFieldSeparator()
        {
            return Append(Dictionary.TableFieldSeparator);
        }

        /// <summary>
        /// Adds a UPDATE to a Query Builder
        /// </summary>
        /// <returns>The updated query builder</returns>
        public BbSqlQueryBuilder Update()
        {
            return Append(Dictionary.Update);
        }


        /// <summary>
        /// Adds a UPDATE to a Query Builder
        /// </summary>
        /// <returns>The updated query builder</returns>
        public BbSqlQueryBuilder Update(string tableName)
        {
            return Update().WhiteSpace().Append(tableName);
        }

        /// <summary>
        /// Adds a SET to a Query Builder
        /// </summary>
        /// <returns>The updated query builder</returns>
        public BbSqlQueryBuilder Set(string[] fields, string[] values)
        {
            if (fields.Length != values.Length)
            {
                return this;
            }
            Append(Dictionary.Set);
            WhiteSpace();
            //WrapLeftListSide();
            var last = fields[^1];
            for (var counter = 0; counter < fields.Length; counter++)
            {
                Append(fields[counter]).Equals().Append(values[counter]);
                if (fields[counter] != last)
                {
                    FieldSeparator().WhiteSpace();
                }
            }
            //WrapRightListSide();
            return this;
        }

        /// <summary>
        /// Adds a VALUES to a Query Builder
        /// </summary>
        /// <returns>The updated query builder</returns>
        public BbSqlQueryBuilder Values()
        {
            return Append(Dictionary.Values);
        }


        /// <summary>
        /// Adds a OFFSET to a Query Builder
        /// </summary>
        /// <returns>The updated query builder</returns>
        public BbSqlQueryBuilder Offset()
        {
            return Append(Dictionary.Offset);
        }

        /// <summary>
        /// Adds a LIMIT to a Query Builder
        /// </summary>
        /// <returns>The updated query builder</returns>
        public BbSqlQueryBuilder Limit()
        {
            return Append(Dictionary.Limit);
        }


        /// <summary>
        /// Adds a WHERE to a Query Builder
        /// </summary>
        /// <returns>The updated query builder</returns>
        public BbSqlQueryBuilder Where()
        {
            return Append(Dictionary.Where);
        }

        /// <summary>
        /// Adds a LEFT_LIST_WRAPPER to a Query Builder
        /// </summary>
        /// <returns>The updated query builder</returns>
        public BbSqlQueryBuilder WrapLeftListSide()
        {
            return Append(Dictionary.LeftListWrapper);
        }

        /// <summary>
        /// Adds a RIGHT_LIST_WRAPPER to a Query Builder
        /// </summary>
        /// <returns>The updated query builder</returns>
        public BbSqlQueryBuilder WrapRightListSide()
        {
            return Append(Dictionary.RightListWrapper);
        }

        /// <summary>
        /// Adds a LEFT_TEXT_WRAPPER to a Query Builder
        /// </summary>
        /// <returns>The updated query builder</returns>
        public BbSqlQueryBuilder WrapTextLeft()
        {
            return Append(Dictionary.LeftTextWrapper);
        }

        /// <summary>
        /// Adds a RIGHT_TEXT_WRAPPER to a Query Builder
        /// </summary>
        /// <returns>The updated query builder</returns>
        public BbSqlQueryBuilder WrapTextRight()
        {
            return Append(Dictionary.RightTextWrapper);
        }

        /// <summary>
        /// Adds a CREATE expression
        /// </summary>
        /// <returns>The updated query</returns>
        public BbSqlQueryBuilder Create()
        {
            return Append(Dictionary.Create);
        }

        /// <summary>
        /// Adds a DATABASE expression
        /// </summary>
        /// <returns>The updated query</returns>
        public BbSqlQueryBuilder Database()
        {
            return Append(Dictionary.DataBase);
        }

        /// <summary>
        /// Adds a non-encrypted field expression
        /// </summary>
        /// <returns>The updated query</returns>
        public BbSqlQueryBuilder NonEncryptedField()
        {
            return Append(Dictionary.NonEncryptedField);
        }

        /// <summary>
        /// Adds a ENCRYPTED expression
        /// </summary>
        /// <returns>The updated query</returns>
        public BbSqlQueryBuilder Encrypted()
        {
            return Append(Dictionary.Encrypted);
        }

        /// <summary>
        /// Adds a REFERENCES expression
        /// </summary>
        /// <returns>The updated query</returns>
        public BbSqlQueryBuilder References()
        {
            return Append(Dictionary.References);
        }

        /// <summary>
        /// Adds a DROP expression
        /// </summary>
        /// <returns>The updated query</returns>
        public BbSqlQueryBuilder Drop()
        {
            return Append(Dictionary.Drop);
        }

        /// <summary>
        /// Adds the use expression to the query
        /// </summary>
        /// <returns></returns>
        public BbSqlQueryBuilder Use()
        {
            return Append(Dictionary.Use);
        }

        /// <summary>
        /// Adds a PRIMARY KEY expression
        /// </summary>
        /// <returns>The updated query</returns>
        public BbSqlQueryBuilder PrimaryKey()
        {
            return Append(Dictionary.PrimaryKey);
        }

        /// <summary>
        /// Adds a RANGE expression
        /// </summary>
        /// <returns>The updated query</returns>
        public BbSqlQueryBuilder Range()
        {
            return Append(Dictionary.Range);
        }

        /// <summary>
        /// Adds a TABLE expression
        /// </summary>
        /// <returns>The updated query</returns>
        public BbSqlQueryBuilder Table()
        {
            return Append(Dictionary.Table);
        }


        /// <summary>
        /// Adds a WHITESPACE expression
        /// </summary>
        /// <returns>The updated query</returns>
        public BbSqlQueryBuilder WhiteSpace()
        {
            return Append(Dictionary.WhiteSpace);
        }
        #endregion

        #region Complex Expressions


        /// <summary>
        /// Adds a ENCRYPTED expression with the bucket value
        /// </summary>
        /// <returns>The updated query</returns>
        public BbSqlQueryBuilder Encrypted(in int fieldBucketCount)
        {
            return Encrypted().WhiteSpace().Append(fieldBucketCount.ToString());
        }

        /// <summary>
        /// Adds a SELECT to a Query Builder with a table and a selector
        /// </summary>
        /// <returns>The updated query builder</returns>
        public BbSqlQueryBuilder SelectFields(FieldValue[] tableFields)
        {
            Select().WhiteSpace();
            var last = tableFields[^1];
            foreach (var tableField in tableFields)
            {
                Append($"{tableField.Table}{Dictionary.TableFieldSeparator}{tableField.Field}");
                if (!tableField.Equals(last))
                {
                    FieldSeparator().WhiteSpace();
                }
            }
            return this;
        }


        /// <summary>
        /// Adds a INTO to a Query Builder with the table's name
        /// </summary>
        /// <returns>The updated query builder</returns>
        public BbSqlQueryBuilder JoinOn(string tableName, string condition)
        {
            return Join().WhiteSpace().Append(tableName).WhiteSpace().On().WhiteSpace().Append(condition);
        }

        /// <summary>
        /// Adds a SELECT ALL expression with the bucket value
        /// </summary>
        /// <returns>The updated query</returns>
        public BbSqlQueryBuilder SelectAll(in string tableName)
        {
            return Select().WhiteSpace().Append(tableName).TableFieldSeparator().Append(Dictionary.AllSelector);
        }

        /// <summary>
        /// Adds a field expression
        /// </summary>
        /// <returns>The updated query</returns>
        public BbSqlQueryBuilder Field(DbFieldInfo field)
        {
            if (!field.IsEncrypted)
            {
                NonEncryptedField();
            }

            Append(field.Name).WhiteSpace();

            if (field.IsRange)
            {
                Encrypted().WhiteSpace().Range(field.MinRange, field.BucketCount, field.MaxRange);
            }
            else if (field.HasBuckets)
            {
                Encrypted(field.BucketCount).WhiteSpace();
            }
            else
            {
                Append(field.Type.ToString()).WhiteSpace();
            }

            if (field.IsPrimaryKey)
            {
                PrimaryKey().WhiteSpace();
            }

            if (field.IsForeignKey)
            {
                ForeignKey(field.ForeignTable, field.ForeignField).WhiteSpace();
            }

            return this;
        }

        /// <summary>
        /// Adds a foreign key expression
        /// </summary>
        /// <param name="table">The database table</param>
        /// <param name="field">The table's field</param>
        /// <returns></returns>
        public BbSqlQueryBuilder ForeignKey(string table, string field)
        {
            return References().WhiteSpace().Append(table).WrapLeftListSide().Append(field).WrapRightListSide();
        }

        /// <summary>
        /// Adds a FROM to a Query Builder
        /// </summary>
        /// <returns>The updated query builder</returns>
        public BbSqlQueryBuilder From(string tableName)
        {
            return From().WhiteSpace().Append(tableName);
        }

        /// <summary>
        /// Adds a field expression
        /// </summary>
        /// <returns>The updated query</returns>
        public BbSqlQueryBuilder Range(int buckets, int minimum, int maximum)
        {
            return Range().WrapLeftListSide().Append(buckets.ToString())
                .FieldSeparator().WhiteSpace().Append(minimum.ToString())
                .FieldSeparator().WhiteSpace().Append(maximum.ToString())
                .WrapRightListSide();
        }

        /// <summary>
        /// Adds a TABLE expression with the database's name
        /// </summary>
        /// <returns>The updated query</returns>
        public BbSqlQueryBuilder Table(string tableName)
        {
            return Table().WhiteSpace().Append(tableName);
        }

        /// <summary>
        /// Adds a jointed table and field expression to a Query Builder
        /// </summary>
        /// <returns>The updated query builder</returns>
        public BbSqlQueryBuilder FieldOnTable(string table, string field)
        {
            return Append(table).TableFieldSeparator().Append(field);
        }


        /// <summary>
        /// Adds a WHERE to a Query Builder
        /// </summary>
        /// <returns>The updated query builder</returns>
        public BbSqlQueryBuilder Where(string condition)
        {
            return Where().WhiteSpace().Append(condition);
        }

        /// <summary>
        /// Generates a condition based on the primary key of an object
        /// </summary>
        /// <param name="record">a record</param>
        /// <returns>a SQL condition string</returns>
        public BbSqlQueryBuilder GenerateConditionFromObject(object record)
        {
            var type = record.GetType();
            var primaryKey = type.GetPrimaryKey();
            if (primaryKey == null)
            {
                return this;
            }
            var tableName = type.GetTableName();
            var fieldName = primaryKey.GetFieldName();
            var value = primaryKey.GetValue(record);
            FieldOnTable(tableName, fieldName).Equals();
            Append(WrapValue(value));
            return this;
        }

        /// <summary>
        /// Parses a complex query
        /// </summary>
        /// <param name="expression">a complex expression</param>
        /// <returns>the query as a string</returns>
        public BbSqlQueryBuilder ParseQuery(Expression expression)
        {
            while (true)
            {
                if (expression.IsOperator())
                {
                    var value = ExecuteExpression(expression);
                    return Append(WrapValue(value));
                }
                if (expression != null && expression.NodeType == ExpressionType.Convert)
                {
                    expression = (expression as UnaryExpression)?.Operand;
                    continue;
                }
                switch (expression)
                {
                    case MemberExpression memberExpression when memberExpression.IsConstantMemberAccess():
                        {
                            var value = ExecuteExpression(memberExpression);
                            Append(WrapValue(value));
                            break;
                        }
                    case MemberExpression memberExpression:
                        {
                            if (memberExpression.IsPropertyMemberAccess())
                            {
                                var tableField = ParsePropertyAccess(memberExpression);
                                FieldOnTable(tableField.Table, tableField.Field);
                            }
                            break;
                        }
                    case BinaryExpression binaryExpression:
                        ParseQuery(binaryExpression.Left)
                            .WhiteSpace().Append(ParseOperator(binaryExpression.NodeType))
                            .WhiteSpace().ParseQuery(binaryExpression.Right);
                        break;
                    case ConstantExpression constantExpression:
                        return Append(WrapValue(constantExpression.Value));
                }
                return this;
            }
        }

        #endregion

        #region Operations

        /// <summary>
        /// Adds a set of fields to a Query Builder
        /// </summary>
        /// <returns>The updated query builder</returns>
        public BbSqlQueryBuilder Fields(string[] fields)
        {
            WrapLeftListSide();
            var length = fields.Length;
            var last = length - 1;
            for (var counter = 0; counter < length; counter++)
            {
                Append(fields[counter]);
                if (counter != last)
                {
                    FieldSeparator().WhiteSpace();
                }
            }
            return WrapRightListSide();
        }

        /// <summary>
        /// Adds a INTO to a Query Builder with the table's name
        /// </summary>
        /// <returns>The updated query builder</returns>
        public BbSqlQueryBuilder Into(string tableName)
        {
            return Into().WhiteSpace().Append(tableName);
        }

        /// <summary>
        /// Returns the value if it is a number or wraps it if not
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>a recognized value expression</returns>
        public string WrapValue(object value)
        {
            if (value.IsNumber())
            {
                return value.ToString();
            }
            return $"{Dictionary.LeftTextWrapper}{value}{Dictionary.RightTextWrapper}";
        }

        /// <summary>
        /// Executes a lambda expression
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        private static object ExecuteExpression(Expression expression)
        {
            var result = Expression.Lambda(expression).Compile().DynamicInvoke();
            return result;
        }


        /// <summary>
        /// Parses a Property Access
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        private static FieldValue ParsePropertyAccess(MemberExpression expression)
        {
            var member = expression.Member;
            var tableName = member.DeclaringType.GetTableName();
            var fieldName = member.GetFieldName();
            return new FieldValue() { Table = tableName, Field = fieldName };
        }

        /// <summary>
        /// Adds a VALUES to a Query Builder
        /// </summary>
        /// <returns>The updated query builder</returns>
        public BbSqlQueryBuilder Values(string[] values)
        {
            Values().WhiteSpace().WrapLeftListSide();
            var length = values.Length;
            var last = length - 1;
            for (var counter = 0; counter < length; counter++)
            {
                Append(values[counter]);
                if (counter != last)
                {
                    FieldSeparator().WhiteSpace();
                }
            }
            return WrapRightListSide();
        }
        #endregion

        #region Auxiliary

        /// <summary>
        /// Parses a comparison or bitwise operator on an expression
        /// </summary>
        /// <param name="nodeType">the node type that describes a</param>
        /// <returns>the SQL expression related to the operator in question</returns>
        private string ParseOperator(ExpressionType nodeType)
        {
            switch (nodeType)
            {
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                    return Dictionary.And;
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                    return Dictionary.Or;
                case ExpressionType.Equal:
                    return Dictionary.ValueEquals;
                case ExpressionType.NotEqual:
                    return Dictionary.DifferentFrom;
                case ExpressionType.GreaterThan:
                    return Dictionary.GreaterThan;
                case ExpressionType.GreaterThanOrEqual:
                    return Dictionary.EqualOrGreaterThan;
                case ExpressionType.LessThan:
                    return Dictionary.LessThan;
                case ExpressionType.LessThanOrEqual:
                    return Dictionary.EqualOrLessThan;
                default: return string.Empty;
            }
        }
        #endregion
    }
}
