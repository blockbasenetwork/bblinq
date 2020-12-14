using System;
using System.Collections.Generic;
using System.Linq;
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
            var organizedTables = SortTablesByDependency(_tables);
            foreach (var type in organizedTables)
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

        private Type[] SortTablesByDependency(Type[] tables)
        {
            var tableList = new List<Type>(tables);
            var organizedTables = new List<Type>();
            
            var add = false;
            for(var tableCounter = 0; tableCounter < tableList.Count; tableCounter++)
            {
                var table = tableList[tableCounter];

                var foreignKeys = table.GetForeignKeys();
                if (foreignKeys.Length == 0)
                {
                    add = true;
                }
                else
                {
                    add = true;
                    foreach(var key in foreignKeys)
                    {
                        var organizedTableNames = organizedTables.Select(x => x.GetTableName());
                        if(!organizedTableNames.ToList().Contains(key.Name))
                        {
                            add = false;
                            break;
                        }
                    }
                }
                if (add)
                {
                    organizedTables.Add(table);
                    tableList.Remove(table);
                    tableCounter--;
                    if (tableList.Count == 0)
                    {
                        break;
                    }
                }
                
                if (tableCounter == tableList.Count-1 && tableList.Count > 0)
                {
                    tableCounter = 0;
                }
            }
            return organizedTables.ToArray();
        }
    }
}
