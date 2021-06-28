using BlockBase.BBLinq.Queries.Interfaces;
using System.Threading.Tasks;

namespace BlockBase.BBLinq.Sets.Interfaces
{
    public interface IFetchableSet<T> : IBlockBaseBaseSet<IFetchableSet<T>>
    {
        public ISelectQuery GetGetQuery(object id);

        public void BatchGet(object id);

        public Task<T> GetAsync(object id);
    }
}
