using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace BlockBase.BBLinq.Queries.BaseQueries
{
    public abstract class SelectRecordQuery<T> : Query
    {
        public LambdaExpression Condition { get; protected set; }

        protected SelectRecordQuery(LambdaExpression condition)
        {
            Condition = condition;
        }
    }
}
