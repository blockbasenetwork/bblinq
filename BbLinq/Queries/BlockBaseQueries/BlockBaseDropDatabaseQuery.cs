using BlockBase.BBLinq.Queries.BaseQueries;
using BlockBase.BBLinq.QueryBuilders;

namespace BlockBase.BBLinq.Queries.BlockBaseQueries
{
    public class BlockBaseDropDatabaseQuery : DropDatabaseQuery
    {
        public BlockBaseDropDatabaseQuery(string databaseName) : base(databaseName)
        {
        }

        public override string ToString()
        {
            var queryBuilder = new BbSqlQueryBuilder();
            queryBuilder.DropDatabase(DatabaseName);
            
            return queryBuilder.ToString();
        }
    }
}
