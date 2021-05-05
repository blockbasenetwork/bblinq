using BlockBase.BBLinq.Builders;
using BlockBase.BBLinq.Queries.Base;
using BlockBase.BBLinq.Queries.Interfaces;

namespace BlockBase.BBLinq.Queries.BlockBaseQueries
{
    public class BlockBaseDropDatabaseQuery : BlockBaseQuery, IQuery
    {
        public string DatabaseName { get; }

        public BlockBaseDropDatabaseQuery(string databaseName) : base(false)
        {
            DatabaseName = databaseName;
        }

        public override string GenerateQueryString()
        {
            var queryBuilder = new BlockBaseQueryBuilder();
            queryBuilder.DropDatabase(DatabaseName, false);
            return queryBuilder.ToString();
        }
    }
}
