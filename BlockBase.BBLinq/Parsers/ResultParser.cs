using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using BlockBase.BBLinq.Dictionaries;
using BlockBase.BBLinq.ExtensionMethods;
using BlockBase.BBLinq.Pocos;
using BlockBase.BBLinq.Results;
using Newtonsoft.Json;

namespace BlockBase.BBLinq.Parsers
{
    public static class ResultParser
    {
        public static async Task<QueryResult> ParseQueryResult(Task<string> queryResult)
        {
            var parseResult = JsonConvert.DeserializeObject<Result>(await queryResult);
            var result = new QueryResult()
            {
                Succeeded = parseResult.Succeeded,
                Exception = parseResult.Exception,
                ResponseMessage = parseResult.ResponseMessage
            };
            if (!result.Succeeded)
            {
                return result;
            }

            foreach (var row in parseResult.Response)
            {
                var queryExecution = ParseQueryExecutionResult(row.Columns, row.Data[0]);
                if (queryExecution != null)
                {
                    result.ResponseMessage += queryExecution.Message;
                    if (!queryExecution.Executed)
                    {
                        result.Succeeded = false;
                    }
                }
            }
            return result;
        }

        public static async Task<QueryResult<T>> ParseFetchResult<T>(Task<string> queryResult, Dictionary<FieldValue, PropertyInfo> properties)
        {
            var parseResult = JsonConvert.DeserializeObject<Result>(await queryResult);
            var result = new QueryResult<T>()
            {
                Succeeded = parseResult.Succeeded,
                Exception = parseResult.Exception,
                ResponseMessage = parseResult.ResponseMessage
            };
            var response = parseResult.Response.ToList();
            if (!parseResult.Response.Any()) return result;
            var messageParse = ParseMessages(response[0]);
            result.Succeeded = messageParse.Item1;
            result.ResponseMessage += messageParse.Item2;
            if (response.Count > 1)
            {
                var res = ParseResults<T>(response[1], properties);
                if (res.Count() > 0)
                    result.Result = res.ToArray()[0];
            }
            return result;
        }

        public static async Task<QueryResult<T>> ParseEncryptedFetchResult<T>(Task<string> queryResult)
        {
            return default;
        }
        
        public static async Task<QueryResult<IEnumerable<T>>> ParseListResult<T>(Task<string> queryResult, Dictionary<FieldValue, PropertyInfo> properties)
        {
            var parseResult = JsonConvert.DeserializeObject<Result>(await queryResult);
            var result = new QueryResult<IEnumerable<T>>()
            {
                Succeeded = parseResult.Succeeded,
                Exception = parseResult.Exception,
                ResponseMessage = parseResult.ResponseMessage
            };
            var response = parseResult.Response.ToList();
            if (!parseResult.Response.Any()) return result;
            var messageParse =  ParseMessages(response[0]);
            result.Succeeded = messageParse.Item1; 
            result.ResponseMessage += messageParse.Item2;
            if (response.Count > 1)
            {
                result.Result = ParseResults<T>(response[1], properties);
            }
            return result;
        }

        private static IEnumerable<T> ParseResults<T>(Response response, Dictionary<FieldValue, PropertyInfo> properties)
        {
            var resultObjects = new List<T>();
            var fields = new Dictionary<string, PropertyInfo>();
            var tempDict = new BbSqlDictionary();
            foreach (KeyValuePair<FieldValue, PropertyInfo> kv in properties)
            {
                var field = kv.Key.Table + tempDict.TableFieldSeparator + kv.Key.Field;
                fields.Add(field, kv.Value);
            }
            foreach (var data in response.Data)
            {
                var values = GenerateConstructorObjects(fields, data);
                if (typeof(T).IsGenerated())
                {
                    resultObjects.Add((T)Activator.CreateInstance(typeof(T), values));
                }
                else
                {
                    var newInstance = Activator.CreateInstance(typeof(T));
                    for(var i = 0; i<values.Length; i++)
                    {
                        var property = fields[response.Columns[i]];
                        property.SetValue(newInstance, values[i]);
                    }
                    resultObjects.Add((T)newInstance);
                }
            }
            return resultObjects;
        }

        private static (bool, string) ParseMessages(Response response)
        {
            var messageResult = string.Empty;
            var success = true;
            foreach (var data in response.Data)
            {
                var result = ParseQueryExecutionResult(response.Columns, data);
                if (result == null) continue;
                messageResult += result.Message;
                if (result.Executed)
                {
                    success = true;
                }
            }
            return (success, messageResult);
        }

        public static async Task<QueryResult<IEnumerable<T>>> ParseEncryptedListResult<T>(Task<string> queryResult)
        {
            return default;
        }

        private static QueryExecutionResult ParseQueryExecutionResult(string[] columns, string[] data)
        {
            if (!columns.Contains("Executed") || !columns.Contains("Message")) return null;
            var res = new QueryExecutionResult() { Executed = data[0].ToLower() == "true", Message = data[1] };
            return res;
        }

        private static object[] GenerateConstructorObjects(Dictionary<string, PropertyInfo> fields, string[] values)
        {
            var args = new List<object>();
            for (var i = 0; i < fields.Count; i++)
            {
                var currentType = fields.Values.ToList()[i].PropertyType;
                var propType = Nullable.GetUnderlyingType(currentType) ?? currentType;
                if(currentType == typeof(Guid))
                {
                    args.Add(Guid.Parse(values[i]));
                }
                else
                {
                    args.Add(Convert.ChangeType(values[i], propType));
                }
            }
            return args.ToArray();
        }
    }
}