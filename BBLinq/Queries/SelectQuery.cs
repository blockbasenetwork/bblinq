using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using agap2IT.Labs.BlockBase.BBLinq.Builders;
using agap2IT.Labs.BlockBase.BBLinq.ExtensionMethods;
using agap2IT.Labs.BlockBase.BBLinq.Parser;
using agap2IT.Labs.BlockBase.BBLinq.Pocos.Components;

namespace agap2IT.Labs.BlockBase.BBLinq.Queries
{
   
    public class SelectQuery<T>
    {
        public Type Origin { get; }
        public IEnumerable<LambdaExpression> Joins { get; }
        public LambdaExpression Where { get; }
        public LambdaExpression Select { get; }

        public SelectQuery(Type origin, IEnumerable<LambdaExpression> joins, LambdaExpression where, LambdaExpression select)
        {
            Origin = origin;
            Joins = joins;
            Where = where;
            Select = select;
        }

        public override string ToString()
        {
            var queryBuilder = new BbSqlQueryBuilder();
            var tableName = Origin.GetTableName();

            if (Select == null)
            {
                queryBuilder.SelectAll(tableName);
            }
            else
            {
                IEnumerable<TableField> tableFieldPairings = null;
                switch (Select.Body)
                {
                    case MemberInitExpression memberInit:
                        tableFieldPairings = memberInit.GetTablesAndFieldsPairings();
                        break;
                    case NewExpression newExpression:
                        tableFieldPairings = newExpression.GetTableAndFieldsPairings();
                        break;
                }

                if (tableFieldPairings != null)
                {
                    queryBuilder.SelectFields(tableFieldPairings.ToArray());
                }
            }

            queryBuilder.WhiteSpace().From(tableName);
            foreach (var join in Joins)
            {
                var parameter = join.Parameters;
                var condition = ExpressionParser.ParseQuery(join.Body);
                queryBuilder.WhiteSpace().JoinOn(parameter[^1].Type.GetTableName(), condition);
            }
            if (Where != null)
            {
                var condition = ExpressionParser.ParseQuery(Where.Body);
                queryBuilder.WhiteSpace().Where(condition);
            }
            
            return queryBuilder.End().ToString();
        }
    }
}
