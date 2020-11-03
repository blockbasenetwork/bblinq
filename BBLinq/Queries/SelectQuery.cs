using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using agap2IT.Labs.BlockBase.BBLinq.Parser;

namespace agap2IT.Labs.BlockBase.BBLinq.Queries
{
   
    public class SelectQuery<T>
    {
        public Type Origin { get; }
        public IEnumerable<LambdaExpression> Joins { get; }
        public LambdaExpression Where { get; }
        public LambdaExpression Select { get; }

        public SelectQuery(Type origin, IEnumerable<LambdaExpression> joins, LambdaExpression where, LambdaExpression select)
        {
            Origin = origin;
            Joins = joins;
            Where = where;
            Select = select;
        }

        public override string ToString()
        {
            return QueryParser.ParseSelectRecordsQuery(this);
        }
    }
}
