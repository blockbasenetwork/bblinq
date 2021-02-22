using BlockBase.BBLinq.Queries.Base;

namespace BlockBase.BBLinq.Queries.BlockBase
{
    public class DropDatabaseQuery : BlockBaseQuery
    {
        private readonly string _databaseName;

        /// <summary>
        /// The default constructor
        /// </summary>
        /// <param name="databaseName">the database's name</param>
        public DropDatabaseQuery(string databaseName)
        {
            _databaseName = databaseName;
        }

        public override string GenerateQuery()
        {
            return QueryBuilder.DropDatabase(_databaseName).ToString();
        }
    }
}
