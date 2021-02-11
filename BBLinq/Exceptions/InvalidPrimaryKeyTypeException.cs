using System;
using System.Reflection;
using System.Runtime.Serialization;

namespace BlockBase.BBLinq.BBLinq.Exceptions
{
    [Serializable]
    public class InvalidPrimaryKeyTypeException : Exception
    {
        private static string GenerateErrorMessage(PropertyInfo property, Type[] acceptableTypes)
        {
            var errorMessage = $"The type provided in the primary key for *{property.DeclaringType.Name}* should be one of the following:\n";
            foreach(var acceptableType in acceptableTypes)
            {
                errorMessage += $"{acceptableType.Name}\n";
            }
            return errorMessage;
        }


        public InvalidPrimaryKeyTypeException(PropertyInfo property, Type[] acceptableTypes): base(GenerateErrorMessage(property, acceptableTypes))
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