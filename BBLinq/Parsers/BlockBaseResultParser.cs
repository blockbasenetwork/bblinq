using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using BlockBase.BBLinq.ExtensionMethods;
using BlockBase.BBLinq.Model.Responses;
using BlockBase.BBLinq.Queries.Interfaces;
using Newtonsoft.Json;

namespace BlockBase.BBLinq.Parsers
{
    internal class Response
    {
        [JsonProperty("response")]
        public ResponseItem[] ResponseItems { get; set; }
    }

    internal class ResponseItem
    {
        [JsonProperty("columns")]
        public string[] Columns { get; set; }

        [JsonProperty("data")]
        public string[][] Value { get; set; }
    }


    internal class BlockBaseResultParser
    {
        public RequestResult<TResult> Parse<TResult>(string result, ISelectQuery query, bool isBatch = false)
        {
            var parsedResult = JsonConvert.DeserializeObject<Response>(result);
            NotifyIfRequestFailed(parsedResult);
            if (isBatch && parsedResult.ResponseItems.Length == 1)
            {
                throw new Exception("A batch operation was not recognized or failed");
            }
            if (query == null)
            {
                return new RequestResult<TResult>() {Succeeded = true};
            }
            var properties = GenerateMapperProperties(query, parsedResult.ResponseItems[1].Columns);
            var rows = GetRows(parsedResult).First();
            var parsedRows = ParseRows(rows, properties);
            var executionResult = ExecuteMapper<TResult>(query.Mapping, parsedRows);
            return new RequestResult<TResult>() {Result = executionResult, Succeeded = true};
        }

        public IEnumerable<TResult> ExecuteMapper<TResult>(LambdaExpression expression, IEnumerable<IEnumerable<object>> parsedRows)
        {
            if (expression == null)
            {
                var result = new List<TResult>();
                foreach (var row in parsedRows)
                {
                    result.Add((TResult)row.FirstOrDefault(x => x.GetType() == typeof(TResult)));
                }
                return result;
            }
            var mapping = expression.Compile();
            var resultList = new List<TResult>();
            foreach (var parsedRow in parsedRows)
            {
                var orderedArguments = OrderArguments(expression.Parameters, parsedRow);
                var result = mapping.DynamicInvoke(orderedArguments.ToArray());
                resultList.Add((TResult)result);
            }
            return resultList;
        }

        public IEnumerable<object> OrderArguments(IReadOnlyCollection<ParameterExpression> parameters, IEnumerable<object> arguments)
        {
            var list = new List<object>();
            foreach (var parameter in parameters)
            {
                list.Add(arguments.FirstOrDefault(x => x.GetType() == parameter.Type));
            }
            return list;
        }

        public IEnumerable<IEnumerable<object>> ParseRows(ResponseItem response, IEnumerable<(string, PropertyInfo)> properties)
        {
            var result = new List<IEnumerable<object>>();
            var propertyIndexes = GetPropertyIndexes(response.Columns, properties).ToArray();
            var propertyArray = properties.ToArray();
            foreach (var row in response.Value)
            {
                var arguments = new List<object>();
                for(var propertyCounter = 0 ; propertyCounter< properties.Count(); propertyCounter++)
                {
                    var property = propertyArray[propertyCounter].Item2;
                    var propertyType = property.ReflectedType;
                    var index = propertyIndexes[propertyCounter];
                    var rowData = row[index];
                    var parsedValue = ParseValue(rowData, property);
                    var obj = arguments.FirstOrDefault(x => x.GetType() == propertyType);
                    if (obj == null)
                    {
                        obj = Activator.CreateInstance(propertyType);
                        arguments.Add(obj);
                    }
                    property.SetValue(obj, parsedValue);
                }
                result.Add(arguments);
            }
            return result;
        }

        private static IEnumerable<int> GetPropertyIndexes(string[] columns, IEnumerable<(string, PropertyInfo)> properties)
        {
            return properties.Select(property => Array.IndexOf(columns, property.Item1)).ToArray();
        }


        public object ParseValue(string value, PropertyInfo property)
        {
            var currentType = property.PropertyType;
            var propType = Nullable.GetUnderlyingType(currentType) ?? currentType;
            if (value == null && property.IsNullable())
            {
                return null;
            }
            if (value == null)
            {
                return default;
            }

            if (propType == typeof(Guid))
            {
                return string.IsNullOrEmpty(value) ? Guid.Empty : Guid.Parse(value);
            }

            if (propType == typeof(DateTime) && property.IsComparableDate())
            {
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

            if (propType == typeof(string))
            {
                value = value?.Replace("`", "'");
            }

            try
            {
                return Convert.ChangeType(value, propType, CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                return Activator.CreateInstance(propType);
            }
        }

        public IEnumerable<ResponseItem> GetRows(Response response)
        {
            return response.ResponseItems.Where(x => x.Columns[0] != "Executed");
        }

        public void NotifyIfRequestFailed(Response response)
        {
            if (!response.ResponseItems.Any())
            {
                throw new Exception("No result provided");
            }
            foreach (var item in response.ResponseItems)
            {
                if (item.Columns[0] == "Executed")
                {
                    if (item.Value[0][0] == "False")
                    {
                        throw new Exception(item.Value[0][1]);
                    }
                }
            }
        }

        public IEnumerable<(string, PropertyInfo)> GenerateMapperProperties(ISelectQuery query, string[] columnNames)
        {
            if (query.Mapping == null)
            {
                return ParseColumns(columnNames, new []{query.ReturnType});
            }
            var arguments = query.Mapping.Parameters.Select(x => x.Type);
            return ParseColumns(columnNames, arguments);
        }


        public (string, PropertyInfo)[] ParseColumns(string[] columnNames, IEnumerable<Type> arguments)
        {
            var argumentNames = arguments.Select(x => (x, x.GetTableName())).ToList();
            var columns = columnNames.Select(x => x.Split(".")).GroupBy(x => x[0]);
            var properties = new List<(string, PropertyInfo)>();
            foreach (var table in columns)
            {
                var type = argumentNames.FirstOrDefault(x => x.Item2 == table.Key);
                if (type.x == null)
                {
                    throw new Exception($"No table {table.Key} found");
                }

                var tableColumns = table.Select(x => x[1]);
                var typeProperties = type.x.GetProperties().Select(x => tableColumns.Contains(x.GetColumnName())?x:null).Where(x => x!=null);
                var propertyList =
                    typeProperties.Select(x => (x.ReflectedType.GetTableName() + "." + x.GetColumnName(), x));
                properties.AddRange(propertyList);
            }
            return properties.ToArray();
        }
    }
}
