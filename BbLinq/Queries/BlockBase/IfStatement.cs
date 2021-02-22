using BlockBase.BBLinq.Context.Base;
using BlockBase.BBLinq.Properties;
using BlockBase.BBLinq.Queries.Base;
using BlockBase.BBLinq.QueryExecutors;
using BlockBase.BBLinq.Result;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlockBase.BBLinq.Queries.BlockBase
{
    public class IfStatement : BlockBaseQuery
    {
        public Query Condition { get; private set; }

        public List<Query> Executables { get; private set; }

        public IfStatement(Query condition)
        {
            Condition = condition;
        }

        public async Task<QueryResult> Then(Func<List<Query>> executingList)
        {
            Executables = executingList.Invoke();
            var executor = ContextCache.Instance.Get<BlockBaseQueryExecutor>(Resources.CACHE_EXECUTOR);
            var queryString = GenerateQuery();
            return await executor.ExecuteQueryAsync(queryString);
        }

        public override string GenerateQuery()
        {
            var conditionString = Condition.GenerateQuery();
            var executables = new List<string>();
            foreach(var executable in Executables)
            {
                executables.Add(executable.GenerateQuery());
            }
            return QueryBuilder.If(conditionString, executables.ToArray()).ToString();
        }
    }
}
