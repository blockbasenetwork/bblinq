using System.Collections.Generic;
using System.Linq;

namespace BlockBase.BBLinq.ExtensionMethods
{
    /// <summary>
    /// Extension Methods for the Enumerable type
    /// </summary>
    public static class EnumerableExtensionMethods
    {
        /// <summary>
        /// Check if the enumerable is null or empty
        /// </summary>
        /// <typeparam name="T">a generic type accepted by enumerable types</typeparam>
        /// <param name="enumerable">an enumerable collection</param>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            return enumerable == null || !enumerable.Any();
        }

    }
}
