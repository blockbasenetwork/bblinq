using System;
using System.Runtime.Serialization;

namespace BlockBase.BBLinq.Exceptions
{
    public class NoParentSetException : Exception
    {

        private static string GenerateErrorMessage(string typeName, string propertyName)
        {
            var errorMessage = $"The foreign key {propertyName} on {typeName} has no parent type set";
            return errorMessage;
        }


        public NoParentSetException(string typeName, string propertyName) : base(GenerateErrorMessage(typeName, propertyName))
        {
        }

        public NoParentSetException(string message) : base(message)
        {
        }

        public NoParentSetException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoParentSetException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
