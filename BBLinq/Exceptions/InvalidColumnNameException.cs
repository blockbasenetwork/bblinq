using System;
using System.Reflection;
using System.Runtime.Serialization;

namespace BlockBase.BBLinq.BBLinq.Exceptions
{
    [Serializable]
    public class InvalidColumnNameException : Exception
    {
        private static string GenerateErrorMessage(Type type, PropertyInfo property, string columnName)
        {
            var errorMessage = $"The type {type.Name} has a property {property.Name} that is associated to a column whose name is not valid. The name should only be composed by alphanumerics and _ (underscore). The inputed column name was {columnName}";
            return errorMessage;
        }


        public InvalidColumnNameException(Type type, PropertyInfo property, string columnName) : base(GenerateErrorMessage(type, property, columnName))
        {
        }

        public InvalidColumnNameException(string message) : base(message)
        {
        }

        public InvalidColumnNameException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidColumnNameException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}