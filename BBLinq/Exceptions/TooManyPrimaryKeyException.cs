using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;

namespace BlockBase.BBLinq.BBLinq.Validators
{
    [Serializable]
    public class TooManyPrimaryKeysException : Exception
    {
        private static string GenerateErrorMessage(Type type, List<PropertyInfo> primaryKeys)
        {
            var errorMessage = $"The type {type.Name} has too many primary keys.\n";
            foreach(var key in primaryKeys)
            {
                errorMessage += $"{key.Name}\n";
            }
            return errorMessage;
        }

        public TooManyPrimaryKeysException(Type type, List<PropertyInfo> primaryKeys) : base (GenerateErrorMessage(type, primaryKeys))
        {
        }

        public TooManyPrimaryKeysException(string message) : base(message)
        {
        }

        public TooManyPrimaryKeysException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TooManyPrimaryKeysException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}