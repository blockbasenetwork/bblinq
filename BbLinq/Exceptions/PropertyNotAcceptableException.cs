using System;
using System.Runtime.Serialization;

namespace BlockBase.BBLinq.Exceptions
{
    [Serializable]
    public class PropertiesNotAcceptableException : Exception
    {
        private static string GenerateErrorMessage(string typeName, string[] propertyNames)
        {
            var errorMessage = $"The following properties on {typeName} is not acceptable and should have a NotMapped attribute.\n";
            foreach(var propertyName in propertyNames)
            {
                errorMessage += $"{propertyName}\n";
            }

            return errorMessage;
        }


        public PropertiesNotAcceptableException(string typeName, string[] propertyNames) : base(GenerateErrorMessage(typeName, propertyNames))
        {

        }

        public PropertiesNotAcceptableException(string message) : base(message)
        {


        }

        public PropertiesNotAcceptableException(string message, Exception innerException) : base(message, innerException)
        {

        }

        protected PropertiesNotAcceptableException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}