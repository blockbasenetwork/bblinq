namespace BBLinq.Queries
{
    public class UpdateQuery<T>
    {
        private T _record;

        public UpdateQuery(T record)
        {
            _record = record;
        }
        
        public override string ToString()
        {
            return string.Empty;
        }
    }
}
