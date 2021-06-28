using BlockBase.BBLinq.Queries.Interfaces;
using BlockBase.BBLinq.Settings.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlockBase.BBLinq.QueryExecutors.Interfaces
{
    public interface IQueryExecutor
    {
        public bool UseDatabase { get; set; }
        public Task ExecuteQueryAsync(IQuery query, DatabaseSettings settings);
        public Task<IEnumerable<TResult>> ExecuteQueryAsync<TResult>(ISelectQuery query, DatabaseSettings settings);
    }
}
