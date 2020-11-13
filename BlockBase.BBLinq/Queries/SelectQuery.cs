using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using BlockBase.BBLinq.Builders;
using BlockBase.BBLinq.ExtensionMethods;
using BlockBase.BBLinq.Parser;
using BlockBase.BBLinq.Pocos.Components;

namespace BlockBase.BBLinq.Queries
{
    public class SelectQuery<T>
    {
        /// <summary>
        /// The original type
        /// </summary>
        public Type Origin { get; }

        /// <summary>
        /// A list of join expressions
        /// </summary>
        public IEnumerable<LambdaExpression> Joins { get; }
        
        /// <summary>
        /// A condition used as reference for the DELETE operation
        /// </summary>
        public LambdaExpression Where { get; }

        /// <summary>
        /// An expression that indicates what is being selected
        /// </summary>
        public LambdaExpression Select { get; }

        /// <summary>
        /// The default constructor
        /// </summary>
        /// <param name="origin">the original type</param>
        /// <param name="joins">a list of joins</param>
        /// <param name="where">a conditional expression</param>
        /// <param name="select">a selection expression</param>
        public SelectQuery(Type origin, IEnumerable<LambdaExpression> joins, LambdaExpression where, LambdaExpression select)
        {
            Origin = origin;
            Joins = joins;
            Where = where;
            Select = select;
        }

        /// <summary>
        /// Returns the SQL query built from the request
        /// </summary>
        /// <returns>A select sql query string</returns>
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
            if (!Joins.IsNullOrEmpty())
            {
                foreach (var join in Joins)
                {
                    var parameter = join.Parameters;
                    var condition = ExpressionParser.ParseQuery(join.Body);
                    queryBuilder.WhiteSpace().JoinOn(parameter[^1].Type.GetTableName(), condition);
                }
            }
            if (Where != null)
            {
                var condition = ExpressionParser.ParseQuery(Where.Body);
                queryBuilder.WhiteSpace().Where(condition);
            }
            
            return queryBuilder.WhiteSpace().End().ToString();
        }
    }
}
