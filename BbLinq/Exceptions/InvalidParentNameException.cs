using System;
using System.Runtime.Serialization;

namespace BlockBase.BBLinq.Exceptions
{
    [Serializable]
    public class InvalidParentTypeException : Exception
    {

        private static string GenerateErrorMessage(string typeName, string propertyName, string parentType)
        {
            var errorMessage = $"The type {parentType} associated to a foreign key {propertyName} on {typeName} does not belong to the model or does not exist. ";
            return errorMessage;
        }


        public InvalidParentTypeException(string typeName, string propertyName, string parentType) : base(GenerateErrorMessage(typeName, propertyName, parentType))
        {
        }

        public InvalidParentTypeException(string message) : base(message)
        {
        }

        public InvalidParentTypeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidParentTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}