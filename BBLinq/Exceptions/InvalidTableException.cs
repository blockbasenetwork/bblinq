using System;
using System.Reflection;
using System.Runtime.Serialization;

namespace BlockBase.BBLinq.Exceptions
{
    [Serializable]
    public class InvalidTableException : Exception
    {
        private static string GenerateErrorMessage(Type type, string tableName)
        {
            var errorMessage = $"The type {type.Name} is associated to a table whose name is not valid. The name should only be composed by alphanumerics and _ (underscore). The inputed table name was {tableName}";
            return errorMessage;
        }


        public InvalidTableException(Type type, string tableName) : base(GenerateErrorMessage(type, tableName))
        {
        }

        public InvalidTableException(string message) : base(message)
        {
        }

        public InvalidTableException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidTableException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}