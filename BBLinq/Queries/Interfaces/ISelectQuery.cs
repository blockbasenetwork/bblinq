using BlockBase.BBLinq.Model.Database;
using BlockBase.BBLinq.Model.Nodes;
using System;
using System.Linq.Expressions;

namespace BlockBase.BBLinq.Queries.Interfaces
{
    public interface ISelectQuery : IQuery
    {
        #region Result Parsing
        public Type ReturnType { get; set; }
        public LambdaExpression Mapping { get; set; }
        #endregion

        #region PropertySelection
        internal BlockBaseColumn[] SelectProperties { get; set; }
        #endregion

        #region Joins
        internal JoinNode[] Joins { get; set; }
        #endregion

        #region Where
        #endregion

        public int? Limit { get; }
        public int? Offset { get; }
    }
}
