using agap2IT.Labs.BlockBase.BBLinq.Parser;

namespace agap2IT.Labs.BlockBase.BBLinq.Queries
{
    public class InsertQuery<T>
    {

        public T Record { get; }
        public InsertQuery(T record)
        {
            Record = record;
        }
        public override string ToString()
        {
            return QueryParser.ParseInsertRecordQuery(this);
        }
    }
}
