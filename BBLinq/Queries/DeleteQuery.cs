namespace BBLinq.Queries
{
    public class DeleteQuery<T>
    {
        private T _record;
        public DeleteQuery(T record)
        {
            _record = record;
        }
        public override string ToString()
        {
            return string.Empty;
        }
    }
}
