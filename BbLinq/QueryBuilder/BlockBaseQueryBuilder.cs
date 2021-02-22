using BlockBase.BBLinq.Dictionaries;
using BlockBase.BBLinq.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using BlockBase.BBLinq.Pocos;
using BlockBase.BBLinq.Queries.Base;

namespace BlockBase.BBLinq.QueryBuilder
{
    public class BlockBaseQueryBuilder
    {
        private string _content;

        public BbSqlDictionary Dictionary { get; set; }

        public BlockBaseQueryBuilder()
        {
            Dictionary = new BbSqlDictionary();
            _content = string.Empty;
        }

        #region Operations

        public BlockBaseQueryBuilder Append(string content)
        {
            _content += content;
            return this;
        }

        #endregion


        #region Simple Expressions

        public BlockBaseQueryBuilder Create()
        {
            return Append(Dictionary.Create);
        }

        public BlockBaseQueryBuilder End()
        {
            return Append(Dictionary.QueryEnd);
        }

        public BlockBaseQueryBuilder Drop()
        {
            return Append(Dictionary.Drop);
        }

        public BlockBaseQueryBuilder Database()
        {
            return Append(Dictionary.DataBase);
        }

        public BlockBaseQueryBuilder Delete()
        {
            return Append(Dictionary.Delete);
        }

        public BlockBaseQueryBuilder From()
        {
            return Append(Dictionary.From);
        }

        public BlockBaseQueryBuilder WhiteSpace()
        {
            return Append(Dictionary.WhiteSpace);
        }

        public BlockBaseQueryBuilder TableColumnSeparator()
        {
            return Append(Dictionary.TableColumnSeparator);
        }

        public BlockBaseQueryBuilder If()
        {
            return Append(Dictionary.If);
        }

        public BlockBaseQueryBuilder Execute()
        {
            return Append(Dictionary.Execute);
        }

        public BlockBaseQueryBuilder QueryBatchLeftWrapper()
        {
            return Append(Dictionary.QueryBatchWrapperLeft);
        }

        public BlockBaseQueryBuilder QueryBatchRightWrapper()
        {
            return Append(Dictionary.QueryBatchWrapperRight);
        }

        public BlockBaseQueryBuilder Where()
        {
            return Append(Dictionary.Where);
        }

        public BlockBaseQueryBuilder EqualTo()
        {
            return Append(Dictionary.ValueEquals);
        }

        public BlockBaseQueryBuilder Insert()
        {
            return Append(Dictionary.Insert);
        }

        public BlockBaseQueryBuilder Into()
        {
            return Append(Dictionary.Into);
        }

        public BlockBaseQueryBuilder ListSeparator()
        {
            return Append(Dictionary.ColumnSeparator);
        }

        public BlockBaseQueryBuilder Values()
        {
            return Append(Dictionary.Values);
        }

        public BlockBaseQueryBuilder WrapListLeft()
        {
            return Append(Dictionary.LeftListWrapper);
        }

        public BlockBaseQueryBuilder WrapListRight()
        {
            return Append(Dictionary.RightListWrapper);
        }

        public BlockBaseQueryBuilder Update()
        {
            return Append(Dictionary.Update);
        }

        public BlockBaseQueryBuilder Set()
        {
            return Append(Dictionary.Set);
        }
        #endregion

        #region Complex Expressions
        public BlockBaseQueryBuilder Clear()
        {
            _content = string.Empty;
            return this;
        }

        public BlockBaseQueryBuilder WrapString(string content)
        {
            return Append(Dictionary.LeftTextWrapper).Append(content).Append(Dictionary.RightTextWrapper);
        }

        public BlockBaseQueryBuilder WrapValue(object value)
        {
            var valueType = value.GetType();

            if (value.IsNumber())
            {
                return Append(value.ToString());
            }

            if (valueType == typeof(Guid))
            {
                return WrapString(value.ToString());
            }

            if (valueType == typeof(DateTime))
            {
                return WrapString(((DateTime)value).ToString(CultureInfo.InvariantCulture));
            }

            return WrapString(value.ToString());
        }

        public BlockBaseQueryBuilder From(string tableName)
        {
            return From().WhiteSpace().Append(tableName);
        }

        public BlockBaseQueryBuilder Into(string tableName)
        {
            return Into().WhiteSpace().Append(tableName);
        }

        public BlockBaseQueryBuilder Where(string condition)
        {
            return Where().WhiteSpace().Append(condition);
        }

        public BlockBaseQueryBuilder ConcatenateTableWithColumn(string tableName, string columnName)
        {
            return Append(tableName).TableColumnSeparator().Append(columnName);
        }

        public BlockBaseQueryBuilder EqualsTo(string tableName, string columnName, object value)
        {
            return ConcatenateTableWithColumn(tableName, columnName).WhiteSpace().EqualTo().WrapValue(value);
        }

