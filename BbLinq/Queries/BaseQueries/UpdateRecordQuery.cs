using System;
using System.Collections.Generic;
using System.Text;

namespace BlockBase.BBLinq.Queries.BaseQueries
{
    public abstract class UpdateQuery<T> : Query
    {
        public List<T> Records { get; protected set; }


        protected UpdateQuery(T record)
        {
            Records = new List<T>();
            Records.Add(record);
        }

        protected UpdateQuery(List<T> records)
        {
            Records = records;
        }
    }
}
