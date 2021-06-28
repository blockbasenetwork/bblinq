using System;

namespace BlockBase.BBLinq.Queries.Interfaces
{
    public interface IUpdateQuery
    {
        public Type EntityType { get; }
        public object Record { get; }
    }
}
