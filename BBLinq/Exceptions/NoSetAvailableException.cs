using System;

namespace BlockBase.BBLinq.Exceptions
{
    public class NoSetAvailableException : Exception
    {
        public static string GenerateMessage(string typeName)
        {
            return $"There's no set with the type {typeName} available";
        }

        public NoSetAvailableException(string typeName) : base(GenerateMessage(typeName)) { }
    }
}
