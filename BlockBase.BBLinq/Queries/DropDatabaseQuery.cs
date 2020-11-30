namespace BlockBase.BBLinq.Queries
{
    public class DropDatabaseQuery : BbQuery
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

        /// <summary>
        /// Generates the SQL query and returns it in a string format
        /// </summary>
        /// <returns>the stringified query</returns>
        public override string ToString()
        {
            QueryBuilder.Clear()
                        .Drop().WhiteSpace().Database().WhiteSpace().Append(_databaseName).End();
            return base.ToString();
        }
    }
}
