using System;
using System.Reflection;
using System.Runtime.Serialization;

namespace BlockBase.BBLinq.Exceptions
{
    [Serializable]
    public class InvalidTableNameException : Exception
    {
        private static string GenerateErrorMessage(string typeName, string tableName)
        {
            var errorMessage = $"The type {typeName} is associated to a table whose name is not valid. The name should only be composed by alphanumerics and _ (underscore). The inputed table name was {tableName}";
            return errorMessage;
        }


        public InvalidTableNameException(string typeName, string tableName) : base(GenerateErrorMessage(typeName, tableName))
        {
        }

        public InvalidTableNameException(string message) : base(message)
        {
        }

        public InvalidTableNameException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidTableNameException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}