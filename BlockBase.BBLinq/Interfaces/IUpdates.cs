using System.Threading.Tasks;
using BlockBase.BBLinq.Results;

namespace BlockBase.BBLinq.Interfaces
{
    public interface IUpdates<in T>
    {
        public Task<QueryResult> Update(T record);
    }
}
