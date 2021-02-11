using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;

namespace BlockBase.BBLinq.Exceptions
{
    [Serializable]
    public class DuplicatedColumnsOnTableException : Exception
    {
        private static string GenerateErrorMessage(Type type, Dictionary<PropertyInfo, string> properties)
        {
            var errorMessage = $"The type {type.Name} has one or more duplicated columns. Check the following:\n Property  -> Column";
            foreach(var property in properties)
            {
                errorMessage += $"{property.Key.Name} -> {property.Value}";
            }
            return errorMessage;
        }


        public DuplicatedColumnsOnTableException(Type type, Dictionary<PropertyInfo, string> properties) : base(GenerateErrorMessage(type, properties))
        {
        }

        public DuplicatedColumnsOnTableException(string message) : base(message)
        {
        }

        public DuplicatedColumnsOnTableException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DuplicatedColumnsOnTableException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}