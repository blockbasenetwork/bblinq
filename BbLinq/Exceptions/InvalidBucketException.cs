using System;
using System.Runtime.Serialization;

namespace BlockBase.BBLinq.Exceptions
{
    [Serializable]
    public class InvalidBucketException : Exception
    {
        private static string GenerateErrorMessage(string typeName, string propertyName, int? buckets)
        {
            var errorMessage = $"The amount of buckets set to {propertyName} on {typeName} is incorrect. The inserted value was {buckets} but it should be equal or greater than 1.";
            return errorMessage;
        }


        public InvalidBucketException(string typeName, string propertyName, int? buckets) : base(GenerateErrorMessage(typeName, propertyName, buckets))
        {
        }

        public InvalidBucketException(string message) : base(message)
        {
        }

        public InvalidBucketException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidBucketException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}