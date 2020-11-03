using System.Collections.Generic;
using System.Linq;
using agap2IT.Labs.BlockBase.BBLinq.Properties;

namespace agap2IT.Labs.BlockBase.BBLinq.Queries
{
    public class QueryBuilder
    {
        private string _content = string.Empty;

        #region Word Appending
        /// <summary>
        /// Appends the expression attributed to ADD
        /// </summary>
        /// <returns>An updated QueryBuilder</returns>
        public QueryBuilder Add()
        {
            _content += Resources.QUERY_ADD;
            return this;
        }

        /// <summary>
        /// Appends the expression attributed to ADD
        /// </summary>
        /// <returns>An updated QueryBuilder</returns>
        public QueryBuilder And()
        {
            _content += Resources.QUERY_AND;
            return this;
        }

        /// <summary>
        /// Appends the expression attributed to ADD
        /// </summary>
        /// <returns>An updated QueryBuilder</returns>
        public QueryBuilder Alter()
        {
            _content += Resources.QUERY_ALTER;
            return this;
        }

        /// <summary>
        /// Appends the expression attributed to COLUMN
        /// </summary>
        /// <returns>An updated QueryBuilder</returns>
        public QueryBuilder Column()
        {
            _content += Resources.QUERY_COLUMN;
            return this;
        }

        /// <summary>
        /// Appends the expression attributed to CREATE
        /// </summary>
        /// <returns>An updated QueryBuilder</returns>
        public QueryBuilder Create()
        {
            _content += Resources.QUERY_CREATE;
            return this;
        }

        /// <summary>
        /// Appends the expression attributed to DATABASE
        /// </summary>
        /// <returns>An updated QueryBuilder</returns>
        public QueryBuilder Database()
        {
            _content += Resources.QUERY_DATABASE;
            return this;
        }
        
        /// <summary>
        /// Appends the expression attributed to Different from
        /// </summary>
        /// <returns>An updated QueryBuilder</returns>
        public QueryBuilder DifferentFrom()
        {
            _content += Resources.QUERY_DIFFERENT;
            return this;
        }

        /// <summary>
        /// Appends the expression attributed to DELETE
        /// </summary>
        /// <returns>An updated QueryBuilder</returns>
        public QueryBuilder Delete()
        {
            _content += Resources.QUERY_DELETE;
            return this;
        }

        /// <summary>
        /// Appends the expression attributed to DROP
        /// </summary>
        /// <returns>An updated QueryBuilder</returns>
        public QueryBuilder Drop()
        {
            _content += Resources.QUERY_DROP;
            return this;
        }

        /// <summary>
        /// Appends the expression attributed to an encrypted query
        /// </summary>
        /// <returns>An updated QueryBuilder</returns>
        public QueryBuilder Encrypted()
        {
            _content += Resources.QUERY_ENCRYPTED;
            return this;
        }
        /// <summary>
        /// Appends the expression attributed to the end of a query
        /// </summary>
        /// <returns>An updated QueryBuilder</returns>
        public QueryBuilder End()
        {
            _content += Resources.QUERY_END;
            return this;
        }

        /// <summary>
        /// Appends the expression attributed to Equal
        /// </summary>
        /// <returns>An updated QueryBuilder</returns>
        public QueryBuilder EqualTo()
        {
            _content += Resources.QUERY_EQUALS;
            return this;
        }
        /// <summary>
        /// Appends the expression attributed to the left field set wrapper
        /// </summary>
        /// <returns>An updated QueryBuilder</returns>
        public QueryBuilder FieldSetWrapperLeft()
        {
            _content += Resources.QUERY_FIELD_WRAPPER_LEFT;
            return this;
        }

        /// <summary>
        /// Appends the expression attributed to the right field set wrapper
        /// </summary>
        /// <returns>An updated QueryBuilder</returns>
        public QueryBuilder FieldSetWrapperRight()
        {
            _content += Resources.QUERY_FIELD_WRAPPER_RIGHT;
            return this;
        }

