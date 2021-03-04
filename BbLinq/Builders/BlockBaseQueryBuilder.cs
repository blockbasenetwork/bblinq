using System;
using System.Globalization;
using BlockBase.BBLinq.Builders.Base;
using BlockBase.BBLinq.Dictionaries;
using BlockBase.BBLinq.Enumerables;
using BlockBase.BBLinq.Exceptions;
using BlockBase.BBLinq.ExtensionMethods;
using BlockBase.BBLinq.Pocos;
using BlockBase.BBLinq.Pocos.Nodes;

namespace BlockBase.BBLinq.Builders
{
    public class BlockBaseQueryBuilder : QueryBuilder<BlockBaseQueryBuilder>
    {
        private readonly BbSqlDictionary _dictionary = new BbSqlDictionary();

        #region SimpleExpressions
        public BlockBaseQueryBuilder Use()
        {
            return Append(_dictionary.Use);
        }

        public BlockBaseQueryBuilder Create()
        {
            return Append(_dictionary.Create);
        }

        public BlockBaseQueryBuilder End()
        {
            return Append(_dictionary.QueryEnd);
        }

        public BlockBaseQueryBuilder Drop()
        {
            return Append(_dictionary.Drop);
        }

        public BlockBaseQueryBuilder Database()
        {
            return Append(_dictionary.DataBase);
        }

        public BlockBaseQueryBuilder Delete()
        {
            return Append(_dictionary.Delete);
        }

        public BlockBaseQueryBuilder From()
        {
            return Append(_dictionary.From);
        }

        public BlockBaseQueryBuilder WhiteSpace()
        {
            return Append(_dictionary.WhiteSpace);
        }

        public BlockBaseQueryBuilder TableColumnSeparator()
        {
            return Append(_dictionary.TableColumnSeparator);
        }

        public BlockBaseQueryBuilder If()
        {
            return Append(_dictionary.If);
        }

        public BlockBaseQueryBuilder Execute()
        {
            return Append(_dictionary.Execute);
        }

        public BlockBaseQueryBuilder QueryBatchLeftWrapper()
        {
            return Append(_dictionary.QueryBatchWrapperLeft);
        }

        public BlockBaseQueryBuilder QueryBatchRightWrapper()
        {
            return Append(_dictionary.QueryBatchWrapperRight);
        }

        public BlockBaseQueryBuilder Where()
        {
            return Append(_dictionary.Where);
        }

        public BlockBaseQueryBuilder EqualTo()
        {
            return Append(_dictionary.ValueEquals);
        }

        public BlockBaseQueryBuilder Insert()
        {
            return Append(_dictionary.Insert);
        }

        public BlockBaseQueryBuilder Into()
        {
            return Append(_dictionary.Into);
        }

        public BlockBaseQueryBuilder ListSeparator()
        {
            return Append(_dictionary.ColumnSeparator);
        }

        public BlockBaseQueryBuilder Values()
        {
            return Append(_dictionary.Values);
        }

        public BlockBaseQueryBuilder WrapListLeft()
        {
            return Append(_dictionary.LeftListWrapper);
        }

        public BlockBaseQueryBuilder WrapListRight()
        {
            return Append(_dictionary.RightListWrapper);
        }

        public BlockBaseQueryBuilder Update()
        {
            return Append(_dictionary.Update);
        }

        public BlockBaseQueryBuilder Set()
        {
            return Append(_dictionary.Set);
        }

        public BlockBaseQueryBuilder NotNull()
        {
            return Append(_dictionary.NotNull);
        }

        public BlockBaseQueryBuilder Select()
        {
            return Append(_dictionary.Select);
        }

        public BlockBaseQueryBuilder Encrypted()
        {
            return Append(_dictionary.Encrypted);
        }

        public BlockBaseQueryBuilder Limit()
        {
            return Append(_dictionary.Limit);
        }

        public BlockBaseQueryBuilder Offset()
        {
            return Append(_dictionary.Offset);
        }

        public BlockBaseQueryBuilder Begin()
        {
            return Append(_dictionary.Begin);
        }

        public BlockBaseQueryBuilder Commit()
        {
            return Append(_dictionary.Commit);
        }

        public BlockBaseQueryBuilder Transaction()
        {
            return Append(_dictionary.Transaction);
        }

        public BlockBaseQueryBuilder Rollback()
        {
            return Append(_dictionary.Rollback);
        }

        public BlockBaseQueryBuilder Table()
        {
            return Append(_dictionary.Table);
        }

        public BlockBaseQueryBuilder NonEncryptedColumn()
        {
            return Append(_dictionary.NonEncryptedColumn);
        }

        public BlockBaseQueryBuilder Join()
        {
            return Append(_dictionary.Join);
        }

        public BlockBaseQueryBuilder On()
        {
            return Append(_dictionary.On);
        }

