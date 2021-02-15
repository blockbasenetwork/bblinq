using System.Linq.Expressions;
using BlockBase.BBLinq.Dictionaries;
using BlockBase.BBLinq.ExtensionMethods;
using BlockBase.BBLinq.Pocos;

namespace BlockBase.BBLinq.QueryBuilders
{
    public class BbSqlQueryBuilder : DbQueryBuilder<BbSqlQueryBuilder>
    {
        protected BbSqlDictionary Dictionary;

        public BbSqlQueryBuilder()
        {
            Dictionary = new BbSqlDictionary();
        }

        /// <summary>
        /// Adds a instruction end to a Query Builder
        /// </summary>
        public BbSqlQueryBuilder End()
        {
            return Append(Dictionary.QueryEnd);
        }

        /// <summary>
        /// Adds a WHERE to a Query Builder
        /// </summary>
        /// <returns>The updated query builder</returns>
        public BbSqlQueryBuilder Where()
        {
            return Append(Dictionary.Where);
        }

        public BbSqlQueryBuilder Delete()
        {
            return Append(Dictionary.Delete);
        }

        public BbSqlQueryBuilder From()
        {
            return Append(Dictionary.From);
        }

        /// <summary>
        /// Adds a WHITESPACE expression
        /// </summary>
        /// <returns>The updated query</returns>
        public BbSqlQueryBuilder WhiteSpace()
        {
            return Append(Dictionary.WhiteSpace);
        }

        public string WrapValue(object value)
        {
            if (value.IsNumber())
            {
                return value.ToString();
            }
            return $"{Dictionary.LeftTextWrapper}{value}{Dictionary.RightTextWrapper}";
        }


        /// <summary>
        /// Adds a Column separator to a Query Builder
        /// </summary>
        public BbSqlQueryBuilder ColumnSeparator()
        {
            Append(Dictionary.ColumnSeparator).WhiteSpace();
            return this;
        }

        public BbSqlQueryBuilder CreateDatabase(string databaseName)
        {
            Append(Dictionary.Create).WhiteSpace().Append(Dictionary.DataBase).WhiteSpace().Append(databaseName);
            return End();
        }

        /// <summary>
        /// Adds a EQUALS to a Query Builder
        /// </summary>
        /// <returns>The updated query builder</returns>
        public BbSqlQueryBuilder Equals()
        {
            return Append(Dictionary.ValueEquals);
        }

        public BbSqlQueryBuilder CreateTable(string tableName, BlockBaseColumn[] columns)
        {
            Append(Dictionary.Create).WhiteSpace().Append(Dictionary.Table)
                .WhiteSpace().Append(tableName)
                .Append(Dictionary.LeftListWrapper)
                .ListColumns(columns).Append(Dictionary.RightListWrapper);
            return End();
        }

        public BbSqlQueryBuilder DropDatabase(string databaseName)
        {
            Append(Dictionary.Drop).WhiteSpace().Append(Dictionary.DataBase).WhiteSpace().Append(databaseName);
            return End();
        }

        private BbSqlQueryBuilder ListColumns(BlockBaseColumn[] columns)
        {
            foreach (var column in columns)
            {
                Column(column);
                if (column != columns[^1])
                {
                    ColumnSeparator();
                }
            }
            return this;
        }

        private void Column(BlockBaseColumn column)
        {
            if (!column.IsEncrypted && !column.IsRange)
            {
                Append(Dictionary.NonEncryptedColumn);
            }

            Append(column.Name).WhiteSpace();

            if (column.IsRange)
            {
                Append(Dictionary.Range).Append(Dictionary.LeftListWrapper)
                    .Append(column.BucketCount.ToString()).ColumnSeparator()
                    .Append(column.MinRange.ToString()).ColumnSeparator()
                    .Append(column.MaxRange.ToString()).Append(Dictionary.RightListWrapper);
            }
            else if (column.HasBuckets)
            {
                Append(Dictionary.Encrypted).WhiteSpace().Append(column.BucketCount.ToString());
            }
            else
            {
                Append(column.Type.ToString());
            }
            Append(Dictionary.WhiteSpace);
            if (column.IsPrimaryKey)
            {
                Append(Dictionary.PrimaryKey);
                Append(Dictionary.WhiteSpace);
            }

            if (column.IsForeignKey)
            {
                Append(Dictionary.References).WhiteSpace()
                    .Append(column.ForeignTable).Append(Dictionary.LeftListWrapper)
                    .Append(column.ForeignColumn).Append(Dictionary.RightListWrapper);
            }
        }

        /// <summary>
        /// Adds a jointed table and Column expression to a Query Builder
        /// </summary>
        /// <returns>The updated query builder</returns>
        public BbSqlQueryBuilder ColumnOnTable(string table, string column)
        {
            return Append(table).Append(Dictionary.TableColumnSeparator).Append(column);
        }

        /// <summary>
        /// Generates a condition based on the primary key of an object
        /// </summary>
        /// <param name="record">a record</param>
        /// <returns>a SQL condition string</returns>
        public BbSqlQueryBuilder GenerateConditionFromObject(object record)
        {
            var type = record.GetType();
            var primaryKeys = type.GetPrimaryKeyProperties();
            var tableName = type.GetTableName();
            var columnName = primaryKeys[0].GetColumnName();
            var value = primaryKeys[0].GetValue(record);
            ColumnOnTable(tableName, columnName).Equals();
            Append(WrapValue(value));
            return this;
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
        private static (string, string) ParsePropertyAccess(MemberExpression expression)
        {
            var member = expression.Member;
            var tableName = member.DeclaringType.GetTableName();
            var fieldName = member.GetColumnName();
            return (tableName, fieldName);
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
                                var tableColumn = ParsePropertyAccess(memberExpression);
                                ColumnOnTable(tableColumn.Item1, tableColumn.Item2);
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

    }
}
