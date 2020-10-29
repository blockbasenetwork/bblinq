using BBLinq.ExtensionMethods;
using BBLinq.Parser;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BBLinq.Queries
{
   
    public class SelectQuery<T>
    {
        public Type Origin { get; }
        public IEnumerable<LambdaExpression> Joins { get; }
        private LambdaExpression Where { get; }
        private LambdaExpression Select { get; }

        public SelectQuery(Type origin, IEnumerable<LambdaExpression> joins, LambdaExpression where, LambdaExpression select)
        {
            Origin = origin;
            Joins = joins;
            Where = where;
            Select = select;
        }

        public override string ToString()
        {
            var select = (Select == null) ? QueryParser.SelectWhole<T>() : QueryParser.ParseSelect(Select);
            var from = QueryParser.From(Origin) + (Joins.IsNullOrEmpty() ?  "" : QueryParser.ParseJoins(Joins));
            var where = (Where != null) ? " " +QueryParser.ParseFilter(Where):"";
            return $"{select} {from}{where}{QueryParser.End()}";
        }
    }
}
