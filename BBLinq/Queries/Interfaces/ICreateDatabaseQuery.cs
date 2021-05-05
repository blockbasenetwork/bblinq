using System;

namespace BlockBase.BBLinq.Queries.Interfaces
{
    public interface ICreateDatabaseQuery : IQuery
    {
        public Type[] EntityTypes { get; }
        public string DatabaseName { get; }
    }
}
