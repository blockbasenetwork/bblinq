namespace BlockBase.BBLinq.Results
{
    /// <summary>
    /// A query response
    /// </summary>
    public class QueryResult
    {
        /// <summary>
        /// Checks if the query executed with success
        /// </summary>
        public bool Succeeded { get; set; }

        /// <summary>
        /// The resulting exception
        /// </summary>
        public string Exception { get; set; }


        /// <summary>
        /// The response message
        /// </summary>
        public string ResponseMessage { get; set; }
    }

    public class QueryResult<T> : QueryResult
    {
        public T Result { get; set; }
    }
}
