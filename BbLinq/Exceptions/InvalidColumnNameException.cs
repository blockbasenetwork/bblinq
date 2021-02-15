using System;
using System.Runtime.Serialization;

namespace BlockBase.BBLinq.Exceptions
{
    [Serializable]
    public class InvalidColumnNameException : Exception
    {
        private static string GenerateErrorMessage(string typeName, string propertyName, string columnName)
        {
            var errorMessage = $"The type {typeName} has a property {propertyName} that is associated to a column whose name is not valid. The name should only be composed by alphanumerics and _ (underscore). The inputted column name was {columnName}";
            return errorMessage;
        }


        public InvalidColumnNameException(string typeName, string propertyName, string columnName) : base(GenerateErrorMessage(typeName, propertyName, columnName))
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