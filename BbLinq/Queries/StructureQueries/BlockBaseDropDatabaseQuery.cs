using BlockBase.BBLinq.Builders;
using BlockBase.BBLinq.Queries.Base;

namespace BlockBase.BBLinq.Queries.StructureQueries
{
    public class BlockBaseDropDatabaseQuery : IQuery
    {
        public string DatabaseName { get; }

        public BlockBaseDropDatabaseQuery(string databaseName)
        {
            DatabaseName = databaseName;
        }

        public string GenerateQueryString()
        {
            var queryBuilder = new BlockBaseQueryBuilder();
            queryBuilder.DropDatabase(DatabaseName);
            return queryBuilder.ToString();
        }
    }
}
