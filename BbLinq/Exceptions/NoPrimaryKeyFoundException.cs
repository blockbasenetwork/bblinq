using System;
using System.Runtime.Serialization;

namespace BlockBase.BBLinq.Exceptions
{
    [Serializable]
    public class NoPrimaryKeyFoundException : Exception
    {
        private static string GenerateErrorMessage(string typeName)
        {
            var errorMessage = $"The type {typeName} has no primary key";
            return errorMessage;
        }

        public NoPrimaryKeyFoundException(string typeName) : base(GenerateErrorMessage(typeName)) 
        {
        }

        public NoPrimaryKeyFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoPrimaryKeyFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}