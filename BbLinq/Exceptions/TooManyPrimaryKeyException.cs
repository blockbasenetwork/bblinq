using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;

namespace BlockBase.BBLinq.Validators
{
    [Serializable]
    public class TooManyPrimaryKeysException : Exception
    {
        private static string GenerateErrorMessage(string typeName, List<string> primaryKeys)
        {
            var errorMessage = $"The type {typeName} has too many primary keys.\n";
            foreach(var key in primaryKeys)
            {
                errorMessage += $"{key}\n";
            }
            return errorMessage;
        }

        public TooManyPrimaryKeysException(string typeName, List<string> primaryKeys) : base (GenerateErrorMessage(typeName, primaryKeys))
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