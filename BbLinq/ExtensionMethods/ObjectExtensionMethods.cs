using System;
using System.Collections.Generic;
using System.Text;

namespace BlockBase.BBLinq.ExtensionMethods
{
    public static class ObjectExtensionMethods
    {
        /// <summary>
        /// Checks if the object is a numeric value
        /// </summary>
        /// <param name="value">the value to check</param>
        /// <returns>true if the object is a number. False otherwise</returns>
        public static bool IsNumber(this object value)
        {
            return value is sbyte
                   || value is byte
                   || value is short
                   || value is ushort
                   || value is int
                   || value is uint
                   || value is long
                   || value is ulong
                   || value is float
                   || value is double
                   || value is decimal;
        }
    }
}
