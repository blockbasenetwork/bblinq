using System;
using System.Runtime.Serialization;

namespace BlockBase.BBLinq.BBLinq.Validators
{
    [Serializable]
    public class NoPrimaryKeyFoundException : Exception
    {
        private static string GenerateErrorMessage(Type type)
        {
            var errorMessage = $"The type {type.Name} has no primary key";
            return errorMessage;
        }

        public NoPrimaryKeyFoundException(Type type) : base(GenerateErrorMessage(type)) 
        {
        }

        public NoPrimaryKeyFoundException(string message) : base(message)
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