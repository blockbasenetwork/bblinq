using System;
using System.Collections.Generic;
using System.Text;

namespace BlockBase.BBLinq.ExtensionMethods
{
    public static class NumberExtensionMethods
    {
        /// <summary>
        /// Checks if the number is equal or greater than a minimum and equal or less than a maximum
        /// </summary>
        public static bool IsBetween(this int number, int minimum, int maximum)
        {
            return number >= minimum && number <= maximum;
        }
    }
}
