using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BlockBase.BBLinq.Exceptions
{
    public class UnavailableOperatorException : Exception
    {

        public static string GenerateMessage(string @operator, string[] availableOperators)
        {
            var res = $"The {@operator} operator is not available. Try one of the following:\n";
            foreach (var availableOperator in availableOperators)
            {
                res += availableOperator + "\n";
            }
            return res;
        }

        public UnavailableOperatorException(string @operator, string[] availableOperators) : base(GenerateMessage(@operator, availableOperators))
        {
        }
    }
}
