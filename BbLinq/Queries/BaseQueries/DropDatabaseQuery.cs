namespace BlockBase.BBLinq.Queries.BaseQueries
{
    public abstract class DropDatabaseQuery : Query
    {
        public string DatabaseName { get; protected set; }

        protected DropDatabaseQuery(string databaseName)
        {
            DatabaseName = databaseName;
        }
    }
}
