using BlockBase.BBLinq.QueryExecutors;
using BlockBase.BBLinq.Settings;

namespace BlockBase.BBLinq.Sets.Base
{
    public class BlockBaseBaseSet<TResult> : DatabaseSet<TResult, BlockBaseQueryExecutor, BlockBaseSettings> where TResult : BlockBaseBaseSet<TResult>
    {
        public BlockBaseBaseSet(BlockBaseQueryExecutor executor, BlockBaseSettings settings) : base(executor, settings)
        {

        }
    }
}
