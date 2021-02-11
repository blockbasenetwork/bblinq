using BlockBase.BBLinq.BBLinq.Annotations;
using System;
using System.Reflection;
using System.Runtime.Serialization;

namespace BlockBase.BBLinq.BBLinq.Exceptions
{
    [Serializable]
    public class InvalidRangeException : Exception
    {
        private static string GenerateErrorMessage(Type type, PropertyInfo property, RangeAttribute range)
        {
            var errorMessage = $"The type {type.Name} as a range on {property.Name} with an unacceptable range. The maximum value should be greater than the minimum value.\nMinimum - {range.MinimumValue}\nMaximum - {range.MaximumValue}";
            return errorMessage;
        }


        public InvalidRangeException(Type type, PropertyInfo property, RangeAttribute range) : base(GenerateErrorMessage(type, property, range))
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