        public BlockBaseQueryBuilder Range()
        {
            return Append(_dictionary.Range);
        }

        public BlockBaseQueryBuilder PrimaryKey()
        {
            return Append(_dictionary.PrimaryKey);
        }

        public BlockBaseQueryBuilder References()
        {
            return Append(_dictionary.References);
        }
        #endregion

        #region Complex Expressions

        public BlockBaseQueryBuilder UseDatabase(string databaseName)
        {
            return Use().WhiteSpace().Append(databaseName).End();
        }

        public BlockBaseQueryBuilder BeginTransaction()
        {
            return Begin().WhiteSpace().Transaction().End();
        }

        public BlockBaseQueryBuilder RollbackTransaction()
        {
            return Rollback().WhiteSpace().Transaction().End();
        }

        public BlockBaseQueryBuilder CommitTransaction()
        {
            return Commit().WhiteSpace().Transaction().End();
        }

        public BlockBaseQueryBuilder CreateDatabase(string databaseName)
        {
            return Create().WhiteSpace().Database().WhiteSpace().Append(databaseName).End();
        }

        public BlockBaseQueryBuilder CreateTable(string tableName, ColumnDefinition[] columns)
        {
            Create().WhiteSpace().Table().WhiteSpace().Append(tableName).WhiteSpace().WrapListLeft();
            foreach (var column in columns)
            {
                ColumnDefinition(column);
                if (column != columns[^1])
                {
                    ListSeparator().WhiteSpace();
                }
            }
            return WrapListRight().End();
        }

        public BlockBaseQueryBuilder DropTable(string tableName)
        {
           return Drop().WhiteSpace().Table().WhiteSpace().Append(tableName).End();
        }

        public BlockBaseQueryBuilder DropDatabase(string databaseName)
        {
            return Drop().WhiteSpace().Database().WhiteSpace().Append(databaseName).End();
        }

        public BlockBaseQueryBuilder ColumnDefinition(ColumnDefinition column)
        {
            if (!column.IsColumnEncrypted)
            {
                NonEncryptedColumn();
            }
            Append(column.Name).WhiteSpace();
            if (IsEncryptedColumn(column))
            {
                Encrypted().WhiteSpace().Append(column.BucketCount.ToString());
                if (column.IsRange)
                {
                    WhiteSpace().Range().WrapListLeft().Append(column.RangeMinimum.ToString())
                        .ListSeparator().WhiteSpace().Append(column.RangeMaximum.ToString()).WrapListRight();
                }
            }
            else
            {
                Append(column.Type.ToString());
            }

            if (column.IsNotNull && !column.IsPrimaryKey)
            {
                WhiteSpace().NotNull();
            }
            if (column.IsPrimaryKey)
            {
                WhiteSpace().PrimaryKey();
            }

            if (column.IsForeignKey)
            {
                WhiteSpace().References().WhiteSpace().Append(column.ParentTableName).WrapListLeft()
                            .Append(column.ParentTableKeyName).WrapListRight();
            }
            return this;
        }

        public BlockBaseQueryBuilder IfStatement(string condition, string[] statements)
        {
            If().WhiteSpace().Append(condition).WhiteSpace().Execute().WhiteSpace().QueryBatchLeftWrapper();
            foreach (var statement in statements)
            {
                Append(statement).WhiteSpace();
            }
            return QueryBatchRightWrapper().End();
        }

        public BlockBaseQueryBuilder Insert(string tableName, string[] columns, object[][] values)
        {
            Insert().WhiteSpace().Into().WhiteSpace().Append(tableName).WhiteSpace().WrapListLeft();
            foreach (var column in columns)
            {
                Append(column);
                if (column != columns[^1])
                {
                    ListSeparator().WhiteSpace();
                }
            }
            WrapListRight().WhiteSpace().Values().WhiteSpace();
            foreach (var record in values)
            {
                WrapListLeft();
                foreach (var column in record)
                {
                    WrapValue(column);
                    if (column != record[^1])
                    {
                        ListSeparator().WhiteSpace();
                    }
                }
                WrapListRight();
                if (record != values[^1])
                {
                    ListSeparator().WhiteSpace();
                }
            }
            return End();
        }

        public BlockBaseQueryBuilder Delete(string tableName, ExpressionNode condition = null)
        {
            Delete().WhiteSpace().From().WhiteSpace().Append(tableName);
            if (condition != null)
            {
                WhiteSpace().Where().WhiteSpace().Condition(condition);
            }

            return End();
        }

        public BlockBaseQueryBuilder Update(string tableName, (string, object)[] values, ExpressionNode condition = null)
        {
            Update().WhiteSpace().Append(tableName).WhiteSpace().Set().WhiteSpace();
            foreach (var (column, value) in values)
            {
                Append(column).WhiteSpace().EqualTo().WhiteSpace().WrapValue(value);
                if ((column, value) != values[^1])
                {
                    ListSeparator().WhiteSpace();
                }
            }
            if (condition != null)
            {
                WhiteSpace().Where().Condition(condition);
            }
            return End();
        }

