using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using BlockBase.BBLinq.Builders;
using BlockBase.BBLinq.ExtensionMethods;
using BlockBase.BBLinq.Model.Database;
using BlockBase.BBLinq.Model.Nodes;
using BlockBase.BBLinq.Model.Responses;
using BlockBase.BBLinq.Queries.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BlockBase.BBLinq.Parsers
{
    internal class BlockBaseResultParser
    {
        public RequestResult<TResult> Parse<TResult>(string result, ISelectQuery query)
        {
            var parsedResult = JsonConvert.DeserializeObject<RequestResult<TResult>>(result);
            if (!parsedResult.Succeeded)
            {
                return parsedResult;
            }

            var responses = JsonConvert.DeserializeObject<JObject>(result)["response"] as JArray;
            foreach (var response in responses)
            {
                var columns = JsonConvert.DeserializeObject<string[]>(response["columns"].ToString());
                var data = JsonConvert.DeserializeObject<string[][]>(response["data"].ToString());
                var operationExecution = ParseOperationExecutionResult<TResult>(columns, data);
                if (operationExecution != null)
                {
                    parsedResult.Responses.Add(operationExecution);
                }
                else
                {
                    var rowsFetchResult = ParseRows<TResult>(columns, data, query);
                    if (rowsFetchResult != null)
                    {
                        parsedResult.Responses.Add(rowsFetchResult);
                    }
                }

            }

            return parsedResult;
        }

        private object[] CreateArgumentListForExpression(object[] objects, IReadOnlyCollection<ParameterExpression> @params)
        {
            var parameterList = new List<object>();
            foreach(var @param in @params)
            {
                var added = false;
                for (var objectCounter = 0; objectCounter < objects.Length; objectCounter++)
                {
                    if (objects[objectCounter]!= null && objects[objectCounter].GetType() == @param.Type)
                    {
                        parameterList.Add(objects[objectCounter]);
                        objects[objectCounter] = null;
                        added = true;
                    }
                }

                if (!added)
                {
                    parameterList.Add(Activator.CreateInstance(@param.Type));
                }
            }

            return parameterList.ToArray();
        }

        public ExecutionResult<TResult> ParseRows<TResult>(string[] columns, string[][] data, ISelectQuery query)
        {
            var resultList = new List<TResult>();
            BlockBaseColumn[] rowColumns = 
                query.Mapping != null? GetResultColumns(columns, query):
                query.ReturnType != null?
                    GetResultColumns(columns, query.ReturnType) :
                GetResultColumns(columns, query.Joins);
            Delegate queryExpression = null;
            if (query.Mapping != null)
            {
                queryExpression = (query.Mapping).Compile();
            }
            foreach (var row in data)
            {
                var rowObjects = ParseRow(rowColumns, row);
                if (queryExpression != null)
                {
                    rowObjects = CreateArgumentListForExpression(rowObjects, query.Mapping.Parameters);
                    resultList.Add((TResult)queryExpression.DynamicInvoke(rowObjects));
                }
                else
                {
                    if (rowObjects.Length == 1)
                    {
                        resultList.Add((TResult)rowObjects[0]);
                    }
                    else
                    {
                        var expando = new Dictionary<string, object>();
                        foreach (var rowObject in rowObjects)
                        {
                            foreach (var property in rowObject.GetType().GetProperties())
                            {
                                var propertyName = rowObject.GetType().GetTableName() + property.GetColumnName();
                                expando.Add(propertyName, property.GetValue(rowObject));
                            }
                        }
                        resultList.Add((dynamic) expando);
                    }
                }
            }
            return new ExecutionResult<TResult>() {Content = resultList};
        }

        public dynamic CombineRowObjects(dynamic[] rowObjects)
        {
            var result = new ExpandoObject(); 
            foreach (var rowObject in rowObjects)
            {
                var dict = (IDictionary<string, object>)rowObject;
                var d = result as IDictionary<string, object>;
                foreach (var pair in dict.Concat(result))
                {
                    d[pair.Key] = pair.Value;
                }
            }

            return result;
        }

        public object[] ParseRow(BlockBaseColumn[] columns, string[] data)
        {
            var result = new List<object>();
            for (var columnIndex = 0; columnIndex < columns.Length; columnIndex++)
            {
                if (columnIndex == 17)
                {

                }
                var property = columns[columnIndex].Property;
                var rawValue = data[columnIndex];
                var value = ParseValue(property, rawValue);
                object currentObject = null;
                foreach (var resultObject in result)
                {
                    if (resultObject.GetType() == property.ReflectedType)
                    {
                        currentObject = resultObject;
                    }
                }

                if (currentObject == null && property.ReflectedType != null)
                {
                    currentObject = Activator.CreateInstance(property.ReflectedType);
                    result.Add(currentObject);
                }
                property.SetValue(currentObject, value);
            }
            return result.ToArray();
        }

        public BlockBaseColumn[] GetResultColumns(string[] columns, JoinNode[] joins)
        {
            var joinTypes = JoinBuilder.DrawTypesFromJoinNodes(joins);
            var selectedColumns = new List<BlockBaseColumn>();
            var properties = new List<PropertyInfo>();
            foreach (var joinType in joinTypes)
            {
                properties.AddRange(joinType.GetProperties());
            }
            foreach (var column in columns)
            {
                foreach (var property in properties)
                {
                    if (property.ReflectedType.GetTableName() + "." + property.GetColumnName() == column)
                    {
                        selectedColumns.Add(BlockBaseColumn.From(property));
                    }
                }
            }
            return selectedColumns.ToArray();
        }

        public BlockBaseColumn[] GetResultColumns(string[] columns, Type type)
        {
            var selectedColumns = new List<BlockBaseColumn>();
            var tableName = type.GetTableName();
            foreach (var column in columns)
            {
                foreach (var property in type.GetProperties())
                {
                    if (tableName + "." + property.GetColumnName() == column)
                    {
                        selectedColumns.Add(BlockBaseColumn.From(property));
                    }
                }
            }
            return selectedColumns.ToArray();
        }

        public BlockBaseColumn[] GetResultColumns(string[] columns, ISelectQuery query)
        {
            var selectedColumns = new List<BlockBaseColumn>();
            foreach (var column in columns)
            {
                foreach (var property in query.SelectProperties)
                {
                    if (property.Table + "." + property.Name == column)
                    {
                        selectedColumns.Add(property);
                    }
                }
            }
            return selectedColumns.ToArray();
        }

        public ExecutionResult<TResult> ParseOperationExecutionResult<TResult>(string[] columns, string[][] data)
        {
            var executedIndex = Array.IndexOf(columns, "Executed");
            var messageIndex = Array.IndexOf(columns, "Message");
            if (executedIndex != -1 && messageIndex != -1)
            {
                return new ExecutionResult<TResult>
                {
                    Executed = data[0][executedIndex] == "True",
                    Message = data[0][messageIndex]
                };
            }
            return null;
        }

        public TResult ParseRow<TResult>(Dictionary<string, PropertyInfo> propertiesOnRow, Dictionary<string, string> dataOnRow) where TResult:new()
        {
            var result = new TResult();
            foreach (var property in propertiesOnRow)
            {
                var propertyInfo = property.Value;
                var value = dataOnRow[property.Key];
                var parsedValue = ParseValue(propertyInfo, value);
                propertyInfo.SetValue(result, parsedValue);
            }
            return result;
        }

        private object ParseValue(PropertyInfo property, string value)
        {
            var currentType = property.PropertyType;
            var propType = Nullable.GetUnderlyingType(currentType) ?? currentType;
            if (value == "" && property.IsNullable())
            {
                return null;
            }
            if (propType == typeof(Guid))
            {
                return string.IsNullOrEmpty(value)? Guid.Empty: Guid.Parse(value);
            }

            if (propType == typeof(DateTime) && property.IsComparableDate())
            {
                if (value == "")
                {
                    return null;
                }
                var timestamp = int.Parse(value);
                var date = new DateTime();
                date.FromUnixTimestamp(timestamp);
                return date;
            }
            if (propType == typeof(DateTime))
            {
                return DateTime.Parse(value, CultureInfo.InvariantCulture);
            }
            if (propType.IsEnum)
            {
                var index = int.Parse(value);
                var res = Enum.ToObject(propType, index);
                return res;
            }
            if (value != string.Empty)
            {
                return Convert.ChangeType(value, propType, CultureInfo.InvariantCulture);
            }

            return null;
        }
    }
}
