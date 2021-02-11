using System;
using System.Reflection;
using System.Runtime.Serialization;

namespace BlockBase.BBLinq.Exceptions
{
    [Serializable]
    public class InvalidBucketException : Exception
    {
        private static string GenerateErrorMessage(Type type, PropertyInfo property, int buckets)
        {
            var errorMessage = $"The amount of buckets set to {property.Name} on {type.Name} is incorrect. The inserted value was {buckets} but it should be equal or greater than 1.";
            return errorMessage;
        }


        public InvalidBucketException(Type type, PropertyInfo property, int buckets) : base(GenerateErrorMessage(type, property, buckets))
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