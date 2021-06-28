using System;

namespace BlockBase.BBLinq.Exceptions
{
    public class NoPropertyFoundException : Exception
    {
        public NoPropertyFoundException(string type, string property) : base($"No property {property} found in {type}")
        {

        }
    }
}
