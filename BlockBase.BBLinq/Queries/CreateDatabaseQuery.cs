using System;
using BlockBase.BBLinq.ExtensionMethods;
using BlockBase.BBLinq.Pocos;

namespace BlockBase.BBLinq.Queries
{
    public class CreateDatabaseQuery : BbQuery
    {
        private readonly string _databaseName;
        private readonly Type[] _tables;

        public CreateDatabaseQuery(string databaseName, Type[] tables)
        {
            _databaseName = databaseName;
            _tables = tables;
        }

        public override string ToString()
        {
            QueryBuilder.Clear()
                        .Create().WhiteSpace().Database().WhiteSpace().Append(_databaseName).End();

            foreach (var type in _tables)
            {
                QueryBuilder.Create().WhiteSpace().Table(type.GetTableName()).WrapLeftListSide();
                var fields = type.GetProperties();
                foreach (var field in fields)
                {
                    var dbField = DbFieldInfo.From(field, _tables);
                    QueryBuilder.Field(dbField);
                    if (field != fields[^1])
                    {
                        QueryBuilder.FieldSeparator().WhiteSpace();
                    }
                }
                QueryBuilder.WrapRightListSide().End();
            }

            return base.ToString();
        }
    }
}
