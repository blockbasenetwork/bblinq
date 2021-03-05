using System.Threading.Tasks;

namespace BlockBase.BBLinq.Executors
{
    public interface IQueryExecutor
    {
        public Task ExecuteQueryAsync(string query, bool setDatabase = true);
        public Task<T> ExecuteQueryAsync<T>(string query, bool setDatabase = true);
    }
}
