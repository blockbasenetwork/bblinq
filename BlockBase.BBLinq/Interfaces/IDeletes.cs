using System.Threading.Tasks;
using BlockBase.BBLinq.Results;

namespace BlockBase.BBLinq.Interfaces
{
    public interface IDeletes<in T>
    {
        public Task<QueryResult> Delete(T record);
        public Task<QueryResult> Delete();
    }
}
