using System.Collections.Generic;

namespace BlockBase.BBLinq.Model.Responses
{
    internal class ExecutionResult
    {
        public bool Executed { get; set; }
        public string Message { get; set; }
    }

    internal class ExecutionResult<T> : ExecutionResult
    {
        public IEnumerable<T> Content { get; set; }
    }
}
