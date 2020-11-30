using System.Collections.Generic;
using BlockBase.BBLinq.Context;
using BlockBase.BBLinq.Dictionaries;

namespace BlockBase.BBLinq.Builders
{

    public abstract class SqlQueryBuilder
    {
        private string _content;

        protected SqlQueryBuilder()
        {
        }

        public TResult Append<TResult>(string content) where TResult:SqlQueryBuilder
        {
            _content += content;
            return (TResult)this;
        }

        public virtual TReturn Clear<TReturn>() where TReturn : SqlQueryBuilder
        {
            _content = string.Empty;
            return (TReturn)this;
        }

        public override string ToString()
        {
            return _content;
        }
    }
}
