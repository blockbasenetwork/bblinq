using BlockBase.BBLinq.Pocos.Components;
using BlockBase.BBLinq.Properties;

namespace BlockBase.BBLinq.Builders
{
    /// <summary>
    /// A query builder for BBSQL queries
    /// </summary>
    public class BbSqlQueryBuilder
    {
        /// <summary>
        /// Appends the expression attributed to Different from
        /// </summary>
        /// <returns>An updated QueryBuilder</returns>
        public BbSqlQueryBuilder DifferentFrom()
        {
            _content += SQLExpressions.DIFFERENT;
            return this;
        }

        /// <summary>
        /// Appends the expression attributed to LESS
        /// </summary>
        /// <returns>An updated QueryBuilder</returns>
        public BbSqlQueryBuilder LessThan()
        {
            _content += SQLExpressions.LESS_THAN;
            return this;
        }

        /// <summary>
        /// Appends the expression attributed to equal or LESS 
        /// </summary>
        /// <returns>An updated QueryBuilder</returns>
        public BbSqlQueryBuilder EqualOrLessThan()
        {
            _content += SQLExpressions.EQUAL_OR_LESS;
            return this;
        }

        /// <summary>
        /// Appends the expression attributed to Greater
        /// </summary>
        /// <returns>An updated QueryBuilder</returns>
        public BbSqlQueryBuilder GreaterThan()
        {
            _content += SQLExpressions.GREATER_THAN;
            return this;
        }

        /// <summary>
        /// Appends the expression attributed to Equal or Greater
        /// </summary>
        /// <returns>An updated QueryBuilder</returns>
        public BbSqlQueryBuilder EqualOrGreaterThan()
        {
            _content += SQLExpressions.EQUAL_OR_GREATER;
            return this;
        }

        /// <summary>
        /// Appends the expression attributed to ADD
        /// </summary>
        /// <returns>An updated QueryBuilder</returns>
        public BbSqlQueryBuilder And()
        {
            Append(SQLExpressions.AND);
            return this;
        }

        /// <summary>
        /// Adds a DELETE to a Query Builder
        /// </summary>
        /// <returns>The updated query builder</returns>
        public BbSqlQueryBuilder Delete()
        {
            return Append(SQLExpressions.DELETE);
        }


        /// <summary>
        /// Adds a END to a Query Builder
        /// </summary>
        /// <returns>The updated query builder</returns>
        public BbSqlQueryBuilder End()
        {
            return Append(SQLExpressions.END);
        }