        public BlockBaseQueryBuilder UpdateCase()
        {
            //TODO : Implement case when scenario;
            throw new NotImplementedException();
        }

        public BlockBaseQueryBuilder Select((string, string)[] columns, string originTable, (TableColumn, TableColumn)[] joins, ComparisonNode condition = null, bool isEncrypted = false, int limit = 0, int offset = 0)
        {
            Select().WhiteSpace();
            foreach (var (table, column) in columns)
            {
                Append(table).Append(_dictionary.TableColumnSeparator).Append(string.IsNullOrEmpty(column)? _dictionary.AllSelector : column);
                if ((table, column) != columns[^1])
                {
                    ListSeparator().WhiteSpace();
                }
            }
            WhiteSpace().From().WhiteSpace().Append(originTable).WhiteSpace();
            if (joins != null)
            {
                var last = joins[^1];
                foreach (var join in joins)
                {
                    Join().WhiteSpace().Append(join.Item1.Table).WhiteSpace().On()
                        .WhiteSpace().Append(join.Item1.Table).TableColumnSeparator().Append(join.Item1.Column)
                        .WhiteSpace().EqualTo().WhiteSpace().Append(join.Item2.Table).TableColumnSeparator()
                        .Append(join.Item2.Column);
                    if (!(join == last))
                    {
                        ListSeparator().WhiteSpace();
                    }
                }
                
            }

            if (condition != null)
            {
                Where().Condition(condition);
            }

            if (isEncrypted)
            {
                WhiteSpace().Encrypted();
            }

            if (limit > 0)
            {
                WhiteSpace().Limit().WhiteSpace().WrapValue(limit);
            }
            if (offset > 0)
            {
                WhiteSpace().Offset().WhiteSpace().WrapValue(offset);
            }
            return End();
        }

        #endregion


        #region Validation

        private static bool IsEncryptedColumn(ColumnDefinition column)
        {
            return column.IsValueEncrypted || column.IsRange;
        }

        #endregion

        #region Operations

        public BlockBaseQueryBuilder WrapValue(object value)
        {
            if (value is Enum enumValue)
            {
                value = Convert.ChangeType(enumValue, enumValue.GetTypeCode());
            }

            if (value is DateTime dtValue)
            {
                value = dtValue.ToString(CultureInfo.InvariantCulture);
            }
            return Append(value.IsNumber() ? value.ToString() : $"{_dictionary.LeftTextWrapper}{value}{_dictionary.RightTextWrapper}");
        }


        #endregion

        #region Condition

        public BlockBaseQueryBuilder Condition(ExpressionNode node)
        {
            return ParseNode(node);
        }

        private BlockBaseQueryBuilder ParseNode(ExpressionNode node)
        {
            switch (node)
            {
                case BinaryNode binaryNode:
                    var leftParse = ParseNode(binaryNode.LeftNode);
                    if (leftParse == null)
                    {
                        throw new InvalidExpressionNodeException(binaryNode);
                    }
                    WhiteSpace();
                    var @operator = ParseOperation(binaryNode.Operator);
                    if (@operator == null)
                    {
                        throw new InvalidExpressionNodeException(binaryNode);
                    }
                    WhiteSpace();
                    var rightParse = ParseNode(binaryNode.RightNode);
                    if (rightParse == null)
                    {
                        throw new InvalidExpressionNodeException(binaryNode);
                    }
                    break;
                case ValueNode valueNode:
                    WrapValue(valueNode.Value);
                    break;
                case PropertyNode propertyNode:
                    var table = propertyNode.Property.DeclaringType.GetTableName();
                    var column = propertyNode.Property.GetColumnName();
                    Append(table).TableColumnSeparator().Append(column);
                    break;
            }

            return this;
        }

        private BlockBaseQueryBuilder ParseOperation(BlockBaseOperator @operator)
        {
            switch (@operator)
            {
                case BlockBaseOperator.And:
                    return Append(_dictionary.And);
                case BlockBaseOperator.Or:
                    return Append(_dictionary.Or);
                case BlockBaseOperator.GreaterOrEqualTo:
                    return Append(_dictionary.EqualOrGreaterThan);
                case BlockBaseOperator.GreaterThan:
                    return Append(_dictionary.GreaterThan);
                case BlockBaseOperator.LessThan:
                    return Append(_dictionary.LessThan);
                case BlockBaseOperator.LessOrEqualTo:
                    return Append(_dictionary.EqualOrLessThan);
                case BlockBaseOperator.DifferentFrom:
                    return Append(_dictionary.DifferentFrom);
                case BlockBaseOperator.EqualTo:
                    return Append(_dictionary.ValueEquals);
                default:
                    return null;
            }
        }


        #endregion
    }
}
