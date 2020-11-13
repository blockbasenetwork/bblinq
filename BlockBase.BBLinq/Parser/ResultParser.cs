using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using BlockBase.BBLinq.ExtensionMethods;
using BlockBase.BBLinq.Pocos.Components;
using BlockBase.BBLinq.Pocos.Results;
using BlockBase.BBLinq.Properties;
using Newtonsoft.Json;

namespace BlockBase.BBLinq.Parser
{
    public class ResultParser
    {
        /// <summary>
        /// Parses a result on a BBSet Request
        /// </summary>
        /// <typeparam name="TR"></typeparam>
        /// <param name="queryResult"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<TR>> ParseResult<TR>(Task<string> queryResult)
        {
            var result = await queryResult;
            var parseResult = JsonConvert.DeserializeObject<Result>(result);
            var tableName = typeof(TR).GetTableName();
            var properties = typeof(TR).GetProperties();
            var fieldNames = new Dictionary<string, PropertyInfo>();
            foreach (var property in properties)
            {
                var fieldName = tableName + SQLExpressions.TABLE_FIELD_SEPARATOR + property.GetFieldName();
                fieldNames.Add(fieldName, property);
            }
            var resultObjects = new List<TR>();
            var columns= parseResult.Response.ToArray()[1].Columns;
            var data = parseResult.Response.ToArray()[1].Data;
            foreach (var line in data)
            {
                var newInstance = Activator.CreateInstance(typeof(TR));
                for (var i = 0; i < columns.Length; i++)
                {
                    var property = fieldNames[columns[i]];
                    if (property == null) continue;
                    property.SetValue(newInstance, Convert.ChangeType(line[i], property.PropertyType));
                }
                resultObjects.Add((TR)newInstance);
            }

            return resultObjects;
        }

        /// <summary>
        /// Parses a result on a Join or Dynamic Request
        /// </summary>
        /// <typeparam name="TR"></typeparam>
        /// <param name="queryResult"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<TR>> ParseResult<TR>(Task<string> queryResult, IEnumerable<PropertyFieldAssignment> properties, Type type)
        {
            var result = await queryResult;
            var parseResult = JsonConvert.DeserializeObject<Result>(result);
            var fieldNames = new Dictionary<string, PropertyInfo>();
            foreach (var property in properties)
            {
                var fieldName = property.TableName + SQLExpressions.TABLE_FIELD_SEPARATOR + property.FieldName;
                fieldNames.Add(fieldName, property.Property);
            }
            var resultObjects = new List<TR>();
            var columns = parseResult.Response.ToArray()[1].Columns;
            var data = parseResult.Response.ToArray()[1].Data;


            foreach (var line in data)
            {
                var newInstance = Activator.CreateInstance(type);
                for (var i = 0; i < columns.Length; i++)
                {
                    var property = fieldNames[columns[i]];
                    var propType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                    property.SetValue(newInstance, Convert.ChangeType(line[i], propType));
                }
                resultObjects.Add((TR)newInstance);
            }
            return resultObjects;
        }
    }
}