        public BlockBaseQueryBuilder WrapColumnList(string[] columns)
        {
            WrapListLeft();
            foreach (var column in columns)
            {
                Append(column);
                if (column != columns[^1])
                {
                    ListSeparator().WhiteSpace();
                }
            }
            return WrapListRight();
        }

        public BlockBaseQueryBuilder WrapValues(object[,] values)
        {
            for (var recordCounter = 0; recordCounter < values.GetLength(0); recordCounter++)
            {
                WrapListLeft();
                for (var propertyCounter = 0; propertyCounter < values.GetLength(1); propertyCounter++)
                {
                    var currentValue = values[recordCounter, propertyCounter];

                    WrapValue(currentValue);
                    if (propertyCounter != values.GetLength(1)-1)
                    {
                        ListSeparator().WhiteSpace();
                    }
                }
                WrapListRight();
                if (recordCounter != values.GetLength(0) - 1)
                {
                    ListSeparator().WhiteSpace();
                }
            }

            return this;
        }

        #endregion

        #region Full Expressions

        #region Create Database
        public BlockBaseQueryBuilder CreateDatabase(string databaseName, Dictionary<Type, List<BlockBaseColumn>> columns)
        {
            Create().WhiteSpace().Database().WhiteSpace().Append(databaseName).End();
            foreach (var table in columns.Keys)
            {
                CreateTable(table.GetTableName(), columns[table].ToArray());
            }
            return this;
        }
        #endregion

        #region Create Table
        public BlockBaseQueryBuilder CreateTable(string tableName, BlockBaseColumn[] columns)
        {
            Append(Dictionary.Create).WhiteSpace().Append(Dictionary.Table)
                .WhiteSpace().Append(tableName)
                .Append(Dictionary.LeftListWrapper)
                .ListColumns(columns).Append(Dictionary.RightListWrapper);
            return End();
        }

        private BlockBaseQueryBuilder ListColumns(BlockBaseColumn[] columns)
        {
            foreach (var column in columns)
            {
                Column(column);
                if (column != columns[^1])
                {
                    ListSeparator().WhiteSpace();
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
                    .Append(column.BucketCount.ToString()).ListSeparator()
                    .Append(column.MinRange.ToString()).ListSeparator()
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
            if (column.IsPrimaryKey)
            {
                WhiteSpace();
                Append(Dictionary.PrimaryKey);
            }

            if (column.IsForeignKey)
            {
                WhiteSpace();
                Append(Dictionary.References).WhiteSpace()
                    .Append(column.ForeignTable).Append(Dictionary.LeftListWrapper)
                    .Append(column.ForeignColumn).Append(Dictionary.RightListWrapper);
            }
        }

        #endregion

        #region Drop Database
        public BlockBaseQueryBuilder DropDatabase(string databaseName)
        {
            return Clear().Drop().WhiteSpace().Database().WhiteSpace().Append(databaseName).End();
        }
        #endregion

        #region Delete
        public BlockBaseQueryBuilder DeleteAllRecords(string tableName)
        {
            return Clear().Delete().WhiteSpace().From(tableName).End();
        }

        public BlockBaseQueryBuilder DeleteRecordWithCondition(string tableName, string condition)
        {
            return Clear().Delete().WhiteSpace().From(tableName).WhiteSpace().Where(condition).End();
        }

        #endregion

        #region Insert

        public BlockBaseQueryBuilder InsertValuesIntoTable(string tableName, string[] columns, object[,] values)
        {
            Clear().Insert().WhiteSpace().Into(tableName).WhiteSpace().WrapColumnList(columns).WhiteSpace().Values().WhiteSpace().WrapValues(values);
            return End();
        }

        #endregion

        #region if statement
        public BlockBaseQueryBuilder If(string queryString, string[] executableStrings)
        {
            If().WhiteSpace().Append(queryString).WhiteSpace().Execute().QueryBatchLeftWrapper();
            foreach(var executable in executableStrings)
            {
                Append(executable);
            }
            return WhiteSpace().QueryBatchRightWrapper().End();
        }

        #endregion

        #region Simple Update

        public BlockBaseQueryBuilder UpdateValuesInTable(string tableName, Dictionary<string, object> values, string condition)
        {
            return Update().WhiteSpace().Append(tableName).WhiteSpace().Set().WhiteSpace().SetValue(values).WhiteSpace()
                .Where(condition).End();
        }

        private BlockBaseQueryBuilder SetValue(Dictionary<string, object> values)
        {
            var last = values.Keys.ToArray()[^1];
            foreach (var (key, val) in values)
            {
                Append(key).WhiteSpace().EqualTo().WhiteSpace().WrapValue(val);

                if (key != last)
                {
                    ListSeparator().WhiteSpace();
                }
            }

            return this;
        }

        #endregion

        public override string ToString()
        {
            return _content;
        }

        #endregion
    }
}
