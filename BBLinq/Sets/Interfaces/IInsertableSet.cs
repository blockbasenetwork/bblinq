using BlockBase.BBLinq.Queries.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlockBase.BBLinq.Sets.Interfaces
{
    public interface IInsertableSet<in T> : IBlockBaseBaseSet<IInsertableSet<T>>
    {
        public IQuery GetInsertQuery(T records);
        public void BatchInsert(T record);
        public Task InsertAsync(T record);

        public IQuery GetInsertQuery(IEnumerable<T> records);
        public void BatchInsert(IEnumerable<T> records);
        public Task InsertAsync(IEnumerable<T> records);
    }
}
