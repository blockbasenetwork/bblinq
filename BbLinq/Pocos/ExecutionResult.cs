namespace BlockBase.BBLinq.Pocos
{
    public class ExecutionResult
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
    }

    public class ExecutionResult<T>
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
        public T Result { get; set; }
    }
}
