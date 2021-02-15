using System;
using System.Runtime.Serialization;

namespace BlockBase.BBLinq.Exceptions
{
    [Serializable]
    public class InvalidRangeException : Exception
    {
        private static string GenerateErrorMessage(string typeName, string propertyName, int minimumRange, int maximumRange)
        {
            var errorMessage = $"The type {typeName} as a range on {propertyName} with an unacceptable range. The maximum value should be greater than the minimum value.\nMinimum - {minimumRange}\nMaximum - {maximumRange}";
            return errorMessage;
        }


        public InvalidRangeException(string typeName, string propertyName, int minimumRange, int maximumRange) : base(GenerateErrorMessage(typeName, propertyName, minimumRange, maximumRange))
        {
        }

        public InvalidRangeException(string message) : base(message)
        {
        }

        public InvalidRangeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidRangeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}