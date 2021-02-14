using System;
using System.Runtime.Serialization;

namespace BlockBase.BBLinq.Exceptions
{
    [Serializable]
    public class DuplicatedTablesOnModelException : Exception
    {
        private static string GenerateErrorMessage((string, string)[] entities)
        {
            var errorMessage = $"The model has one or more duplicated tables. Check the following:\n Entity  -> Table";
            foreach(var entity in entities)
            {
                errorMessage += $"{entity.Item1} -> {entity.Item2}";
            }
            return errorMessage;
        }


        public DuplicatedTablesOnModelException((string, string)[] entities) : base(GenerateErrorMessage(entities))
        {

        }

        public DuplicatedTablesOnModelException(string message) : base(message)
        {
        }

        public DuplicatedTablesOnModelException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DuplicatedTablesOnModelException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}