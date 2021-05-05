using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace BlockBase.BBLinq.Exceptions
{
    public class PropertyNotBooleanException : Exception
    {
        public PropertyNotBooleanException(PropertyInfo property) : base($"The property {property.Name} is not boolean, so it isn't possible to check its truth value")
        {

        }
    }
}
