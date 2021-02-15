using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace BlockBase.BBLinq.Exceptions
{
    [Serializable]
    public class IncompleteModelException : Exception
    {
        private static string GenerateErrorMessage(IEnumerable<string> entities)
        {
            var errorMessage = "The provided model is incomplete. The following entities have unsolved dependencies";
            foreach (var entity in entities)
            {
                errorMessage += $"{entity}";
            }
            return errorMessage;
        }


        public IncompleteModelException(IEnumerable<string> entities) : base(GenerateErrorMessage(entities))
        {

        }

        public IncompleteModelException(string message) : base(message)
        {
        }

        public IncompleteModelException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected IncompleteModelException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
