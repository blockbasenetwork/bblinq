using System;
using System.Collections.Generic;
using System.Text;

namespace BlockBase.BBLinq.Exceptions
{
    public class QueryExecutionException : Exception
    {
        public QueryExecutionException(string message) : base($"The query execution failed. {message}.")
        {
        }
    }
}
