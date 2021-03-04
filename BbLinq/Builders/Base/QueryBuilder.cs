namespace BlockBase.BBLinq.Builders.Base
{
    public abstract class QueryBuilder<T> where T : QueryBuilder<T>
    {
        protected string Content;

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
            return (T) this;
        }

        public override string ToString()
        {
            return Content;
        }
    }
}
