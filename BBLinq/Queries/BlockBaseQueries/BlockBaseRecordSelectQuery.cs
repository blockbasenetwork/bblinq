using BlockBase.BBLinq.Builders;
using BlockBase.BBLinq.Model.Base;
using BlockBase.BBLinq.Model.Database;
using BlockBase.BBLinq.Model.Nodes;
using BlockBase.BBLinq.Queries.Base;
using BlockBase.BBLinq.Queries.Interfaces;
using System;
using System.Linq.Expressions;

namespace BlockBase.BBLinq.Queries.BlockBaseQueries
{
    internal class BlockBaseRecordSelectQuery : BlockBaseQuery, ISelectQuery
    {
        private BlockBaseColumn[] _selectProperties;
        private JoinNode[] _joins;

        internal BlockBaseRecordSelectQuery(Type returnType, LambdaExpression mapping, BlockBaseColumn[] selectedProperties, JoinNode[] joins, ExpressionNode condition, int? limit, int? offset, bool isEncrypted) : base(isEncrypted)
        {
            ReturnType = returnType;
            Mapping = mapping;
            _selectProperties = selectedProperties;
            _joins = joins;
            Condition = condition;
            Limit = limit;
            Offset = offset;
        }

        public Type ReturnType { get; set; }
        public LambdaExpression Mapping { get; set; }

        BlockBaseColumn[] ISelectQuery.SelectProperties
        {
            get => _selectProperties;
            set => _selectProperties = value;
        }

        JoinNode[] ISelectQuery.Joins
        {
            get => _joins;
            set => _joins = value;
        }


        public ExpressionNode Condition { get; }
        public int? Limit { get; }
        public int? Offset { get; }

        public override string GenerateQueryString()
        {
            var queryBuilder = new BlockBaseQueryBuilder();
            queryBuilder.SelectRecord(_selectProperties, _joins, Condition, Limit, Offset, IsEncrypted);
            return queryBuilder.ToString();
        }
    }
}
