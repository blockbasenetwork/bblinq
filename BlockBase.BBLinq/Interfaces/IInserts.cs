using System.Threading.Tasks;
using BlockBase.BBLinq.Results;

namespace BlockBase.BBLinq.Interfaces
{
    public interface IInserts<in T>
    {
        public Task<QueryResult> Insert(T record);
    }
}
