using System;
using BlockBase.BBLinq.Model.Base;

namespace BlockBase.BBLinq.Queries.Interfaces
{
    public interface IDeleteQuery : IQuery
    {
        public Type EntityType {get;}
        internal ExpressionNode Condition {get;}
    }
}
