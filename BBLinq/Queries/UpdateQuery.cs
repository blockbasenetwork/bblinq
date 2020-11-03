using System.Linq.Expressions;
using agap2IT.Labs.BlockBase.BBLinq.Parser;

namespace agap2IT.Labs.BlockBase.BBLinq.Queries
{
    public class UpdateQuery<T>
    {
        public T Record { get; }
        public LambdaExpression Where { get; }
        public UpdateQuery(T record, LambdaExpression where)
        {
            Record = record;
            Where = where;
        }
        
        public override string ToString()
        {
            return QueryParser.ParseUpdateRecordQuery<T>(this);
        }
    }
}
