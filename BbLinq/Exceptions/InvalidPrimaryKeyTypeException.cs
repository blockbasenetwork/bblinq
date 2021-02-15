using System;
using System.Runtime.Serialization;

namespace BlockBase.BBLinq.Exceptions
{
    [Serializable]
    public class InvalidForeignKeyTypeException : Exception
    {
        private static string GenerateErrorMessage(string typeName, string propertyName, string parentKeyTypeName, string foreignKeyTypeName)
        {
            var errorMessage = $"The type provided in the foreign key for *{propertyName}* on {typeName} should be {parentKeyTypeName} instead of {foreignKeyTypeName}:\n";
            return errorMessage;
        }


        public InvalidForeignKeyTypeException(string typeName, string propertyName, string parentKeyTypeName, string foreignKeyTypeName) : base(GenerateErrorMessage(typeName, propertyName, parentKeyTypeName, foreignKeyTypeName))
        {
        }

        public InvalidForeignKeyTypeException(string message) : base(message)
        {
        }

        public InvalidForeignKeyTypeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidForeignKeyTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}