﻿using BlockBase.BBLinq.Context;
using BlockBase.BBLinq.Dictionaries;

namespace BlockBase.BBLinq.Builders
{
    /// <summary>
    /// A base query builder
    /// </summary>
    /// <typeparam name="TDictionary">The dictionary being used to build the queries</typeparam>
    public abstract class SqlQueryBuilder<TDictionary> where TDictionary:SqlDictionary
    {
        private string _content;

        protected TDictionary Dictionary = ContextCache.Instance.Get<TDictionary>("dictionary");

        /// <summary>
        /// Appends content to a query builder and returns it updated
        /// </summary>
        /// <typeparam name="TReturn">the resulting query builder type</typeparam>
        /// <param name="content">the string content</param>
        /// <returns>the updated query builder</returns>
        public TReturn Append<TReturn>(string content) where TReturn : SqlQueryBuilder<TDictionary>
        {
            _content += content;
            return (TReturn)this;
        }

        /// <summary>
        /// Cleans the content on a query
        /// </summary>
        /// <typeparam name="TReturn">the resulting query builder type</typeparam>
        /// <returns>the updated query builder</returns>
        public virtual TReturn Clear<TReturn>() where TReturn : SqlQueryBuilder<TDictionary>
        {
            _content = string.Empty;
            return (TReturn)this;
        }

        /// <summary>
        /// Returns the string query
        /// </summary>
        /// <returns>the query's string</returns>
        public override string ToString()
        {
            return _content;
        }
    }
}
