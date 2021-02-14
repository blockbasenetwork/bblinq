using System;
using System.Runtime.Serialization;

namespace BlockBase.BBLinq.Exceptions
{
    [Serializable]
    public class DuplicatedColumnsOnTableException : Exception
    {
        private static string GenerateErrorMessage(string typeName, (string, string)[] columns)
        {
            var errorMessage = $"The type {typeName} has one or more duplicated columns. Check the following:\n Property  -> Column";
            foreach (var column in columns)
            {
                errorMessage += $"{column.Item1} -> {column.Item2}";
            }
            return errorMessage;
        }


        public DuplicatedColumnsOnTableException(string typeName, (string, string)[] columns) : base(GenerateErrorMessage(typeName, columns))
        {
        }

        public DuplicatedColumnsOnTableException(string message) : base(message)
        {
        }

        public DuplicatedColumnsOnTableException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DuplicatedColumnsOnTableException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}