        /// <summary>
        /// Appends the expression attributed to a field separator
        /// </summary>
        /// <returns>An updated QueryBuilder</returns>
        public QueryBuilder FieldSeparator()
        {
            _content += Resources.QUERY_FIELD_SEPARATOR;
            return this;
        }

        /// <summary>
        /// Appends the expression attributed to FROM
        /// </summary>
        /// <returns>An updated QueryBuilder</returns>
        public QueryBuilder From()
        {
            _content += Resources.QUERY_FROM;
            return this;
        }

        /// <summary>
        /// Appends the expression attributed to Greater
        /// </summary>
        /// <returns>An updated QueryBuilder</returns>
        public QueryBuilder GreaterThan()
        {
            _content += Resources.QUERY_GREATER;
            return this;
        }

        /// <summary>
        /// Appends the expression attributed to Equal or Greater
        /// </summary>
        /// <returns>An updated QueryBuilder</returns>
        public QueryBuilder EqualOrGreaterThan()
        {
            _content += Resources.QUERY_EQUAL_OR_GREATER;
            return this;
        }
        /// <summary>
        /// Appends the expression attributed to INSERT
        /// </summary>
        /// <returns>An updated QueryBuilder</returns>
        public QueryBuilder Insert()
        {
            _content += Resources.QUERY_INSERT;
            return this;
        }

        /// <summary>
        /// Appends the expression attributed to INTO
        /// </summary>
        /// <returns>An updated QueryBuilder</returns>
        public QueryBuilder Into()
        {
            _content += Resources.QUERY_INTO;
            return this;
        }

        /// <summary>
        /// Appends the expression attributed to JOIN
        /// </summary>
        /// <returns>An updated QueryBuilder</returns>
        public QueryBuilder Join()
        {
            _content += Resources.QUERY_JOIN;
            return this;
        }

        /// <summary>
        /// Appends the expression attributed to LESS
        /// </summary>
        /// <returns>An updated QueryBuilder</returns>
        public QueryBuilder LessThan()
        {
            _content += Resources.QUERY_LESS;
            return this;
        }

        /// <summary>
        /// Appends the expression attributed to equal or LESS 
        /// </summary>
        /// <returns>An updated QueryBuilder</returns>
        public QueryBuilder EqualOrLessThan()
        {
            _content += Resources.QUERY_EQUAL_OR_LESS;
            return this;
        }

        /// <summary>
        /// Adds the ON expression
        /// </summary>
        /// <returns>An updated QueryBuilder</returns>
        public QueryBuilder On()
        {
            _content += $" {Resources.QUERY_ON}";
            return this;
        }

        /// <summary>
        /// Appends the expression attributed to OR
        /// </summary>
        /// <returns>An updated QueryBuilder</returns>
        public QueryBuilder Or()
        {
            _content += Resources.QUERY_OR;
            return this;
        }

        /// <summary>
        /// Appends the expression attributed to RENAME
        /// </summary>
        /// <returns>An updated QueryBuilder</returns>
        public QueryBuilder Rename()
        {
            _content += Resources.QUERY_RENAME;
            return this;
        }

        /// <summary>
        /// Appends the expression attributed to RANGE
        /// </summary>
        /// <returns>An updated QueryBuilder</returns>
        public QueryBuilder Range()
        {
            _content += Resources.QUERY_RANGE;
            return this;
        }

        /// <summary>
        /// Appends the expression attributed to SELECT
        /// </summary>
        /// <returns>An updated QueryBuilder</returns>
        public QueryBuilder Select()
        {
            _content += Resources.QUERY_SELECT;
            return this;
        }

        /// <summary>
        /// Appends the expression attributed to SET
        /// </summary>
        /// <returns>An updated QueryBuilder</returns>
        public QueryBuilder Set()
        {
            _content += Resources.QUERY_SET;
            return this;
        }

        /// <summary>
        /// Appends the expression attributed to TABLE
        /// </summary>
        /// <returns>An updated QueryBuilder</returns>
        public QueryBuilder Table()
        {
            _content += Resources.QUERY_TABLE;
            return this;
        }

