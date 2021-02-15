namespace BlockBase.BBLinq.QueryBuilders
{
    public class DbQueryBuilder<TReturn> where TReturn : DbQueryBuilder<TReturn>
    {
        private string _content;

        /// <summary>
        /// Appends content to a query builder and returns it updated
        /// </summary>
        public TReturn Append(string content)
        {
            _content += content;
            return (TReturn)this;
        }

        /// <summary>
        /// Cleans the content on a query
        /// </summary>
        public virtual TReturn Clear()
        {
            _content = string.Empty;
            return (TReturn)this;
        }

        /// <summary>
        /// Returns the string query
        /// </summary>
        public override string ToString()
        {
            return _content;
        }
    }
}
