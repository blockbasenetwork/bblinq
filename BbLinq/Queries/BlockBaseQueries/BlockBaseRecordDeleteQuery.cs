using BlockBase.BBLinq.Builders;
using BlockBase.BBLinq.Model.Base;
using BlockBase.BBLinq.Queries.Base;

namespace BlockBase.BBLinq.Queries.BlockBaseQueries
{
    public class BlockBaseRecordDeleteQuery:BlockBaseQuery, IDeleteDatabaseQuery
    {
        public string DatabaseName { get; }
        internal ExpressionNode Condition { get; }

        public BlockBaseRecordDeleteQuery(string databaseName, bool isEncrypted) : base(isEncrypted)
        {
            DatabaseName = databaseName;
        }

        internal BlockBaseRecordDeleteQuery(string databaseName, ExpressionNode condition, bool isEncrypted) : base(isEncrypted)
        {
            DatabaseName = databaseName;
            Condition = condition;
        }

        public override string GenerateQueryString()
        {
            var queryBuilder = new BlockBaseQueryBuilder();
            queryBuilder.DeleteRecord(DatabaseName, Condition, IsEncrypted);
            return queryBuilder.ToString();
        }
    }
}
