using System.Linq.Expressions;
using agap2IT.Labs.BlockBase.BBLinq.Parser;

namespace agap2IT.Labs.BlockBase.BBLinq.Queries
{
    public class DeleteQuery<T>
    {
        public LambdaExpression Where { get; }
        public DeleteQuery(LambdaExpression where)
        {
            Where = where;
        }
        public override string ToString()
        {
            return QueryParser.ParseDeleteRecordQuery(this);
        }
    }
}
