using System;
using System.Collections.Generic;
using System.Text;

namespace BlockBase.BBLinq.Queries.BaseQueries
{
    public abstract class InsertRecordQuery<T> : Query
    {
        /// <summary>
        /// An object used as reference for the DELETE operation
        /// </summary>
        public T Record { get; protected set; }

        protected InsertRecordQuery(T record)
        {
            Record = record;
        }
    }
}