        /// <summary>
        /// Adds a EQUALS to a Query Builder
        /// </summary>
        /// <returns>The updated query builder</returns>
        public BbSqlQueryBuilder Equals()
        {
            return Append(SQLExpressions.VALUE_EQUALS);
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
        /// Adds a field to a Query Builder
        /// </summary>
        /// <returns>The updated query builder</returns>
        public BbSqlQueryBuilder Field(string field)
        {
            return Append(field);
        }

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
                    SeparateFieldOrValue();
                }
            }
            return WrapRightListSide();
        }

        /// <summary>
        /// Adds a FROM to a Query Builder
        /// </summary>
        /// <returns>The updated query builder</returns>
        public BbSqlQueryBuilder From()
        {
            return Append(SQLExpressions.FROM);
        }

        /// <summary>
        /// Adds a END to a Query Builder
        /// </summary>
        /// <returns>The updated query builder</returns>
        public BbSqlQueryBuilder From(string tableName)
        {
            return From().WhiteSpace().Append(tableName);
        }

        /// <summary>
        /// Adds a INSERT to a Query Builder
        /// </summary>
        /// <returns>The updated query builder</returns>
        public BbSqlQueryBuilder Insert()
        {
            return Append(SQLExpressions.INSERT);
        }

        /// <summary>
        /// Adds a INTO to a Query Builder
        /// </summary>
        /// <returns>The updated query builder</returns>
        public BbSqlQueryBuilder Into()
        {
            return Append(SQLExpressions.INTO);
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
        /// Adds a INTO to a Query Builder with the table's name
        /// </summary>
        /// <returns>The updated query builder</returns>
        public BbSqlQueryBuilder Join()
        {
            return Append(SQLExpressions.JOIN);
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
        /// Appends the expression attributed to OR
        /// </summary>
        /// <returns>An updated QueryBuilder</returns>
        public BbSqlQueryBuilder On()
        {
            _content += SQLExpressions.ON;
            return this;
        }

        /// <summary>
        /// Appends the expression attributed to OR
        /// </summary>
        /// <returns>An updated QueryBuilder</returns>
        public BbSqlQueryBuilder Or()
        {
            _content += SQLExpressions.OR;
            return this;
        }

        /// <summary>
        /// Adds a SELECT to a Query Builder
        /// </summary>
        /// <returns>The updated query builder</returns>
        public BbSqlQueryBuilder Select()
        {
            return Append(SQLExpressions.SELECT);
        }

        /// <summary>
        /// Adds a SELECT to a Query Builder with a table and a selector
        /// </summary>
        /// <returns>The updated query builder</returns>
        public BbSqlQueryBuilder SelectFields(TableField[] tableFields)
        {
            Select().WhiteSpace();
            var last = tableFields[^1];
            foreach (var tableField in tableFields)
            {
                Append($"{tableField.TableName}{SQLExpressions.TABLE_FIELD_SEPARATOR}{tableField.FieldName}");
                if (!tableField.Equals(last))
                {
                    Append($"{SQLExpressions.FIELD_OR_VALUE_SEPARATOR} ");
                }
            }
            return this;
        }

        /// <summary>
        /// Adds a SELECT to a Query Builder with a table and a selector
        /// </summary>
        /// <returns>The updated query builder</returns>
        public BbSqlQueryBuilder SelectAll(string tableName)
        {
            return Append($"{SQLExpressions.SELECT} {tableName}{SQLExpressions.TABLE_FIELD_SEPARATOR}{SQLExpressions.ALL_SELECTOR}");
        }

        /// <summary>
        /// Adds a value or field separator to a Query Builder
        /// </summary>
        /// <returns>The updated query builder</returns>
        public BbSqlQueryBuilder SeparateFieldOrValue()
        {
            return Append(SQLExpressions.FIELD_OR_VALUE_SEPARATOR).WhiteSpace();
        }

        /// <summary>
        /// Adds a SET to a Query Builder
        /// </summary>
        /// <returns>The updated query builder</returns>
        public BbSqlQueryBuilder Set()
        {
            return Append(SQLExpressions.SET);
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
            Append(SQLExpressions.SET);
            WhiteSpace();
            //WrapLeftListSide();
            var last = fields[^1];
            for (var counter = 0; counter < fields.Length; counter++)
            {
                Field(fields[counter]);
                Equals();
                Value(values[counter]);
                if (fields[counter] != last)
                {
                    SeparateFieldOrValue();
                }
            }
            //WrapRightListSide();
            return this;
        }

        /// <summary>
        /// Adds a separator between a table and a field to a Query Builder
        /// </summary>
        /// <returns>The updated query builder</returns>
        public BbSqlQueryBuilder TableFieldSeparator()
        {
            return Append(SQLExpressions.TABLE_FIELD_SEPARATOR);
        }

        /// <summary>
        /// Adds a UPDATE to a Query Builder
        /// </summary>
        /// <returns>The updated query builder</returns>
        public BbSqlQueryBuilder Update()
        {
            return Append(SQLExpressions.UPDATE);
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
        /// Adds a value to a Query Builder
        /// </summary>
        /// <returns>The updated query builder</returns>
        public BbSqlQueryBuilder Value(string value)
        {
            return Append(value);
        }

        /// <summary>
        /// Adds a VALUES to a Query Builder
        /// </summary>
        /// <returns>The updated query builder</returns>
        public BbSqlQueryBuilder Values()
        {
            return Append(SQLExpressions.VALUES);
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
                Value(values[counter]);
                if (counter != last)
                {
                    SeparateFieldOrValue();
                }
            }
            return WrapRightListSide();
        }

        /// <summary>
        /// Adds a WHERE to a Query Builder
        /// </summary>
        /// <returns>The updated query builder</returns>
        public BbSqlQueryBuilder Where()
        {
            return Append(SQLExpressions.WHERE);
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
        /// Adds a WHITE_SPACE to a Query Builder
        /// </summary>
        /// <returns>The updated query builder</returns>
        public BbSqlQueryBuilder WhiteSpace()
        {
            return Append(" ");
        }

        /// <summary>
        /// Adds a LEFT_LIST_WRAPPER to a Query Builder
        /// </summary>
        /// <returns>The updated query builder</returns>
        public BbSqlQueryBuilder WrapLeftListSide()
        {
            return Append(SQLExpressions.LEFT_LIST_WRAPPER);
        }

        /// <summary>
        /// Adds a RIGHT_LIST_WRAPPER to a Query Builder
        /// </summary>
        /// <returns>The updated query builder</returns>
        public BbSqlQueryBuilder WrapRightListSide()
        {
            return Append(SQLExpressions.RIGHT_LIST_WRAPPER);
        }

        /// <summary>
        /// Wraps text with the textWRapper to a Query Builder
        /// </summary>
        /// <returns>The updated query builder</returns>
        public BbSqlQueryBuilder WrapText(string text)
        {
            return WrapTextLeft().Append(text).WrapTextRight();
        }

        /// <summary>
        /// Adds a LEFT_TEXT_WRAPPER to a Query Builder
        /// </summary>
        /// <returns>The updated query builder</returns>
        public BbSqlQueryBuilder WrapTextLeft()
        {
            return Append(SQLExpressions.RIGHT_TEXT_WRAPPER);
        }

        /// <summary>
        /// Adds a RIGHT_TEXT_WRAPPER to a Query Builder
        /// </summary>
        /// <returns>The updated query builder</returns>
        public BbSqlQueryBuilder WrapTextRight()
        {
            return Append(SQLExpressions.RIGHT_TEXT_WRAPPER);
        }


        #region Base
        private string _content = string.Empty;

        /// <summary>
        /// Adds the content to the query builder
        /// </summary>
        /// <param name="content">text content</param>
        /// <returns>The updated query builder</returns>
        public BbSqlQueryBuilder Append(string content)
        {
            _content += content;
            return this;
        }

        /// <summary>
        /// Joins two query builder
        /// </summary>
        /// <param name="qb">a query builder</param>
        /// <returns>The updated query builder</returns>
        public BbSqlQueryBuilder Join(BbSqlQueryBuilder qb)
        {
            return Append(qb.ToString());
        }

        /// <summary>
        /// Resets the query builder's content
        /// </summary>
        /// <returns>The updated query builder</returns>
        public BbSqlQueryBuilder Clear()
        {
            _content = string.Empty;
            return this;
        }

        /// <summary>
        /// Returns the content on a builder
        /// </summary>
        /// <returns>the builder content</returns>
        public override string ToString()
        {
            return _content;
        }

        #endregion

    }
}
