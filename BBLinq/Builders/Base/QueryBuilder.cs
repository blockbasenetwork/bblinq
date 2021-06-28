using BlockBase.BBLinq.Dictionaries.Base;

namespace BlockBase.BBLinq.Builders.Base
{
    internal abstract class QueryBuilder<T, TDictionary> where T : QueryBuilder<T, TDictionary> where TDictionary : IDictionary
    {
        protected string Content;

        protected TDictionary Dictionary { get; set; }

        protected QueryBuilder()
        {
            Content = string.Empty;
        }

        public T Append(string content)
        {
            Content += content;
            return (T)this;
        }

        public T Clear()
        {
            Content = string.Empty;
            return (T)this;
        }

        public override string ToString()
        {
            return Content;
        }
    }
}
