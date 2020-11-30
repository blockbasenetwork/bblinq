using System;

namespace BlockBase.BBLinq.Exceptions
{
    /// <summary>
    /// Exception thrown where a query is executed without an available context
    /// </summary>
    public class NoContextAvailableException: Exception
    {
        public NoContextAvailableException():base("No context set for query execution"){}
    }
}