        /// <summary>
        /// Appends the expression attributed to TABLE
        /// </summary>
        /// <returns>An updated QueryBuilder</returns>
        public QueryBuilder TextWrapperLeft()
        {
            _content += Resources.QUERY_TEXT_WRAPPER_LEFT;
            return this;
        }

        /// <summary>
        /// Appends the expression attributed to TABLE
        /// </summary>
        /// <returns>An updated QueryBuilder</returns>
        public QueryBuilder TextWrapperRight()
        {
            _content += Resources.QUERY_TEXT_WRAPPER_RIGHT;
            return this;
        }

        /// <summary>
        /// Appends the expression attributed to TABLE
        /// </summary>
        /// <returns>An updated QueryBuilder</returns>
        public QueryBuilder To()
        {
            _content += Resources.QUERY_TO;
            return this;
        }

        /// <summary>
        /// Appends the expression attributed to a separator between a table and a field
        /// </summary>
        /// <returns>An updated QueryBuilder</returns>
        public QueryBuilder TableFieldSeparator()
        {
            _content += Resources.QUERY_TABLE_FIELD_SEPARATOR;
            return this;
        }

        /// <summary>
        /// Appends the expression attributed to WHERE
        /// </summary>
        /// <returns>An updated QueryBuilder</returns>
        public QueryBuilder Where()
        {
            _content += Resources.QUERY_WHERE;
            return this;
        }

        /// <summary>
        /// Appends a whitespace
        /// </summary>
        /// <returns>An updated QueryBuilder</returns>
        public QueryBuilder WhiteSpace()
        {
            _content += " ";
            return this;
        }

        /// <summary>
        /// Appends the expression attributed to UPDATE
        /// </summary>
        /// <returns>An updated QueryBuilder</returns>
        public QueryBuilder Update()
        {
            _content += Resources.QUERY_UPDATE;
            return this;
        }

        /// <summary>
        /// Appends the expression attributed to USE
        /// </summary>
        /// <returns>An updated QueryBuilder</returns>
        public QueryBuilder Use()
        {
            _content += Resources.QUERY_USE;
            return this;
        }

        /// <summary>
        /// Appends the expression attributed to VALUE
        /// </summary>
        /// <returns>An updated QueryBuilder</returns>
        public QueryBuilder Values()
        {
            _content += Resources.QUERY_VALUES;
            return this;
        }
        /// <summary>
        /// Appends the expression attributed to VALUE
        /// </summary>
        /// <returns>An updated QueryBuilder</returns>
        public QueryBuilder ValueAssigner()
        {
            _content += Resources.QUERY_VALUE_ASSIGNER;
            return this;
        }
        #endregion

        #region Value Appending

        /// <summary>
        /// Adds the column's name for a COLUMN expression
        /// </summary>
        /// <param name="columnName">the column's name</param>
        /// <returns>An updated QueryBuilder</returns>
        public QueryBuilder Column(string columnName)
        {
            return Column().Append($" { columnName}");
        }

        /// <summary>
        /// Adds the column's name and type for a COLUMN expression
        /// </summary>
        /// <param name="columnName">the column's name</param>
        /// <param name="columnType">the column's type</param>
        /// <returns>An updated QueryBuilder</returns>
        public QueryBuilder Column(string columnName, string columnType)
        {
            return Column().Append($" {columnName} {columnType}");
        }

        /// <summary>
        /// Adds the database's name for a DATABASE expression
        /// </summary>
        /// <param name="databaseName">the database's name</param>
        /// <returns>An updated QueryBuilder</returns>
        public QueryBuilder Database(string databaseName)
        {
            return Database().Append($" {databaseName}");
        }

        /// <summary>
        /// Wraps a set of fields with the respective expressions
        /// </summary>
        /// <param name="fields">a set of fields</param>
        /// <returns>An updated QueryBuilder</returns>
        public QueryBuilder WrapFields(IEnumerable<string> fields)
        {
            return FieldSetWrapperLeft().JoinExpressionsOnFieldSeparator(fields).FieldSetWrapperRight();
        }

