using BlockBase.BBLinq.Queries.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BlockBase.BBLinq.Queries.BlockBase
{
    public class SelectQuery : Query
    {
        /// <summary>
        /// The original type
        /// </summary>
        public Type Origin { get; protected set; }

        /// <summary>
        /// A list of join expressions
        /// </summary>
        public IEnumerable<LambdaExpression> Joins { get; protected set; }

        /// <summary>
        /// A condition used as reference for the DELETE operation
        /// </summary>
        public LambdaExpression Where { get; protected set; }

        /// <summary>
        /// An expression that indicates what is being selected
        /// </summary>
        public LambdaExpression Select { get; protected set; }

        /// <summary>
        /// The amount of expected results
        /// </summary>
        public int Limit { get; protected set; }

        /// <summary>
        /// The amount of expected results
        /// </summary>
        public int Skip { get; protected set; }


        /// <summary>
        /// The amount of expected results
        /// </summary>
        public bool Encrypted { get; protected set; }

        /// <summary>
        /// The object's key
        /// </summary>
        public object Key { get; protected set; }

        /// <summary>
        /// The default constructor
        /// </summary>
        public SelectQuery(Type origin, IEnumerable<LambdaExpression> joins, LambdaExpression where, bool encrypted, LambdaExpression select, int limit, int skip)
        {
            Origin = origin;
            Joins = joins;
            Where = where;
            Select = select;
            Limit = limit;
            Skip = skip;
            Encrypted = encrypted;
        }

        /// <summary>
        /// The constructor for single object fetch
        /// </summary>
        public SelectQuery(Type origin, object key, LambdaExpression select, bool encrypted)
        {
            Origin = origin;
            Key = key;
            Encrypted = encrypted;
        }


        public override string GenerateQuery()
        {
            throw new System.NotImplementedException();
        }
    }
}
