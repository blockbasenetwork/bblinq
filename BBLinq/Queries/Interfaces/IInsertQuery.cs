using System;

namespace BlockBase.BBLinq.Queries.Interfaces
{
    public interface IInsertQuery : IQuery
    {
        public Type EntityType { get; }
        public object[] Records { get; }
    }
}
