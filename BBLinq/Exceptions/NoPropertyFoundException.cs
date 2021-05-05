using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace BlockBase.BBLinq.Exceptions
{
    public class NoPropertyFoundException : Exception
    {
        public NoPropertyFoundException(string type, string property) : base($"No property {property} found in {type}")
        {

        }
    }
}
