using System;
using BlockBase.BBLinq.Model.Base;

namespace BlockBase.BBLinq.Queries.Interfaces
{
    public interface IUpdateQuery
    {
        public Type EntityType { get; }
        public object Record { get; }
    }
}