        /// <summary>
        /// Joins a series of expressions separated
        /// </summary>
        /// <param name="expressions"></param>
        /// <returns></returns>
        public QueryBuilder JoinExpressionsOnFieldSeparator(IEnumerable<string> expressions)
        {
            return Append(string.Join(Resources.QUERY_FIELD_SEPARATOR, expressions));
        }

        /// <summary>
        /// Wraps a set of fields with the respective expressions
        /// </summary>
        /// <param name="fields">a set of fields</param>
        /// <returns>An updated QueryBuilder</returns>
        public QueryBuilder WrapFields((string, string)[] fields)
        {
            _content += $" {Resources.QUERY_FIELD_WRAPPER_LEFT}";
            for (var counter = 0; counter < fields.Length; counter++)
            {
                _content += $"{fields[counter].Item1} {fields[counter].Item2}";
                if (counter < fields.Length - 1) _content += $"{Resources.QUERY_FIELD_SEPARATOR}";
            }
            _content += $" {Resources.QUERY_FIELD_WRAPPER_RIGHT}";
            return this;
        }

        /// <summary>
        /// Adds a field to the builder
        /// </summary>
        /// <param name="tableName">the table's name</param>
        /// <param name="propertyName">the property's name</param>
        /// <returns>An updated QueryBuilder</returns>
        public QueryBuilder Field(string tableName, string propertyName)
        {
            _content += $"{tableName}{Resources.QUERY_TABLE_FIELD_SEPARATOR}{propertyName}";
            return this;
        }

        /// <summary>
        /// Adds the table's name for a FROM expression
        /// </summary>
        /// <param name="tableName">the table's name</param>
        /// <returns>An updated QueryBuilder</returns>
        public QueryBuilder From(string tableName)
        {
            _content += $" {Resources.QUERY_FROM} {tableName}";
            return this;
        }

        /// <summary>
        /// Wraps a set of fields with the respective expressions for an INTO expression
        /// </summary>
        /// <param name="fields">a set of fields</param>
        /// <returns>An updated QueryBuilder</returns>
        public QueryBuilder Into(string[] fields)
        {
            return Into().WrapFields(fields);
        }

        /// <summary>
        /// Adds a join expression with a table's name
        /// </summary>
        /// <param name="tableName">the table's name</param>
        /// <returns></returns>
        public QueryBuilder Join(string tableName)
        {
            return Join().Append($" {tableName}");
        }
        
        /// <summary>
        /// Adds the JOIN condition with an ON
        /// </summary>
        /// <param name="on">The expression used on the join</param>
        /// <returns>An updated QueryBuilder</returns>
        public QueryBuilder On(string on)
        {
            _content += $" {Resources.QUERY_ON} {on}";
            return this;
        }

        /// <summary>
        /// Adds a "select all columns" expression to the query
        /// </summary>
        /// <param name="tableName">the table name</param>
        /// <returns>An updated QueryBuilder</returns>
        public QueryBuilder SelectAll(string tableName)
        {
            _content += $"{tableName}{Resources.QUERY_SELECT_ALL}";
            return this;
        }

        /// <summary>
        /// Adds a series of selectable fields to the query
        /// </summary>
        /// <param name="fields">a series of fields to add</param>
        /// <returns>An updated QueryBuilder</returns>
        public QueryBuilder Select(string[] fields)
        {
            _content += $"{Resources.QUERY_SELECT} ";
            foreach (var field in fields)
            {
                _content += $"{field}{(field != fields[fields.Length] ? Resources.QUERY_FIELD_SEPARATOR : "")}";
            }
            return this;
        }

        /// <summary>
        /// Adds a series of selectable fields to the query
        /// </summary>
        /// <param name="fields">a series of fields to add</param>
        /// <param name="tableName">the table's name</param>
        /// <returns>An updated QueryBuilder</returns>
        public QueryBuilder Select(string[] fields, string tableName)
        {
            _content += $"{Resources.QUERY_SELECT} ";
            foreach (var field in fields)
            {
                _content += $"{tableName}{Resources.QUERY_TABLE_FIELD_SEPARATOR}{field}";
            }
            return this;
        }

