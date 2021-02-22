namespace BlockBase.BBLinq.Result
{
    public class QueryResult
    {
        public bool Success { get; }
        public string Message { get; }

        public QueryResult(bool success, string message)
        {
            Success = success;
            Message = message;
        }
    }

    public class QueryResult<T> : QueryResult
    {
        public T Result { get; }

        public QueryResult(bool success, string message, T result) : base(success, message)
        {
            Result = result;
        }
    }
}
