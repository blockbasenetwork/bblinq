using System;
using System.Reflection;

namespace BlockBase.BBLinq.Exceptions
{
    public class PropertyNotBooleanException : Exception
    {
        public PropertyNotBooleanException(PropertyInfo property) : base($"The property {property.Name} is not boolean, so it isn't possible to check its truth value")
        {

        }
    }
}