        /// <summary>
        /// Fills a set expression with values
        /// </summary>
        /// <param name="fields">a set of fields</param>
        /// <returns>An updated QueryBuilder</returns>
        public QueryBuilder Set(IEnumerable<(string, string)> fields)
        {
            var lst = fields as List<(string, string)>;
            var size = lst.Count;
            _content += $" {Resources.QUERY_FIELD_WRAPPER_LEFT}";
            for (var counter = 0; counter < size; counter++)
            {
                _content += $"{lst[counter].Item1} {Resources.QUERY_VALUE_ASSIGNER} {lst[counter].Item2}";
                if (counter < size - 1) _content += $"{Resources.QUERY_FIELD_SEPARATOR}";
            }
            _content += $" {Resources.QUERY_FIELD_WRAPPER_RIGHT}";
            return this;
        }

        /// <summary>
        /// Adds a update expression to the query
        /// </summary>
        /// <param name="tableName">the table name</param>
        /// <returns>An updated QueryBuilder</returns>
        public QueryBuilder Table(string tableName)
        {
            return Table().Append($" {tableName}");
        }

        /// <summary>
        /// Adds a TO expression to the query
        /// </summary>
        /// <param name="tableName">the table name</param>
        /// <returns>An updated QueryBuilder</returns>
        public QueryBuilder To(string tableName)
        {
            return To().Append($"{tableName}");
        }

        /// <summary>
        /// Adds a update expression to the query
        /// </summary>
        /// <param name="tableName">the table name</param>
        /// <returns>An updated QueryBuilder</returns>
        public QueryBuilder Update(string tableName)
        {
            return Update().Append($" {tableName}");
        }

        /// <summary>
        /// Adds a use expression to the query
        /// </summary>
        /// <param name="tableName">the table name</param>
        /// <returns>An updated QueryBuilder</returns>
        public QueryBuilder Use(string tableName)
        {
            return Use().Append($"{tableName}");
        }

        /// <summary>
        /// Adds a series of selectable fields to the query
        /// </summary>
        /// <param name="values">a series of values to add</param>
        /// <returns>An updated QueryBuilder</returns>
        public QueryBuilder Values(IEnumerable<string> values)
        {
            var vals = values as List<string>;
            _content += $"{Resources.QUERY_VALUES}{Resources.QUERY_FIELD_WRAPPER_LEFT}";
            foreach (var value in vals)
            {
                _content += $"{value}";
                if (value != vals[vals.Count - 1])
                    _content += Resources.QUERY_FIELD_SEPARATOR;
            }
            _content += $"{Resources.QUERY_FIELD_WRAPPER_RIGHT}";

            return this;
        }


        /// <summary>
        /// Adds a text value to the query builder
        /// </summary>
        /// <param name="text">the text to introduce in the query</param>
        /// <returns>An updated QueryBuilder</returns>
        public QueryBuilder WrapText(string text)
        {
            _content += $"{Resources.QUERY_TEXT_WRAPPER_LEFT}{text}{Resources.QUERY_TEXT_WRAPPER_RIGHT}";
            return this;
        }

        /// <summary>
        /// Adds the filter for a WHERE expression
        /// </summary>
        /// <param name="filter"></param>
        /// <returns>An updated QueryBuilder</returns>
        public QueryBuilder Where(string filter)
        {
            _content += $" {Resources.QUERY_WHERE} {filter}";
            return this;
        }
    
        #endregion 
        /// <summary>
        /// Converts the builder to a string
        /// </summary>
        /// <returns>a string representing a query</returns>
        public override string ToString()
        {
            return _content;
        }

        /// <summary>
        /// Appends a string to the content
        /// </summary>
        /// <param name="content">the content to be appended</param>
        /// <returns>An updated query builder</returns>
        public QueryBuilder Append(string content)
        {
            _content += content;
            return this;
        }
    }
}
