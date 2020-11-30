using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BlockBase.BBLinq.Builders;
using BlockBase.BBLinq.ExtensionMethods;
using BlockBase.BBLinq.Pocos;

namespace BlockBase.BBLinq.Queries
{
    /// <summary>
    /// A SELECT query used to perform fluent SQL deletions.
    /// </summary>
    public class SelectQuery : BbQuery
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
        /// The amount of expected results
        /// </summary>
        public int Limit { get; }

        /// <summary>
        /// The amount of expected results
        /// </summary>
        public int Skip { get; }


        /// <summary>
        /// The amount of expected results
        /// </summary>
        public bool Encrypted { get; }

        /// <summary>
        /// The object's key
        /// </summary>
        public object Key { get; }

        /// <summary>
        /// The default constructor
        /// </summary>
        /// <param name="origin">the original type</param>
        /// <param name="joins">a list of joins</param>
        /// <param name="where">a conditional expression</param>
        /// <param name="select">a selection expression</param>
        /// <param name="encrypted">an encryption flag</param>
        /// <param name="limit">the amount of records that it should return</param>
        /// <param name="skip">the amount of records that must be skipped</param>
        /// <param name="key"></param>
        public SelectQuery(Type origin, IEnumerable<LambdaExpression> joins, LambdaExpression where, bool encrypted, LambdaExpression select, int limit, int skip, object key)
        {
            Origin = origin;
            Joins = joins;
            Where = where;
            Select = select;
            Limit = limit;
            Skip = skip;
            Key = key;
            Encrypted = encrypted;
        }

        public override string ToString()
        {
            var tableName = Origin.GetTableName();
            QueryBuilder.Clear();
            if (Select == null)
            {
                QueryBuilder.SelectAll(tableName);
            }
            else
            {
                IEnumerable<FieldValue> tableFields = Select.Body switch
                {
                    MemberInitExpression memberInit => memberInit.GetFields(),
                    NewExpression newExpression => newExpression.GetFields(),
                    _ => null
                };

                if (tableFields != null)
                {
                    QueryBuilder.SelectFields(tableFields.ToArray());
                }
            }
            QueryBuilder.WhiteSpace().From(tableName);
            if (!Joins.IsNullOrEmpty())
            {
                var list = new List<Type>(){Origin};
                foreach (var join in Joins)
                {
                    var parameter = join.Parameters;
                    var condition = (new BbSqlQueryBuilder()).ParseQuery(join.Body);
                    var joinTableName = string.Empty;
                    if (!list.Contains(parameter[0].Type))
                    {
                        list.Add(parameter[0].Type);
                        joinTableName = parameter[0].Type.GetTableName();
                    }
                    else if(!list.Contains(parameter[1].Type))
                    {
                        list.Add(parameter[1].Type);
                        joinTableName = parameter[1].Type.GetTableName();
                    }

                    QueryBuilder.WhiteSpace().JoinOn(joinTableName, condition.ToString());
                }
            }
            if (Where != null)
            {
                var condition = (new BbSqlQueryBuilder()).ParseQuery(Where.Body);
                QueryBuilder.WhiteSpace().Where(condition.ToString());
            }
            else if (Key != null)
            {
                var record = Activator.CreateInstance(Origin);
                var primaryKey = Origin.GetPrimaryKey();
                primaryKey.SetValue(record, Key);
                var condition = new BbSqlQueryBuilder().GenerateConditionFromObject(record);
                QueryBuilder.WhiteSpace().Where(condition.ToString());
            }

            if (Limit > 0)
            {
                QueryBuilder.WhiteSpace().Limit().WhiteSpace().Append(Limit.ToString());
            }

            if (Skip > 0)
            {
                QueryBuilder.WhiteSpace().Offset().WhiteSpace().Append(Skip.ToString());
            }

            if (Encrypted)
            {
                QueryBuilder.WhiteSpace().Encrypted();
            }
            return QueryBuilder.End().ToString();
        }
    }
}
