using System;

namespace BlockBase.BBLinq.Exceptions
{
    public class QueryExecutionException : Exception
    {
        public QueryExecutionException(string message) : base($"The query execution failed. {message}.")
        {
        }
    }
}
