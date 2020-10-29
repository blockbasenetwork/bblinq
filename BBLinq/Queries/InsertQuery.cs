using BBLinq.Parser;

namespace BBLinq.Queries
{
    public class InsertQuery<T>
    {
        private T _record;
        public InsertQuery(T record)
        {
            _record = record;
        }
        public override string ToString()
        {
            return QueryParser.ParseInsert(_record);
        }
    }
}
