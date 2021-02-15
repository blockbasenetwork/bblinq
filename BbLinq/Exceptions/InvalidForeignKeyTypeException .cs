using System;
using System.Runtime.Serialization;

namespace BlockBase.BBLinq.Exceptions
{
    [Serializable]
    public class InvalidPrimaryKeyTypeException : Exception
    {
        private static string GenerateErrorMessage(string propertyName, string[] acceptableTypes)
        {
            var errorMessage = $"The type provided in the primary key for *{propertyName}* should be one of the following:\n";
            foreach(var acceptableType in acceptableTypes)
            {
                errorMessage += $"{acceptableType}\n";
            }
            return errorMessage;
        }


        public InvalidPrimaryKeyTypeException(string propertyName, string[] acceptableTypes): base(GenerateErrorMessage(propertyName, acceptableTypes))
        {
        }

        public InvalidPrimaryKeyTypeException(string message) : base(message)
        {
        }

        public InvalidPrimaryKeyTypeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidPrimaryKeyTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}