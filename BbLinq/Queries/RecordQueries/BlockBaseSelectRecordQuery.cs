using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using BlockBase.BBLinq.Builders;
using BlockBase.BBLinq.ExtensionMethods;
using BlockBase.BBLinq.Parsers;
using BlockBase.BBLinq.Pocos;
using BlockBase.BBLinq.Pocos.Nodes;
using BlockBase.BBLinq.Queries.Base;

namespace BlockBase.BBLinq.Queries.RecordQueries
{
    public class BlockBaseSelectRecordQuery<T> : BlockBaseQuery, IQuery
    {
        public Type Origin { get; }
        public PropertyInfo[] Fields { get; }
        public JoinNode[] Joins { get;  }
        public ExpressionNode Predicate { get;  }
        public int Offset { get; }
        public int Limit { get; }
        public bool IsEncrypted { get; }
        public Expression Mapper { get;  }

        public BlockBaseSelectRecordQuery(Type origin, PropertyInfo[] fields, JoinNode[] joins, Expression mapper, Expression predicate, int limit, int offset, bool isEncrypted)
        {
            var expressionParser = new ExpressionParser();
            Predicate = expressionParser.Reduce(expressionParser.ParseExpression(predicate));
            Origin = origin;
            Fields = fields;
            Mapper = mapper;
            Joins = joins;
            IsEncrypted = isEncrypted;
            Limit = limit;
            Offset = offset;
        }

        public (TableColumn, TableColumn)[] GenerateJoins()
        {
            var columns = new List<(TableColumn, TableColumn)>();
            foreach (var join in Joins)
            {
                var left = (join.LeftNode as PropertyNode)?.Property;
                var right = (join.LeftNode as PropertyNode)?.Property;

                if (left == null || right == null)
                {
                    return null;
                }

                var leftTableColumn = new TableColumn()
                    {Table = left.DeclaringType.GetTableName(), Column = left.GetColumnName()};

                var rightTableColumn = new TableColumn()
                    { Table = right.DeclaringType.GetTableName(), Column = right.GetColumnName() };
                columns.Add((leftTableColumn, rightTableColumn));
            }
            return columns.ToArray();
        }

        public (string, string)[] GenerateFields()
        {
            var fields = new List<(string, string)>();
            foreach (var field in Fields)
            {
                fields.Add((field.DeclaringType.GetTableName(), field.GetColumnName()));
            }
            return fields.ToArray();
        }

        public string GenerateQueryString()
        {
            var columns = GenerateFields();
            var originTable = Origin.GetTableName();
            var queryBuilder = new BlockBaseQueryBuilder();
            var joins = GenerateJoins();
            queryBuilder.Select(columns, originTable, joins, Predicate, IsEncrypted, Limit, Offset);
            return queryBuilder.ToString();
        }
    }
}
